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
using Excel = Microsoft.Office.Interop.Excel; //Ссылка на Excel компоненты

namespace ARM_System_Administrator.Forms
{
    public partial class Prepods : Form
    {
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public Prepods(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public int Check_Button;

        public BindingSource binSourcePrepod = new BindingSource();

        //Обработка закрытия формы, а именно после ее закрытия
        private void Prepods_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
        }

        public bool SaveTable = true;

        //Загрузка формы
        private void Prepods_Load(object sender, EventArgs e)
        {
            dataGridVPrepod.AllowUserToAddRows = true;
            dataGridVPrepod.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVPrepod.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVPrepod.MultiSelect = false;
            conMySQL.LoadTable("prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
            if (conMySQL.QueryToBool("SELECT * FROM Prepod") == true)
            {
                dataGridVPrepod.Columns[0].Visible = false;
            }
            dataGridVPrepod.Columns[1].HeaderText = "Имя преподавателя";
            dataGridVPrepod.Columns[2].HeaderText = "Фамилия преподавателя";
            dataGridVPrepod.Columns[3].HeaderText = "Отчество преподавателя";
            dataGridVPrepod.Columns[4].HeaderText = "Дисциплина преподавателя";

            Main main = new Main(conMySQL);
            main.dataGridMaxLengthColumn50(dataGridVPrepod.ColumnCount, dataGridVPrepod);
        }

        public void ClearText()
        {
            textAddImyaPrepod.Clear();
            textAddFamPrepod.Clear();
            textAddOtchPrepod.Clear();
            textAddDisPrepod.Clear();
        }
        
        //Для сохранения изменений в базе
        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (SaveTable == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                    SaveTable = true;
                }
            }
            else
            {
                Main main = new Main(conMySQL);
                if (main.checkEmptyDataGridCell(0, dataGridVPrepod) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrepod.CurrentRow.Index;

                    string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Prepod`");

                    int chislo = Convert.ToInt32(Count);
                    chislo--;

                    if (i <= chislo)
                    {
                        string queryString = "UPDATE `Prepod` SET `Name_prep` = '"
                            + dataGridVPrepod[1, i].Value + "', `Fam_prep` = '"
                            + dataGridVPrepod[2, i].Value + "', `Otch_prep` = '"
                            + dataGridVPrepod[3, i].Value + "', `Distsiplina` = '"
                            + dataGridVPrepod[4, i].Value + "' WHERE `ID` = " + dataGridVPrepod[0, i].Value;
                        conMySQL.QueryToBool(queryString);

                        conMySQL.LoadTable("prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                    }
                    else
                    {
                        string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Prepod`");
                        int int_maxID = Convert.ToInt32(maxID);
                        int_maxID++;

                        string queryString = "INSERT INTO `Prepod` VALUES (" + int_maxID + ",'" + dataGridVPrepod[1, i].Value + "','" + dataGridVPrepod[2, i].Value + "','" +
                            dataGridVPrepod[3, i].Value + "','" + dataGridVPrepod[4, i].Value + "')";
                        conMySQL.QueryToBool(queryString);

                        conMySQL.LoadTable("Prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                    }

                    i++;
                    MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + main.indexRow + ", ячейка: " + main.indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", для удаления записи
        private void buttonDeleteRecordPrepod_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Prepod`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrepod.CurrentRow.Index;
                    string id_Prepod = Convert.ToString(dataGridVPrepod[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Prepod` WHERE `ID` = " + id_Prepod);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourcePrepod.RemoveAt(i);
                    conMySQL.LoadTable("Prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                }
            }
        }

        //Кнопка "Фильтрация данных", для фильтрации значений
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (textImyaPrepod.Text.Equals("") && textFamPrepod.Text.Equals("") &&
                textDisPrepod.Text.Equals("") && textOtchPrepod.Text.Equals(""))
            {
                SaveTable = true;
            }
            else
            {
                SaveTable = false;
            }

            string queryFilter = "SELECT * FROM `Prepod` WHERE (`Name_prep` LIKE '%" + textImyaPrepod.Text + "%' OR `Name_prep` = '') and (`Fam_prep` LIKE '%" +
                    textFamPrepod.Text + "%' OR `Fam_prep` = '') and (`Otch_prep` LIKE '%" + textOtchPrepod.Text + "%' OR `Otch_prep` = '') and (`Distsiplina` LIKE '%" +
                    textDisPrepod.Text + "%' OR `Distsiplina` = '')";

            conMySQL.LoadTable("Prepod", queryFilter, binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
        }

        //Показать всю таблицу
        private void buttonShowTable_Click(object sender, EventArgs e)
        {
            SaveTable = true;
            conMySQL.LoadTable("prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
        }

        //Кнопка "Очистка", для очистки полей ввода
        private void buttonClearPrepod_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        //Кнопка "Добавить/Изменить запись", для добавления или изменения записи
        private void buttonAddRecordPrepod_Click(object sender, EventArgs e)
        {
            if (textAddIdPrepod.Text.Equals("") || textAddImyaPrepod.Text.Equals("") ||
                textAddFamPrepod.Text.Equals("") || textAddOtchPrepod.Text.Equals("") ||
                textAddDisPrepod.Text.Equals(""))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    string queryString = "INSERT INTO `Prepod` VALUES (" + textAddIdPrepod.Text + ",'" +
                        textAddImyaPrepod.Text + "','" + textAddFamPrepod.Text + "','" + textAddOtchPrepod.Text + "','" +
                        textAddDisPrepod.Text + "')";
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                    ClearText();
                    int x = int.Parse(textAddIdPrepod.Text);
                    x++;
                    textAddIdPrepod.Text = Convert.ToString(x);
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrepod.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_prepod = Convert.ToString(dataGridVPrepod[0, i].Value);
                    string queryString = "UPDATE `Prepod` SET `ID` = "
                        + textAddIdPrepod.Text + ", `Name_prep` = '"
                        + textAddImyaPrepod.Text + "', `Fam_prep` = '"
                        + textAddFamPrepod.Text + "', `Otch_prep` = '"
                        + textAddOtchPrepod.Text + "', `Distsiplina` = '"
                        + textAddDisPrepod.Text + "' WHERE `ID` = " + id_prepod;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("prepod", "SELECT * FROM `Prepod`", binSourcePrepod, dataGridVPrepod, NavigatorPrepod);
                    ClearText();
                    panelPrepod.Visible = false;
                    buttonAdd.Enabled = true;
                    buttonEdit.Text = "Изменить";
                }
            }
        }

        //Кнопка "Крестик"
        private void buttonClosePanel_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelPrepod.Visible = false;
            buttonEdit.Enabled = true;
            buttonAdd.Enabled = true;
            ClearText();
        }

