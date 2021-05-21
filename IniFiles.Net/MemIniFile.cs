using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Collections;

namespace IniFiles.Net
{
    /// <summary>
    /// Inifile cached in memory, offers better performance then IniFile. Original file is not modified until UpdateFile() is called.
    /// </summary>
    public class MemIniFile : BaseIni
    {
        /// <summary>
        /// Private, contains [name]:NameValueCollection values
        /// </summary>
        private HybridDictionary values = new HybridDictionary(10, false);


        #region Load and Save;
        /// <summary>
        /// Create the class, load the ini file to memory
        /// </summary>
        /// <param name="fileName">File to load</param>    
        public MemIniFile(string fileName)
        {
            FileName = fileName;
            //Load
            loadIni(this.fileName);

        }

        /// <summary>
        /// Reloads ini from the file without updating it.
        /// </summary>
        public void ReLoad()
        {
            values.Clear();
            loadIni(this.fileName);
        }

        /// <summary>
        /// Load ini to memory. Private
        /// </summary>
        /// <param name="fileName"></param>
        private void loadIni(string fileName)
        {
            try
            {
                values.Clear();
                //no file found, nothing to load
                if (!File.Exists(fileName)) return;
                //read file to string array
                string[] allLines = File.ReadAllLines(fileName);

                #region Local variables
                string sHeader = "";
                string sTmp = "";
                string[] sKeys = new string[2];
                NameValueCollection keys = new NameValueCollection();
                #endregion


                //and restructure it in memory for faster access
                foreach (string s in allLines)
                {
                    sTmp = s.Trim();

                    //empty line, continue
                    if (string.IsNullOrEmpty(sTmp)) continue;

                    //age-old compromise/standard/tradition - anything that starts with ";" is a comment
                    if (sTmp.StartsWith(";")) continue;

                    //[header]
                    if (sTmp.StartsWith("[") && sTmp.EndsWith("]"))
                    {
                        //add values to collection
                        if (sHeader != "") values.Add(sHeader, new NameValueCollection(keys));
                        //new header
                        sHeader = sTmp.Substring(1, sTmp.Length - 2);
                        keys.Clear();
                        continue;
                    }
                    if (sTmp.IndexOf('=') == -1) keys.Add(sTmp, "");
                    else
                    {
                        sKeys = sTmp.Split(new Char[] { '=' }, 2);
                        keys.Add(sKeys[0], sKeys[1]);
                    }
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    WriteString(sHeader, keys.GetKey(i), keys.Get(i));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void FromString(string value)
        {
            try
            {
                values.Clear();

                //read file to string array
                string[] allLines = value.Split(new char[] { '\n' });

                #region Local variables
                string sHeader = "";
                string sTmp = "";
                string[] sKeys = new string[2];
                NameValueCollection keys = new NameValueCollection();
                #endregion

                //and restructure it in memory for faster access
                foreach (string s in allLines)
                {
                    sTmp = s.Trim();

                    //empty line, continue
                    if (string.IsNullOrEmpty(sTmp)) continue;

                    //age-old compromise/standard/tradition - anything that starts with ";" is a comment
                    if (sTmp.StartsWith(";")) continue;

                    //[header]
                    if (sTmp.StartsWith("[") && sTmp.EndsWith("]"))
                    {
                        //add values to collection
                        if (sHeader != "") values.Add(sHeader, new NameValueCollection(keys));
                        //new header
                        sHeader = sTmp.Substring(1, sTmp.Length - 2);
                        keys.Clear();
                        continue;
                    }
                    if (sTmp.IndexOf('=') == -1) keys.Add(sTmp, "");
                    else
                    {
                        sKeys = sTmp.Split(new Char[] { '=' }, 2);
                        keys.Add(sKeys[0], sKeys[1]);
                    }
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    WriteString(sHeader, keys.GetKey(i), keys.Get(i));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override string ToString()
        {
            try
            {
                List<string> list = new List<string>();
                foreach (string Section in values.Keys)
                {
                    list.Add(string.Concat("[" + Section + "]"));
                    NameValueCollection nv = (NameValueCollection)values[Section];
                    foreach (string Key in nv.Keys)
                    {
                        list.Add(Key + "=" + nv[Key]);
                    }
                }
                return String.Join("\n", list.ToArray());

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Save/update the ini file
        /// </summary>   
        public void UpdateFile()
        {
            try
            {
                File.Delete(this.fileName);

                using (StreamWriter sw = File.CreateText(this.fileName))
                {
                    foreach (string Section in values.Keys)
                    {
                        sw.WriteLine(string.Concat("[" + Section + "]"));
                        NameValueCollection nv = (NameValueCollection)values[Section];
                        foreach (string Key in nv.Keys)
                        {
                            sw.WriteLine(Key + "=" + nv[Key]);
                        }
                        sw.WriteLine(); //empty line after section name
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Clears the ini information from memory, does not delete the contents of the ini file - call UpdateFile() after Clear() if you want to do that.
        /// </summary>
        public override void Clear()
        {
            values.Clear();
        }

        #endregion

        #region Read and write basic values
        /// <summary>
        /// Read string from ini
        /// </summary>
        /// <param name="Section">Section name without []</param>
        /// <param name="Ident">Key</param>
        /// <param name="Default">Default value to return in case the value is missing</param>
        /// <returns>String</returns>
        public override string ReadString(string Section, string Ident, string Default)
        {
            if (!values.Contains(Section)) return Default;
            NameValueCollection nv = (NameValueCollection)values[Section];
            string s = nv.Get(Ident);
            if (s == null) return Default;//blank string is still a string, so no nullorempty
            return s;
        }

        /// <summary>
        /// Write string to ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public override void WriteString(string Section, string Ident, string Value)
        {
            NameValueCollection nv = null;
            if (!values.Contains(Section))
            {
                nv = new NameValueCollection();
                nv[Ident] = Value;
                values.Add(Section, nv);
            }
            else
            {
                nv = (NameValueCollection)values[Section];
                nv[Ident] = Value;
            }
        }



        #endregion

        #region Advanced read/manipulate

        /// <summary>
        /// Returns NameValueCollection of key-value pairs found in that section. If section does not exist, a blank NameValueCollection is returned.
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override NameValueCollection ReadSectionNamesAndValues(string Section)
        {
            if (!values.Contains(Section))
            {
                return new NameValueCollection();
            }
            else
            {
                return new NameValueCollection((NameValueCollection)values[Section]);
            }
        }

        /// <summary>
        /// Delete section from the ini.
        /// </summary>
        /// <param name="Section"></param>
        public override void EraseSection(string Section)
        {
            values.Remove(Section);
        }

        /// <summary>
        /// Read the key names in a section
        /// </summary>
        /// <param name="Section"></param>
        /// <returns>string[]</returns>
        public override string[] ReadSectionKeys(string Section)
        {
            if (!values.Contains(Section))
            {
                return new string[0];
            }
            else
            {
                return ((NameValueCollection)values[Section]).AllKeys;
            }
        }

        /// <summary>
        /// Return all values in string[] array
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override string[] ReadSectionValues(string Section)
        {
            if (!values.Contains(Section))
            {
                return new string[0];
            }
            else
            {

                ArrayList arr = new ArrayList();
                NameValueCollection nv = new NameValueCollection((NameValueCollection)values[Section]);
                foreach (string s in (nv.Keys))
                {
                    arr.Add(nv[s]);
                }
                return (string[])arr.ToArray(typeof(string));
            }
        }

        /// <summary>
        /// Read all section names
        /// </summary>
        /// <returns></returns>
        public override string[] ReadSections()
        {            
            string[] result = new string[values.Keys.Count];
            int i = 0;
            foreach(string item in values.Keys)
            {
                result[i++] = item;
            }
            return (string[])result;
        }

        /// <summary>
        /// Deletes key from section
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        public override void DeleteKey(string Section, string Ident)
        {
            if (!values.Contains(Section)) return;

            ((NameValueCollection)values[Section]).Remove(Ident);
        }

        /// <summary>
        /// Check if a section exists
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override bool SectionExists(string Section)
        {
            return values.Contains(Section);
        }

        /// <summary>
        /// Check if a key exists in specified section
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <returns></returns>
        public override bool KeyExists(string Section, string Ident)
        {
            if (!values.Contains(Section)) return false;
            return (((NameValueCollection)values[Section]).Get(Ident) != null);
        }

        #endregion

    }
}
