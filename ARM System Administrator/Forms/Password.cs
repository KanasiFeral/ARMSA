using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Forms
{
    public partial class Password : Form
    {
        public bool KakaFormaBilaVibrana;
        public Classes.ConnectorSQL conMySQL;
        public Password(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        //Заход в настройки/смену пароля
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Vhod();
        }

        //Отмена захода в настройки/смену пароля
        private void buttonCancel_Click(object sender, EventArgs e)
        {            
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
            this.Close();
        }

        //Загрузка формы
        private void Password_Load(object sender, EventArgs e)
        {
            password_user.UseSystemPasswordChar = true; //Включаем системное отображение пароля
        }

        //Процедура пусчания куды нужно после нажатия на кнопку ОК
        public void Vhod()
        {
            string queryToBool = "SELECT * FROM `Users` WHERE `Name_user` = 'admin' AND `Pass_user` = '" + password_user.Text + "'";

            if (conMySQL.QueryToBool(queryToBool) == true)
            {
                if (KakaFormaBilaVibrana == true) //Была выбрана форма настроек
                {
                    Settings Stngs = new Settings(conMySQL);
                    Stngs.button1.Enabled = false;
                    Stngs.button3.Enabled = true;
                    this.Close();
                    Stngs.Show();
                }
                else //Если была выбрана форма смены пароля
                {
                    Users users = new Users(conMySQL);
                    users.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Ваш пароль неверен, проверьте корректность данных", "Ошибка");
            }
        }

        //Происходит при нажатии клавишы клавиатуры
        private void password_user_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Vhod();
            }
        }
    }
}