        //Кнопка "Добавить"
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textAddIdPrepod.Clear();
            string queryMaxIdPrepod = "SELECT MAX(ID) FROM `Prepod`";

            if (conMySQL.QueryToBool("SELECT * FROM `Prepod`") == true)
            {
                string MaxNomerIdPrepod = conMySQL.AgregateQueryToDataGrid(queryMaxIdPrepod);
                int x = Int16.Parse(MaxNomerIdPrepod);
                x++;
                textAddIdPrepod.Text = Convert.ToString(x);
            }
            else
            {
                textAddIdPrepod.Text = "1";
            }

            Check_Button = 0;
            buttonEdit.Enabled = false;
            panelPrepod.Visible = true;
            ClearText();
            label7.Text = "Добавление нового преподователя";
        }

        //Кнопка "Изменить"
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (conMySQL.QueryToBool("SELECT * FROM `Prepod`") == true)
            {
                ClearText();
                int x = dataGridVPrepod.CurrentRow.Index;
                //Забираем значение ячейки
                textAddIdPrepod.Text = Convert.ToString(dataGridVPrepod[0, x].Value);
                textAddImyaPrepod.Text = Convert.ToString(dataGridVPrepod[1, x].Value);
                textAddFamPrepod.Text = Convert.ToString(dataGridVPrepod[2, x].Value);
                textAddOtchPrepod.Text = Convert.ToString(dataGridVPrepod[3, x].Value);
                textAddDisPrepod.Text = Convert.ToString(dataGridVPrepod[4, x].Value);

                Check_Button = 1;
                buttonAdd.Enabled = false;
                panelPrepod.Visible = true;
                label7.Text = "Изменение данных преподователя";
            }
            else
            {
                MessageBox.Show("Нету данных для изменения!", "Ошибка!");
            }
        }

        //Поиск
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.search_datagrid(dataGridVPrepod, textSearch);
        }

        //Очистка
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.clear_datagrid(dataGridVPrepod);
        }

        //Печать
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVPrepod);
        }
    }
}