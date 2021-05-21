using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaseServer;

namespace DataEditor
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CustomExceptionHandler eh = new CustomExceptionHandler();
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(eh.OnThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            argsString = String.Join(" ", args);
            CheckArguments(args);
            Application.Run(new frmDataEditor());
        }

        public static string argsString = String.Empty;

        static string UnquoteString(string arg)
        {
            if (arg.StartsWith("\"") && arg.EndsWith("\""))
                return arg.Substring(1, arg.Length - 2);
            else
                return arg;
        }

        public static void CheckArguments(params string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.ToLower().StartsWith("-s"))
                {
                    int station;
                    if (int.TryParse(arg.Substring(2), out station))
                    {
                        Properties.Settings settings = Properties.Settings.Default;
                        settings.Station = station;
                        settings.Save();
                    }
                }
                else if (arg.ToLower().StartsWith("-r"))
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.CurrentUser = UnquoteString(arg.Substring(2));
                }
                else if (arg.ToLower().StartsWith("-p"))
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.CurrentPoint = UnquoteString(arg.Substring(2));
                }
                else if (arg.ToLower().StartsWith("-d"))
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.CurrentScheme = UnquoteString(arg.Substring(2));
                }
                else if (arg.ToLower().StartsWith("-t"))
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.CurrentReport = UnquoteString(arg.Substring(2));
                }
                else if (arg.ToLower().StartsWith("-g"))
                {
                    int groupno;
                    if (int.TryParse(arg.Substring(2), out groupno))
                    {
                        Properties.Settings settings = Properties.Settings.Default;
                        settings.CurrentGroup = groupno;
                    }
                }
                else if (arg.ToLower().StartsWith("-q"))
                {
                    int groupno;
                    if (int.TryParse(arg.Substring(2), out groupno))
                    {
                        Properties.Settings settings = Properties.Settings.Default;
                        settings.CurrentGroup = groupno;
                    }
                }
            }
        }

        internal class CustomExceptionHandler
        {
            // обработчик исключения
            public void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs t)
            {
                //MessageBox.Show(t.Exception.Message);
                Data.SendToSystemLog(Properties.Settings.Default.Station, "Редактор ресурсов",
                   String.Format("Фатальная ошибка: {0}", t.Exception.Message));
            }
        }
    }
}
