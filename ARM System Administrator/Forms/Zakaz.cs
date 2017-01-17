using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARM_System_Administrator.Classes; //Ссылка на MySQL компоненты
using MySql.Data.MySqlClient;

namespace ARM_System_Administrator.Forms
{
    public partial class Zakaz : Form
    {
        public Classes.ConnectorSQL conMySQL;
        public Zakaz(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;
        }

        //Загрузка формы
        private void Zakaz_Load(object sender, EventArgs e)
        {
            conMySQL.QueryToComboBox("SELECT CONCAT_WS(' ', `Fam_sotr`, `Name_sotr`, `Otch_sotr`) FIO FROM `Sotrudnik`", textFIO, "FIO");

            // Программное добавление колонок            
            dataGridVZakaz.Columns.Add("Count", "Количество");
            dataGridVZakaz.AllowUserToAddRows = true;
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn(); //Создаем экземпляр класса выпадающего списка
           
            dataGridVZakaz.Columns.Insert(0, column);
            dataGridVZakaz.Columns[0].HeaderText = "Тип оборудования";

            column.Items.AddRange("Компьютер", "Ноутбук", "Принтер", "Монитор", "Клавиатура", "Мышь", "Прочее");
        }

        // ПУТЬ К ШАБЛОНУ ПО ДАННЫМ ФОРМЫ
        private string pathToTemplate { get { return Application.StartupPath + @"\Data\Shablon.doc"; } }

        //Открытие шаблона ворд и перенос значений туды, вкладка "Одиночный заказ заказ"
        private void buttonWordGO_Click(object sender, EventArgs e)
        {
            if (textFIO.Text.Equals("") || textDolgnost.Text.Equals("") || textDopInf.Text.Equals(""))
            {
                MessageBox.Show("Имеются не заполненные значения!", "Ошибка!");
            }
            else
            {
                WordDocument wordDoc = null;
                try
                {
                    wordDoc = new WordDocument(pathToTemplate);
                    //Вставляем ФИО
                    wordDoc.ReplaceAllStrings("@@FIO", textFIO.Text);

                    //Вставляем должность
                    wordDoc.SetSelectionToText("@@Dolgnost");
                    wordDoc.Selection.Text = Convert.ToString(textDolgnost.Text);

                    //Вставляем текущую системную дату
                    wordDoc.SetSelectionToText("@@NowDate");
                    wordDoc.Selection.Text = Convert.ToString(DateTime.Now);                    

                    //Вставляем количество оборудования
                    wordDoc.SetSelectionToText("@@DopInf");
                    wordDoc.Selection.Text = Convert.ToString(textDopInf.Text);

                    // такая конвертация типов работает не всегда, не на всех сочетаниях типов
                    int tableNum = 1;
                    wordDoc.SelectTable(tableNum);

                    int addRowsCount = (int)dataGridVZakaz.Rows.Count - 2;

                    for (int addRowsNum = 0; addRowsNum <= addRowsCount; addRowsNum++)
                    {
                        wordDoc.AddRowToTable();
                        wordDoc.SetSelectionToCell(addRowsNum + 2, 1);
                        wordDoc.Selection.Text = Convert.ToString(dataGridVZakaz[0, addRowsNum].Value);
                        wordDoc.SetSelectionToCell(addRowsNum + 2, 2);
                        wordDoc.Selection.Text = Convert.ToString(dataGridVZakaz[1, addRowsNum].Value);
                    }
                }
                catch (Exception error)
                {
                    if (wordDoc != null) { wordDoc.Close(); }
                    MessageBox.Show("Ошибка при замене текста на метке в документе  Word. Подробности " + error.Message);
                    return;
                }
                wordDoc.Visible = true;
                this.Close();
            }
        }

        //Загрузка должности
        private void textFIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string FIO = textFIO.Text;
                string Fam_Sotr;
                string Name_Otch_Sotr;
                string Name_Sotr;
                string Otch_Sotr;

                int Probel_Posle_Fam = FIO.IndexOf(" ", 0); //Определяю вхождение первого пробела (После фамилии)

                Probel_Posle_Fam++;
                Name_Otch_Sotr = FIO.Substring(Probel_Posle_Fam, FIO.Length - Probel_Posle_Fam); //Копирование (Имя, Отчество)

                Fam_Sotr = FIO.Remove(Probel_Posle_Fam); //Копируем фамилию сотрудника в переменную

                int Probel_Posle_Imeni = Name_Otch_Sotr.IndexOf(" ", 1); //Определяю вхождение первого пробела (После имени)

                Probel_Posle_Imeni++;
                Otch_Sotr = Name_Otch_Sotr.Substring(Probel_Posle_Imeni, Name_Otch_Sotr.Length - Probel_Posle_Imeni); //Копирование (Отчество)

                Name_Sotr = Name_Otch_Sotr.Remove(Probel_Posle_Imeni); //Копируем имя сотрудника в переменную

                string queryStringDolgnostSotr = "SELECT `Dolgnost` FROM Sotrudnik WHERE `Name_sotr` = '" + Name_Sotr +
                    "' AND `Fam_sotr` = '" + Fam_Sotr + "' AND `Otch_sotr` = '" + Otch_Sotr + "'";


                if (conMySQL.QueryToBool(queryStringDolgnostSotr) == true)
                {
                    textDolgnost.Text = conMySQL.AgregateQueryToDataGrid(queryStringDolgnostSotr);
                }
                else
                {
                    textDolgnost.Text = "";
                }                
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
    }
}
