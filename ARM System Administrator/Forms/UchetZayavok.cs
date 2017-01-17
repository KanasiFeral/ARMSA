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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel; //Ссылка на Excel компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class UchetZayavok : Form
    {
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public UchetZayavok(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public int Check_Button;

        public BindingSource binSourceUchetZayavok = new BindingSource();

        //Загрузка формы
        private void UchetZayavok_Load(object sender, EventArgs e)
        {
            dataGridVUchet.ReadOnly = true;
            dataGridVUchet.AllowUserToAddRows = true;
            dataGridVUchet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVUchet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVUchet.MultiSelect = false;
            conMySQL.LoadTable("Zayavka", "SELECT * FROM `Zayavka`", binSourceUchetZayavok, dataGridVUchet, NavigatorUchet);

            dataGridVUchet.Columns[0].HeaderText = "Номер заявки(Системный)";
            dataGridVUchet.Columns[2].HeaderText = "Номер пользователя(Системный)";

            dataGridVUchet.Columns[0].Visible = false;
            dataGridVUchet.Columns[2].Visible = false;

            dataGridVUchet.Columns[1].HeaderText = "ФИО Преподавателя";
            dataGridVUchet.Columns[3].HeaderText = "Тип оборудования";
            dataGridVUchet.Columns[4].HeaderText = "Номер аудитории";
            dataGridVUchet.Columns[5].HeaderText = "Инвентарный номер";
            dataGridVUchet.Columns[6].HeaderText = "Цель заявки";
            dataGridVUchet.Columns[7].HeaderText = "Дата заявки";
            dataGridVUchet.Columns[8].HeaderText = "Дата возврата";
            dataGridVUchet.Columns[9].HeaderText = "Статут заявки";

            conMySQL.QueryToComboBox("SELECT CONCAT_WS(' ', `Fam_prep`, `Name_prep`, `Otch_prep`) FIO FROM Prepod", textFIO, "FIO");

            textFIO.SelectedIndex = 0;
            textTipUser.SelectedIndex = 0;
            textTipOborud.SelectedIndex = 0;
        }

        //Обработка события закрытия формы, а именно после закрытия формы
        private void UchetZayavok_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool CloseApp = true;

            if (conMySQL.QueryToBool("SELECT * FROM `Zayavka`") == false)
            {
                Main main = new Main(conMySQL);
                main.admin_status = true;
                main.Show();
            }
            else
            {
                for (int i = 0; i < dataGridVUchet.RowCount - 1; i++)
                {
                    if (dataGridVUchet[9, i].Value.Equals(""))
                    {
                        CloseApp = true;
                        break;
                    }
                    else
                    {
                        CloseApp = false;
                        //break;
                    }
                }

                if (CloseApp == true)
                {
                    MessageBox.Show("Проверьте статусы заявок", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                {
                    Main main = new Main(conMySQL);
                    main.admin_status = true;
                    main.Show();
                }
            }
        }

        //Кнопка "Удалить запись"
        private void buttonDeleteRecord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM Zayavka") == false)
                {
                    //Если все заявки удалены, записываем в таблицу число 0
                    conMySQL.QueryToBool("UPDATE `Count_Zayavok` SET `count_zayavok` = 0 WHERE `ID` = 1");

                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVUchet.CurrentRow.Index;
                    string id_Uchet = Convert.ToString(dataGridVUchet[0, i].Value);

                    if (dataGridVUchet[9, i].Value.Equals(""))
                    {
                        MessageBox.Show("Нельзя удалить непритяную заявку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Удаление строки
                        conMySQL.QueryToBool("DELETE FROM `Zayavka` WHERE `ID` = " + id_Uchet);
                        //Зачем здесь эта строка? Во славу Сатане конечно :3
                        binSourceUchetZayavok.RemoveAt(i);
                        conMySQL.LoadTable("Zayavka", "SELECT * FROM `Zayavka`", binSourceUchetZayavok, dataGridVUchet, NavigatorUchet);

                        try//Одеваем резинку на запись/чтение - Безопасность превыше всего :DDD
                        {
                            //Уменьшаем на 1 количество в таблице. Сначала читаем количество с сервера
                            string queryCount = conMySQL.AgregateQueryToDataGrid("SELECT `count_zayavok` FROM `Count_Zayavok` WHERE `ID` = 1");
                            //Потом читаем количество в строковую переменную
                            int CountZayavok = Convert.ToInt32(queryCount);
                            //Уменьшаем количество на 1
                            CountZayavok--;
                            //Создаем строку запроса
                            string QueryCount = "UPDATE `Count_Zayavok` SET `count_zayavok` = " + CountZayavok + " WHERE `ID` = 1";
                            //Меняем количество записей в таблице
                            conMySQL.QueryToBool(QueryCount);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "Ошибка");
                        }
                    }
                }
            }
        }

        //Кнопка "Фильтрация данных"
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            string DataZayavki, DataSdachi, FIO, TypeUser, TypeOborud;

            if (checkBoxDataZayavki.Checked == true)
            { DataZayavki = textDataZayavki.Text; }
            else
            { DataZayavki = String.Empty; }

            if (checkBoxDataSdachi.Checked == true)
            { DataSdachi = textDataSdachi.Text; }
            else
            { DataSdachi = String.Empty; }

            if (checkBoxFIO.Checked == true)
            { FIO = Convert.ToString(textFIO.Text); }
            else
            { FIO = String.Empty; }

            if (checkBoxTypeUser.Checked == true)
            { TypeUser = Convert.ToString(textTipUser.SelectedIndex); }
            else
            { TypeUser = String.Empty; }

            if (checkBoxTypeOborud.Checked == true)
            { TypeOborud = Convert.ToString(textTipOborud.Text); }
            else
            { TypeOborud = String.Empty; }



            string queryFilter = "SELECT * FROM `Zayavka` WHERE (`FIO_Prepod` LIKE '%" + FIO + "%' OR `FIO_Prepod` = '') and (`id_User` LIKE '%" + TypeUser +
                "%' OR `id_User` = '') and (`Type_Oborud` LIKE '%" + TypeOborud + "%' OR `Type_Oborud` = '') and (`Auditoria` LIKE '%" + textNomerAudit.Text +
                "%' OR `Auditoria` = '') and (`Inv_nomer` LIKE '%" + textInvNomer.Text + "%' OR `Inv_nomer` = '') and (`Tsel` LIKE '%" + textTselZayavki.Text + 
                "%' OR `Tsel` = '') and (`Data_zayavki` LIKE '%" + DataZayavki + "%' OR `Data_zayavki` = '') and (`Data_sdachi` LIKE '%" + DataSdachi + 
                "%' OR `Data_sdachi` = '') and (`Status_zayavki` LIKE '%" + textStatusZayavki.Text + "%' OR `Status_zayavki` = '')";

            conMySQL.LoadTable("Zayavka", queryFilter, binSourceUchetZayavok, dataGridVUchet, NavigatorUchet);
        }

        //Изменение статуса заявки
        private void comboBoxNewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Определяем индекс выбранной строки
            int i = dataGridVUchet.CurrentRow.Index;
            string id_Zayavki = Convert.ToString(dataGridVUchet[0, i].Value);
            string queryString = "UPDATE `Zayavka` SET `ID` = "
                        + id_Zayavki + ", `Status_zayavki` = '"
                        + comboBoxNewStatus.Text + "' WHERE `ID` = " + id_Zayavki;
            conMySQL.QueryToBool(queryString);
            conMySQL.LoadTable("Zayavka", "SELECT * FROM `Zayavka`", binSourceUchetZayavok, dataGridVUchet, NavigatorUchet);
        }

        //Кнопка "Поиск по таблице"
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.search_datagrid(dataGridVUchet, textSearchUchet);
        }

        //Кнопка "Очистка результатов поиска"
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.clear_datagrid(dataGridVUchet);
        }

        //Кнопка "Печать"
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVUchet);
        }

        //Кнопка "Показать все записи"
        private void buttonShowTable_Click(object sender, EventArgs e)
        {
            conMySQL.LoadTable("Zayavka", "SELECT * FROM `Zayavka`", binSourceUchetZayavok, dataGridVUchet, NavigatorUchet);
        }
    }
}