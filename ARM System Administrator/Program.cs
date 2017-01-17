using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARM_System_Administrator.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Classes.ConnectorSQL ClassConSQL = new Classes.ConnectorSQL();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*Splashscreen first = new Splashscreen();
            DateTime end = DateTime.Now + TimeSpan.FromSeconds(1);
            first.Show();
            while (end > DateTime.Now)
            {
                Application.DoEvents();
            }
            first.Close();
            first.Dispose();*/
            //Application.Run(new Main());
            Application.Run(new Splashscreen(ClassConSQL));
        }
    }
}
