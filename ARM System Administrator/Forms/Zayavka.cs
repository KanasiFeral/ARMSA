using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Ссылка на Word компоненты
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARM_System_Administrator.Forms;
using MySql.Data.MySqlClient; //Ссылка на MySQL компоненты
using Excel = Microsoft.Office.Interop.Excel; //Ссылка на Excel компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class Zayavka : Form
    {
        public bool admin_status;
        public Classes.ConnectorSQL conMySQL;
        public Zayavka(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }
        
        public BindingSource binSourceZayavka = new BindingSource();

        //Загрузка формы
        private void Zayavka_Load(object sender, EventArgs e)
        {
            textDataSdachi.MinDate = DateTime.Now;

            if (conMySQL.QueryToBool("SELECT * FROM Prepod") == true)
            {
                if (conMySQL.QueryToBool("SELECT * FROM Auditoria") == true)
                {
                    if (admin_status == true)
                    {
                        textUser.Text = "Администратор";
                    }
                    else
                    {
                        textUser.Text = "Преподаватель";
                    }

                    if (conMySQL.QueryToBool("SELECT * FROM `Zayavka`") == true)
                    {
                        textNomerZ.Text = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM Zayavka");
                        string LastId = textNomerZ.Text;
                        int x = int.Parse(LastId);
                        x++;
                        textIdZayavka.Text = Convert.ToString(x);
                    }
                    else
                    {
                        textIdZayavka.Text = "1";
                    }

                    try
                    {
                        conMySQL.QueryToComboBox("SELECT CONCAT_WS(' ', `Fam_prep`, `Name_prep`, `Otch_prep`) FIO FROM `Prepod`", textFIO, "FIO");

                        conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAudit, "Nomer");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("В таблице аудитория отсутствуют данные!", "Ошибка!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("В таблице преподавателя отсутствуют данные!", "Ошибка!");
                this.Close();
            }
        }


        //Кнопка "Отмена", для отмены заявки
        private void buttonOtmena_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.admin_status = admin_status;
            this.Close();
        }

        //Кнопка "Сделать заявку", для добавления заявки
        private void buttonAddZayavka_Click(object sender, EventArgs e)
        {
            string textUser;
            string TselZayavki;

            if (textTipPO.Visible == true)
            {
                if (checkBoxAll.Checked == true)
                {
                    TselZayavki = textTsel.Text + " (" + checkBoxAll.Text + ")" + ": " + textTipPO.Text + ", " + textNamePO.Text;
                }
                else
                {
                    TselZayavki = textTsel.Text + ": " + textTipPO.Text + ", " + textNamePO.Text;
                }
            }
            else
            {
                TselZayavki = textTsel.Text + " (" + textOborud.Text + ", " + textDopInf.Text + ")";
            }          

            if (admin_status == true) 
            { textUser = "1"; }
            else 
            { textUser = "2"; }

            string FIO = textFIO.Text;
            string Fam_prep;
            string Name_Otch_prep;
            string Name_prep;
            string Otch_prep;

            //Селиверствова Дарья Николаевна
            int Probel_Posle_Fam = FIO.IndexOf(" ", 0); //Определяю вхождение первого пробела (После фамилии)

            Probel_Posle_Fam++;
            Name_Otch_prep = FIO.Substring(Probel_Posle_Fam, FIO.Length - Probel_Posle_Fam); //Копирование (Имя, Отчество)

            Fam_prep = FIO.Remove(Probel_Posle_Fam); //Копируем фамилию преподователя в переменную

            //Дарья Николаевна
            int Probel_Posle_Imeni = Name_Otch_prep.IndexOf(" ", 1); //Определяю вхождение первого пробела (После имени)

            Probel_Posle_Imeni++;
            Otch_prep = Name_Otch_prep.Substring(Probel_Posle_Imeni, Name_Otch_prep.Length - Probel_Posle_Imeni); //Копирование (Отчество)
                        
            Name_prep = Name_Otch_prep.Remove(Probel_Posle_Imeni); //Копируем имя преподователя в переменную
            
            if ((textTsel.SelectedIndex == 2) || (textTsel.SelectedIndex == 3)) //Установка и переустановка ПО
            {
                if (textFIO.Text.Equals("") || textOborud.Text.Equals("") || textAudit.Text.Equals("") ||
                textInvNomer.Text.Equals("") || textTsel.Text.Equals("") || textDataZayavki.Text.Equals("") ||
                textDataSdachi.Text.Equals(""))
                {
                    MessageBox.Show("Не все поля выбраны или введены", "Ошибка!");
                }
                else
                {
                    string queryString = "INSERT INTO Zayavka VALUES(" + textIdZayavka.Text + ",'" + textFIO.Text + "'," +
                        textUser + ",'" + textOborud.Text + "'," + textAudit.Text + ",'" + textInvNomer.Text + "','" +
                        TselZayavki + "','" + textDataZayavki.Text + "','" + textDataSdachi.Text + "','')";
                    conMySQL.QueryToBool(queryString);
                    MessageBox.Show("Выша заявка принята\nВычислительный центр рассмотрит вашу заявку в скорем времени\nПо всем вопросам обращайтесь в 426 аудиторию",
                        "Удачно!"); 
                    this.Close();                    
                }
            }
            else
            {
                if (textFIO.Text.Equals("") || textOborud.Text.Equals("") || textAudit.Text.Equals("") ||
                textInvNomer.Text.Equals("") || textTsel.Text.Equals("") || textDataZayavki.Text.Equals("") ||
                textDataSdachi.Text.Equals("") || textDopInf.Text.Equals(""))
                {
                    MessageBox.Show("Не все поля выбраны или введены", "Ошибка!");
                }
                else
                {
                    string queryString = "INSERT INTO Zayavka VALUES(" + textIdZayavka.Text + ",'" + textFIO.Text + "'," +
                        textUser + ",'" + textOborud.Text + "'," + textAudit.Text + ",'" + textInvNomer.Text + "','" +
                        TselZayavki + "','" + textDataZayavki.Text + "','" + textDataSdachi.Text + "','')";
                    conMySQL.QueryToBool(queryString);
                    MessageBox.Show("Выша заявка принята\nВычислительный центр рассмотрит вашу заявку в скорем времени\nПо всем вопросам обращайтесь в 426 аудиторию",
                        "Удачно!");
                    this.Close();
                }
            }
        }

        //Обработка закрытия формы, а именно после закрытия формы
        private void Zayavka_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (admin_status == true)
            {
                Main main = new Main(conMySQL);
                main.admin_status = true;
                main.Show();
            }
            else
            {
                MenuPrepod menuPrep = new MenuPrepod(conMySQL);
                menuPrep.admin_status = false;
                menuPrep.Show();
            }
        }

        //Цель заявки, указываем доп. условия
        private void textTsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textTsel.SelectedIndex == 3) //Если выбрана переустановка ПО
            {
                label12.Visible = true;
                textTipPO.Visible = true;
                textNamePO.Visible = true;
                string queryString = "SELECT DISTINCT `Tip_PO` FROM Program_Obespech WHERE `Auditoria` = '" + textAudit.Text + "'";
                conMySQL.QueryToComboBox(queryString, textTipPO, "Tip_PO");

                label4.Visible = true;
                label6.Visible = true;
                textOborud.Visible = true;
                textInvNomer.Visible = true;
                textOborud.SelectedIndex = 0;
                textOborud.Enabled = false;
                checkBoxAll.Visible = true;
                checkBoxAll.Text = "Переустановить на всех компьютерах";

                labelDopInf.Visible = false;
                textDopInf.Visible = false;
            }
            else if (textTsel.SelectedIndex == 2) //Если выбрано установка ПО
            {
                label12.Visible = true;
                textTipPO.Visible = true;
                textNamePO.Visible = true;
                string queryString = "SELECT DISTINCT `Tip_PO` FROM Program_Obespech";
                conMySQL.QueryToComboBox(queryString, textTipPO, "Tip_PO");

                label4.Visible = true;
                label6.Visible = true;
                textOborud.Visible = true;
                textInvNomer.Visible = true;

                textOborud.SelectedIndex = 0;
                textOborud.Enabled = false;
                checkBoxAll.Visible = true;
                checkBoxAll.Text = "Установить на всех компьютерах";

                labelDopInf.Visible = false; //Надпись, делаем видимой
                textDopInf.Visible = false; //Доп. инфа делаем видимой
            }
            else//Если выбрана чистка или ремонт
            {
                label12.Visible = false;
                textTipPO.Visible = false;
                textNamePO.Visible = false;
                label4.Visible = true;
                label6.Visible = true;
                textOborud.Visible = true;
                textInvNomer.Visible = true;

                textOborud.SelectedIndex = 0;
                textOborud.Enabled = true;
                checkBoxAll.Visible = false;

                labelDopInf.Visible = true; //Надпись, делаем видимой
                textDopInf.Visible = true; //Доп. инфа делаем видимой
            }
        }

        //Прогружаем названия ПО
        private void textTipPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TipPO = textTipPO.Text;
            string queryString = "SELECT DISTINCT `Nazvanie_PO` FROM `Program_Obespech` WHERE `Tip_PO` = '" + TipPO + "'";

            conMySQL.QueryToComboBox(queryString, textNamePO, "Nazvanie_PO");
        }

        //Процедура прогрузки инвентарных номеров
        public void Inv_Nomer()
        {
            string NomerAudit = textAudit.Text;
            string TipOborud = textOborud.Text;
            string TipOborudQuery = "";
            if (TipOborud == "Компьютер")
            {
                //Компьютеры
                if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == true)
                {
                    TipOborudQuery = "Param_sis_blokov";

                    try
                    {
                        string queryStringInvNomer = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        if (conMySQL.QueryToBool(queryStringInvNomer) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_sis_blokov` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
            //Ноутбуки
            if (TipOborud == "Ноутбук")
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_nout`") == true)
                {
                    TipOborudQuery = "Param_nout";

                    try
                    {
                        string queryStringInvNomer = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                        if (conMySQL.QueryToBool(queryStringInvNomer) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_nout` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
            //Принтеры
            if (TipOborud == "Принтер")
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_print`") == true)
                {
                    TipOborudQuery = "Param_print";

                    try
                    {
                        string queryStringInvNomer = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                        if (conMySQL.QueryToBool(queryStringInvNomer) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_print` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
            //Мышки
            if (TipOborud == "Мышь")
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_mishek`") == true)
                {
                    TipOborudQuery = "Param_mishek";

                    try
                    {
                        string queryStringInvNomerMishek = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        conMySQL.QueryToComboBox(queryStringInvNomerMishek, textInvNomer, "Inv_nomer");
                        if (conMySQL.QueryToBool(queryStringInvNomerMishek) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomerMishek, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_mishek` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
            //Мониторы
            if (TipOborud == "Монитор")
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_monitor`") == true)
                {
                    TipOborudQuery = "Param_monitor";

                    try
                    {
                        string queryStringInvNomer = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                        if (conMySQL.QueryToBool(queryStringInvNomer) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_monitor` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
            //Клавиатуры
            if (TipOborud == "Клавиатура")
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_klava`") == true)
                {
                    TipOborudQuery = "Param_klava";

                    try
                    {
                        string queryStringInvNomer = "SELECT `Inv_nomer` FROM `"
                            + TipOborudQuery + "` WHERE `Auditoria` = " + NomerAudit + " AND `Status` = 'Используется'";
                        conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                        if (conMySQL.QueryToBool(queryStringInvNomer) == false)
                        {
                            MessageBox.Show("В аудитории " + textAudit.Text + " отсутствует данное оборудование", "Ошибка!");
                            textInvNomer.Visible = false;
                        }
                        else
                        {
                            conMySQL.QueryToComboBox(queryStringInvNomer, textInvNomer, "Inv_nomer");
                            textInvNomer.Visible = true;
                        }
                    }
                    catch
                    {
                        textInvNomer.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Таблица `Param_klava` пуста!", "Ошибка!");
                    textInvNomer.Visible = false;
                }
            }
        }

        //Прогрузка инвентарных номеров
        private void textAudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inv_Nomer();
        }

        //Выбор оборудования
        private void textOborud_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inv_Nomer();
        }
    }
}