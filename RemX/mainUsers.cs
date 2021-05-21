using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using UsersEditor;

namespace RemXcs
{
    public partial class frmMain : Form, IUserInfo
    {
        private List<Пользователь> userList = new List<Пользователь>();

        public List<Пользователь> GetUserList(UserStyle style)
        {
            List<Пользователь> list = new List<Пользователь>();
            var QueryResults = from n in userList
                               where n.Категория == style
                               orderby n
                               select n;
            foreach (var item in QueryResults) list.Add(item);
            return list;
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
            if (File.Exists("users.xml"))
            {
                XmlSerializer s = new XmlSerializer(typeof(List<Пользователь>));
                using (TextReader r = new StreamReader("users.xml"))
                {
                    userList = (List<Пользователь>)s.Deserialize(r);
                }
            }
        }

        private void SaveUsersList()
        {
            XmlSerializer s = new XmlSerializer(typeof(List<Пользователь>));
            using (TextWriter w = new StreamWriter("users.xml"))
            {
                s.Serialize(w, userList);
                w.Flush();
            }
        }
    }
}
