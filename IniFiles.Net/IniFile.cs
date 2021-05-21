using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

namespace IniFiles.Net
{
    /// <summary>
    /// Slower implementation of ini handling. File is not stored in the memory for extended period of time, all changes are instantly written to the file, all reads open the file for reading again. Does not use unmanaged code.
    /// </summary>
    public class IniFile : BaseIni
    {

        /// <summary>
        /// Initializes new TIniFile instance
        /// </summary>
        /// <param name="FileName"></param>
        public IniFile(string FileName)
        {
            this.FileName = FileName;
        }


        public override bool KeyExists(string Section, string Ident)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override bool SectionExists(string Section)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #region Read/write basic values
        /// <summary>
        /// Read string value from the file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public override string ReadString(string Section, string Ident, string Default)
        {
            return getVantedValue(Section, Ident, Default);
        }


        /// <summary>
        /// Write string value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public override void WriteString(string Section, string Ident, string Value)
        {
            writeValue(Section, Ident, Value);
        }
        #endregion


        #region Advanced read/write

        /// <summary>
        /// Read the key names in a section
        /// </summary>
        /// <param name="Section"></param>
        /// <returns>string[]</returns>
        public override string[] ReadSectionKeys(string Section)
        {
            return ReadSectionNamesAndValues(Section).AllKeys;
        }

        /// <summary>
        /// Returns NameValueCollection of key-value pairs found in that section. If section does not exist, a blank NameValueCollection is returned.
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override NameValueCollection ReadSectionNamesAndValues(string Section)
        {
            NameValueCollection keys = new NameValueCollection(10);
            string sTmp = ""; string[] sKeys = new string[2];
            foreach (string s in getSection(Section))
            {
                sTmp = s.Trim();
                if (sTmp.IndexOf('=') == -1) keys.Add(sTmp, "");
                else
                {
                    sKeys = sTmp.Split(new Char[] { '=' }, 2);
                    keys.Add(sKeys[0], sKeys[1]);
                }
            }

            return keys;
        }

        /// <summary>
        /// Read all section names
        /// </summary>
        /// <returns></returns>
        public override string[] ReadSections()
        {
            ArrayList arr = new ArrayList(10);
            string sTmp = "";
            foreach (string s in readFile())
            {
                sTmp = s.Trim();
                if (sTmp.StartsWith("[") && sTmp.EndsWith("]"))
                    arr.Add(sTmp);
            }
            return (string[])arr.ToArray(typeof(string));
        }

        // <summary>
        /// Return all values in string[] array
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override string[] ReadSectionValues(string Section)
        {
            NameValueCollection nv = new NameValueCollection(ReadSectionNamesAndValues(Section));
            ArrayList arr = new ArrayList(nv.Count);
            for (int i = 0; i < nv.Count; i++)
            {
                arr.Add(nv[i]);
            }
            return (string[])arr.ToArray(typeof(string));
        }


        public override void DeleteKey(string Section, string Ident)
        {
            Ident += "=";
            string sTmp = "";
            Section = String.Concat("[", Section, "]");
            bool sectionFound = false;
            foreach (string s in readFile())
            {
                sTmp = s.Trim();

                //if it isn't section start, look no further
                if ((!sectionFound) && (sTmp.Equals(Section)))
                {
                    sectionFound = true;
                    continue;
                }
                else if (!sectionFound) continue;

                //does string start with "Ident="
                if (sTmp.StartsWith(Ident))
                {
                    string sResult = sTmp;
                    break;
                }

                //if we get here, then a new section starts
                if (sTmp.StartsWith("[") && sTmp.EndsWith("]")) break;
            }
        }

        public override void EraseSection(string Section)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion



        #region general helper methods

        /// <summary>
        /// Reads the file to string[]
        /// </summary>
        /// <returns></returns>
        private string[] readFile()
        {
            if (File.Exists(fileName))
                return File.ReadAllLines(fileName);
            else return new string[0];
        }

