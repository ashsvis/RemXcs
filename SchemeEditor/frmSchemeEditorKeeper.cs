using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class frmSchemeEditorKeeper : Form, ISchemeEditor
    {
        List<Form> childforms = new List<Form>();
        public frmSchemeEditorKeeper()
        {
            InitializeComponent();
        }

// ================  Реализация ISchemeEditor ==========================================
  
        public void NewSchemeForm(string FileName)
        {
            frmSchemeEditor form = new frmSchemeEditor();
            childforms.Add(form);
            form.MdiParent = this;
            form.LoadFile(FileName);
            form.Show();
        }

        public void OpenSchemeFormAs()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                NewSchemeForm(openFileDialog.FileName);
            }
        }

        public void SaveSchemeAs()
        {
            Form activeChild = this.ActiveMdiChild;
            if ((activeChild != null) && (saveFileDialog.ShowDialog() == DialogResult.OK))
            {
                frmSchemeEditor cf = activeChild as frmSchemeEditor;
                if (cf != null) cf.SaveFileAs(saveFileDialog.FileName);
            }
        }

// ==============================================================

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSchemeForm("");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSchemeFormAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSchemeAs();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.SaveScheme();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.CutSelectedToClipboard();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.CopySelectedToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.PasteFromClipboardAndSelected();
        }

        private void miSelectAll_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.SelectAllFigures();
        }

        private void miUndo_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.UndoChanges();
        }

        private void miRedo_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;
            frmSchemeEditor cf = activeChild as frmSchemeEditor;
            if (cf != null) cf.RedoChanges();
        }
    }
}
