using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading;

namespace BaseServer
{
    public enum ParamGroup { Trend, Table }
    public enum ParamTable { Minutes, Hours, Days, Months }
    public sealed class ServerSQL : IDisposable
    {
        private string connectionString = String.Empty;
        private static string lasterrorString = String.Empty;
        public MySqlConnection myConnection;
        private bool serverConnected = false;
        private string databaseName = String.Empty;

        public static string LastError { get { return lasterrorString; } }

        public void Dispose() 
        {
            myConnection.Dispose();
        }
        private static string getDatabaseName(DatabaseFrom kind, bool localhost)
        {
            if (kind == DatabaseFrom.Database)
                return Properties.Settings.Default.Database; 
            else
                if (kind == DatabaseFrom.Fetchbase)
                    return Properties.Settings.Default.Fetchbase; 
                else
                    return String.Empty;
        }

        public static bool HostIsLocalhost()
        {
            string host = Properties.Settings.Default.Host.ToLower();
            return host.Equals("localhost") || host.Equals("127.0.0.1");
        }

        public ServerSQL(DatabaseFrom kind, bool localhost = false)
        {
            databaseName = getDatabaseName(kind, localhost);
            string server = (localhost) ? "localhost" : Properties.Settings.Default.Host;
            connectionString = "server=" + server +
                ";user=" + Properties.Settings.Default.User +
                ";database=" + databaseName +
                ";port=" + Properties.Settings.Default.Port +
                ";password=" + Properties.Settings.Default.Password + ";";
            myConnection = new MySqlConnection(connectionString);
            serverConnected = tryToConnect();
        }

        private bool tryToConnect()
        {
            try
            {
                // произведем попытку подключения
                myConnection.Open();
                lasterrorString = String.Empty;
                return true;
            }
            catch (Exception e)
            {
                lasterrorString = e.Message;
                return false;
            }
        }

        public bool Connected
        { 
            get
            {
                //Thread.Sleep(100);
                return serverConnected;
            }
        }

        public string CurrentDatabase { get { return databaseName; } }

