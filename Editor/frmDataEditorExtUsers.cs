using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Points.Plugins;

namespace DataEditor
{
    public partial class frmDataEditor : Form, IUserInfo
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
        }

        public void ResetLogin()
        {
            currUserFullname = String.Empty;
            currStyle = UserLevel.Диспетчер;
            currWorked = false;
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

        private void LoadUsersList()
        {
            string usersfilename = Application.StartupPath + "\\users.xml";
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
            string usersfilename = Application.StartupPath + "\\users.xml";
            XmlSerializer s = new XmlSerializer(typeof(List<Пользователь>));
            using (TextWriter w = new StreamWriter(usersfilename))
            {
                s.Serialize(w, userList);
                w.Flush();
            }
        }
    }
}
