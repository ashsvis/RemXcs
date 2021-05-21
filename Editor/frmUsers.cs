using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Points.Plugins;

namespace DataEditor
{

    public enum UsersEditorMode {Выбор, Правка}

    public partial class frmUsers : Form
    {
        Form DataHost;
        bool EditorMode;
        public frmUsers(Form host, UsersEditorMode mode)
        {
            InitializeComponent();
            DataHost = host;
            EditorMode = (mode == UsersEditorMode.Правка);
            pntTools.Visible = EditorMode;
            panel1.Visible = !EditorMode;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            fillTree();
            IUserInfo ui = (IUserInfo)DataHost;
            if (ui.UserLogged() && !EditorMode)
            {
                findTree(ui.CurrentUserFullname());
                btnCancelRegistration.Enabled = true;
            }
            btnCancelRegistration.Visible = !EditorMode;
            Idler.Enabled = true;
            UpdateButtons();
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

        private void UpdateButtons()
        {
            btnAdd.Enabled = (tvList.SelectedNode != null);
            bool editmode = (tvList.SelectedNode != null) &&
                (tvList.SelectedNode.Level == 1);
            btnChange.Enabled = editmode;
            btnDelete.Enabled = editmode;
        }

        private void Idler_Tick(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserLevel style = UserLevel.Диспетчер;
            TreeNode node = tvList.SelectedNode;
            if (node != null)
            {
                while (node.Level > 0) node = node.Parent;
                style = (UserLevel)node.Tag;
            }
            using (frmUserInfo form = new frmUserInfo(style, null))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    IUserInfo ui = (IUserInfo)DataHost;
                    ui.AddNewUser(form.UserInfo);
                    fillTree();
                    findTree(form.UserInfo.ПолноеИмя());
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            TreeNode node = tvList.SelectedNode;
            if (node != null)
            {
                IUserInfo ui = (IUserInfo)DataHost;
                Пользователь user = ui.FindByFullName(node.Text);
                if (user != null)
                {
                    using (frmUserInfo form = new frmUserInfo(UserLevel.Диспетчер, user))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            ui.ChangeUser(user, form.UserInfo);
                            fillTree();
                            findTree(form.UserInfo.ПолноеИмя());
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = tvList.SelectedNode;
            if (node != null)
            {
                IUserInfo ui = (IUserInfo)DataHost;
                Пользователь user = ui.FindByFullName(node.Text);
                if (user != null)
                {
                    if (MessageBox.Show(this, "Удалить пользователя?",
                        "Редактор пользователей",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        ui.DeleteUser(user);
                        node = node.Parent;
                        fillTree();
                        if (node != null) findTree(node.Text);
                    }
                }
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
            if (btnChange.Enabled)
            {
                if (EditorMode)
                    btnChange.PerformClick();
                else
                    CheckPassword();
            }
        }

        private void tvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                if (btnChange.Enabled)
                {
                    if (EditorMode)
                        btnChange.PerformClick();
                    else
                        CheckPassword();
                }
            }
        }
    }
}
