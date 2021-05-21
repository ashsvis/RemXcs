using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace IniFiles.Net
{
    public abstract class BaseIni
    {


        protected string fileName;

        /// <summary>
        /// Ini file to read/write
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }


        //abstract methods, overridden in all ini versions
        public abstract bool KeyExists(string Section, string Ident);
        public abstract bool SectionExists(string Section);
        public abstract string[] ReadSectionKeys(string Section);
        public abstract NameValueCollection ReadSectionNamesAndValues(string Section);
        public abstract string[] ReadSections();
        public abstract string[] ReadSectionValues(string Section);
        public abstract void DeleteKey(string Section, string Ident);

        public abstract string ReadString(string Section, string Ident, string Default);
        public abstract void WriteString(string Section, string Ident, string Value);

        /// <summary>
        /// Clears the ini file. 
        /// </summary>
        public virtual void Clear()
        {
            try
            {
                File.WriteAllText(this.fileName, "");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Cannot be implemented in all ini versions, therefore virtual
        /// </summary>
        /// <param name="Section"></param>
        public virtual void EraseSection(string Section) { throw new NotImplementedException(); }

        /// <summary>
        /// Read integer from ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public virtual int ReadInteger(string Section, string Ident, int Default)
        {
            try
            {
                return Convert.ToInt32(this.ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch { return Default; }
        }

        /// <summary>
        /// Write integer to ini file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public void WriteInteger(string Section, string Ident, int Value)
        {
            try
            {
                this.WriteString(Section, Ident, Convert.ToString(Value));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Returns float value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public Double ReadFloat(string Section, string Ident, Double Default)
        {
            try
            {
                return Convert.ToDouble(this.ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch { return Default; }
        }

        /// <summary>
        /// Write float (double) to ini file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public void WriteFloat(string Section, string Ident, Double Value)
        {
            try
            {
                this.WriteString(Section, Ident, Convert.ToString(Value));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Read DateTime
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public DateTime ReadDate(string Section, string Ident, DateTime Default)
        {
            try
            {
                return Convert.ToDateTime(this.ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch { return Default; }
        }

        /// <summary>
        /// Write DateTime
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public void WriteDate(string Section, string Ident, DateTime Value)
        {
            try
            {
                this.WriteString(Section, Ident, Convert.ToString(Value));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Read boolean value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public bool ReadBool(string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(Convert.ToInt32((this.ReadString(Section, Ident, Convert.ToString(Default))))); //we want same behavior as Delphi, ie 1=true, 0=false
            }
            catch { return Default; }
        }

        /// <summary>
        /// Write boolean value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public void WriteBool(string Section, string Ident, bool Value)
        {
            try
            {
                this.WriteString(Section, Ident, Convert.ToString(Convert.ToInt32(Value)));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

    }
}
