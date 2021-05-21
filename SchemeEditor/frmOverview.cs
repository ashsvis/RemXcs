#define TEST1
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Threading; // for Thread.Sleep

namespace SchemeEditor
{
    public partial class frmOverview : Form
    {
        string CurrFileName = String.Empty;
        AllDraws DrawsList = new AllDraws();

        public frmOverview()
        {
            InitializeComponent();
            drawBox.Location = new Point(0, 0);
            drawBox.Size = new Size(1280, 1024);
        }

        private void TestDriver()
        {
            string scriptFile = @"TestCScript.vbs";
            string scriptArgs = "arg1 arg2 arg3";
            bool waitForExit = true;
            ILaunchScript script = null;
            #if TEST1
                // Run using WScript.exe rather than CScript.exe
                script = new SimpleLaunchWsh( scriptFile, scriptArgs ) as ILaunchScript;
            #elif TEST2
                // Don't wait for script to finish - check on it later
                waitForExit = false;
                script = new SimpleLaunchWsh( scriptFile, scriptArgs, waitForExit ) as ILaunchScript;
            #else
                // The 5 sec timeout will interrupt the script which sleeps 10 seconds
                waitForExit = false;
                script = new SimpleLaunchWsh( scriptFile, scriptArgs, waitForExit, 5 ) as ILaunchScript;
            #endif
            script.Launch();

            string format = "{0} returned an Exit Code of {1}";
            string msg;
            // right after launch returns
            msg = string.Format( format, scriptFile, script.ExitCode );
            MessageBox.Show(msg, "Console", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //Console.WriteLine( msg );

            if (!waitForExit)
            {
                // do something else - Wait for 11 seconds
                Thread.Sleep(11000);
                msg = string.Format(format, scriptFile, script.ExitCode);
                //Console.WriteLine(msg);
                MessageBox.Show(msg, "Console", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmOverview_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
           // TestDriver();
        }

        public void LoadFile(string FileName)
        {
            if (FileName != "")
            {
                CurrFileName = FileName;
                this.Text = CurrFileName;
                FileStream fs = new FileStream(FileName, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    DrawsList = (AllDraws)formatter.Deserialize(fs);
                    foreach (Draws drw in DrawsList) drw.Initialize();
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
                this.Refresh();
            }
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            // Отрисовка массива фигур
            foreach (Draws drw in DrawsList) drw.DrawFigure(g);
        }

        private void drawBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int step = -Math.Sign(e.Delta) * 32;
            if (pnlScrollBox.VerticalScroll.Value + step < 0)
                pnlScrollBox.VerticalScroll.Value = 0;
            else
                pnlScrollBox.VerticalScroll.Value += step;
        }

        private void frmOverview_Activated(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }
    }
}
