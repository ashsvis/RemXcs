using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Points.Plugins
{
    public partial class ClassPoint : IPointPlugin
    {
        static public string[] DigitalTextColors = new string[] 
                        {"Чёрный","Тёмно-синий","Фиолетовый","Коричневый","Синий","Лиловый",
                         "Красный","Тёмно-бирюзовый","Травяной","Зелёный","Ярко-зелёный",
                         "Бирюзовый","Жёлтый","Тёмно-серый","Светло-серый","Белый"};
        static public Color[] DigitalColors = new Color[]
                        { Color.Black, Color.DarkBlue, Color.DarkViolet, Color.Brown, Color.Blue,
                          Color.Violet, Color.Red, Color.DarkCyan, Color.Olive, Color.Green,
                          Color.LightGreen, Color.Cyan, Color.Yellow, Color.DarkGray, Color.LightGray,
                          Color.White};
        public int EnumEx(string value)
        {
            string[] vals = value.Split(new char[] { ';' });
            return (vals.Length == 2) ? int.Parse(vals[1]) : 0;
        }
        public decimal FloatEx(string value)
        {
            string[] vals = value.Split(new char[] { ';' });
            return FloatParse(vals[0]);
        }
        public decimal EnsureRange(decimal value, decimal low, decimal high)
        {
            return (value < low) ? low : (value > high) ? high : value; 
        }
        public decimal FloatParse(string value)
        {
            decimal result = 0m;
            string sval = value.Replace(',', '.');
            if (decimal.TryParse(sval, out result))
                return result;
            else
            {
                sval = value.Replace('.', ',');
                if (decimal.TryParse(sval, out result))
                    return result;
                else
                {
                    bool bres = false;
                    if (bool.TryParse(sval, out bres))
                        return (bres) ? 1m : 0m;
                }
            }
            return result;
        }
        protected string Float(object value)
        {
            return Float(value, 6);
        }
        string[] formats = new string[] { "{0:0}", "{0:0.0}", "{0:0.00}", "{0:0.000}",
                                          "{0:0000}", "{0:0.00000}", "{0:0.000000}" };
        protected string Float(object value, int format)
        {
            decimal val = FloatParse(value.ToString());
            return String.Format(formats[format], val);
        }
        protected void Add(string prop, int[] range)
        {
            intRanges.Add(prop, range);
        }
        protected void Add(string prop, string enumerate)
        {
            enumprops.Add(prop, enumerate);
        }
        protected void Add(string name, string desc, int type, object def)
        {
            descs.Add(name, desc);
            types.Add(name, type);
            defs.Add(name, def);
        }
        public string PluginName()
        {
            throw new NotImplementedException(); // реализуется потомками
        }
        public string PluginVersion
        {
            get { throw new NotSupportedException(); } // реализуется потомками
        }
        public string PluginAuthor
        {
            get { return "ASh Home"; }
        }
        public int PluginType
        {
            get { return PointPlaginType.Nothing; }
        }
        public string PluginShortType
        {
            get { throw new NotSupportedException(); }
        }
        public string PluginDescriptor
        {
            get { throw new NotSupportedException(); }
        }
        public string PluginCategory
        {
            get { throw new NotSupportedException(); } // реализуется потомками
        }
        public IDictionary<string, object> PropValueDefs() { return defs; }
        public Func<string, string> GetPropDesc
        {
            get
            {
                return (name) =>
                { return (descs.ContainsKey(name)) ? descs[name] : String.Empty; };
            }
        }
        public Func<string, int> GetPropType
        {
            get
            {
                return (name) =>
                { return (types.ContainsKey(name)) ? types[name] : PropType.Nothing; };
            }
        }

        public Color GetIconColor
        {
            get { throw new NotSupportedException(); } // реализуется потомками
        }

        public string[] GetFilterChildTypes
        {
            get { return new string[] {}; }
        }

        public string GetEnumValue(string propName, object propValue)
        {
            string result;
            if (enumprops.ContainsKey(propName))
            {
                string ename = enumprops[propName];
                if (enums.ContainsKey(ename))
                {
                    string[] vals = enums[ename];
                    int index = int.Parse(propValue.ToString());
                    result = (index < vals.Length) ? vals[index] : propValue.ToString();
                }
                else
                    result = String.Format("Enumeration \"{0}\" not found!", ename);
            }
            else
                result = String.Format("Enum prop \"{0}\" not found!", propName);
            return result;
        }
        public string GetFineValue(string propname, object propvalue)
        {
            return GetFineValue(propname, propvalue, 0);
        }
        public string GetFineValue(string propname, object propvalue, int ptformat)
        {
            int propType = GetPropType(propname);
            string result = String.Empty;
            switch (propType)
            {
                case PropType.Boolean:
                    result = ((bool)propvalue) ? "Да" : "Нет";
                    break;
                case PropType.Link:
                    if (propname == "Source")
                        result = ((string)propvalue == String.Empty) ? "Авто" : propvalue.ToString();
                    else
                        result = propvalue.ToString();
                    break;
                case PropType.Enumeration:
                    result = GetEnumValue(propname, propvalue);
                    break;
                case PropType.Float:
                    if (propname.Equals("Koeff") || propname.Equals("Offset"))
                        result = Float(propvalue);
                    else
                        result = Float(propvalue, ptformat);
                    break;
                case PropType.FloatEx:
                    string floatex = (string)propvalue;
                    string[] fvals = floatex.Split(new char[] { ';' });
                    string fval = Float(fvals[0], ptformat);
                    if (fvals.Length == 2)
                        result = String.Format("{0} ({1})", fval,
                            GetEnumValue(propname, int.Parse(fvals[1])));
                    else
                        result = String.Format("{0} ({1})", fval,
                            GetEnumValue(propname, 0));
                    break;
                case PropType.TypeOPC:
                    result = PropType.TextCDT(int.Parse(propvalue.ToString()));
                    break;
                default:
                    result = propvalue.ToString();
                    break;
            }
            return result;
        }
        public Func<string, IDictionary<string, object>, ChildDesc, List<FineRow>> GetFinePropList
        {
            get
            {
                return (name, values, callback) =>
                {
                    List<FineRow> results = new List<FineRow>();
                    int ptformat = (values.ContainsKey("FormatPV")) ? 
                        int.Parse(values["FormatPV"].ToString()) : 3;
                    foreach (KeyValuePair<string, object> prop in values)
                    {
                        if (GetPropType(prop.Key) != PropType.Invisible)
                        {
                            results.Add(new FineRow(GetPropDesc(prop.Key),
                                GetFineValue(prop.Key, prop.Value, ptformat), prop.Key, prop.Value,
                                GetPropType(prop.Key)));
                        }
                    }
                    return results;
                };
            }
        }
        public List<string> GetEnumItems(string propname)
        {
            List<string> result = new List<string>();
            if (enumprops.ContainsKey(propname))
            {
                string enumname = enumprops[propname];
                if (enums.ContainsKey(enumname))
                {
                    string[] items = enums[enumname];
                    foreach (string item in items) result.Add(item);
                }
            }
            return result;
        }
        public int[] GetIntPropRanges(string propname)
        {
            if (intRanges.ContainsKey(propname))
                return intRanges[propname];
            else
                return new int[2] { 0, 0 };
        }
        public IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            throw new NotImplementedException(); // реализуется потомками
        }
        public Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return null; // реализуется потомками
        }
        public Color BaseColor(int colorindex)
        {
            if (colorindex >= 0 && colorindex < DigitalColors.Length)
                return DigitalColors[colorindex];
            else
                return Color.Transparent;
        }
    }
}
