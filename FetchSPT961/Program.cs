using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaseServer;

namespace FetchSPT961
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CustomExceptionHandler eh = new CustomExceptionHandler();
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(eh.OnThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmFetchingSPT961());
        }

        internal class CustomExceptionHandler
        {
            // обработчик исключения
            public void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs t)
            {
                //MessageBox.Show(t.Exception.Message);
                Data.SendToSystemLog(Properties.Settings.Default.Station, "Опрос СПТ961",
                   String.Format("Фатальная ошибка: {0}: {1}", sender.ToString(),
                   t.Exception.Message));
            }
        }
    }
}
