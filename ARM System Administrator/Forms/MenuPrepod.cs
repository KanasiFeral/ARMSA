using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //Ссылка на MySQL компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class MenuPrepod : Form
    {
        public bool admin_status;
        public Classes.ConnectorSQL conMySQL;
        public MenuPrepod(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        //Сделать заявку
        private void buttonZayavka_Click(object sender, EventArgs e)
        {
            Zayavka zayavka = new Zayavka(conMySQL);
            zayavka.admin_status = false;
            zayavka.Show();
            this.Hide();
        }

        //Закрытие формы
        private void MenuPrepod_FormClosed(object sender, FormClosedEventArgs e)
        {
            Autorizatsia auto = new Autorizatsia(conMySQL);
            auto.Show();
        }

        //Проверить заявку
        private void buttonUchet_Click(object sender, EventArgs e)
        {
            UchetZayavokPrepod uchet = new UchetZayavokPrepod(conMySQL);
            uchet.Show();
            this.Hide();
        }
    }
}
