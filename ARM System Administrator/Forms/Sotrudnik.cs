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
    public partial class Sotrudnik : Form
    {
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public Sotrudnik(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public int Check_Button;

        public BindingSource binSourceSotr = new BindingSource();

        public bool SaveTable = true;

        //Обработка закрытия формы, а именно после ее закрытия
        private void Sotrudnik_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
        }

        //Для сохранения изменений в базе
        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (SaveTable == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Sotrudnik", "SELECT * FROM Sotrudnik", binSourceSotr, dataGridVSotr, NavigatorSotr);
                    SaveTable = true;
                }
            }
            else
            {
                Main main = new Main(conMySQL);
                if (main.checkEmptyDataGridCell(0, dataGridVSotr) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVSotr.CurrentRow.Index;

                    string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Sotrudnik`");

                    int chislo = Convert.ToInt32(Count);
                    chislo--;

                    if (i <= chislo)
                    {
                        string queryString = "UPDATE `Sotrudnik` SET `Fam_sotr` = '"
                        + dataGridVSotr[1, i].Value + "', `Name_sotr` = '"
                        + dataGridVSotr[2, i].Value + "', `Otch_sotr` = '"
                        + dataGridVSotr[3, i].Value + "', `Dolgnost` = '"
                        + dataGridVSotr[4, i].Value + "', `Auditoria` = '"
                        + dataGridVSotr[5, i].Value + "' WHERE `ID` = " + dataGridVSotr[0, i].Value;
                        conMySQL.QueryToBool(queryString);
                    }
                    else
                    {
                        string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Sotrudnik`");
                        int int_maxID = Convert.ToInt32(maxID);
                        int_maxID++;

                        string queryString = "INSERT INTO `Sotrudnik` VALUES (" + int_maxID + ",'" + dataGridVSotr[1, i].Value + "','" +
                        dataGridVSotr[2, i].Value + "','" + dataGridVSotr[3, i].Value + "','" + dataGridVSotr[4, i].Value + "','" +
                        dataGridVSotr[5, i].Value + "')";
                        conMySQL.QueryToBool(queryString);
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

        //Загрузка формы
        private void Sotrudnik_Load(object sender, EventArgs e)
        {
            dataGridVSotr.AllowUserToAddRows = true;
            dataGridVSotr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVSotr.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVSotr.MultiSelect = false;
            conMySQL.LoadTable("Sotrudnik", "SELECT * FROM `Sotrudnik`", binSourceSotr, dataGridVSotr, NavigatorSotr);
            if (conMySQL.QueryToBool("SELECT * FROM Sotrudnik") == true)
            {
                dataGridVSotr.Columns[0].Visible = false;
            }
            dataGridVSotr.Columns[1].HeaderText = "Фамилия сотрудника";
            dataGridVSotr.Columns[2].HeaderText = "Имя сотрудника";
            dataGridVSotr.Columns[3].HeaderText = "Отчество сотрудника";
            dataGridVSotr.Columns[4].HeaderText = "Должность сотрудника";

            Main main = new Main(conMySQL);
            main.dataGridMaxLengthColumn50(dataGridVSotr.ColumnCount, dataGridVSotr);
        }

        //Кнопка "Удалить запись", для удаления записи
        private void buttonDeleteRecord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Sotrudnik`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVSotr.CurrentRow.Index;
                    string id_Sotr = Convert.ToString(dataGridVSotr[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Sotrudnik` WHERE `ID` = " + id_Sotr);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceSotr.RemoveAt(i);
                    conMySQL.LoadTable("Sotrudnik", "SELECT * FROM `Sotrudnik`", binSourceSotr, dataGridVSotr, NavigatorSotr);
                }
            }
        }

        //Кнопка "Фильтрация данных", для фильтрации значений
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (textImyaPrepod.Text.Equals("") && textFamPrepod.Text.Equals("") && 
                textOtchPrepod.Text.Equals("") && textDisPrepod.Text.Equals(""))
            {
                SaveTable = true;
            }
            else
            {
                SaveTable = false;
            }

            string queryFilter = "SELECT * FROM `Sotrudnik` WHERE (`Fam_sotr` LIKE '%" + textImyaPrepod.Text + "%' OR `Fam_sotr` = '') and (`Name_sotr` LIKE '%" +
                textFamPrepod.Text + "%' OR `Name_sotr` = '') and (`Otch_sotr` LIKE '%" + textOtchPrepod.Text + "%' OR `Otch_sotr` = '') and (`Dolgnost` LIKE '%" + 
                textDisPrepod.Text + "%' OR `Dolgnost` = '')";

            conMySQL.LoadTable("Sotrudnik", queryFilter, binSourceSotr, dataGridVSotr, NavigatorSotr);
        }

        public void ClearText()
        {
            textAddImyaSotr.Clear();
            textAddFamSotr.Clear();
            textAddOtchSotr.Clear();
            textAddDolgnSotr.Clear();
        }

        //Кнопка "Крестик"
        private void buttonClosePanel_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelSotr.Visible = false;
            buttonEdit.Enabled = true;
            buttonAdd.Enabled = true;
            ClearText();
        }

        //Кнопка "Очистка", для очистки полей ввода
        private void buttonClearSotr_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        //Кнопка "Добавить"
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textAddIdSotr.Clear();
            string queryMaxIdSotr = "SELECT MAX(ID) FROM `Sotrudnik`";

            if (conMySQL.QueryToBool("SELECT * FROM `Sotrudnik`") == true)
            {
                string MaxNomerIdSotr = conMySQL.AgregateQueryToDataGrid(queryMaxIdSotr);
                int x = Int16.Parse(MaxNomerIdSotr);
                x++;
                textAddIdSotr.Text = Convert.ToString(x);
            }
            else
            {
                textAddIdSotr.Text = "1";
            }

            Check_Button = 0;
            buttonEdit.Enabled = false;
            panelSotr.Visible = true;
            ClearText();
            label7.Text = "Добавление нового сотрудника";
        }

        //Кнопка "Изменить"
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (conMySQL.QueryToBool("SELECT * FROM `Sotrudnik`") == true)
            {
                ClearText();
                int x = dataGridVSotr.CurrentRow.Index;
                //Забираем значение ячейки
                textAddIdSotr.Text = Convert.ToString(dataGridVSotr[0, x].Value);
                textAddImyaSotr.Text = Convert.ToString(dataGridVSotr[1, x].Value);
                textAddFamSotr.Text = Convert.ToString(dataGridVSotr[2, x].Value);
                textAddOtchSotr.Text = Convert.ToString(dataGridVSotr[3, x].Value);
                textAddDolgnSotr.Text = Convert.ToString(dataGridVSotr[4, x].Value);

                Check_Button = 1;
                buttonAdd.Enabled = false;
                panelSotr.Visible = true;
                label7.Text = "Изменение данных сотрудника";
            }
            else
            {
                MessageBox.Show("Нету данных для изменения!", "Ошибка!");
            }
        }

        //Кнопка "Добавить/Изменить запись", для добавления или изменения записи
        private void buttonAddRecordSotr_Click(object sender, EventArgs e)
        {
            if (textAddIdSotr.Text.Equals("") || textAddImyaSotr.Text.Equals("") ||
                textAddFamSotr.Text.Equals("") || textAddOtchSotr.Text.Equals("") ||
                textAddDolgnSotr.Text.Equals(""))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    string queryString = "INSERT INTO `Sotrudnik` VALUES (" + textAddIdSotr.Text + ",'" +
                        textAddImyaSotr.Text + "','" + textAddFamSotr.Text + "','" + textAddOtchSotr.Text + "','" +
                        textAddDolgnSotr.Text + "')";
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Sotrudnik", "SELECT * FROM `Sotrudnik`", binSourceSotr, dataGridVSotr, NavigatorSotr);
                    ClearText();
                    int x = int.Parse(textAddIdSotr.Text);
                    x++;
                    textAddIdSotr.Text = Convert.ToString(x);
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVSotr.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_Sotr = Convert.ToString(dataGridVSotr[0, i].Value);
                    string queryString = "UPDATE `Sotrudnik` SET `ID` = "
                        + textAddIdSotr.Text + ", `Fam_sotr` = '"
                        + textAddImyaSotr.Text + "', `Name_sotr` = '"
                        + textAddFamSotr.Text + "', `Otch_sotr` = '"
                        + textAddOtchSotr.Text + "', `Dolgnost` = '"
                        + textAddDolgnSotr.Text + "' WHERE `ID` = " + id_Sotr;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Sotrudnik", "SELECT * FROM Sotrudnik", binSourceSotr, dataGridVSotr, NavigatorSotr);
                    ClearText();
                    panelSotr.Visible = false;
                    buttonAdd.Enabled = true;
                    buttonEdit.Text = "Изменить";
                }
            }
        }

        //Поиск
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.search_datagrid(dataGridVSotr, textSearch);
        }

        //Очистка
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.clear_datagrid(dataGridVSotr);
        }

        //Печать
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVSotr);
        }
    }
}