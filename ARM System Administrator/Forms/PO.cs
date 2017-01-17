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
    public partial class PO : Form
    {
        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public PO(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        public int Check_Button;

        public BindingSource binSourcePO = new BindingSource();

        //Очистка полей ввода
        public void ClearText()
        {
            textAddTipPO.Clear();
            textAddNamePO.Clear();
        }

        public bool SaveTable = true;

        //Загрузка формы
        private void PO_Load(object sender, EventArgs e)
        {
            dataGridVPO.ReadOnly = false;
            dataGridVPO.AllowUserToAddRows = true;
            dataGridVPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridVPO.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridVPO.MultiSelect = false;
            conMySQL.LoadTable("Program_Obespech", "SELECT * FROM `Program_Obespech`", binSourcePO, dataGridVPO, NavigatorPO);
            if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
            {
                conMySQL.QueryToComboBox("SELECT `Nomer` FROM Auditoria", textAddAudit, "Nomer");
            }

            dataGridVPO.Columns[0].Visible = false;

            dataGridVPO.Columns[1].HeaderText = "Тип программного обеспечения";
            dataGridVPO.Columns[2].HeaderText = "Название программного обеспечения";
            dataGridVPO.Columns[3].HeaderText = "Аудитория";

            Main main = new Main(conMySQL);
            main.dataGridMaxLengthColumn50(dataGridVPO.ColumnCount, dataGridVPO);
        }

        //Обработка закрытия формы, а именно после ее закрытия
        private void PO_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main main = new Main(conMySQL);
            main.admin_status = true;
            main.Show();
        }
        
        //Кнопка "Удалить запись"
        private void buttonDeleteRecord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Program_Obespech`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPO.CurrentRow.Index;
                    string id_PO = Convert.ToString(dataGridVPO[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Program_Obespech` WHERE `ID` = " + id_PO);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourcePO.RemoveAt(i);
                    conMySQL.LoadTable("Program_Obespech", "SELECT * FROM `Program_Obespech`", binSourcePO, dataGridVPO, NavigatorPO);
                }
            }
        }

        //Кнопка "Фильтрация данных"
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (textTipPO.Text.Equals("") && textNamePO.Text.Equals("") &&
                textAudit.Text.Equals(""))
            {
                SaveTable = true;
            }
            else
            {
                SaveTable = false;
            }

            string queryFilter = "SELECT * FROM `Program_Obespech` WHERE (`Tip_PO` LIKE '%" + textTipPO.Text + "%' OR `Tip_PO` = '') and (`Nazvanie_PO` LIKE '%" + 
                textNamePO.Text + "%' OR `Nazvanie_PO` = '') and (`Auditoria` LIKE '%" + textAudit.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Program_Obespech", queryFilter, binSourcePO, dataGridVPO, NavigatorPO);
        }

        //кнопка "Сохранить данные"
        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (SaveTable == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Program_Obespech", "SELECT * FROM `Program_Obespech`", binSourcePO, dataGridVPO, NavigatorPO);
                    SaveTable = true;
                }
            }
            else
            {
                Main main = new Main(conMySQL);

                if (main.checkEmptyDataGridCell(0, dataGridVPO) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPO.CurrentRow.Index;

                    string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Program_Obespech`");

                    int chislo = Convert.ToInt32(Count);
                    chislo--;

                    if (i <= chislo)
                    {
                        string queryString = "UPDATE `Program_Obespech` SET `Tip_PO` = '"
                            + dataGridVPO[1, i].Value + "', `Nazvanie_PO` = '"
                            + dataGridVPO[2, i].Value + "', `Auditoria` = '"
                            + dataGridVPO[3, i].Value + "' WHERE `ID` = " + dataGridVPO[0, i].Value;
                        conMySQL.QueryToBool(queryString);
                    }
                    else
                    {
                        string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Program_Obespech`");
                        int int_maxID = Convert.ToInt32(maxID);
                        int_maxID++;

                        string queryString = "INSERT INTO `Program_Obespech` VALUES (" + int_maxID + ",'" + dataGridVPO[1, i].Value + "','" +
                            dataGridVPO[2, i].Value + "','" + dataGridVPO[3, i].Value + "')";
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

        //Кнопка "Очистка"
        private void buttonClearPO_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        //Кнопка "Добавить"
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAudit, "Nomer");

            textAddIdPO.Clear();

            string queryMaxIdPO = "SELECT MAX(ID) FROM `Program_Obespech`";

            if (conMySQL.QueryToBool("SELECT * FROM `Program_Obespech`") == true)
            {
                string MaxNomerIdPrepod = conMySQL.AgregateQueryToDataGrid(queryMaxIdPO);
                int x = Int16.Parse(MaxNomerIdPrepod);
                x++;
                textAddIdPO.Text = Convert.ToString(x);
            }
            else
            {
                textAddIdPO.Text = "1";
            }

            Check_Button = 0;
            buttonEdit.Enabled = false;
            panelPO.Visible = true;
            ClearText();
            label7.Text = "Добавление нового ПО";
        }

        //Кнопка "Изменить"
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            ClearText();
            int x = dataGridVPO.CurrentRow.Index;
            //Забираем значение ячейки
            textAddIdPO.Text = Convert.ToString(dataGridVPO[0, x].Value);
            textAddTipPO.Text = Convert.ToString(dataGridVPO[1, x].Value);
            textAddNamePO.Text = Convert.ToString(dataGridVPO[2, x].Value);
            textAddAudit.Text = Convert.ToString(dataGridVPO[3, x].Value);

            Check_Button = 1;
            buttonAdd.Enabled = false;
            panelPO.Visible = true;
            label7.Text = "Изменение данных ПО";
        }

        //Кнопка "ОК"
        private void buttonAddRecordPO_Click(object sender, EventArgs e)
        {
            if (textAddTipPO.Text.Equals("") || textAddNamePO.Text.Equals("") ||
              textAddAudit.Text.Equals(""))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    string queryString = "INSERT INTO `Program_Obespech` VALUES (" + textAddIdPO.Text + ",'" +
                        textAddTipPO.Text + "','" + textAddNamePO.Text + "','" + textAddAudit.Text + "')";
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Program_Obespech", "SELECT * FROM Program_Obespech", binSourcePO, dataGridVPO, NavigatorPO);
                    ClearText();
                    int x = int.Parse(textAddIdPO.Text);
                    x++;
                    textAddIdPO.Text = Convert.ToString(x);
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPO.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_PO = Convert.ToString(dataGridVPO[0, i].Value);
                    string queryString = "UPDATE `Program_Obespech` SET `ID` = "
                        + textAddIdPO.Text + ", `Tip_PO` = '"
                        + textAddTipPO.Text + "', `Nazvanie_PO` = '"
                        + textAddNamePO.Text + "', `Auditoria` = '"
                        + textAddAudit.Text + "' WHERE `ID` = " + id_PO;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Program_Obespech", "SELECT * FROM Program_Obespech", binSourcePO, dataGridVPO, NavigatorPO);
                    ClearText();
                    panelPO.Visible = false;
                    buttonAdd.Enabled = true;
                }
            }
        }

        //Кнопка "Крестик"
        private void buttonClosePanel_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelPO.Visible = false;
            buttonEdit.Enabled = true;
            buttonAdd.Enabled = true;
            ClearText();
        }

        //Печать
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVPO);
        }

        //Очистка результатов поиска
        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.clear_datagrid(dataGridVPO);
        }

        //Поиск по таблице
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Main main = new Main(conMySQL);
            main.search_datagrid(dataGridVPO, textSearchPO);
        }

        //Кнопка показать всю таблицу
        private void buttonShowTable_Click(object sender, EventArgs e)
        {
            conMySQL.LoadTable("Program_Obespech", "SELECT * FROM Program_Obespech", binSourcePO, dataGridVPO, NavigatorPO);
        }
    }
}