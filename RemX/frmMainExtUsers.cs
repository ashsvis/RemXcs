using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Points.Plugins;
using BaseServer;

namespace RemXcs
{
    public partial class frmMain : Form, IUserInfo
    {
        private string currUserFullname = String.Empty;
        private UserLevel currStyle = UserLevel.Диспетчер;
        private bool currWorked = false;

        private List<Пользователь> userList = new List<Пользователь>();

        public List<Пользователь> GetUserList(UserLevel style)
        {
            List<Пользователь> list = new List<Пользователь>();
            var QueryResults = from n in userList
                               where n.Категория == style
                               orderby n
                               select n;
            foreach (var item in QueryResults) list.Add(item);
            return list;
        }

        public bool UserLogged()
        {
            return currWorked;
        }

        public string CurrentUserFullname()
        {
            return currUserFullname;
        }

        public void LoginUser(string fullname, string shortname, UserLevel level)
        {
            currUserFullname = fullname;
            currStyle = level;
            currWorked = true;
            if (panels != null)
                foreach (Form pan in panels)
                {
                    IUpdatePanel pm = (IUpdatePanel)pan;
                    pm.UpdateUserLogged(shortname, level);
                    pm.UpdateTuningMenu(level >= UserLevel.Инженер);
                }
        }

        public void ResetLogin()
        {
            currUserFullname = String.Empty;
            currStyle = UserLevel.Диспетчер;
            currWorked = false;
            if (panels != null)
                foreach (Form pan in panels)
                {
                    IUpdatePanel pm = (IUpdatePanel)pan;
                    pm.UpdateUserLogged("Нет регистрации", UserLevel.Диспетчер);
                    pm.UpdateTuningMenu(false);
                }
        }


        public void AddNewUser(Пользователь user)
        {
            userList.Add(user);
            SaveUsersList();
        }

        public void ChangeUser(Пользователь user, Пользователь source)
        {
            user.CopyFrom(source);
            SaveUsersList();
        }

        public void DeleteUser(Пользователь user)
        {
            userList.Remove(user);
            SaveUsersList();
        }

        public Пользователь FindByFullName(string fullname)
        {
            foreach (Пользователь user in userList)
            {
                if (String.Equals(user.ПолноеИмя(), fullname))
                {
                    return user;
                }
            }
            return null;
        }

        private void LoadUsersList(string usersfilename)
        {
            if (!File.Exists(usersfilename))
            {
                string contens = Properties.Resources.userlist;
                try { File.WriteAllText(usersfilename, contens); }
                catch (Exception ex)
                {
                    Data.SendToSystemLog(StationNumber, "Пользователи",
                        "Ошибка: " + ex.Message);
                }
            }
            if (File.Exists(usersfilename))
            {
                XmlSerializer s = new XmlSerializer(typeof(List<Пользователь>));
                using (TextReader r = new StreamReader(usersfilename))
                {
                    userList = (List<Пользователь>)s.Deserialize(r);
                }
            }
        }

        private void SaveUsersList()
        {
            // заглушка
        }
    }
}
