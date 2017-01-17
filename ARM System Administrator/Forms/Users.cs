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
    public partial class Users : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public Users(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public BindingSource binSourceUsers = new BindingSource();

        //Загрузка формы
        private void Users_Load(object sender, EventArgs e)
        {
            comboBoxTypeUser.SelectedIndex = 0;
            textNewPass.UseSystemPasswordChar = true;
            textOldPass.UseSystemPasswordChar = true;
        }

        //Отмена смены пароля
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
            this.Close();
        }

        //Сменяем пароль
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string oldPass = textOldPass.Text;
            string newPass = textNewPass.Text;

            if (comboBoxTypeUser.SelectedIndex == 0) //Выбран администратор
            {
                string query = "SELECT * FROM `Users` WHERE `Name_user` = 'admin' AND `Pass_user` = '" + oldPass + "'";
                if (conMySQL.QueryToBool(query) == true)
                {
                    string changePass = "UPDATE `Users` SET `Pass_user` = '" + newPass + "' WHERE `Name_user` = 'admin'";

                    if (MessageBox.Show("Изменить пароль?\nДля сохранения изменений требуется перезапустить программу.\nВ случае отказа, пароль не будет изменен.",
                        "Ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        conMySQL.QueryToBool(changePass);
                        Splashscreen splash = new Splashscreen(conMySQL);
                        this.Close();
                        splash.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пароль не сохранен!", "Сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Старый пароль неверен!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //Выбран преподаватель
            {
                string query = "SELECT * FROM `Users` WHERE `Name_user` = 'user' AND `Pass_user` = '" + oldPass + "'";
                if (conMySQL.QueryToBool(query) == true)
                {
                    string changePass = "UPDATE `Users` SET `Pass_user` = '" + newPass + "' WHERE `Name_user` = 'user'";

                    if (MessageBox.Show("Изменить пароль?\nДля сохранения изменений требуется перезапустить программу.\nВ случае отказа, пароль не будет изменен.",
                        "Ошибка!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        conMySQL.QueryToBool(changePass);
                        Splashscreen splash = new Splashscreen(conMySQL);
                        this.Close();
                        splash.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пароль не сохранен!", "Сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Старый пароль неверен!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
