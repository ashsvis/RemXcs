using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;

namespace IniFiles.Net
{
    /// <summary>
    /// Ini file handling relying on kernel32.dll functions. Calls unmanaged code. Most read/write calls are probably faster then TIniFile, but a bit less functionality. Also, ini file size cannot go over 64(32?)KB.
    /// </summary>
    public class TApiIniFile : BaseIni
    {

        /// <summary>
        /// Initilizes the class, sets the filename
        /// </summary>
        /// <param name="FileName"></param>
        public TApiIniFile(string FileName)
        {
            //in case file name isn't fullpath            
            this.fileName = new FileInfo(FileName).FullName;
        }

        #region kernel32.dll imports
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntA")]
        private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);//stringbuilder
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, IntPtr lpReturnedString, int nSize, string lpFileName); //IntPtr
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringA", SetLastError = true)]
        private static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        #endregion




        /// <summary>
        /// Does specified key exists in the section?
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <returns></returns>
        public override bool KeyExists(string Section, string Ident)
        {
            return (Array.BinarySearch(ReadSectionKeys(Section), Ident) >= 0);
        }

        /// <summary>
        /// Does specified section exist?
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override bool SectionExists(string Section)
        {
            return (Array.BinarySearch(ReadSections(), Section) >= 0);
        }



        #region Advanced read/modify

        /// <summary>
        /// Return all keys in specified section
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override string[] ReadSectionKeys(string Section)
        {
            //this code from http://pinvoke.net/default.aspx/kernel32.GetPrivateProfileSectionNames, modified
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER);
            uint bytesReturned = GetPrivateProfileString(Section, null, null, pReturnedString, MAX_BUFFER, this.FileName);
            if (bytesReturned == 0)
                return new string[0];//cannot return null here
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            return local.Substring(0, local.Length - 1).Split('\0');
        }

        /// <summary>
        /// Return all keys and values in section as a NameValueCollection
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override NameValueCollection ReadSectionNamesAndValues(string Section)
        {

            //GetPrivateProfileSection+marshalling is supposedly buggy, so we use this method here.
            NameValueCollection nv = new NameValueCollection();
            foreach (string key in ReadSectionKeys(Section))
            {
                nv.Add(key, ReadString(Section, key, ""));
            }
            return nv;
        }

        /// <summary>
        /// Read all section names
        /// </summary>
        /// <returns></returns>
        public override string[] ReadSections()
        {
            //this code from http://pinvoke.net/default.aspx/kernel32.GetPrivateProfileSectionNames
            uint MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER);
            uint bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, this.FileName);
            if (bytesReturned == 0)
                return new string[0]; //cannot return null here
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            return local.Substring(0, local.Length - 1).Split('\0');
        }

        /// <summary>
        /// Returns all values in the section
        /// </summary>
        /// <param name="Section"></param>
        /// <returns></returns>
        public override string[] ReadSectionValues(string Section)
        {
            ArrayList arr = new ArrayList();

            foreach (string key in ReadSectionKeys(Section))
            {
                arr.Add(ReadString(Section, key, ""));
            }

            return (string[])arr.ToArray(typeof(string));
        }

        /// <summary>
        /// Delete key from the file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        public override void DeleteKey(string Section, string Ident)
        {
            WriteString(Section, Ident, null);
        }

        #endregion

        #region basic read/write operations

        /// <summary>
        /// Read string from ini
        /// </summary>
        /// <param name="Section">Section name without []</param>
        /// <param name="Ident">Key</param>
        /// <param name="Default">Default value to return in case the value is missing</param>
        /// <returns>String</returns>
        public override string ReadString(string Section, string Ident, string Default)
        {
            StringBuilder sb = new StringBuilder(256);
            if (GetPrivateProfileString(Section, Ident, Default, sb, sb.Capacity, this.fileName) > 0) return sb.ToString();
            else return Default;
        }

        /// <summary>
        /// Write string to ini, throw error if fails
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public override void WriteString(string Section, string Ident, string Value)
        {
            if (WritePrivateProfileString(Section, Ident, Value, this.FileName) == 0) throw new Win32Exception();
            //Marshal.GetLastWin32Error should be called automagically
        }
        /// <summary>
        /// Read integer from ini
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public override int ReadInteger(string Section, string Ident, int Default)
        {
            return GetPrivateProfileInt(Section, Ident, Default, this.FileName);
        }


        #endregion
    }
}
