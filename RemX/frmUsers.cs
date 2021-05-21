using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Points.Plugins;

namespace RemXcs
{

    public enum UsersEditorMode {Выбор, Правка}

    public partial class frmUsers : Form
    {
        Form DataHost;
        public frmUsers(Form host)
        {
            InitializeComponent();
            DataHost = host;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            fillTree();
            IUserInfo ui = (IUserInfo)DataHost;
            if (ui.UserLogged())
            {
                findTree(ui.CurrentUserFullname());
                btnCancelRegistration.Enabled = true;
            }
        }

        private void findTree(string fullname)
        {
            TreeNode[] nodes = tvList.Nodes.Find(fullname, true);
            if (nodes.Length >= 1)
            {
                tvList.SelectedNode = nodes[0];
                tvList.SelectedNode.Expand();
            }
        }

        private void fillTree()
        {
            IUserInfo ui = (IUserInfo)DataHost;
            List<Пользователь> list;
            UserLevel[] UserStyleArray = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
            tvList.BeginUpdate();
            try
            {
                tvList.Nodes.Clear();
                foreach (UserLevel category in UserStyleArray)
                {
                    TreeNode child;
                    TreeNode node = tvList.Nodes.Add(category.ToString());
                    node.Name = category.ToString();
                    node.Tag = category;
                    node.SelectedImageIndex = 0;
                    node.ImageIndex = 0;
                    list = ui.GetUserList(category);
                    foreach (Пользователь item in list)
                    {
                        child = node.Nodes.Add(item.ПолноеИмя());
                        child.Name = item.ПолноеИмя();
                        child.Tag = category;
                        child.SelectedImageIndex = 1;
                        child.ImageIndex = 1;
                    }
                }
            }
            finally
            {
                tvList.EndUpdate();
            }
        }

        private void CheckPassword()
        {
            using (frmPassword form = new frmPassword())
            {
                TreeNode node = tvList.SelectedNode;
                if (node != null)
                {
                    IUserInfo ui = (IUserInfo)DataHost;
                    Пользователь user = ui.FindByFullName(node.Text);
                    if (user != null)
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            if (String.Equals(user.Пароль, form.Password))
                            {
                                ui.LoginUser(user.ПолноеИмя(), user.ФИО(), user.Категория);
                                this.Close();
                            }
                            else
                                MessageBox.Show(this, "Пароль неверный.",
                                    "Регистрация пользователя",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void tvList_DoubleClick(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void tvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                CheckPassword();
            }
        }
    }
}
