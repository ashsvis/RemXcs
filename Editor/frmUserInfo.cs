using System;
using System.Windows.Forms;
using Points.Plugins;

namespace DataEditor
{
    public partial class frmUserInfo : Form
    {
        UserLevel initstyle;
        Пользователь user = null;
        bool anew = true;
        public frmUserInfo(UserLevel preselect, Пользователь preuser)
        {
            InitializeComponent();
            initstyle = preselect;
            if (preuser != null)
            {
                user = preuser.Clone();
                anew = false;
            }
            else
            {
                user = new Пользователь();
                user.Категория = preselect;
            }
        }

        public Пользователь UserInfo { get { return user; } }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            cbCategory.Items.Clear();
            UserLevel[] UserStyleArray = 
                (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
            foreach (UserLevel cat in UserStyleArray)
            {
                cbCategory.Items.Add(cat.ToString());
            }
            cbCategory.SelectedIndex = (int)initstyle;
            if (!anew)
            {
                tbLastName.Text = user.Фамилия;
                tbFirstName.Text = user.Имя;
                tbMiddleName.Text = user.Отчество;
                cbCategory.SelectedIndex = (int)user.Категория;
                tbPassword.Text = user.Пароль;
                tbDoublePassword.Text = user.Пароль;
            }
            Idler.Enabled = true;
            UpdateButtons();
        }

        private void tbLastName_TextChanged(object sender, EventArgs e)
        {
            user.Фамилия = tbLastName.Text;
        }

        private void tbFirstName_TextChanged(object sender, EventArgs e)
        {
            user.Имя = tbFirstName.Text;
        }

        private void tbMiddleName_TextChanged(object sender, EventArgs e)
        {
            user.Отчество = tbMiddleName.Text;
        }

        private void cbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            user.Категория = (UserLevel)cbCategory.SelectedIndex;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            user.Пароль = tbPassword.Text;
        }

        private void UpdateButtons()
        {
            bool flag = true;
            flag = flag && !String.IsNullOrEmpty(tbLastName.Text.Trim());
            flag = flag && !String.IsNullOrEmpty(tbFirstName.Text.Trim());
            flag = flag && !String.IsNullOrEmpty(tbMiddleName.Text.Trim());
            flag = flag && !String.IsNullOrEmpty(tbPassword.Text.Trim());
            flag = flag && String.Equals(tbPassword.Text.Trim(),
                tbDoublePassword.Text.Trim());
            btnOk.Enabled = flag;
        }

        private void Idler_Tick(object sender, EventArgs e)
        {
            UpdateButtons();
        }
    }
}