        public IDictionary<string, string> ReadPoints()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (serverConnected)
            {
                string SQL = "select `name`,`value` from `Points` where (`prop`='Plugin')";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string ptname = reader.GetString(0);
                                if (!result.ContainsKey(ptname))
                                    result.Add(ptname, reader.GetString(1));
                            }
                        }
                    }
                }
                SQL = "select `name`,`value` from `Points` where (`prop`='PtType')";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string ptname = reader.GetString(0);
                                    if (result.ContainsKey(ptname))
                                        result[ptname] += reader.GetString(1);
                                }
                            }
                        }
                    }
                    catch //(MySqlException ex)
                    {
                        //
                    }
                }
            }
            return result;
        }
        public IDictionary<string, string> ReadPropValues(string name)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (serverConnected)
            {
                string SQL = "select `prop`,`value`,`snaptime` from `Points` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DateTime snaptime = DateTime.MinValue;
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0), reader.GetString(1));
                                DateTime pdt = reader.GetDateTime(2);
                                if (pdt > snaptime) snaptime = pdt;
                            }
                            if (result.ContainsKey("Version")) result["Version"] = snaptime.ToString();
                            else result.Add("Version", snaptime.ToString());
                        }
                    }
                }
            }
            return result;
        }

        public string ReadPropValue(string name, string prop, string def)
        {
            if (serverConnected)
            {
                string SQL = "select `value` from `Points` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", name);
                        lasterrorString = String.Empty;
                        string result = (string)command.ExecuteScalar();
                        if (result != null)
                            return result;
                        else
                            return def;
                    }
                    catch (MySqlException e)
                    {
                        lasterrorString = e.Message;
                        return def;
                    }
                }
            }
            else
                return def;
        }

        public IDictionary<string, string> ReadRealValues(string name)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (serverConnected)
            {
                string SQL = "select `prop`,`value`,`snaptime` from `RealVals` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DateTime dt = DateTime.MinValue;
                                while (reader.Read())
                                {
                                    result.Add(reader.GetString(0), reader.GetString(1));
                                    DateTime snaptime = reader.GetDateTime(2);
                                    if (snaptime > dt) dt = snaptime;
                                }
                                if (result.ContainsKey("FactTime")) result["FactTime"] = dt.ToString();
                                else result.Add("FactTime", dt.ToString());
                            }
                        }
                    }
                    catch (MySqlException)
                    {
                        Settings.DropRealValsTable(this);
                        Settings.CreateRealValsTable(this);
                        this.AddToSystemLog(0, "Сервер SQL", "Таблица опроса RealVals восстановлена.");
                    }
                }
            }
            return result;
        }

        public DateTime ReadVersion(string name)
        {
            DateTime snaptime = DateTime.MinValue;
            if (serverConnected)
            {
                string SQL = "select `snaptime` from `Points` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime pdt = reader.GetDateTime(0);
                                if (pdt > snaptime) snaptime = pdt;
                            }
                        }
                    }
                }
            }
            return snaptime;
        }

        public void WriteRealValue(string name, string prop, string value)
        {
            if (serverConnected)
            {
                if (value.IndexOf("'") >= 0) value = value.Replace("'", "°");
                string SQL = String.Format("replace into `RealVals`" +
                        " (`name`,`prop`,`value`) values ('{0}','{1}','{2}')", name, prop, value);
                ExecSQL(SQL);
            }
        }

        public void WriteRealValue(string name, string prop, string value, DateTime snaptime)
        {
            if (serverConnected)
            {
                if (value.IndexOf("'") >= 0) value = value.Replace("'", "°");
                string SQL = String.Format("replace into `RealVals`" +
                        " (`name`,`prop`,`value`,`snaptime`) values ('{0}','{1}','{2}','{3}')",
                        name, prop, value, snaptime.ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }

        public void WritePropValue(string name, string prop, string value)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Points`" +
                        " (`name`,`prop`,`value`) values ('{0}','{1}','{2}')",
                        cut(name, 25), cut(prop, 25), cut(value, 100));
                ExecSQL(SQL);
            }
        }

        public void WritePropValue(string name, string prop, string value, DateTime snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Points`" +
                        " (`name`,`prop`,`value`,`snaptime`) values ('{0}','{1}','{2}','{3}')",
                        cut(name, 25), cut(prop, 25), cut(value, 100),
                        snaptime.ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }

        public void AddIntoTable(DateTime dt, string name, Single value, bool kind,
            int tabletype, int accumtype)
        {
            if (serverConnected)
            {
                string[] tables = new string[4] { "minutes", "hours", "days", "months" };
                string[] formats = new string[4] { "yyyy-MM-dd HH:mm:00", "yyyy-MM-dd HH:00:00",
                    "yyyy-MM-dd 00:00:00", "yyyy-MM-01 00:00:00" };
                DateTime dt1 = dt;
                switch (tabletype)
                {
                    case 0: dt1 = dt.AddMinutes(1); break;
                    case 1: dt1 = dt.AddHours(1); break;
                    case 2: dt1 = dt.AddDays(1); break;
                    case 3: dt1 = dt.AddMonths(1); break;
                }
                string sval = String.Format("{0}", value);
                sval = sval.Replace(',', '.');
                string SQL;
                string frm;
                switch (accumtype)
                {
                    case 0: // Запись значения как есть
                        frm = formats[tabletype];
                        SQL = String.Format("select count(`kind`) from `{0}` where `name`='{1}' and " +
                            "`snaptime`>='{2}' and `snaptime`<'{3}' and kind='Y'",
                            tables[tabletype], cut(name, 25), dt.ToString(frm), dt1.ToString(frm));
                        if (kind && !GoodRecordExists(SQL))
                        {
                            SQL = String.Format("replace into `{0}` (`name`,`value`,`kind`,`snaptime`) " +
                                "values ('{1}',{2},'{3}','{4}')", tables[tabletype], cut(name, 25), sval,
                                ((kind) ? "Y" : "N"), dt.ToString(formats[tabletype]));
                            ExecSQL(SQL);
                        }
                        break;
                    case 1: // Запись накапливаемого значения
                    case 2: // Запись усредняемого значения
                        string tablename = (tabletype > 0) ? tables[tabletype - 1] : "trends";
                        frm = formats[tabletype];
                        SQL = String.Format("select count(`kind`) from `{0}` where `name`='{1}' and " +
                            "`snaptime`>='{2}' and `snaptime`<'{3}' and kind='N'",
                            tablename, cut(name, 25), dt.ToString(frm), dt1.ToString(frm));
                        kind = BadRecordExists(kind, SQL);
                        string op = (accumtype == 1) ? "sum" : "avg";
                        SQL = String.Format("select {0}(`value`) from `{1}` where `name`='{2}' and " +
                            "`snaptime`>='{3}' and `snaptime`<'{4}' and kind='Y'",
                            op, tablename, cut(name, 25), dt.ToString(frm), dt1.ToString(frm));
                        using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                        {
                            try
                            {
                                object result = command.ExecuteScalar();
                                if (result != null)
                                {
                                    Double summ;
                                    if (Double.TryParse(result.ToString(), out summ))
                                    {
                                        sval = String.Format("{0}", summ);
                                        sval = sval.Replace(',', '.');
                                        SQL = String.Format("replace into `{0}` (`name`,`value`,`kind`,`snaptime`) " +
                                            "values ('{1}',{2},'{3}','{4}')", tables[tabletype], cut(name, 25), sval,
                                            ((kind) ? "Y" : "N"), dt.ToString(formats[tabletype]));
                                        ExecSQL(SQL);
                                    }
                                }
                                lasterrorString = String.Empty;
                            }
                            catch (MySqlException e)
                            {
                                lasterrorString = e.Message;
                            }
                        }
                        break;
                }
            }
        }

        private bool GoodRecordExists(string SQL)
        {
            bool kind = false;
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                try
                {
                    object result = command.ExecuteScalar();
                    int goodcount;
                    if (result != null)
                    {
                        goodcount = int.Parse(result.ToString());
                        kind = (goodcount > 0);
                    }
                    else
                        kind = false;
                    lasterrorString = String.Empty;
                }
                catch (MySqlException e)
                {
                    lasterrorString = e.Message;
                    kind = false;
                }
            }
            return kind;
        }

        private bool BadRecordExists(bool kind, string SQL)
        {
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                try
                {
                    object result = command.ExecuteScalar();
                    int badcount;
                    if (result != null)
                    {
                        badcount = int.Parse(result.ToString());
                        kind = (badcount == 0);
                    }
                    else
                        kind = false;
                    lasterrorString = String.Empty;
                }
                catch (MySqlException e)
                {
                    lasterrorString = e.Message;
                    kind = false;
                }
            }
            return kind;
        }

        public byte[] GetImageData(string name)
        {
            byte[] result = new byte[] { };
            if (serverConnected)
            {
                string SQL = "select `value` from `Images` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    try
                    {
                        object obj = command.ExecuteScalar();
                        result = (byte[])obj;
                        lasterrorString = String.Empty;
                    }
                    catch (MySqlException e)
                    {
                        lasterrorString = e.Message;
                    }                   
                }
            }
            return result;
        }

        public DateTime GetImageFileTime(string name)
        {
            DateTime result = DateTime.MinValue;
            if (serverConnected)
            {
                string SQL = "select `filetime` from `Images` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    try
                    {
                        object obj = command.ExecuteScalar();
                        result = (DateTime)obj;
                        lasterrorString = String.Empty;
                    }
                    catch (MySqlException e)
                    {
                        lasterrorString = e.Message;
                    }
                }
            }
            return result;
        }
        public List<string> GetImagesList()
        {
            List<string> result = new List<string>();
            string SQL = "select `name` from `Images`";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            while (reader.Read())
                                result.Add(reader.GetString(0));
                    }
                    lasterrorString = String.Empty;
                }
                catch (MySqlException e)
                {
                    lasterrorString = e.Message;
                }
            }
            return result;
        }

        public byte[] GetReportData(string name)
        {
            byte[] result = new byte[] { };
            if (serverConnected)
            {
                string SQL = "select `value` from `Reports` where (`name`=@name)";
                using (MySqlCommand cmd = new MySqlCommand(SQL, myConnection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    object obj = cmd.ExecuteScalar();
                    result = (byte[])obj;
                }
            }
            return result;
        }

        public DateTime GetReportFileTime(string name)
        {
            DateTime result = DateTime.MinValue;
            if (serverConnected)
            {
                string SQL = "select `filetime` from `Reports` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    object obj = command.ExecuteScalar();
                    result = (DateTime)obj;
                }
            }
            return result;
        }

        public ArrayList GetReportsList()
        {
            ArrayList results = new ArrayList();
            string SQL = "select `name`,`descriptor` from `Reports` order by `name`";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string rec;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            rec = String.Format("{0};{1}", reader.GetString(0),
                                                reader.GetString(1));
                            results.Add(rec);
                        }
                }
            }
            return results;
        }

        public ArrayList GetSchemesList()
        {
            ArrayList results = new ArrayList();
            string SQL = "select `scheme`,`value` from `Dinamics` " +
                "where (`name`='Background' and `prop`='Descriptor')";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string rec;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rec = String.Format("{0};{1}", reader.GetString(0),
                                                reader.GetString(1));
                            results.Add(rec);
                        }
                    }
                }
            }
            return results;
        }

        public ArrayList GetDinList(string schemename)
        {
            ArrayList results = new ArrayList();
            string SQL = "select distinct `name` from `Dinamics`" +
                         " where (`scheme`=@scheme) order by `npp`";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@scheme", schemename);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            results.Add(reader.GetString(0));
                    }
                }
            }
            return results;
        }

        public IDictionary<string, string> ReadDinProps(string schemename, string dinname)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (serverConnected)
            {
                string SQL = "select `prop`,`value` from `Dinamics`" +
                    " where (`scheme`=@scheme and `name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@scheme", schemename);
                    command.Parameters.AddWithValue("@name", dinname);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void WriteDinValue(string scheme, int npp, string name, string prop, string value)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Dinamics`" +
                        " (`scheme`,`npp`,`name`,`prop`,`value`) values ('{0}',{1},'{2}','{3}','{4}')",
                        cut(scheme, 25), npp, cut(name, 25), cut(prop, 25), cut(value, 100));
                ExecSQL(SQL);
            }
        }

        public void WriteDinValue(string scheme, int npp, string name, string prop, string value,
            DateTime snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Dinamics`" +
                        " (`scheme`,`npp`,`name`,`prop`,`value`,`snaptime`)" +
                        " values ('{0}',{1},'{2}','{3}','{4}','{5}')",
                        cut(scheme, 25), npp, cut(name, 25), cut(prop, 25), cut(value, 100),
                        snaptime.ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }

        public void DeleteDin(string scheme, string name)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Dinamics` " +
                    "where (`scheme`='{0}' and `name`='{1}')", scheme, name);
                ExecSQL(SQL);
            }
        }
        public void EraseEntity(string name)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Points` where (`name`='{0}')", name);
                ExecSQL(SQL);
            }
        }
        public void EmptyGroups(ParamGroup kind)
        { 
            if (serverConnected)
            { 
                ExecSQL(String.Format("delete from `{0}`",
                    (kind == ParamGroup.Trend) ? "Groups" : "TableGroups"));
            }
        }
        public void EmptyPoints() { if (serverConnected) { ExecSQL("delete from `Points`"); } }
        public void EmptyImages() { if (serverConnected) ExecSQL("delete from `Images`"); }
        public void EmptyReports() { if (serverConnected) ExecSQL("delete from `Reports`"); }
        public void AddImage(string name, DateTime filetime, byte[] image)
        {
            if (serverConnected)
            {
                string SQL = "insert into `Images` (`name`,`filetime`,`value`) values(@name,@filetime,@value)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@filetime", filetime.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@value", image);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddReport(string name, DateTime filetime, byte[] image, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = "insert into `Reports` (`name`,`filetime`,`value`,`descriptor`)" +
                    " values(@name,@filetime,@value,@descriptor)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@filetime",
                        filetime.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@value", image);
                    command.Parameters.AddWithValue("@descriptor", descriptor);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddReport(string name, DateTime filetime, byte[] image, string descriptor,
            string auto, string time, int period)
        {
            if (serverConnected)
            {
                string SQL = "insert into `Reports` (`name`,`filetime`,`value`,`descriptor`,`auto`,`time`,`period`)" +
                    " values(@name,@filetime,@value,@descriptor,@auto,@time,@period)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@filetime",
                        filetime.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@value", image);
                    command.Parameters.AddWithValue("@descriptor", descriptor);
                    command.Parameters.AddWithValue("@auto", auto);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@period", period);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EmptyReport(string reportname)
        {
            if (serverConnected)
                ExecSQL(String.Format("delete from `Reports` where (`name`='{0}')", reportname));
        }

        public void EmptyDinamics(string schemename)
        {
            if (serverConnected)
                ExecSQL(String.Format("delete from `Dinamics` where (`scheme`='{0}')", schemename));
        }

        public void RenameScheme(string oldname, string newname)
        {
            if (serverConnected)
                ExecSQL(String.Format("update `Dinamics` set `scheme`='{0}' where (`scheme`='{1}')",
                                        newname, oldname));
        }

        public void RenameEntity(string oldname, string newname)
        {
            if (serverConnected)
            {
                string SQL = String.Format("update `Points` set `value`='{0}' " +
                        " where (`value`='{1}')", cut(newname, 100), cut(oldname, 100));
                ExecSQL(SQL);
                SQL = String.Format("update `Points` set `name`='{0}' " +
                        " where (`name`='{1}')", cut(newname, 25), cut(oldname, 25));
                ExecSQL(SQL);
                SQL = String.Format("update `Dinamics` set `value`='{0}' " +
                    " where (`prop`='PtName' and `value`='{1}')", cut(newname, 25), cut(oldname, 25));
                ExecSQL(SQL);
                SQL = String.Format("update `Dinamics` set `value`='{0}' " +
                    " where (`prop`='PtName1' and `value`='{1}')", cut(newname, 25), cut(oldname, 25));
                ExecSQL(SQL);
            }
        }

        public void UpdateGroupDesc(int groupno, string desc, ParamGroup kind)
        {
            if (serverConnected)
            {
                string SQL;
                string tablename = (kind == ParamGroup.Trend) ? "GroupNames" : "TableGroupNames";
                if (String.IsNullOrWhiteSpace(desc))
                    SQL = String.Format("delete from `{1}` where (`group`={0})", groupno, tablename);
                else
                    SQL = String.Format("replace into `{2}`" +
                            " (`group`,`descriptor`) values ({0},'{1}')", groupno, cut(desc, 200), tablename);
                ExecSQL(SQL);
            }
        }

        public string GetGroupDesc(int groupno, ParamGroup kind)
        {
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `descriptor` from `GroupNames` where (`group`=@group)" :
                    "select `descriptor` from `TableGroupNames` where (`group`=@group)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@group", groupno);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                return reader.GetString(0);
                            return String.Empty;
                        }
                        else
                            return String.Empty;
                    }
                }
            }
            else
                return String.Empty;
        }

        public int[] GetGroupNo(string name, ParamGroup kind)
        {
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `group`, `place` from `Groups` where (`name`=@name)" :
                    "select `group`, `place` from `TableGroups` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                return  new int[] { reader.GetInt32(0), reader.GetInt32(1) };
                            return new int[] {};
                        }
                        else
                            return new int[] { };
                    }
                }
            }
            else
                return new int[] { };
        }

        public string GroupEntity(int group, int place, ParamGroup kind)
        {
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `name` from `Groups` where (`group`=@group and `place`=@place)" :
                    "select `name` from `TableGroups` where (`group`=@group and `place`=@place)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@group", group);
                    command.Parameters.AddWithValue("@place", place);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read()) return reader.GetString(0);
                        }
                    }
                }
                return String.Empty;
            }
            else
                return String.Empty;
        }

        public string[] GroupItem(int group, int place, ParamGroup kind)
        {
            string[] items = new string[5];
            items[0] = "0";  items[1] = "0";
            items[2] = String.Empty; items[3] = String.Empty; items[4] = String.Empty;
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `group`,`place`,`name` from `Groups` where (`group`=@group and `place`=@place)" :
                "select `group`,`place`,`name` from `TableGroups` where (`group`=@group and `place`=@place)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@group", group);
                    command.Parameters.AddWithValue("@place", place);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                items[0] = reader.GetString(0);
                                items[1] = reader.GetString(1);
                                string[] args = reader.GetString(2).Split(new char[] { '.' });
                                items[2] = args[0];
                                items[3] = (args.Length == 2) ? args[1] : "PV";
                                items[4] = (args[0].Length > 0) ? (items[2] + "." + items[3]) : String.Empty;
                                break;
                            }
                        }
                    }
                }
            }
            return items;
        }

        public string[] GroupItems(int group, int placeCount, ParamGroup kind)
        {
            string[] items = new string[placeCount];
            for (int i = 0; i < items.Length; i++) items[i] = String.Empty;
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `place`,`name` from `Groups` where (`group`=@group)" :
                    "select `place`,`name` from `TableGroups` where (`group`=@group)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@group", group);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int place = int.Parse(reader.GetString(0));
                                string name = reader.GetString(1);
                                if (place > 0 && place <= placeCount)
                                    items[place - 1] = name;
                            }
                        }
                    }
                }
            }
            return items;
        }

        public bool GroupIsEmpty(int index, ParamGroup kind)
        {
            if (serverConnected)
            {
                string SQL = (kind == ParamGroup.Trend) ?
                    "select `name`,`place` from `Groups` where (`group`=@group)" :
                    "select `name`,`place` from `TableGroups` where (`group`=@group)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@group", index);
                    using (MySqlDataReader reader = command.ExecuteReader())
                        return !reader.HasRows;
                }
            }
            else
                return true;
        }

        public ArrayList GetGroupNames(ParamGroup kind) // не восстребовано почему-то
        {
            ArrayList results = new ArrayList();
            string tablename = (kind == ParamGroup.Trend) ? "Groups" : "TableGroups";
            string SQL = String.Format("select `group`,`place`,`name` from `{0}`", tablename);
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string rec;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rec = String.Format("{0},{1},{2}", reader.GetInt16(0),
                                reader.GetInt16(1), reader.GetString(2));
                            results.Add(rec);
                        }
                    }
                }
            }
            return results;
        }

        public ArrayList GetAlarmRecords(string SQL)
        {
            ArrayList results = new ArrayList();
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string[] rec;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rec = new string[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i == 1)
                                {
                                    DateTime t = DateTime.Parse(reader.GetString(1));
                                    rec[1] = String.Format("{0}",
                                        t.ToString("dd ddd HH:mm:ss.fff"));
                                }
                                else
                                    rec[i] = reader.GetString(i);
                            }
                            results.Add(rec);
                        }
                    }
                }
            }
            return results;
        }
        public ArrayList GetSwitchRecords(string SQL)
        {
            ArrayList results = new ArrayList();
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string[] rec;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rec = new string[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i == 0)
                                {
                                    DateTime t = DateTime.Parse(reader.GetString(0));
                                    rec[0] = String.Format("{0}",
                                        t.ToString("dd ddd HH:mm:ss.fff"));
                                }
                                else
                                    rec[i] = reader.GetString(i);
                            }
                            results.Add(rec);
                        }
                    }
                }
            }
            return results;
        }
        public ArrayList GetLogRecords(string SQL, string format)
        {
            ArrayList results = new ArrayList();
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                string[] rec;
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rec = new string[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (i == 0)
                                    {
                                        DateTime t = DateTime.Parse(reader.GetString(0));
                                        rec[0] = String.Format("{0}", t.ToString(format));
                                    }
                                    else
                                        rec[i] = reader.GetString(i);
                                }
                                results.Add(rec);
                            }
                            results.Reverse();
                        }
                    }
                }
                catch //(MySqlException ex)
                {
                    //
                }
            }
            return results;
        }

        public ArrayList GetTableMatrix(string SQL, string[] columns, int[] formats, int pos, int count)
        {
            string[] fs = new string[] { "0", "0.0", "0.00", "0.000" };
            string[,] rec = new string[count, columns.Length + 1];
            int col = 1;
            foreach (string column in columns)
            {
                if (!String.IsNullOrEmpty(column))
                {
                    using (MySqlCommand command =
                        new MySqlCommand(String.Format(SQL, column, pos, count), myConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int row = 0;
                                while (reader.Read())
                                {
                                    rec[row, 0] = DateTime.Parse(
                                        reader.GetString(0)).ToString("dd.MM.yy ddd HH:mm");
                                    string entity = reader.GetString(1);
                                    string value = reader.GetDouble(2).ToString(fs[formats[col-1]]);
                                    string kind = reader.GetString(3);
                                    rec[row, col] = (kind.Equals("Y")) ? value : "------";
                                    row++;
                                }
                            }
                        }
                    }
                }
                col++;
            }
            ArrayList result = new ArrayList();
            for (int row = 0; row < count; row++)
            {
                string[] cols = new string[columns.Length + 1];
                for (int column = 0; column < cols.Length; column++)
                    cols[column] = rec[row, column];
                if (!String.IsNullOrEmpty(String.Join(" ", cols).Trim())) result.Add(cols);
            }
            result.Reverse();
            return result;
        }

        private string lastException = String.Empty;

        public bool CheckTable(string name, bool repareIfCrash = false)
        {
            bool result = false;
            using (MySqlCommand command = new MySqlCommand("CHECK TABLE `" + name + "` EXTENDED", myConnection))
            {
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            string Table = ""; // Table 
                            string Op = ""; // Op
                            string Msg_type = ""; // Msg_type
                            string Msg_text = ""; // Msg_text
                            while (reader.Read())
                            {
                                Table = reader.GetString(0); // Table 
                                Op = reader.GetString(1); // Op
                                Msg_type = reader.GetString(2); // Msg_type
                                Msg_text = reader.GetString(3); // Msg_text
                            }
                            if (Op.ToLower().Equals("check") &&
                                Msg_type.ToLower().Equals("status") &&
                                Msg_text.ToLower().Equals("ok"))
                            {
                                lasterrorString = String.Empty;
                                result = true;
                            }
                            else
                            {
                                if (repareIfCrash)
                                    result = RepairTable(name);
                                else
                                {
                                    lasterrorString = "Table " + Table + " is " + Msg_text;
                                    result = false;
                                }
                            }
                        }
                    }
                    if (!result)
                        AddToSystemLog(0, "Check", lasterrorString);
                }
                catch (Exception ex)
                {
                    lasterrorString = ex.Message;
                }
            }
            return result;
        }

        public bool RepairTable(string name)
        {
            bool result = false;
            using (MySqlCommand command = new MySqlCommand("REPAIR TABLE `" + name + "` EXTENDED", myConnection))
            {
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            string Table = ""; // Table 
                            string Op = ""; // Op
                            string Msg_type = ""; // Msg_type
                            string Msg_text = ""; // Msg_text
                            while (reader.Read())
                            {
                                Table = reader.GetString(0); // Table 
                                Op = reader.GetString(1); // Op
                                Msg_type = reader.GetString(2); // Msg_type
                                Msg_text = reader.GetString(3); // Msg_text
                            }
                            if (Op.ToLower().Equals("repair") &&
                                Msg_type.ToLower().Equals("status") &&
                                Msg_text.ToLower().Equals("ok"))
                            {
                                lasterrorString = String.Empty;
                                result = true;
                            }
                            else
                            {
                                lasterrorString = "Table " + Table + " is " + Msg_text;
                                result = false;
                            }
                        }
                    }
                    if (result)
                        AddToSystemLog(0, "Repair",  "Table " + name + " is OK");
                    else
                        AddToSystemLog(0, "Repair", lasterrorString);
                }
                catch (Exception ex)
                {
                    lasterrorString = ex.Message;
                }
            }
            return result;
        }

        public bool ExecSQL(string SQL)
        {
            bool result = false;
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    lasterrorString = String.Empty;
                }
                catch (Exception e)
                {
                    lasterrorString = e.Message;
                    if (!e.Message.Equals(lastException))
                    {
                        lastException = e.Message;
                        string ExceptionSQL = String.Format("insert into {0}.`SystemLog`" +
                            " (`station`,`name`,`descriptor`) values (0,'ExecSQL','{1}')",
                            getDatabaseName(DatabaseFrom.Database, false),
                            cut(e.Message, 200).Replace('\'', '"'));
                        using (MySqlCommand ExceptionCommand =
                            new MySqlCommand(ExceptionSQL, myConnection))
                        {
                            try
                            {
                                ExceptionCommand.ExecuteNonQuery();
                                lasterrorString = String.Empty;
                            }
                            catch (Exception ex)
                            {
                                lasterrorString = ex.Message;
                            }
                        }
                    }
                    return false;
                }
                //command.ExecuteNonQuery();
            }
            return result;
        }
        public string GetLastAlarm()
        {
            string result = String.Empty; 
            if (serverConnected)
            {
                string SQL = "select `name`,`param` from `alarms`" +
                    " order by str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f') desc limit 0,1";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    string[] rec;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rec = new string[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                    rec[i] = reader.GetString(i);
                                result = rec[0] + "." + rec[1];
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public string GetLastSwitch()
        {
            string result = String.Empty;
            if (serverConnected)
            {
                string SQL = "select `name`,`param` from `switchs`" +
                    " order by str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f') desc limit 0,1";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    string[] rec;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rec = new string[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                    rec[i] = reader.GetString(i);
                                result = rec[0] + "." + rec[1];
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void UpdateAlarmValue(int station, string name, string value)
        {
            if (serverConnected)
            {
                string SQL = String.Format("update `Alarms` set `value`='{2}' " +
                " where (`station`={0} and `name`='{1}')", station, name, value);
                ExecSQL(SQL);
            }
        }

        public void UpdateAlarm(string key, int station, string name, string param, string value,
            string setpoint, string message, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Alarms`" +
                    " (`key`,`station`,`name`,`param`,`value`,`setpoint`,`message`,`descriptor`,`snaptime`)" +
                    " values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    cut(key, 20), station, cut(name, 20), cut(param, 20), cut(value, 20),
                    cut(setpoint, 20), cut(message, 50), cut(descriptor, 50),
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
            }
        }

        public void UpdateAlarm(string key, int station, string name, string param, string value,
            string setpoint, string message, string descriptor, string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Alarms`" +
                    " (`key`,`station`,`name`,`param`,`value`,`setpoint`,`message`,`descriptor`,`snaptime`)" +
                    " values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    cut(key, 20), station, cut(name, 20), cut(param, 20), cut(value, 20),
                    cut(setpoint, 20), cut(message, 50), cut(descriptor, 50),
                    cut(snaptime, 23));
                ExecSQL(SQL);
            }
        }

        public void RemoveAlarm(string key, string name, string param)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Alarms`" +
                    " where (`key`='{0}' and `name`='{1}' and `param`='{2}')",
                    cut(key, 20), cut(name, 20), cut(param, 20));
                ExecSQL(SQL);
            }
        }

        public void RemoveAlarms(string name)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Alarms` where (`name`='{0}')", cut(name, 20));
                ExecSQL(SQL);
            }
        }

        public void RemoveAllAlarmsLocal()
        {
            if (serverConnected)
            {
                ExecSQL("delete from `Alarms`");
            }
        }

        public void UpdateSwitch(int station, string name, string param, string value, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Switchs`" +
                    " (`station`,`name`,`param`,`value`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}')",
                    station, cut(name, 20), cut(param, 20), cut(value, 20),
                    cut(descriptor, 50), DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
            }
        }

        public void UpdateSwitch(int station, string name, string param, string value, string descriptor,
            string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Switchs`" +
                    " (`station`,`name`,`param`,`value`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}')",
                    station, cut(name, 20), cut(param, 20), cut(value, 20),
                    cut(descriptor, 50), cut(snaptime, 23));
                ExecSQL(SQL);
            }
        }

        public void RemoveSwitch(string name, string param)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Switchs`" +
                    " where (`name`='{0}' and `param`='{1}')",
                    cut(name, 20), cut(param, 20));
                ExecSQL(SQL);
            }
        }
 
        public void RemoveSwitchs(string name)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Switchs`" +
                    " where (`name`='{0}')", cut(name, 20));
                ExecSQL(SQL);
            }
        }

        public void RemoveAllSwitchsLocal()
        {
            if (serverConnected)
            {
                ExecSQL("delete from `Switchs`");
            }
        }

        public void AddToAlarmLog(int station, string name, string param, string value, string setpoint,
            string message, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `AlarmLog`" +
                    " (`station`,`name`,`param`,`value`,`setpoint`,`message`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    station, cut(name, 20), cut(param, 20), cut(value, 20), cut(setpoint, 20),
                    cut(message.Replace('\'', '"'), 50), cut(descriptor, 50),
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
            }
        }

        public void AddToAlarmLog(int station, string name, string param, string value, string setpoint,
            string message, string descriptor, string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `AlarmLog`" +
                    " (`station`,`name`,`param`,`value`,`setpoint`,`message`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    station, cut(name, 20), cut(param, 20), cut(value, 20), cut(setpoint, 20),
                    cut(message.Replace('\'', '"'), 50), cut(descriptor, 50), cut(snaptime, 23));
                ExecSQL(SQL);
            }
        }

        public void AddToSwitchLog(int station, string name, string param, string oldvalue,
            string newvalue, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `SwitchLog`" +
                    " (`station`,`name`,`param`,`oldvalue`,`newvalue`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                    station, cut(name, 20), cut(param, 20), cut(oldvalue.Replace('\'', '"'), 20),
                    cut(newvalue.Replace('\'', '"'), 20),
                    cut(descriptor.Replace('\'', '"'), 50), DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
            }
        }

        public void AddToSwitchLog(int station, string name, string param, string oldvalue,
            string newvalue, string descriptor, string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `SwitchLog`" +
                    " (`station`,`name`,`param`,`oldvalue`,`newvalue`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                    station, cut(name, 20), cut(param, 20), cut(oldvalue.Replace('\'', '"'), 20),
                    cut(newvalue.Replace('\'', '"'), 20),
                    cut(descriptor.Replace('\'', '"'), 50), cut(snaptime, 23));
                ExecSQL(SQL);
            }
        }

        private string cut(string value, int len)
        {
            return value.Substring(0, Math.Min(len, value.Length));
        }

        public void AddToChangeLog(int station, string name, string param, string oldvalue,
            string newvalue, string autor, string descriptor)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `ChangeLog`" +
                    " (`station`,`name`,`param`,`oldvalue`,`newvalue`,`autor`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    station, cut(name, 20), cut(param, 50), cut(oldvalue.Replace('\'', '"'), 50),
                    cut(newvalue.Replace('\'', '"'), 50),
                    cut(autor, 20), cut(descriptor.Replace('\'', '"'), 50), DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
            }
        }

        public void AddToChangeLog(int station, string name, string param, string oldvalue,
            string newvalue, string autor, string descriptor, string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("insert into `ChangeLog`" +
                    " (`station`,`name`,`param`,`oldvalue`,`newvalue`,`autor`,`descriptor`,`snaptime`)" +
                    " values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    station, cut(name, 20), cut(param, 50), cut(oldvalue.Replace('\'', '"'), 50),
                    cut(newvalue.Replace('\'', '"'), 50),
                    cut(autor, 20), cut(descriptor.Replace('\'', '"'), 50), cut(snaptime, 23));
                ExecSQL(SQL);
            }
        }

        private static string lastSystemMessage = String.Empty;
        public void AddToSystemLog(int station, string name, string descriptor)
        {
            if (serverConnected && !lastSystemMessage.Equals(station + name + descriptor))
            {
                lastSystemMessage = station + name + descriptor;
                string SQL = String.Format("insert into {0}.`SystemLog`" +
                    " (`station`,`name`,`descriptor`) values ({1},'{2}','{3}')",
                    getDatabaseName(DatabaseFrom.Database, false),
                    station, cut(name, 50), cut(descriptor.Replace('\'', '"'), 200));
                ExecSQL(SQL);
            }
        }

        public void AddToSystemLog(int station, string name, string descriptor, DateTime snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into {0}.`SystemLog`" +
                    " (`station`,`name`,`descriptor`,`snaptime`)" +
                    " values ({1},'{2}','{3}','{4}')",
                    getDatabaseName(DatabaseFrom.Database, false),
                    station, cut(name, 50), cut(descriptor.Replace('\'', '"'), 200),
                    snaptime.ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }

        public void DeleteFromTrends(DateTime snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `Trends` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy-MM-dd"));
                ExecSQL(SQL);
            }
        }
        public void DeleteFromLogs(DateTime snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `AlarmLog` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
                SQL = String.Format("delete from `SwitchLog` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
                SQL = String.Format("delete from `ChangeLog` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy.MM.dd HH:mm:ss.fff"));
                ExecSQL(SQL);
                SQL = String.Format("delete from `SystemLog` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy-MM-dd"));
                ExecSQL(SQL);
            }
        }
        public void DeleteFromTable(DateTime snaptime, string tabletype)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `{0}` where (`snaptime` < '{1}')",
                    tabletype, snaptime.ToString("yyyy-MM-dd"));
                ExecSQL(SQL);
            }
        }
        public void DeleteFromReportLog(DateTime snaptime)
        {
            if (serverConnected)
            {
                // stab заглушка
                string SQL = String.Format("delete from `ReportLog` where (`snaptime` < '{0}')",
                    snaptime.ToString("yyyy-MM-dd"));
                //ExecSQL(SQL);
            }
        }

        public Double GetHourData(string name, DateTime dt)
        {
            return GetTableData(name, dt, ParamTable.Hours);
        }

        public Double GetDailyData(string name, DateTime dt)
        {
            return GetTableData(name, dt, ParamTable.Days);
        }

        public Double GetMonthData(string name, DateTime dt)
        {
            return GetTableData(name, dt, ParamTable.Months);
        }

        private Double GetTableData(string name, DateTime dt, ParamTable kind)
        {
            Double result = Double.NaN;
            if (serverConnected)
            {
                string SQL;
                string timemask;
                switch (kind)
                {
                    case ParamTable.Minutes:
                        SQL = "select `value` from `minutes` " +
                            "where (`name`=@name and `kind`='Y' and `snaptime`=@snaptime)";
                        timemask = "yyyy-MM-dd HH:mm:00";
                        break;
                    case ParamTable.Hours:
                        SQL = "select `value` from `hours` " +
                            "where (`name`=@name and `kind`='Y' and `snaptime`=@snaptime)";
                        timemask = "yyyy-MM-dd HH:00:00";
                        break;
                    case ParamTable.Days:
                        SQL = "select `value` from `days` " +
                            "where (`name`=@name and `kind`='Y' and `snaptime`=@snaptime)";
                        timemask = "yyyy-MM-dd 00:00:00";
                        break;
                    case ParamTable.Months:
                        SQL = "select `value` from `months` " +
                            "where (`name`=@name and `kind`='Y' and `snaptime`=@snaptime)";
                        timemask = "yyyy-MM-01 00:00:00";
                        break;
                    default:
                        return result;
                }
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@snaptime", dt.ToString(timemask));
                    try
                    {
                        object data = command.ExecuteScalar();
                        if (data != null)
                        {
                            Double value;
                            if (Double.TryParse(data.ToString(), out value))
                            {
                                result = value;
                            }
                        }
                        lasterrorString = String.Empty;
                    }
                    catch (MySqlException e)
                    {
                        lasterrorString = e.Message;
                    }
                }
            }
            return result;
        }

        public void AddToTrend(string trendname, double value, bool kind)
        {
            if (serverConnected)
            {
                string sval = String.Format("{0}", value);
                sval = sval.Replace(',', '.');
                //string SQL = "insert into `Trends` (`name`,`value`,`kind`) values ("+
                //    "'" + cut(trendname, 25) + "'," + sval + ",'" + ((kind) ? "Y" : "N")+"')";
                string SQL = String.Format("replace into `Trends` (`name`,`value`,`kind`) " +
                    "values ('{0}',{1},'{2}')", cut(trendname, 25), sval, ((kind) ? "Y" : "N"));
                ExecSQL(SQL);
            }
        }

        public void AddToTrend(string trendname, double value, bool kind, DateTime snaptime)
        {
            if (serverConnected)
            {
                string sval = String.Format("{0}", value);
                sval = sval.Replace(',', '.');
                string SQL = String.Format("replace into `Trends` (`name`,`value`,`kind`,`snaptime`) " +
                    "values ('{0}',{1},'{2}','{3}')", cut(trendname, 25), sval,
                    ((kind) ? "Y" : "N"), snaptime.ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }
        
        public SortedList<DateTime, double> LoadTrend(string trendname, DateTime from, DateTime to, bool avg)
        {
            SortedList<DateTime, double> results = new SortedList<DateTime, double>();
            string SQL;
            if (!avg) 
                SQL = "select date_format(`snaptime`,'%Y-%m-%d %H:%i:%s'), avg(`value`), `kind` from `Trends` " +
                "where (`name`=@name and `snaptime`>=@fromtime and `snaptime`<=@totime) " +
                "group by date_format(`snaptime`,'%Y-%m-%d %H:%i:%s') order by `snaptime`";
            else
                SQL = "select date_format(`snaptime`,'%Y-%m-%d %H:%i'), avg(`value`), `kind` from `Trends` " +
                "where (`name`=@name and `snaptime`>=@fromtime and `snaptime`<=@totime) " +
                "group by date_format(`snaptime`,'%Y-%m-%d %H:%i') order by `snaptime`";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@name", trendname);
                command.Parameters.AddWithValue("@fromtime", from.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@totime", to.ToString("yyyy-MM-dd HH:mm:ss"));
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime dt = DateTime.Parse(reader.GetString(0)); //reader.GetDateTime(0);
                            double value = reader.GetDouble(1);
                            bool kind = (reader.GetString(2) == "Y") ? true : false;
                            if (!kind) value = double.NaN; 
                            if (!results.ContainsKey(dt)) results.Add(dt, value);
                        }
                    }
                }
            }
            return results;
        }

        public bool ImLive(string clientID, string clientKind, int stationNumber, string stationName)
        {
            if (serverConnected)
            {
                bool exists = false;
                string SQL = "select `id` from `Clients` where (`id`=@id)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@id", clientID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        exists = reader.HasRows;
                    }
                }
                if (exists)
                {
                    SQL = String.Format("update `Clients` set `snaptime`='{1}' " +
                    " where (`id`='{0}')", clientID,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ExecSQL(SQL);
                    return true;
                }
            }
            return false;
        }

        public List<string> GetLostClients(uint lostseconds)
        {
            List<string> result = new List<string>();
            string SQL = "select `id` from `Clients` where (`snaptime` < @snaptime)";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@snaptime",
                    DateTime.Now.AddSeconds(-lostseconds).ToString("yyyy-MM-dd HH:mm:ss"));
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            result.Add(reader.GetString(0));
                    }
                }
            }
            return result;
        }

        public void ClearLostClients(uint lostseconds)
        {
            if (serverConnected)
            {
                // удаление "висящих" клиентов
                string SQL = String.Format("delete from `Clients` where (`snaptime` < '{0}')",
                    DateTime.Now.AddSeconds(-lostseconds).ToString("yyyy-MM-dd HH:mm:ss"));
                ExecSQL(SQL);
            }
        }

        public void ClearAllClientsLocal()
        {
            if (serverConnected)
            {
                ExecSQL("delete from `Clients`");
            }
        }

        public void SendClientCommand(string clientID, string command, string args)
        {
            if (serverConnected)
            {
                string SQL = String.Format(
                    "update `Clients` set `command`='{1}',`params`='{2}',`answers`='{3}' " +
                    " where (`id`='{0}')", cut(clientID, 20), cut(command, 20), cut(args, 128), "");
                ExecSQL(SQL);
            }

        }

        public void UpdateClients(string kind, int station, string id, string descriptor, DateTime snaptime,
            string cmd, string pars, string answers, string status)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `Clients`" +
                    " (`kind`,`station`,`id`,`descriptor`,`snaptime`,`command`,`params`,`answers`,`status`)" +
                    " values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    cut(kind, 1), station, cut(id, 20), cut(descriptor, 50),
                    snaptime.ToString("yyyy-MM-dd HH:mm:ss"), cut(cmd, 20), cut(pars, 128),
                    cut(answers, 128), cut(status, 128));
                ExecSQL(SQL);
            }
        }

        public void SendShortUpCommand()
        {
            if (serverConnected)
            {
                string SQL = "update `Clients` set `command`='SHORTUP',`params`='',`answers`='' " +
                    " where (`kind`='S')";
                ExecSQL(SQL);
            }

        }

        public void SendClientAnswers(string clientID, string answers)
        {
            if (serverConnected)
            {
                string SQL = String.Format(
                    "update `Clients` set `command`='{1}',`params`='{2}',`answers`='{3}' " +
                    " where (`id`='{0}')", cut(clientID, 20), "", "", cut(answers, 128));
                ExecSQL(SQL);
            }

        }

        public string[] GetClientTypeAndDesc(string clientID)
        {
            string[] result = new string[] { "", "" };
            string SQL = "select `kind`,`descriptor` from `Clients` where (`id`=@id)";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@id", clientID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result[0] = reader.GetString(0);
                            result[1] = reader.GetString(1);
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        public string[] GetClientCommand(string clientID)
        {
            string[] result = new string[] {"", ""};
            string SQL = "select `command`,`params` from `Clients` where (`id`=@id)";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@id", clientID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result[0] = reader.GetString(0);
                            result[1] = reader.GetString(1);
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        public List<string> GetClients(int station)
        {
            List<string> result = new List<string>();
            string SQL = "select `id`,`descriptor` from `Clients` " +
                            "where (`station`=@station and `kind`='S')";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@station", station);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            result.Add(reader.GetString(0) + "=" + reader.GetString(1));
                    }
                }
            }
            bool noworkstation = (result.Count == 0);
            SQL = "select `id`,`descriptor` from `Clients` " +
                    "where (`station`=@station and `kind`<>'S')";
            using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
            {
                command.Parameters.AddWithValue("@station", station);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            result.Add(reader.GetString(0) + "=" + reader.GetString(1));
                    }
                }
            }
            if (result.Count > 0 && noworkstation)
                result.Insert(0, "=(без рабочей станции)");
            return result;
        }

        public ArrayList GetClientFetchList(string id)
        {
            ArrayList result = new ArrayList();
            if (serverConnected)
            {
                string SQL = "select `name`,`descriptor`,`value`,`quality`,`snaptime` " +
                                "from  `FetchLists` where `id`=@id";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> values = new List<string>();
                            values.Add(reader.GetString(0));
                            values.Add(reader.GetString(1));
                            values.Add(reader.GetString(2));
                            values.Add(reader.GetString(3));
                            string sDate = reader.GetString(4);
                            DateTime dt;
                            if (DateTime.TryParse(sDate, out dt))
                                values.Add(dt.ToString("yy-MM-dd HH:mm:ss"));
                            else
                                values.Add(sDate);
                            result.Add(values);
                        }
                    }
                }
            }
            return result;
        }

        public void UpdateClientFetchList(string id, string name, string desc, string value,
            string quality, string snaptime)
        {
            if (serverConnected)
            {
                string SQL = String.Format("replace into `FetchLists`" +
                    " (`id`,`name`,`descriptor`,`value`,`quality`,`snaptime`)" +
                    " values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    cut(id, 20), cut(name, 25), cut(desc, 100), cut(value, 25),
                    cut(quality, 25), cut(snaptime, 25));
                ExecSQL(SQL);
            }

        }

        public void ClearClientFetchList(string id)
        {
            if (serverConnected)
            {
                string SQL = String.Format("delete from `FetchLists` " +
                    "where (`id`='{0}')", id);
                ExecSQL(SQL);
            }
        }

    }

    public enum DatabaseFrom
    {
        Empty,
        Database,
        Fetchbase
    }

    public static class Settings
    {
        private static string lasterrorString = String.Empty;

        public static string LastError { get { return lasterrorString; } }

        public static bool CreateFetchbase(bool localhost)
        {
            bool result = false;
            using (ServerSQL server = new ServerSQL(DatabaseFrom.Empty, localhost))
            {
                if (server.Connected)
                {
                    string SQL = String.Format("CREATE DATABASE IF NOT EXISTS `{0}`" +
                                 " DEFAULT CHARACTER SET cp1251" +
                                 " DEFAULT COLLATE cp1251_general_ci",
                                 Fetchbase);
                    try
                    {
                        server.ExecSQL(SQL);
                        lasterrorString = String.Empty;
                        result = true;
                    }
                    catch (Exception e)
                    {
                        lasterrorString = e.Message;
                        result = false;
                    }
                }
                else
                    lasterrorString = ServerSQL.LastError;
            }
            if (result)
            {
                using (ServerSQL server = new ServerSQL(DatabaseFrom.Fetchbase, localhost))
                {
                    if (server.Connected)
                    {
                        try
                        {
                            server.ExecSQL("USE " + server.CurrentDatabase);
                            server.ExecSQL("SET CHARSET cp1251");
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `FetchLists` (" +
                                "`id` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`name` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Позиция', " +
                                "`descriptor` VARCHAR( 100 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`value` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Значение', " +
                                "`quality` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Качество', " +
                                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Время опроса', " +
                                "PRIMARY KEY (`id`,`name`)) ENGINE = MYISAM");
                            server.CheckTable("FetchLists", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Clients` (" +
                                "`kind` ENUM('S','F','E') NOT NULL DEFAULT 'S' COMMENT 'Тип клиента', " +
                                "`station` SMALLINT UNSIGNED NOT NULL DEFAULT 0 COMMENT 'Станция', " +
                                "`id` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Время записи', " +
                                "`command` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Команда', " +
                                "`params` VARCHAR( 128 ) NOT NULL DEFAULT '' COMMENT 'Параметры', " +
                                "`answers` VARCHAR( 128 ) NOT NULL DEFAULT '' COMMENT 'Ответы', " +
                                "`status` VARCHAR( 128 ) NOT NULL DEFAULT '' COMMENT 'Статус') ENGINE = MYISAM");
                            server.CheckTable("Clients", true);
                            CreateRealValsTable(server);
                            server.CheckTable("RealVals", true);
                            CreateAlarmsTable(server);
                            server.CheckTable("Alarms", true);
                            CreateSwitchsTable(server);
                            server.CheckTable("Switchs", true);
                            lasterrorString = String.Empty;
                            result = true;
                        }
                        catch (Exception e)
                        {
                            lasterrorString = e.Message;
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        public static void CreateSwitchsTable(ServerSQL server)
        {
            string SQL =
            SQL = "CREATE TABLE IF NOT EXISTS `Switchs` (" +
                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                "`name` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                "`param` VARCHAR( 20 ) NOT NULL COMMENT 'Параметр', " +
                "`value` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Значение', " +
                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Время записи', " +
                "PRIMARY KEY (`station`,`name`,`param`)) ENGINE = MYISAM";
            server.ExecSQL(SQL);
        }

        public static string CreateAlarmsTable(ServerSQL server)
        {
            string SQL;
            SQL = "CREATE TABLE IF NOT EXISTS `Alarms` (" +
                "`key` VARCHAR( 20 ) NOT NULL COMMENT 'Аларм', " +
                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                "`name` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                "`param` VARCHAR( 20 ) NOT NULL COMMENT 'Параметр', " +
                "`value` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Значение', " +
                "`setpoint` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Уставка', " +
                "`message` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Сообщение', " +
                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Время записи', " +
                "PRIMARY KEY (`key`,`station`,`name`,`param`)) ENGINE = MYISAM";
            server.ExecSQL(SQL);
            return SQL;
        }

        public static void CreateRealValsTable(ServerSQL server)
        {
            server.ExecSQL("CREATE TABLE IF NOT EXISTS `RealVals` (" +
                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                "`prop` VARCHAR( 25 ) NOT NULL COMMENT 'Свойство', " +
                "`value` VARCHAR( 50 ) NOT NULL COMMENT 'Значение', " +
                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                "ON UPDATE CURRENT_TIMESTAMP COMMENT 'Время записи', " +
                "PRIMARY KEY (`name`,`prop`)) ENGINE = MYISAM");
        }

        public static void DropRealValsTable(ServerSQL server)
        {
            server.ExecSQL("DROP TABLE IF EXISTS `RealVals`");
        }

        public static bool CreateDataAndFetchBases()
        {
            bool result = CreateDatabase(true);
            result = result && CreateFetchbase(true);
            if (!ServerSQL.HostIsLocalhost())
            {
                result = result && CreateDatabase(false);
                result = result && CreateFetchbase(false);
            }
            return result;
        }

        public static bool CreateDatabase(bool localhost)
        {
            bool result = false;
            using (ServerSQL server = new ServerSQL(DatabaseFrom.Empty, localhost))
            {
                if (server.Connected)
                {
                    string SQL = String.Format("CREATE DATABASE IF NOT EXISTS `{0}`" +
                                 " DEFAULT CHARACTER SET cp1251" +
                                 " DEFAULT COLLATE cp1251_general_ci",
                                 Database);
                    try
                    {
                        server.ExecSQL(SQL);
                        lasterrorString = String.Empty;
                        result = true;
                    }
                    catch (Exception e)
                    {
                        lasterrorString = e.Message;
                        result = false;
                    }
                }
                else
                    lasterrorString = ServerSQL.LastError;
            }
            if (result)
            {
                using (ServerSQL server = new ServerSQL(DatabaseFrom.Database, localhost))
                {
                    if (server.Connected)
                    {
                        try
                        {
                            string SQL;
                            server.ExecSQL("USE " + server.CurrentDatabase);
                            server.ExecSQL("SET CHARSET cp1251");
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Reports` (" +
                                "`name` VARCHAR( 128 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`value` LONGBLOB NOT NULL COMMENT 'Значение', " +
                                "`auto` ENUM('N','Y') NOT NULL DEFAULT 'N' COMMENT 'Автоматически', " +
                                "`time` VARCHAR( 8 ) NOT NULL DEFAULT '08:05:00' COMMENT 'Время печати', " +
                                "`period` INT( 4 ) NOT NULL DEFAULT 0 COMMENT 'Период печати', " +
                                "`filetime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP) ENGINE = MYISAM");
                            server.CheckTable("Reports", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Images` (" +
                                "`name` VARCHAR( 128 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`value` LONGBLOB NOT NULL COMMENT 'Значение', " +
                                "`filetime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP) ENGINE = MYISAM");
                            server.CheckTable("Images", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Dinamics` (" +
                                "`scheme` VARCHAR( 25 ) NOT NULL COMMENT 'Мнемосхема', " +
                                "`npp` INT( 4 ) NOT NULL COMMENT 'Номер по порядку', " +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`prop` VARCHAR( 25 ) NOT NULL COMMENT 'Свойство', " +
                                "`value` VARCHAR( 100 ) NOT NULL COMMENT 'Значение', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                                "ON UPDATE CURRENT_TIMESTAMP COMMENT 'Время записи', " +
                                "PRIMARY KEY (`scheme`,`name`,`prop`)) ENGINE = MYISAM");
                            server.CheckTable("Dinamics", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Points` (" +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`prop` VARCHAR( 25 ) NOT NULL COMMENT 'Свойство', " +
                                "`value` VARCHAR( 100 ) NOT NULL COMMENT 'Значение', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                                "ON UPDATE CURRENT_TIMESTAMP COMMENT 'Время записи', " +
                                "PRIMARY KEY (`name`,`prop`)) ENGINE = MYISAM");
                            server.CheckTable("Points", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Trends` (" +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`value` DOUBLE NOT NULL DEFAULT '0' COMMENT 'Значение', " +
                                "`kind` ENUM('N','Y') NOT NULL DEFAULT 'N' COMMENT 'Качество', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                                "ON UPDATE CURRENT_TIMESTAMP COMMENT 'Время записи') ENGINE = MYISAM");
                            server.CheckTable("Trends", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `GroupNames` (" +
                                "`group` INT( 3 ) NOT NULL COMMENT 'Группа', " +
                                "`descriptor` VARCHAR( 200 ) NOT NULL COMMENT 'Дескриптор', " +
                                "PRIMARY KEY (`group`)) ENGINE = MYISAM");
                            server.CheckTable("GroupNames", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `TableGroupNames` (" +
                                "`group` INT( 3 ) NOT NULL COMMENT 'Группа', " +
                                "`descriptor` VARCHAR( 200 ) NOT NULL COMMENT 'Дескриптор', " +
                                "PRIMARY KEY (`group`)) ENGINE = MYISAM");
                            server.CheckTable("TableGroupNames", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `Groups` (" +
                                "`group` INT( 3 ) NOT NULL COMMENT 'Группа', " +
                                "`place` INT( 3 ) NOT NULL COMMENT 'Место', " +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "PRIMARY KEY (`group`,`place`)) ENGINE = MYISAM");
                            server.CheckTable("Groups", true);
                            server.ExecSQL("CREATE TABLE IF NOT EXISTS `TableGroups` (" +
                                "`group` INT( 3 ) NOT NULL COMMENT 'Группа', " +
                                "`place` INT( 3 ) NOT NULL COMMENT 'Место', " +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "PRIMARY KEY (`group`,`place`)) ENGINE = MYISAM");
                            server.CheckTable("TableGroups", true);
                            string tableSQL = "CREATE TABLE IF NOT EXISTS `{0}` (" +
                                "`name` VARCHAR( 25 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`value` DOUBLE NOT NULL DEFAULT '0' COMMENT 'Значение', " +
                                "`kind` ENUM('N','Y') NOT NULL DEFAULT 'N' COMMENT 'Качество', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                                "COMMENT 'Время записи', PRIMARY KEY (`name`,`snaptime`)) ENGINE = MYISAM";
                            string[] tables = new string[4] { "minutes", "hours", "days", "months" };
                            foreach (string table in tables)
                            {
                                server.ExecSQL(String.Format(tableSQL, table));
                                server.CheckTable(table, true);
                            }
                            SQL = "CREATE TABLE IF NOT EXISTS `AlarmLog` (" +
                                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                                "`name` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`param` VARCHAR( 20 ) NOT NULL COMMENT 'Параметр', " +
                                "`value` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Значение', " +
                                "`setpoint` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Уставка', " +
                                "`message` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Сообщение', " +
                                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Время записи') ENGINE = MYISAM";
                            server.ExecSQL(SQL);
                            server.CheckTable("AlarmLog", true);
                            SQL = "CREATE TABLE IF NOT EXISTS `SwitchLog` (" +
                                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                                "`name` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`param` VARCHAR( 20 ) NOT NULL COMMENT 'Параметр', " +
                                "`oldvalue` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Было', " +
                                "`newvalue` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Стало', " +
                                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '' COMMENT 'Время записи') ENGINE = MYISAM";
                            server.ExecSQL(SQL);
                            server.CheckTable("SwitchLog", true);
                            SQL = "CREATE TABLE IF NOT EXISTS `ChangeLog` (" +
                                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                                "`name` VARCHAR( 20 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`param` VARCHAR( 50 ) NOT NULL COMMENT 'Параметр', " +
                                "`oldvalue` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Было', " +
                                "`newvalue` VARCHAR( 50 ) NOT NULL DEFAULT '' COMMENT 'Стало', " +
                                "`autor` VARCHAR( 20 ) NOT NULL DEFAULT '' COMMENT 'Кто сделал', " +
                                "`descriptor` VARCHAR( 50 ) NOT NULL DEFAULT '''' COMMENT 'Дескриптор', " +
                                "`snaptime` VARCHAR( 25 ) NOT NULL DEFAULT '''' COMMENT 'Время записи') ENGINE = MYISAM";
                            server.ExecSQL(SQL);
                            server.CheckTable("ChangeLog", true);
                            SQL = "CREATE TABLE IF NOT EXISTS `SystemLog` (" +
                                "`station` SMALLINT UNSIGNED NOT NULL COMMENT 'Станция', " +
                                "`name` VARCHAR( 50 ) NOT NULL COMMENT 'Идентификатор', " +
                                "`descriptor` VARCHAR( 200 ) NOT NULL DEFAULT '' COMMENT 'Дескриптор', " +
                                "`snaptime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP " +
                                "ON UPDATE CURRENT_TIMESTAMP COMMENT 'Время записи') ENGINE = MYISAM";
                            server.ExecSQL(SQL);
                            server.CheckTable("SystemLog", true);
                            lasterrorString = String.Empty;
                            result = true;
                        }
                        catch (Exception e)
                        {
                            lasterrorString = e.Message;
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        public static string Database
        {
            get { return Properties.Settings.Default.Database; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.Database = value;
                settings.Save();
            }
        }
        public static string Fetchbase
        {
            get { return Properties.Settings.Default.Fetchbase; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.Fetchbase = value;
                settings.Save();
            }
        }
        public static string Host
        {
            get { return Properties.Settings.Default.Host; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.Host = value;
                settings.Save();
            }
        }
        public static string Port
        {
            get { return Properties.Settings.Default.Port; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.Port = value;
                settings.Save();
            }
        }
        public static string User
        {
            get { return Properties.Settings.Default.User; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.User = value;
                settings.Save();
            }
        }
        public static string Password
        {
            get { return Properties.Settings.Default.Password; }
            set
            {
                Properties.Settings settings = Properties.Settings.Default;
                settings.Password = value;
                settings.Save();
            }
        }
    }
}