        /// <summary>
        /// Write string[] to file, replacing existing contents
        /// </summary>
        /// <param name="lines"></param>
        private void writeFile(string[] lines)
        {
            try
            {
                File.WriteAllLines(this.fileName, lines);
            }
            catch { throw; } ///???
        }

        /// <summary>
        /// Gives line which is in given section and starts with "key="    
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <returns></returns>
        private string readWantedLine(string Section, string Ident)
        {
            Ident += "=";
            string sResult = "", sTmp = "";
            Section = String.Concat("[", Section, "]");
            bool sectionFound = false;
            foreach (string s in readFile())
            {
                sTmp = s.Trim();

                //if it isn't section start, look no further
                if ((!sectionFound) && (sTmp.Equals(Section)))
                {
                    sectionFound = true;
                    continue;
                }
                else if (!sectionFound) continue;

                //does string start with "Ident="
                if (sTmp.StartsWith(Ident))
                {
                    sResult = sTmp;
                    break;
                }

                //if we get here, then a new section starts
                if (sTmp.StartsWith("[") && sTmp.EndsWith("]")) break;
            }
            return sResult;
        }

        private ArrayList getSection(string Section)
        {

            Section = String.Concat("[", Section, "]");
            bool sectionFound = false;
            ArrayList arr = new ArrayList();
            string sTmp = "";
            foreach (string s in readFile())
            {
                sTmp = s.Trim();

                //if it isn't section start, look no further
                if ((!sectionFound) && (sTmp.Equals(Section)))
                {
                    sectionFound = true;
                    continue;
                }
                else if (!sectionFound) continue;

                //if we get here, then a new section starts
                if (sTmp.StartsWith("[") && sTmp.EndsWith("]") && sectionFound) break;

                //add to arraylist
                arr.Add(s);
            }

            return arr;
        }

        //TODO: getWantedValue and writeValue could use a whole lot of optimization and changes, especially writeValue!
        /// <summary>
        /// Returns value as string
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        private string getVantedValue(string Section, string Ident, string Default)
        {
            try
            {
                string sTmp = readWantedLine(Section, Ident);
                if (string.IsNullOrEmpty(sTmp)) return Default;
                sTmp = sTmp.Substring(sTmp.IndexOf('=') + 1);
                return sTmp.Trim();
            }
            catch { return Default; }
        }

        /// <summary>
        /// Write value to the file, replacing as needed. Needs badly better implementation!
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="value"></param>
        private void writeValue(string Section, string Ident, string value)
        {
            string IdentUpdated = Ident + "=";
            string SectionUpdated = String.Concat("[", Section, "]");
            string sTmp = "";

            StringBuilder sb = new StringBuilder();

            bool sectionExists = false, valueAdded = false, correctSection = false;

            foreach (string s in readFile())
            {
                sTmp = s.Trim();

                //if it isn't section start, look no further
                if ((!sectionExists) && (sTmp.Equals(SectionUpdated)) && (!valueAdded))
                {
                    sb.AppendLine(s);
                    correctSection = true;
                    sectionExists = true;
                    continue;
                }

                //does string start with "Ident="
                if (sTmp.StartsWith(IdentUpdated) && (correctSection == true))
                {
                    sb.AppendLine(string.Concat(Ident, "=", value));
                    valueAdded = true;
                    continue;
                }

                //if we get here, then a new section starts
                if (sTmp.StartsWith("[") && sTmp.EndsWith("]"))
                {
                    if ((correctSection) && (!valueAdded))
                    {
                        sb.AppendLine(string.Concat(Ident, "=", value));
                        valueAdded = true;
                    }
                    correctSection = false;
                }
                sb.AppendLine(s);
            }

            //end of readFile array and no value added --> no section or no value
            if (!valueAdded)
            {
                if (!sectionExists) sb.AppendLine(string.Concat("[", Section, "]"));
                sb.AppendLine(string.Concat(Ident, "=", value));
            }

            StreamWriter sw = File.CreateText(this.fileName);
            sw.Write(sb.ToString());
            sw.Close();
        }

        #endregion


    }
}
