using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO; //Ссылка на Word компоненты
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARM_System_Administrator.Forms;
using MySql.Data.MySqlClient; //Ссылка на MySQL компоненты
using Excel = Microsoft.Office.Interop.Excel; //Ссылка на Excel компоненты
using Word = Microsoft.Office.Interop.Word;

namespace ARM_System_Administrator
{
    public partial class Main : Form
    {
        public int Check_Button; //Если 0 - Кнопка "Добавить", если 1 - Кнопка "Изменить"
        public bool admin_status;

        public BindingSource binSourceAll;

        public BindingSource binSourceComp = new BindingSource();
        public BindingSource binSourceNout = new BindingSource();
        public BindingSource binSourcePrint = new BindingSource();
        public BindingSource binSourceMouse = new BindingSource();
        public BindingSource binSourceMonitor = new BindingSource();
        public BindingSource binSourceKlava = new BindingSource();
        public BindingSource binSourceAudit = new BindingSource();
        public BindingSource binSourceOborud = new BindingSource();
        public BindingSource binSourceTipOborud = new BindingSource();
        public BindingSource binSourceProchee = new BindingSource();


        public Classes.Exports ExportsTo = new Classes.Exports();
        public Classes.ConnectorSQL conMySQL;
        public Main(Classes.ConnectorSQL ClassConSQL)
        {
            InitializeComponent();
            this.conMySQL = ClassConSQL;

            //Настройка иконок на вкладки
            string Comp = @"Data\Img\TabPages\Sistemnik.png";
            string Nout = @"Data\Img\TabPages\Nout.png";
            string Printer = @"Data\Img\TabPages\Printer.png";
            string Mouse = @"Data\Img\TabPages\Mouse.png";
            string Monitor = @"Data\Img\TabPages\Monitor.png";
            string Klava = @"Data\Img\TabPages\Klava.png";
            string Prochee = @"Data\Img\TabPages\Prochee.png";
            string Audit = @"Data\Img\TabPages\Audit.png";
            //Создаем лист изображений
            ImageList imgList = new ImageList();
            //Загружаем изображения в лист
            imgList.Images.Add(new System.Drawing.Bitmap(Comp));
            imgList.Images.Add(new System.Drawing.Bitmap(Nout));
            imgList.Images.Add(new System.Drawing.Bitmap(Printer));
            imgList.Images.Add(new System.Drawing.Bitmap(Mouse));
            imgList.Images.Add(new System.Drawing.Bitmap(Monitor));
            imgList.Images.Add(new System.Drawing.Bitmap(Klava));
            imgList.Images.Add(new System.Drawing.Bitmap(Prochee));
            imgList.Images.Add(new System.Drawing.Bitmap(Audit));

            tabControlARM.ImageList = imgList;
            //Добавляем их на вкладку
            tabControlARM.TabPages[0].ImageIndex = 0;
            tabControlARM.TabPages[1].ImageIndex = 1;
            tabControlARM.TabPages[2].ImageIndex = 2;
            tabControlARM.TabPages[3].ImageIndex = 3;
            tabControlARM.TabPages[4].ImageIndex = 4;
            tabControlARM.TabPages[5].ImageIndex = 5;
            tabControlARM.TabPages[6].ImageIndex = 6;
            tabControlARM.TabPages[7].ImageIndex = 7;
            
        }

        //В душе не знаю как оно работает, но оно работает - Блокирование креста закрытия формы
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //--------------------Процедура программы -------------------//

        //Процедура "клик по таблице"
        public void Click_for_table(DataGridView dg, PictureBox pb, string Folder)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("Отсутсвуют строки в таблице!", "Ошибка!");
            }
            else
            {
                //Очистка поля для фото
                pb.Image = null;
                pb.BackgroundImage = null;
                //Установка растяжения по всей площади
                pb.BackgroundImageLayout = ImageLayout.Stretch;
                int i = 0;
                //Определяем индекс строки
                i = dg.SelectedCells[0].RowIndex;
                try
                {
                    //Если файл существует по пути, то загружаем фото, если нету, то картинку с ошибкой
                    if (File.Exists(@"Data\Img\" + Folder + "\\" +
                        Convert.ToString(dg[0, i].Value) + ".bmp"))
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(@"Data\Img\" +
                            Folder + "\\" + Convert.ToString(dg[0, i].Value) + ".bmp", System.IO.FileMode.Open);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                        fs.Close();
                        pb.Image = img;
                    }
                    else
                    {
                        pb.BackgroundImage = Image.FromFile(@"Data\Img\404.png");
                    }
                }
                catch { }
            }
        }

        //Процедура добавления фото
        public void add_picture(string Name_file, string Folder)
        {
            openFileDialog1.Filter = ("Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*");
            try
            {
                //Добавление фотографии
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(@"Data\Img\" + Folder + '\\' + Name_file + ".bmp"))
                    {
                        File.Delete(@"Data\Img\" + Folder + '\\' + Name_file + ".bmp");
                        File.Copy(openFileDialog1.FileName, @"Data\Img\" + Folder + '\\' + Name_file + ".bmp");
                        MessageBox.Show("Повторное добавление", "Уже существует");
                    }
                    else
                    {
                        File.Copy(openFileDialog1.FileName, @"Data\Img\" + Folder + '\\' + Name_file + ".bmp");
                        MessageBox.Show("Изображение добавлено!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Отмена добавления", "Отмена действия!");
                }
            }
            catch { }
        }

        //Процедура удаления фото
        public void Delete_Photo(DataGridView dg, string Folder)
        {
            //Определяем индекс выбранной строки
            int i = dg.CurrentRow.Index;
            //Забор значения из 0 столбца i-тый строки
            string name_file = Convert.ToString(dg[0, i].Value);
            //Удаление старых фотографий
            if (File.Exists(@"Data\Img\" + Folder + "\\" + name_file + ".bmp"))
            {
                File.Delete(@"Data\Img\" + Folder + "\\" + name_file + ".bmp");
                MessageBox.Show("Изображение удалено!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Процедура изменения картинки
        public void Change_picture(DataGridView dGV, string Folder)
        {
            //Проверка на пустоту базы
            if (dGV.RowCount == 0)
            {
                MessageBox.Show("Отсутсвуют записи в таблице", "Ошибка добавления фото");
            }
            else
            {
                int i = dGV.SelectedCells[0].RowIndex;
                string s = Convert.ToString(dGV[0, i].Value);
                //Вызов процедуры добавления фото
                add_picture(s, Folder);
            }
        }

        //Процедура сортировки плюс
        public void Sort_Plus(DataGridView dGV, int x)
        {
            dGV.Sort(dGV.Columns[x], ListSortDirection.Ascending);
        }

        //Процедура сортировки минус
        public void Sort_Minus(DataGridView dGV, int x)
        {
            dGV.Sort(dGV.Columns[x], ListSortDirection.Descending);
        }

        //Процедура "Поиска по datagrid"
        public void search_datagrid(DataGridView dGV, ToolStripTextBox text)
        {
            if (text.Text.Equals(""))
            {
                MessageBox.Show("Введите искомое значение!", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int i, j = 0;
                for (i = 0; i < dGV.ColumnCount; i++)
                {
                    for (j = 0; j < dGV.RowCount; j++)
                    {
                        dGV[i, j].Style.BackColor = Color.White;
                        dGV[i, j].Style.ForeColor = Color.Black;
                    }
                }
                for (i = 0; i < dGV.ColumnCount; i++)
                {
                    for (j = 0; j < dGV.RowCount; j++)
                    {
                        if ((dGV[i, j].FormattedValue.ToString().Contains(text.Text.Trim())))
                        {
                            dGV[i, j].Style.BackColor = Color.RoyalBlue;
                            dGV[i, j].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        //Процедура очистки результатов поиска
        public void clear_datagrid(DataGridView dGV)
        {
            int i;
            int j;
            for (i = 0; i <= dGV.ColumnCount - 1; i++)
            {
                for (j = 0; j <= dGV.RowCount - 1; j++)
                {
                    dGV[i, j].Style.BackColor = Color.White;
                    dGV[i, j].Style.ForeColor = Color.Black;
                }
            }
        }

        //Процедура настройки дата грида
        public void dataGridSettings(DataGridView dataGV)
        {
            if (admin_status == true)
            {
                dataGV.ReadOnly = false;
                dataGV.AllowUserToAddRows = true;
            }
            else
            {
                dataGV.ReadOnly = true;
                dataGV.AllowUserToAddRows = false;
            }
            
            dataGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGV.MultiSelect = false;
        }

        //Процедура органичения на количество символов дата грида = 100 символов
        public void dataGridMaxLengthColumn100(int countColumn, DataGridView dataGrid)
        {
            for (int i = 0; i < countColumn; i++)
            {
                ((DataGridViewTextBoxColumn)dataGrid.Columns[i]).MaxInputLength = 100;
            }
        }

        //Процедура органичения на количество символов дата грида = 50 символов
        public void dataGridMaxLengthColumn50(int countColumn, DataGridView dataGrid)
        {
            for (int i = 0; i < countColumn; i++)
            {
                ((DataGridViewTextBoxColumn)dataGrid.Columns[i]).MaxInputLength = 100;
            }
        }

        public int indexRow, indexColumn = 0;

        //Проверка на пустоту ячейки таблицы
        public bool checkEmptyDataGridCell(int freeColumn, DataGridView dataGrid)
        {
            //int indexRow, indexColumn = 0;
            for (int i = 0; i < dataGrid.RowCount - 1; i++)
            {
                for (int j = 1; j < dataGrid.ColumnCount; j++)
                {
                    if ((dataGrid.Rows[i].Cells[j].Value == null) || (dataGrid.Rows[i].Cells[j].Value.ToString() == ""))
                    {
                        if (j == freeColumn)
                        {
                            //j = freeColumn;
                            continue;
                        }
                        else
                        {
                            indexRow = i;
                            indexColumn = j;
                            return false;
                        }
                    }
                }
                
            }
            return true;
        }

        //--------------------Конец Процедуры -------------------//

        //--------------------Другие процедуры -----------------//

        //Сброс пароля для администратора
        private void администраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить настройки автоматического входа для администратора?", "Сообщение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(@"Data\Txt\PasswordAdmin.txt");
            }
        }

        //Сброс пароля для преподавателя
        private void преподавательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить настройки автоматического входа для преподавателя?", "Сообщение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(@"Data\Txt\PasswordUser.txt");
            }
        }

        //Кнопка "Учет оборудования"
        private void учетОборудованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UchetOborudovania uchet = new UchetOborudovania(conMySQL);
            uchet.Show();
        }

        //Кнопка "О программе", вызов формы с информацией о программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About Ab = new About();
            Ab.Show();
        }        

        //Кнопка запуска стороннего приложения
        private void запуситьAIDA64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string URL = @"Data\aida64extreme520\aida64.exe";
            try
            {
                Process.Start(URL);
            }
            catch
            {
                MessageBox.Show("Действие отменено!","Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Кнопка "Учет заявок", для открытия форм в зависимости от пользователя
        private void учетЗаявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (admin_status == true) //Если админ, то открываем учет заявок для администратора
            {
                int CountZayavok;

                string count = conMySQL.AgregateQueryToDataGrid("SELECT count_zayavok FROM `Count_Zayavok` WHERE `ID` = 1");
                CountZayavok = Convert.ToInt32(count);

                string Query = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Zayavka`");
                int countZayavok = Convert.ToInt32(Query);
                if (countZayavok > CountZayavok)
                {
                    int Result = countZayavok - CountZayavok;
                    if (MessageBox.Show("Имеются новые заявки, количество: " + Convert.ToString(Result) + " шт.\nХотите принять заявки?", "Сообщение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string queryString = "UPDATE `Count_Zayavok` SET `count_zayavok` = " + countZayavok + " WHERE `ID` = 1";
                        conMySQL.QueryToBool(queryString);
                        this.Close();
                        UchetZayavok uchet = new UchetZayavok(conMySQL);
                        uchet.Show();
                    }
                }
                else
                {
                    //MessageBox.Show("Новых заявок нету", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UchetZayavok Uchet = new UchetZayavok(conMySQL);
                    this.Close();
                    Uchet.Show();
                }                
            }
        }

        //Кнопка "Справка(F1)", вызывает справку
        private void справкаF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }

        //Кнопка "Выход", выход из программы
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conMySQL.CloseConnection();
            Application.Exit();
        }

        public bool Click_Uchet;

        //Загрузка формы
        private void Main_Load(object sender, EventArgs e)
        {
            helpProvider1.HelpNamespace = @"Data\Help\Helpout.chm";
            helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider1.SetShowHelp(this, true);

            настройкиToolStripMenuItem.Enabled = true;
            оформитьЗаказToolStripMenuItem.Enabled = true;
            сделатьЗапросToolStripMenuItem.Enabled = true;
            работаСТаблицамиToolStripMenuItem.Enabled = true;
            правкаToolStripMenuItem.Enabled = true;
            запуситьAIDA64ToolStripMenuItem.Enabled = true;

            this.Text = "АРМ системный администратор";
            buttonDeleteRecordComp.Enabled = true; buttonSaveRecordComp.Enabled = true;
            buttonDeleteRecordNout.Enabled = true; buttonSaveRecordNout.Enabled = true;
            buttonDeleteRecordPrint.Enabled = true; buttonSaveRecordPrint.Enabled = true;
            buttonDeleteRecordMouse.Enabled = true; buttonSaveRecordMouse.Enabled = true;
            buttonDeleteRecordMon.Enabled = true; buttonSaveRecordMon.Enabled = true;
            buttonDeleteRecordKlava.Enabled = true; buttonSaveRecordKlava.Enabled = true;
            buttonDeleteRecordAudit.Enabled = true;
            buttonDeleteRecordProchee.Enabled = true; buttonSaveRecordProchee.Enabled = true;

            //Выбор активной вкладки, вкладки "Компьютеры"
            this.tabControlARM.SelectedTab = tabPageComp;
            conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);

            try
            {
                dataGridVComp.Columns[0].HeaderText = "Системный номер";
                dataGridVComp.Columns[0].Visible = false;
                dataGridVComp.Columns[1].HeaderText = "Инвентарный номер";
                dataGridVComp.Columns[2].HeaderText = "Операционная система";
                dataGridVComp.Columns[3].HeaderText = "Привод";
                dataGridVComp.Columns[4].HeaderText = "Материнская плата";
                dataGridVComp.Columns[5].HeaderText = "ПЗУ";
                dataGridVComp.Columns[6].HeaderText = "Кулер";
                dataGridVComp.Columns[7].HeaderText = "Видеокарта";
                dataGridVComp.Columns[8].HeaderText = "Жесткий диск";
                dataGridVComp.Columns[9].HeaderText = "Процессор";
                dataGridVComp.Columns[10].HeaderText = "Блок питания";
                dataGridVComp.Columns[11].HeaderText = "Статус";
                dataGridVComp.Columns[12].HeaderText = "Цена";
                dataGridVComp.Columns[13].HeaderText = "Дополнительная информация";
                dataGridVComp.Columns[14].HeaderText = "Дата ввода в эксплуатацию";
                dataGridVComp.Columns[15].HeaderText = "Дата списания оборудования";
                dataGridVComp.Columns[16].HeaderText = "Аудитория";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            dataGridSettings(dataGridVComp);

            if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
            {
                conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditComp, "Nomer");
                conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textBoxAuditComp, "Nomer");
            }

            textBoxStatusComp.SelectedIndex = 0;
            textAddStatusComp.SelectedIndex = 0;

            dataGridMaxLengthColumn100(dataGridVComp.ColumnCount, dataGridVComp);

            comboBoxDataGridStatusComp.Hide();
            mcComp.Hide();
        }

        //Происходит при смене вкладки
        private void tabControlARM_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControlARM.SelectedTab == tabPageComp) //Вкладка "Компьютеры"
            {
                conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);

                try
                {
                    dataGridSettings(dataGridVComp);
                    dataGridVComp.Columns[0].Visible = false;
                    dataGridVComp.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVComp.Columns[2].HeaderText = "Операционная система";
                    dataGridVComp.Columns[3].HeaderText = "Привод";
                    dataGridVComp.Columns[4].HeaderText = "Материнская плата";
                    dataGridVComp.Columns[5].HeaderText = "ПЗУ";
                    dataGridVComp.Columns[6].HeaderText = "Кулер";
                    dataGridVComp.Columns[7].HeaderText = "Видеокарта";
                    dataGridVComp.Columns[8].HeaderText = "Жесткий диск";
                    dataGridVComp.Columns[9].HeaderText = "Процессор";
                    dataGridVComp.Columns[10].HeaderText = "Блок питания";
                    dataGridVComp.Columns[11].HeaderText = "Статус";
                    dataGridVComp.Columns[12].HeaderText = "Цена";
                    dataGridVComp.Columns[13].HeaderText = "Дополнительная информация";
                    dataGridVComp.Columns[14].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVComp.Columns[15].HeaderText = "Дата списания оборудования";
                    dataGridVComp.Columns[16].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditComp, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textBoxAuditComp, "Nomer");
                }

                textBoxStatusComp.SelectedIndex = 0;
                textAddStatusComp.SelectedIndex = 0;

                dataGridMaxLengthColumn100(dataGridVComp.ColumnCount, dataGridVComp);

                comboBoxDataGridStatusComp.Hide();
                mcComp.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageNout) //Вкладка "Ноутбуки"
            {
                conMySQL.LoadTable("Param_nout", "SELECT * FROM `Param_nout`", binSourceNout, dataGridVNout, NavigatorNout);

                try
                {
                    dataGridVNout.Columns[0].Visible = false;
                    dataGridVNout.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVNout.Columns[2].HeaderText = "Операционная система";
                    dataGridVNout.Columns[3].HeaderText = "Название";
                    dataGridVNout.Columns[4].HeaderText = "Процессор";
                    dataGridVNout.Columns[5].HeaderText = "Видео";
                    dataGridVNout.Columns[6].HeaderText = "Диагональ";
                    dataGridVNout.Columns[7].HeaderText = "Память";
                    dataGridVNout.Columns[8].HeaderText = "Жесткий диск";
                    dataGridVNout.Columns[9].HeaderText = "Статус";
                    dataGridVNout.Columns[10].HeaderText = "Цена";
                    dataGridVNout.Columns[11].HeaderText = "Дополнительная информация";
                    dataGridVNout.Columns[12].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVNout.Columns[13].HeaderText = "Дата списания оборудования";
                    dataGridVNout.Columns[14].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVNout);
                
                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditNout, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditNout, "Nomer");
                }

                textStatusNout.SelectedIndex = 0;
                textAddStatusNout.SelectedIndex = 0;

                dataGridMaxLengthColumn100(dataGridVNout.ColumnCount, dataGridVNout);
                
                comboBoxDataGridStatusNout.Hide();
                mcNout.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPagePrint) //Вкладка "Принтеры"
            {
                conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVPrint, NavigatorPrint);

                try
                {
                    dataGridVPrint.Columns[0].Visible = false;
                    dataGridVPrint.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVPrint.Columns[2].HeaderText = "Название";
                    dataGridVPrint.Columns[3].HeaderText = "Максимальный размер";
                    dataGridVPrint.Columns[4].HeaderText = "Размеры";
                    dataGridVPrint.Columns[5].HeaderText = "Память";
                    dataGridVPrint.Columns[6].HeaderText = "Скорость печати";
                    dataGridVPrint.Columns[7].HeaderText = "Максимальный формат";
                    dataGridVPrint.Columns[8].HeaderText = "Количество страниц";
                    dataGridVPrint.Columns[9].HeaderText = "Статус";
                    dataGridVPrint.Columns[10].HeaderText = "Цена";
                    dataGridVPrint.Columns[11].HeaderText = "Дополнительная информация";
                    dataGridVPrint.Columns[12].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVPrint.Columns[13].HeaderText = "Дата списания оборудования";
                    dataGridVPrint.Columns[14].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVPrint);
                                
                dataGridMaxLengthColumn50(dataGridVPrint.ColumnCount, dataGridVPrint);

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditPrint, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditPrint, "Nomer");                   
                }
                 
                textStatusPrint.SelectedIndex = 0;
                textAddStatusPrint.SelectedIndex = 0;

                comboBoxDataGridStatusPrint.Hide();
                mcPrint.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageMouse) //Вкладка "Мышки"
            {
                conMySQL.LoadTable("Param_mishek", "SELECT * FROM `Param_mishek`", binSourceMouse, dataGridVMishek, NavigatorMishek);

                try
                {
                    dataGridVMishek.Columns[0].Visible = false;
                    dataGridVMishek.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVMishek.Columns[2].HeaderText = "Тип подключения";
                    dataGridVMishek.Columns[3].HeaderText = "Статус";
                    dataGridVMishek.Columns[4].HeaderText = "Цена";
                    dataGridVMishek.Columns[5].HeaderText = "Дополнительная информация";
                    dataGridVMishek.Columns[6].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVMishek.Columns[7].HeaderText = "Дата списания оборудования";
                    dataGridVMishek.Columns[8].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVMishek);
               
                dataGridMaxLengthColumn50(dataGridVMishek.ColumnCount, dataGridVMishek);

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditMouse, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditMouse, "Nomer");                  
                }

                textStatusMouse.SelectedIndex = 0;
                textAddStatusMouse.SelectedIndex = 0;

                comboBoxDataGridStatusPrint.Hide();
                mcPrint.Hide();
                comboBoxDataGridTypePodMouse.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageMonitor) //Вкладка "Мониторы"
            {
                conMySQL.LoadTable("Param_monitor", "SELECT * FROM `Param_monitor`", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);

                try
                {
                    dataGridVMonitor.Columns[0].Visible = false;
                    dataGridVMonitor.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVMonitor.Columns[2].HeaderText = "Разрешение";
                    dataGridVMonitor.Columns[3].HeaderText = "Диагональ";
                    dataGridVMonitor.Columns[4].HeaderText = "Статус";
                    dataGridVMonitor.Columns[5].HeaderText = "Цена";
                    dataGridVMonitor.Columns[6].HeaderText = "Дополнительная информация";
                    dataGridVMonitor.Columns[7].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVMonitor.Columns[8].HeaderText = "Дата списания оборудования";
                    dataGridVMonitor.Columns[9].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVMonitor);

                dataGridMaxLengthColumn50(dataGridVMonitor.ColumnCount, dataGridVMonitor);

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditMon, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditMon, "Nomer");                   
                }
                 
                textStatusMon.SelectedIndex = 0;
                textAddStatusMon.SelectedIndex = 0;

                comboBoxDataGridStatusMon.Hide();
                mcMon.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageKeyboard) //Вкладка "Клавиатуры"
            {
                conMySQL.LoadTable("Param_klava", "SELECT * FROM `Param_klava`", binSourceKlava, dataGridVKlava, NavigatorKlava);

                try
                {
                    dataGridVKlava.Columns[0].Visible = false;
                    dataGridVKlava.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVKlava.Columns[2].HeaderText = "Тип подключения";
                    dataGridVKlava.Columns[3].HeaderText = "Количество клавиш";
                    dataGridVKlava.Columns[4].HeaderText = "Статус";
                    dataGridVKlava.Columns[5].HeaderText = "Цена";
                    dataGridVKlava.Columns[6].HeaderText = "Дополнительная информация";
                    dataGridVKlava.Columns[7].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVKlava.Columns[8].HeaderText = "Дата списания оборудования";
                    dataGridVKlava.Columns[9].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVKlava);

                dataGridMaxLengthColumn50(dataGridVKlava.ColumnCount, dataGridVKlava);

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditKlava, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditKlava, "Nomer");                  
                }
                 
                textStatusKlava.SelectedIndex = 0;
                textAddStatusKlava.SelectedIndex = 0;

                comboBoxDataGridStatusKlava.Hide();
                mcKlava.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageProchee) //Вкладка "Прочее"
            {
                conMySQL.LoadTable("Prochee", "SELECT * FROM `Prochee`", binSourceProchee, dataGridVProchee, NavigatorProchee);

                try
                {
                    dataGridVProchee.Columns[0].Visible = false;
                    dataGridVProchee.Columns[1].HeaderText = "Инвентарный номер";
                    dataGridVProchee.Columns[2].HeaderText = "Название";
                    dataGridVProchee.Columns[3].HeaderText = "Статус";
                    dataGridVProchee.Columns[4].HeaderText = "Цена";
                    dataGridVProchee.Columns[5].HeaderText = "Дополнительная информация";
                    dataGridVProchee.Columns[6].HeaderText = "Дата ввода в эксплуатацию";
                    dataGridVProchee.Columns[7].HeaderText = "Дата списания оборудования";
                    dataGridVProchee.Columns[8].HeaderText = "Аудитория";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridSettings(dataGridVProchee);

                dataGridMaxLengthColumn100(dataGridVProchee.ColumnCount, dataGridVProchee);

                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == true)
                {
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAddAuditProchee, "Nomer");
                    conMySQL.QueryToComboBox("SELECT `Nomer` FROM `Auditoria`", textAuditProchee, "Nomer");
                }

                textStatusProchee.SelectedIndex = 0;
                textAddStatusProchee.SelectedIndex = 0;

                comboBoxDataGridStatus.Hide();
                mcProchee.Hide();
            }

            else if (tabControlARM.SelectedTab == tabPageAuditoria) //Вкладка "Аудитория"
            {
                conMySQL.LoadTable("Auditoria", "SELECT * FROM `Auditoria`", binSourceAudit, dataGridVAudit, NavigatorAudit);

                try
                {
                    dataGridVAudit.Columns[0].HeaderText = "Номер аудитории";
                    dataGridVAudit.Columns[1].HeaderText = "Ответственное лицо";
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                dataGridVAudit.ReadOnly = true;
                dataGridVAudit.AllowUserToAddRows = false;


                dataGridVAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridVAudit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridVAudit.MultiSelect = false;

                dataGridMaxLengthColumn50(dataGridVAudit.ColumnCount, dataGridVAudit);
            }
        }

        //Кнопка "Добавить запись", Открытие своей панели добавление в зависимости от выбранной в данным момент вкладки
        private void добавитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlARM.SelectedTab == tabPageComp) //Вкладка "Компьютеры"
            {
                Check_Button = 0;
                panelCompAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxComp();
                label25.Text = "Добавление нового компьютера";

                comboBoxDataGridStatusComp.Hide();
                mcComp.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageNout) //Вкладка "Ноутбуки"
            {
                Check_Button = 0;
                panelNoutAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxNout();
                label52.Text = "Добавление нового ноутбука";

                comboBoxDataGridStatusNout.Hide();
                mcNout.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPagePrint) //Вкладка "Принтеры"
            {
                Check_Button = 0;
                panelPrintAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxPrint();
                label86.Text = "Добавление нового принтера";

                comboBoxDataGridStatusPrint.Hide();
                mcPrint.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageMouse) //Вкладка "Мышки"
            {
                Check_Button = 0;
                panelMouseAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxMouse();
                label119.Text = "Добавление новой мышки";

                comboBoxDataGridStatusMouse.Hide();
                mcMouse.Hide();
                comboBoxDataGridTypePodMouse.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageMonitor) //Вкладка "Мониторы"
            {
                Check_Button = 0;
                panelMonAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxMon();
                label139.Text = "Добавление нового монитора";

                comboBoxDataGridStatusMon.Hide();
                mcMon.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageKeyboard) //Вкладка "Клавиатуры"
            {
                Check_Button = 0;
                panelKlavaAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxKlava();
                label163.Text = "Добавление новой клавиатуры";

                comboBoxDataGridStatusKlava.Hide();
                mcKlava.Hide();
            }
            else if (tabControlARM.SelectedTab == tabPageProchee) //Вкладка "Прочее"
            {
                Check_Button = 0;
                panelProcheeAdd.Visible = true;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                ClearTextBoxProchee();
                label33.Text = "Добавление прочего оборудования";

                comboBoxDataGridStatus.Hide();
                mcProchee.Hide();
            }

            else if (tabControlARM.SelectedTab == tabPageAuditoria) //Вкладка "Аудитория"
            {
                Check_Button = 0;
                редактироватьЗаписьToolStripMenuItem.Enabled = false;
                panelAuditAdd.Visible = true;
                ClearTextBoxAudit();
                label44.Text = "Добавление новой аудитории";
            }
        }

        //Кнопка "Редактировать запись", Открытие своей панели редактирования в зависимости от выбранной в данным момент вкладки
        private void редактироватьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlARM.SelectedTab == tabPageComp) //Вкладка "Компьютеры"
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == true)
                {
                    ClearTextBoxComp();
                    int x = dataGridVComp.CurrentRow.Index;
                    //Забираем значение ячейки
                    textAddInvNomerComp.Text = Convert.ToString(dataGridVComp[1, x].Value);
                    textAddOSComp.Text = Convert.ToString(dataGridVComp[2, x].Value);
                    textAddPrivodComp.Text = Convert.ToString(dataGridVComp[3, x].Value);
                    textAddMatPlataComp.Text = Convert.ToString(dataGridVComp[4, x].Value);
                    textAddOZUComp.Text = Convert.ToString(dataGridVComp[5, x].Value);
                    textAddKuleraComp.Text = Convert.ToString(dataGridVComp[6, x].Value);
                    textAddVideoComp.Text = Convert.ToString(dataGridVComp[7, x].Value);
                    textAddHDDComp.Text = Convert.ToString(dataGridVComp[8, x].Value);
                    textAddProcComp.Text = Convert.ToString(dataGridVComp[9, x].Value);
                    textAddPitanieComp.Text = Convert.ToString(dataGridVComp[10, x].Value);
                    textAddStatusComp.Text = Convert.ToString(dataGridVComp[11, x].Value);
                    textAddTsenaComp.Text = Convert.ToString(dataGridVComp[12, x].Value);
                    textAddDopInfComp.Text = Convert.ToString(dataGridVComp[13, x].Value);
                    textAddDataVvodaComp.Text = Convert.ToString(dataGridVComp[14, x].Value);
                    textAddDataSpisaniaComp.Text = Convert.ToString(dataGridVComp[15, x].Value);
                    textAddAuditComp.Text = Convert.ToString(dataGridVComp[16, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelCompAdd.Visible = true;
                    label25.Text = "Изменение данных компьютера";

                    comboBoxDataGridStatusComp.Hide();
                    mcComp.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPageNout) //Вкладка "Ноутбуки"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_nout") == true)
                {
                    ClearTextBoxNout();
                    int x = dataGridVNout.CurrentRow.Index;
                    //Забираем значение ячей
                    textAddInvNomerNout.Text = Convert.ToString(dataGridVNout[1, x].Value);
                    textAddOSNout.Text = Convert.ToString(dataGridVNout[2, x].Value);
                    textAddNazvNout.Text = Convert.ToString(dataGridVNout[3, x].Value);
                    textAddProcNout.Text = Convert.ToString(dataGridVNout[4, x].Value);
                    textAddVideoNout.Text = Convert.ToString(dataGridVNout[5, x].Value);
                    textAddDiagNout.Text = Convert.ToString(dataGridVNout[6, x].Value);
                    textAddPamyatNout.Text = Convert.ToString(dataGridVNout[7, x].Value);
                    textAddHDDNout.Text = Convert.ToString(dataGridVNout[8, x].Value);
                    textAddStatusNout.Text = Convert.ToString(dataGridVNout[9, x].Value);
                    textAddTsenaNout.Text = Convert.ToString(dataGridVNout[10, x].Value);
                    textAddDopInfNout.Text = Convert.ToString(dataGridVNout[11, x].Value);
                    textAddDataVvodaNout.Text = Convert.ToString(dataGridVNout[12, x].Value);
                    textAddDataSpisaniaNout.Text = Convert.ToString(dataGridVNout[13, x].Value);
                    textAddAuditNout.Text = Convert.ToString(dataGridVNout[14, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelNoutAdd.Visible = true;
                    label52.Text = "Изменение данных ноутбука";

                    comboBoxDataGridStatusNout.Hide();
                    mcNout.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPagePrint) //Вкладка "Принтеры"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_print") == true)
                {
                    ClearTextBoxPrint();
                    int x = dataGridVPrint.CurrentRow.Index;
                    //Забираем значение ячей
                    textAddInvNomerPrint.Text = Convert.ToString(dataGridVPrint[1, x].Value);
                    textAddNazvPrint.Text = Convert.ToString(dataGridVPrint[2, x].Value);
                    textAddMaxRazPrint.Text = Convert.ToString(dataGridVPrint[3, x].Value);
                    textAddRazmeriPrint.Text = Convert.ToString(dataGridVPrint[4, x].Value);
                    textAddPamyatPrint.Text = Convert.ToString(dataGridVPrint[5, x].Value);
                    textAddSkorPechPrint.Text = Convert.ToString(dataGridVPrint[6, x].Value);
                    textAddMaxFormatPrint.Text = Convert.ToString(dataGridVPrint[7, x].Value);
                    textAddCountStrPrint.Text = Convert.ToString(dataGridVPrint[8, x].Value);
                    textAddStatusPrint.Text = Convert.ToString(dataGridVPrint[9, x].Value);
                    textAddTsenaPrint.Text = Convert.ToString(dataGridVPrint[10, x].Value);
                    textAddDopInfPrint.Text = Convert.ToString(dataGridVPrint[11, x].Value);
                    textAddDataVvodaPrint.Text = Convert.ToString(dataGridVPrint[12, x].Value);
                    textAddDataSpisaniaPrint.Text = Convert.ToString(dataGridVPrint[13, x].Value);
                    textAddAuditPrint.Text = Convert.ToString(dataGridVPrint[14, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelPrintAdd.Visible = true;
                    label86.Text = "Изменение данных принтера";

                    comboBoxDataGridStatusPrint.Hide();
                    mcPrint.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPageMouse) //Вкладка "Мышки"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_mishek") == true)
                {
                    ClearTextBoxMouse();
                    int x = dataGridVMishek.CurrentRow.Index;
                    //Забираем значение ячейки
                    textAddInvNomerMouse.Text = Convert.ToString(dataGridVMishek[1, x].Value);
                    textAddTipPodMouse.Text = Convert.ToString(dataGridVMishek[2, x].Value);
                    textAddStatusMouse.Text = Convert.ToString(dataGridVMishek[3, x].Value);
                    textAddTsenaMouse.Text = Convert.ToString(dataGridVMishek[4, x].Value);
                    textAddDopInfMouse.Text = Convert.ToString(dataGridVMishek[5, x].Value);
                    textAddDataVvodaMouse.Text = Convert.ToString(dataGridVMishek[6, x].Value);
                    textAddDataSpisaniaMouse.Text = Convert.ToString(dataGridVMishek[7, x].Value);
                    textAddAuditMouse.Text = Convert.ToString(dataGridVMishek[8, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelMouseAdd.Visible = true;
                    label119.Text = "Изменение данных мышки";

                    comboBoxDataGridStatusMouse.Hide();
                    mcMouse.Hide();
                    comboBoxDataGridTypePodMouse.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPageMonitor) //Вкладка "Мониторы"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_monitor") == true)
                {
                    ClearTextBoxMon();
                    int x = dataGridVMonitor.CurrentRow.Index;
                    //Забираем значение ячейки
                    textAddInvNomerMon.Text = Convert.ToString(dataGridVMonitor[1, x].Value);
                    textAddRazreshMon.Text = Convert.ToString(dataGridVMonitor[2, x].Value);
                    textAddDiagMon.Text = Convert.ToString(dataGridVMonitor[3, x].Value);
                    textAddStatusMon.Text = Convert.ToString(dataGridVMonitor[4, x].Value);
                    textAddTsenaMon.Text = Convert.ToString(dataGridVMonitor[5, x].Value);
                    textAddDopInfMon.Text = Convert.ToString(dataGridVMonitor[6, x].Value);
                    textAddDataVvodaMonitor.Text = Convert.ToString(dataGridVMonitor[7, x].Value);
                    textAddDataSpisaniaMonitor.Text = Convert.ToString(dataGridVMonitor[8, x].Value);
                    textAddAuditMon.Text = Convert.ToString(dataGridVMonitor[9, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelMonAdd.Visible = true;
                    label139.Text = "Изменение данных монитора";

                    comboBoxDataGridStatusMon.Hide();
                    mcMon.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPageKeyboard) //Вкладка "Клавиатуры"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_klava") == true)
                {
                    ClearTextBoxKlava();
                    int x = dataGridVKlava.CurrentRow.Index;
                    //Забираем значение ячейки
                    textAddInvNomerKlava.Text = Convert.ToString(dataGridVKlava[1, x].Value);
                    textAddTipPodKlava.Text = Convert.ToString(dataGridVKlava[2, x].Value);
                    textAddCountKlavishKlava.Text = Convert.ToString(dataGridVKlava[3, x].Value);
                    textAddStatusKlava.Text = Convert.ToString(dataGridVKlava[4, x].Value);
                    textAddTsenaKlava.Text = Convert.ToString(dataGridVKlava[5, x].Value);
                    textAddDopInfKlava.Text = Convert.ToString(dataGridVKlava[6, x].Value);
                    textAddDataVvodaKlava.Text = Convert.ToString(dataGridVKlava[7, x].Value);
                    textAddDataSpisaniaKlava.Text = Convert.ToString(dataGridVKlava[8, x].Value);
                    textAddAuditKlava.Text = Convert.ToString(dataGridVKlava[9, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelKlavaAdd.Visible = true;
                    label163.Text = "Изменение данных клавиатуры";

                    comboBoxDataGridStatusKlava.Hide();
                    mcKlava.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
            else if (tabControlARM.SelectedTab == tabPageProchee) //Вкладка "Прочее"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Prochee") == true)
                {
                    ClearTextBoxProchee();
                    int x = dataGridVProchee.CurrentRow.Index;
                    //Забираем значение ячейки
                    textAddInvNomerProchee.Text = Convert.ToString(dataGridVProchee[1, x].Value);
                    textAddNazvanieProchee.Text = Convert.ToString(dataGridVProchee[2, x].Value);
                    textAddStatusProchee.Text = Convert.ToString(dataGridVProchee[3, x].Value);
                    textAddTsenaProchee.Text = Convert.ToString(dataGridVProchee[4, x].Value);
                    textAddDopInfProchee.Text = Convert.ToString(dataGridVProchee[5, x].Value);
                    textAddDataVvodaProchee.Text = Convert.ToString(dataGridVProchee[6, x].Value);
                    textAddDataSpisaniaProchee.Text = Convert.ToString(dataGridVProchee[7, x].Value);
                    textAddAuditProchee.Text = Convert.ToString(dataGridVProchee[8, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelProcheeAdd.Visible = true;
                    label33.Text = "Изменение прочего оборудования";

                    comboBoxDataGridStatus.Hide();
                    mcProchee.Hide();
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }

            else if (tabControlARM.SelectedTab == tabPageAuditoria) //Вкладка "Аудитория"
            {
                if (conMySQL.QueryToBool("SELECT * FROM Auditoria") == true)
                {
                    ClearTextBoxAudit();
                    int x = dataGridVAudit.CurrentRow.Index;
                    OldNameAudit = Convert.ToString(dataGridVAudit[0, x].Value);
                    //Забираем значение ячейки
                    textAddNomerAudit.Text = Convert.ToString(dataGridVAudit[0, x].Value);
                    textAddOtvet.Text = Convert.ToString(dataGridVAudit[1, x].Value);

                    Check_Button = 1;
                    добавитьЗаписьToolStripMenuItem.Enabled = false;
                    panelAuditAdd.Visible = true;
                    label44.Text = "Изменение данных аудитории";
                }
                else
                {
                    MessageBox.Show("Нету данных для изменения!", "Ошибка!");
                }
            }
        }

        //Кнопка "Редактирование типов ПО", для открытия формы с ПО
        private void редактированиеТипоПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PO po = new PO(conMySQL);
            this.Close();
            po.Show();
        }

        //Кнопка "Редактировать список сотрудников", для открытия формы с сотрудниками
        private void редактироватьСписокСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sotrudnik sotr = new Sotrudnik(conMySQL);
            this.Close();
            sotr.Show();
        }

        //Кнопка "Оформить заказ", для открытия формы оформления заказа
        private void оформитьЗаказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zakaz zakaz = new Zakaz(conMySQL);
            zakaz.Show();
        }

        //Кнопка сделать запрос, для открытия формы запроса
        private void сделатьЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query query = new Query(conMySQL);
            query.Show();
        }

        //Кнопка "Сделать заявку", для открытия формы заявки
        private void сделатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zayavka zayavka = new Zayavka(conMySQL);
            zayavka.admin_status = admin_status;
            this.Close();
            zayavka.Show();
        }

        //Кнопка "Настроить соеденение", для открытия формы настройки соеденения
        private void настройкиСоедененияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password pass = new Password(conMySQL);
            pass.KakaFormaBilaVibrana = true;
            pass.Show();
            this.Close();
            /*
            Settings Stngs = new Settings(conMySQL);
            Stngs.button1.Enabled = false;
            Stngs.button3.Enabled = true;
            this.Close();
            Stngs.Show();*/
        }

        //Кнопка "Вход в систему", для входа в систему
        private void входToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Autorizatsia aut = new Autorizatsia(conMySQL);
            aut.Show();
            this.Close();
        }

        //Открытия формы добавления записи через файл
        private void добавитьЧерезФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRecordFromFile FromFile = new AddRecordFromFile(conMySQL);
            FromFile.Show();
        }

        //Кнопка "Редактирование пользователей", для открытия формы "Пользователи"
        private void редактированиеПользователйеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password pass = new Password(conMySQL);
            pass.KakaFormaBilaVibrana = false;
            pass.Show();
            this.Close();
        }

        //Кнопка "Редактирование преподователей", для открытия формы редактирования списка преподователей
        private void редактированиеПреподователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prepods prepod = new Prepods(conMySQL);
            prepod.Show();
            this.Hide();
        }

        //--------------------Конец другие процедуры -------------------//

        //--------------------Вкладка Компьютеры ------------------//

        public bool SaveTableComp = true;
        public int ID_Param_sis_blokov;

        //Кнопка "Очистка полей" Фильтрация, вкладка "Компьютеры"
        private void buttonClearFilter_Click(object sender, EventArgs e)
        {
            textBoxInvNomerComp.DataBindings.Clear();
            textBoxOSComp.DataBindings.Clear();
            textBoxMatComp.Clear();
            textBoxOZUComp.Clear();
            textBoxKuleraComp.Clear();
            textBoxVideoComp.Clear();
            textBoxHDDComp.Clear();
            textBoxProcComp.Clear();
            textBoxPitanieComp.Clear();
            textBoxTsenaComp.Clear();
            textBoxDopInfComp.Clear();
        }

        //Панель нафигации, вкладка "Компьютеры"
        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerComp.Panel2.Width < 293)
            {
                splitContainerComp.Panel2.Hide();
            }
            else
            {
                splitContainerComp.Panel2.Show();
            }
        }

        //Показать дату ввода
        private void checkBoxDataVvoda_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaComp.Checked == true)
            {
                textBoxDataVvodaComp.Visible = true;
            }
            else
            {
                textBoxDataVvodaComp.Visible = false;
            }
        }

        //Показать дату списания
        private void checkBoxDataSpisania_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaComp.Checked == true)
            {
                textBoxDataSpisaniaComp.Visible = true;
            }
            else
            {
                textBoxDataSpisaniaComp.Visible = false;
            }
        }

        //Кнопка "Фильтрация данных", вкладка Компьютеры
        private void buttonFilterComp_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaComp.Checked == true)
            { DataVvoda = textBoxDataVvodaComp.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaComp.Checked == true)
            { DataSpisania = textBoxDataSpisaniaComp.Text; }
            else
            { DataSpisania = String.Empty; }


            if (textBoxInvNomerComp.Text.Equals("") && textBoxOSComp.Text.Equals("") &&
                textBoxPrivodComp.Text.Equals("") && textBoxMatComp.Text.Equals("") &&
                textBoxOZUComp.Text.Equals("") && textBoxKuleraComp.Text.Equals("") &&
                textBoxVideoComp.Text.Equals("") && textBoxHDDComp.Text.Equals("") &&
                textBoxProcComp.Text.Equals("") && textBoxPitanieComp.Text.Equals("") &&
                textBoxStatusComp.Text.Equals("") && textBoxTsenaComp.Text.Equals("") &&
                textBoxDopInfComp.Text.Equals("") && textBoxAuditComp.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty)
            {
                SaveTableComp = true;
            }
            else
            {
                SaveTableComp = false;
            }

            string queryFilter = "SELECT * FROM `Param_sis_blokov` WHERE (`Inv_nomer` LIKE '%" + textBoxInvNomerComp.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`OS` LIKE '%" + textBoxOSComp.Text + "%' OR `OS` = '') and (`Privod` LIKE '%" + textBoxPrivodComp.Text + "%' OR `Privod` = '') and "
                    + "(`Mat_plata` LIKE '%" + textBoxMatComp.Text + "%' OR `Mat_plata` = '') and (`Pzu` LIKE '%" + textBoxOZUComp.Text + "%' OR `Pzu` = '') and "
                    + "(`Kulera` LIKE '%" + textBoxKuleraComp.Text + "%' OR `Kulera` = '') and (`Video` LIKE '%" + textBoxVideoComp.Text + "%' OR `Video` = '') and"
                    + "(`HDD` LIKE '%" + textBoxHDDComp.Text + "%' OR `HDD` = '') and (`Protsessor` LIKE '%" + textBoxProcComp.Text + "%' OR `Protsessor` = '') and "
                    + "(`Blok_pitania` LIKE '%" + textBoxPitanieComp.Text + "%' OR `Blok_pitania` = '') and (`Status` LIKE '%" + textBoxStatusComp.Text + "%' OR `Status` = '') and "
                    + "(`Tsena` LIKE '%" + textBoxTsenaComp.Text + "%' OR `Tsena` = '') and (`Dop_Inf` LIKE '%" + textBoxDopInfComp.Text + 
                    "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and (`Data_spisania` LIKE '%" +
                    DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textBoxAuditComp.Text + "%' OR `Auditoria` = '') LIMIT 0 , 100";

            conMySQL.LoadTable("Param_sis_blokov", queryFilter, binSourceComp, dataGridVComp, NavigatorComp);
        }

        //Кнопка "Показать всю таблицу"
        private void buttonShowTableComp_Click(object sender, EventArgs e)
        {
            SaveTableComp = true;
            conMySQL.LoadTable("Param_sis_blokov", "select * from `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);
        }

        //Кнопка "Очистка полей", для очистки полей, вкладка "Компьютеры"
        private void buttonClearComp_Click(object sender, EventArgs e)
        {
            ClearTextBoxComp();
        }

        //Кнопка "Добавить запись", для добавления записи, вкладка "Компьютеры"
        private void buttonAddRecordComp_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerComp.Text.Equals("")) || (textAddOSComp.Text.Equals("")) ||
                (textAddPrivodComp.Text.Equals("")) || (textAddMatPlataComp.Text.Equals("")) ||
                (textAddOZUComp.Text.Equals("")) || (textAddKuleraComp.Text.Equals("")) ||
                (textAddVideoComp.Text.Equals("")) || (textAddHDDComp.Text.Equals("")) ||
                (textAddProcComp.Text.Equals("")) || (textAddPitanieComp.Text.Equals("")) ||
                (textAddStatusComp.Text.Equals("")) || (textAddTsenaComp.Text.Equals("")) ||
                (textAddAuditComp.Text.Equals("")) ||
                (textAddDataVvodaComp.Text.Equals("")) || (textAddDataSpisaniaComp.Text.Equals("")))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_sis_blokov`");
                        try
                        {
                            ID_Param_sis_blokov = Convert.ToInt32(ID);
                            ID_Param_sis_blokov++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_sis_blokov = 1;
                    }


                    string queryString = "INSERT INTO `Param_sis_blokov` VALUES (" + ID_Param_sis_blokov + ",'" + textAddInvNomerComp.Text +
                                        "','" + textAddOSComp.Text + "','" + textAddPrivodComp.Text +
                                        "','" + textAddMatPlataComp.Text + "','" + textAddOZUComp.Text +
                                        "','" + textAddKuleraComp.Text + "','" + textAddVideoComp.Text +
                                        "','" + textAddHDDComp.Text + "','" + textAddProcComp.Text +
                                        "','" + textAddPitanieComp.Text + "','" + textAddStatusComp.Text +
                                        "','" + textAddTsenaComp.Text + "','" + textAddDopInfComp.Text + "','" +
                                        textAddDataVvodaComp.Text + "','" + textAddDataSpisaniaComp.Text + "','" + textAddAuditComp.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM Param_sis_blokov", binSourceComp, dataGridVComp, NavigatorComp);
                    ClearTextBoxComp();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVComp.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    ID_Param_sis_blokov = Convert.ToInt32(dataGridVComp[0, i].Value);
                    string queryString = "UPDATE `Param_sis_blokov` SET `Inv_nomer` = '"
                        + textAddInvNomerComp.Text + "', `OS` = '"
                        + textAddOSComp.Text + "', `Privod` = '"
                        + textAddPrivodComp.Text + "', `Mat_plata` = '"
                        + textAddMatPlataComp.Text + "', `Pzu` = '"
                        + textAddOZUComp.Text + "', `Kulera` = '"
                        + textAddKuleraComp.Text + "', `Video` = '"
                        + textAddVideoComp.Text + "', `HDD` = '"
                        + textAddHDDComp.Text + "', `Protsessor` = '"
                        + textAddProcComp.Text + "', `Blok_pitania` = '"
                        + textAddPitanieComp.Text + "', `Status` = '"
                        + textAddStatusComp.Text + "', `Tsena` = '"
                        + textAddTsenaComp.Text + "', `Dop_Inf` = '"
                        + textAddDopInfComp.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaComp.Text + "', `Data_spisania` = '"                        
                        + textAddDataSpisaniaComp.Text + "', `Auditoria` = '"
                        + textAddAuditComp.Text + "' WHERE `ID` = " + ID_Param_sis_blokov;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);
                    ClearTextBoxComp();
                    panelCompAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxComp()
        {
            textAddInvNomerComp.Clear();
            textAddMatPlataComp.Clear();
            textAddOZUComp.Clear();
            textAddKuleraComp.Clear();
            textAddVideoComp.Clear();
            textAddHDDComp.Clear();
            textAddProcComp.Clear();
            textAddPitanieComp.Clear();
            textAddTsenaComp.Clear();
            textAddDopInfComp.Clear();
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Компьютеры"
        private void buttonClosePanel_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelCompAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxComp();
        }

        //Кнопка "Сохранить данные", вкладка "Компьютеры"
        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (SaveTableComp == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);
                    SaveTableComp = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(13, dataGridVComp) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVComp.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_sis_blokov` VALUES (" + 1 + ",'" + dataGridVComp[1, i].Value +
                                            "','" + dataGridVComp[2, i].Value + "','" + dataGridVComp[3, i].Value +
                                            "','" + dataGridVComp[4, i].Value + "','" + dataGridVComp[5, i].Value +
                                            "','" + dataGridVComp[6, i].Value + "','" + dataGridVComp[7, i].Value +
                                            "','" + dataGridVComp[8, i].Value + "','" + dataGridVComp[9, i].Value +
                                            "','" + dataGridVComp[10, i].Value + "','" + dataGridVComp[11, i].Value +
                                            "','" + dataGridVComp[12, i].Value + "','" + dataGridVComp[13, i].Value + "','" +
                                            dataGridVComp[14, i].Value + "','" + dataGridVComp[15, i].Value + "','" + dataGridVComp[16, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_sis_blokov`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_sis_blokov` SET `Inv_nomer` = '"
                            + dataGridVComp[1, i].Value + "', `OS` = '"
                            + dataGridVComp[2, i].Value + "', `Privod` = '"
                            + dataGridVComp[3, i].Value + "', `Mat_plata` = '"
                            + dataGridVComp[4, i].Value + "', `Pzu` = '"
                            + dataGridVComp[5, i].Value + "', `Kulera` = '"
                            + dataGridVComp[6, i].Value + "', `Video` = '"
                            + dataGridVComp[7, i].Value + "', `HDD` = '"
                            + dataGridVComp[8, i].Value + "', `Protsessor` = '"
                            + dataGridVComp[9, i].Value + "', `Blok_pitania` = '"
                            + dataGridVComp[10, i].Value + "', `Status` = '"
                            + dataGridVComp[11, i].Value + "', `Tsena` = '"
                            + dataGridVComp[12, i].Value + "', `Dop_Inf` = '"
                            + dataGridVComp[13, i].Value + "', `Data_vvoda` = '"
                            + dataGridVComp[14, i].Value + "', `Data_spisania` = '"
                            + dataGridVComp[15, i].Value + "', `Auditoria` = '"
                            + dataGridVComp[16, i].Value + "' WHERE `ID` = " + dataGridVComp[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_sis_blokov`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_sis_blokov` VALUES (" + int_maxID + ",'" + dataGridVComp[1, i].Value +
                                            "','" + dataGridVComp[2, i].Value + "','" + dataGridVComp[3, i].Value +
                                            "','" + dataGridVComp[4, i].Value + "','" + dataGridVComp[5, i].Value +
                                            "','" + dataGridVComp[6, i].Value + "','" + dataGridVComp[7, i].Value +
                                            "','" + dataGridVComp[8, i].Value + "','" + dataGridVComp[9, i].Value +
                                            "','" + dataGridVComp[10, i].Value + "','" + dataGridVComp[11, i].Value +
                                            "','" + dataGridVComp[12, i].Value + "','" + dataGridVComp[13, i].Value + "','" +
                                            dataGridVComp[14, i].Value + "','" + dataGridVComp[15, i].Value + "','" + dataGridVComp[16, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    indexRow++;
                    indexColumn++;
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", вкладка "Компьютеры"
        private void buttonDeleteRecord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_sis_blokov`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVComp.CurrentRow.Index;
                    ID_Param_sis_blokov = Convert.ToInt32(dataGridVComp[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_sis_blokov` WHERE `ID` = " + ID_Param_sis_blokov);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceComp.RemoveAt(i);
                    conMySQL.LoadTable("Param_sis_blokov", "SELECT * FROM `Param_sis_blokov`", binSourceComp, dataGridVComp, NavigatorComp);
                }
            }
        }

        //Кнопка "Поиск по таблице", поиск данных в таблице, вкладка компьютеры
        private void buttonSearchComp_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVComp, textSearchComp);
        }

        //Кнопка "Очистка результатов поиска", очистка таблицы, вкладка Компьютеры
        private void buttomClearComp_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVComp);
        }

        //Кнопка "Печать", Печать таблицы в Excel, вкладка Компьютеры
        private void buttonPrintComp_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVComp);
        }

        //Клик по таблице, вкладка "Компьютеры"
        private void dataGridVComp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 14) || (e.ColumnIndex == 15))
                {
                    int indexRow = dataGridVComp.CurrentRow.Index;
                    int count = dataGridVComp.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        if (count < 9)
                        {
                            Rectangle curcell = dataGridVComp.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcComp.Location = p;
                            mcComp.Visible = true;
                            mcComp.Show();
                        }
                        else
                        {
                            Rectangle curcell = dataGridVComp.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcComp.Location = p;
                            mcComp.Visible = true;
                            mcComp.Show();
                        }
                    }
                    else
                    {
                        Rectangle curcell = dataGridVComp.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcComp.Location = p;
                        mcComp.Visible = true;
                        mcComp.Show();
                    }
                }
                else
                {
                    mcComp.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 11)
                {
                    Rectangle curcell1 = dataGridVComp.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusComp.Location = p1;
                    comboBoxDataGridStatusComp.Visible = true;
                    comboBoxDataGridStatusComp.Show();
                }
                else
                {
                    comboBoxDataGridStatusComp.Visible = false;
                }
            }
        }

        //Происходит при выборе даты, вкладка "Компьютеры"
        private void mcComp_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVComp.CurrentCell.Value = e.Start;
                mcComp.Visible = false;
                dataGridVComp.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии на кнопку клавиатуры, вкладка "Компьютеры"
        private void mcComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcComp.Hide();
            }
        }

        //Происходит при нажатии на кнопку клавиатуры, вкладка "Компьютеры"
        private void comboBoxDataGridStatusComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusComp.Hide();
            }
        }
        
        //Происходит при выборе элемента списка, вкладка "Компьютеры"
        private void comboBoxDataGridStatusComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVComp.CurrentCell.Value = comboBoxDataGridStatusComp.Text;
                comboBoxDataGridStatusComp.Visible = false;
                dataGridVComp.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Компьютеры -------------------//

        //--------------------Вкладка Ноутбуки -------------------//

        public bool SaveTableNout = true;
        public int ID_Param_nout;

        //Скрытие элементов управления, вкладка "Ноутбуки"
        private void splitContainerNout_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerNout.Panel2.Width < 293)
            {
                splitContainerNout.Panel2.Hide();
            }
            else
            {
                splitContainerNout.Panel2.Show();
            }
        }

        //Кнопка "Добавить/Изменить запись", вкладка "Ноутбуки"
        private void buttonAddRecordNout_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerNout.Text.Equals("")) ||
                (textAddOSNout.Text.Equals("")) || (textAddNazvNout.Text.Equals("")) ||
                (textAddProcNout.Text.Equals("")) || (textAddVideoNout.Text.Equals("")) ||
                (textAddDiagNout.Text.Equals("")) || (textAddPamyatNout.Text.Equals("")) ||
                (textAddHDDNout.Text.Equals("")) || (textAddStatusNout.Text.Equals("")) ||
                (textAddTsenaNout.Text.Equals("")) || ((textAddAuditNout.Text.Equals(""))))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_nout`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_nout`");
                        try
                        {
                            ID_Param_nout = Convert.ToInt32(ID);
                            ID_Param_nout++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_nout = 1;
                    }

                    string queryString = "INSERT INTO `Param_nout` VALUES (" + ID_Param_nout + ",'" + textAddInvNomerNout.Text +
                                        "','" + textAddOSNout.Text + "','" + textAddNazvNout.Text +
                                        "','" + textAddProcNout.Text + "','" + textAddVideoNout.Text +
                                        "','" + textAddDiagNout.Text + "','" + textAddPamyatNout.Text +
                                        "','" + textAddHDDNout.Text + "','" + textAddStatusNout.Text +
                                        "','" + textAddTsenaNout.Text + "','" + textAddDopInfNout.Text + 
                                        "','" + textAddDataVvodaNout.Text + "','" + textAddDataSpisaniaNout.Text + "'," + textAddAuditNout.Text + ")";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_nout", "SELECT * FROM Param_nout", binSourceNout, dataGridVNout, NavigatorNout);
                    ClearTextBoxNout();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVNout.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    ID_Param_nout = Convert.ToInt32(dataGridVNout[0, i].Value);
                    string queryString = "UPDATE `Param_nout` SET `Inv_Nomer` = '"
                        + textAddInvNomerNout.Text + "', `OS` = '"
                        + textAddOSNout.Text + "', `Nazvanie` = '"
                        + textAddNazvNout.Text + "', `Protsessor` = '"
                        + textAddProcNout.Text + "', `Video` = '"
                        + textAddVideoNout.Text + "', `Diag` = '"
                        + textAddDiagNout.Text + "', `Pamyat` = '"
                        + textAddPamyatNout.Text + "', `HDD` = '"
                        + textAddHDDNout.Text + "', `Status` = '"
                        + textAddStatusNout.Text + "', `Tsena` = '"
                        + textAddTsenaNout.Text + "', `Dop_Inf` = '"
                        + textAddDopInfNout.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaNout.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaNout.Text + "', `Auditoria` = '"
                        + textAddAuditNout.Text + "' WHERE `ID` = " + ID_Param_nout;
                    conMySQL.QueryToBool(queryString); 
                    conMySQL.LoadTable("Param_nout", "SELECT * FROM `Param_nout`", binSourceNout, dataGridVNout, NavigatorNout);
                    ClearTextBoxNout();
                    panelNoutAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxNout()
        {
            textAddInvNomerNout.Clear();
            textAddNazvNout.Clear();
            textAddProcNout.Clear();
            textAddVideoNout.Clear();
            textAddDiagNout.Clear();
            textAddPamyatNout.Clear();
            textAddHDDNout.Clear();
            textAddTsenaNout.Clear();
            textAddDopInfNout.Clear();
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Ноутбуки"
        private void buttonClosePanelNout_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelNoutAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxNout();
        }

        //Кнопка "Очистка полей ввода", вкладка "Ноутбуки"
        private void buttonClearNout_Click(object sender, EventArgs e)
        {
            ClearTextBoxNout();
        }   

        //Кнопка "Сохранить данные", вкладка "Ноутбуки"
        private void buttonSaveRecordNout_Click(object sender, EventArgs e)
        {
            if (SaveTableNout == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_nout", "SELECT * FROM `Param_nout`", binSourceNout, dataGridVNout, NavigatorNout);
                    SaveTableNout = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(11, dataGridVNout) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVNout.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_nout`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_nout` VALUES (" + 1 + ",'" + dataGridVNout[1, i].Value +
                                            "','" + dataGridVNout[2, i].Value + "','" + dataGridVNout[3, i].Value +
                                            "','" + dataGridVNout[4, i].Value + "','" + dataGridVNout[5, i].Value +
                                            "','" + dataGridVNout[6, i].Value + "','" + dataGridVNout[7, i].Value +
                                            "','" + dataGridVNout[8, i].Value + "','" + dataGridVNout[9, i].Value +
                                            "','" + dataGridVNout[10, i].Value + "','" + dataGridVNout[11, i].Value +
                                            "','" + dataGridVNout[12, i].Value + "','" + dataGridVNout[13, i].Value + "','" + dataGridVNout[14, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_nout`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_nout` SET `Inv_Nomer` = '"
                            + dataGridVNout[1, i].Value + "', `OS` = '"
                            + dataGridVNout[2, i].Value + "', `Nazvanie` = '"
                            + dataGridVNout[3, i].Value + "', `Protsessor` = '"
                            + dataGridVNout[4, i].Value + "', `Video` = '"
                            + dataGridVNout[5, i].Value + "', `Diag` = '"
                            + dataGridVNout[6, i].Value + "', `Pamyat` = '"
                            + dataGridVNout[7, i].Value + "', `HDD` = '"
                            + dataGridVNout[8, i].Value + "', `Status` = '"
                            + dataGridVNout[9, i].Value + "', `Tsena` = '"
                            + dataGridVNout[10, i].Value + "', `Dop_Inf` = '"
                            + dataGridVNout[11, i].Value + "', `Data_vvoda` = '"
                            + dataGridVNout[12, i].Value + "', `Data_spisania` = '"
                            + dataGridVNout[13, i].Value + "', `Auditoria` = '"
                            + dataGridVNout[14, i].Value + "' WHERE `ID` = " + dataGridVNout[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_nout`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_nout` VALUES (" + int_maxID + ",'" + dataGridVNout[1, i].Value +
                                            "','" + dataGridVNout[2, i].Value + "','" + dataGridVNout[3, i].Value +
                                            "','" + dataGridVNout[4, i].Value + "','" + dataGridVNout[5, i].Value +
                                            "','" + dataGridVNout[6, i].Value + "','" + dataGridVNout[7, i].Value +
                                            "','" + dataGridVNout[8, i].Value + "','" + dataGridVNout[9, i].Value +
                                            "','" + dataGridVNout[10, i].Value + "','" + dataGridVNout[11, i].Value +
                                            "','" + dataGridVNout[12, i].Value + "','" + dataGridVNout[13, i].Value + "','" + dataGridVNout[14, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", вкладка "Ноутбуки"
        private void buttonDeleteRecodNout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_nout`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVNout.CurrentRow.Index;
                    string id_Nout = Convert.ToString(dataGridVNout[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_nout` WHERE `ID` = '" + id_Nout + "'");
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceNout.RemoveAt(i);
                    conMySQL.LoadTable("Param_nout", "SELECT * FROM `Param_nout`", binSourceNout, dataGridVNout, NavigatorNout);
                }
            }
        }

        //Показать дату ввода
        private void checkBoxDataSpisaniaNout_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaNout.Checked == true)
            {
                textDataVvodaNout.Visible = true;
            }
            else
            {
                textDataVvodaNout.Visible = false;
            }
        }

        //Показать дату списания
        private void checkBoxDataVvodaNout_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaNout.Checked == true)
            {
                textDataSpisaniaNout.Visible = true;
            }
            else
            {
                textDataSpisaniaNout.Visible = false;
            }
        } 

        //Кнопка "Фильтрация данных", вкладка "Ноутбуки"
        private void buttonFilterNout_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaNout.Checked == true)
            { DataVvoda = textDataVvodaNout.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaNout.Checked == true)
            { DataSpisania = textDataSpisaniaNout.Text; }
            else
            { DataSpisania = String.Empty; }


            if (textInvNomerNout.Text.Equals("") && textOSNout.Text.Equals("") &&
                textNazvNout.Text.Equals("") && textProcNout.Text.Equals("") &&
                textVideoNout.Text.Equals("") && textDiagNout.Text.Equals("") &&
                textPamyatNout.Text.Equals("") && textTsenaNout.Text.Equals("") &&
                textDopInfNout.Text.Equals("") && textAuditNout.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty)
            {
                SaveTableNout = true;
            }
            else
            {
                SaveTableNout = false;
            }

            string queryFilterNout = "SELECT * FROM `Param_nout` WHERE (`Inv_nomer` LIKE '%" + textInvNomerNout.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`OS` LIKE '%" + textOSNout.Text + "%' OR `OS` = '') and (`Nazvanie` LIKE '%" + textNazvNout.Text + "%' OR `Nazvanie` = '') and "
                    + "(`Protsessor` LIKE '%" + textProcNout.Text + "%' OR `Protsessor` = '') and (`Video` LIKE '%" + textVideoNout.Text + "%' OR `Video` = '') and "
                    + "(`Diag` LIKE '%" + textDiagNout.Text + "%' OR `Diag` = '') and (`Pamyat` LIKE '%" + textPamyatNout.Text + "%' OR `Pamyat` = '') and"
                    + "(`HDD` LIKE '%" + textHDDNout.Text + "%' OR `HDD` = '') and (`Status` LIKE '%" + textStatusNout.Text + "%' OR `Status` = '') and "
                    + "(`Tsena` LIKE '%" + textTsenaNout.Text + "%' OR `Tsena` = '') and "
                    + "(`Dop_Inf` LIKE '%" + textDopInfNout.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditNout.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Param_nout", queryFilterNout, binSourceNout, dataGridVNout, NavigatorNout);
        }

        //Кнопка "Поиск", вкладка "Ноутбуки"
        private void buttonSearchNout_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVNout, textSearchNout);
        } 

        //Кнопка "Очистка результатов поиска", вкладка "Ноутубуки"
        private void buttomClearNout_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVNout);
        }

        //Кнопка "Печать", вкладка "Ноутбуки"
        private void buttonPrintNout_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVNout);
        }

        //Кнопка "Показать всю таблицу"
        private void buttonShowTableNout_Click(object sender, EventArgs e)
        {
            SaveTableNout = true;
            conMySQL.LoadTable("Param_nout", "SELECT * FROM `Param_nout`", binSourceNout, dataGridVNout, NavigatorNout);
        }

        //Кнопка "Очистка полей" Фильтрация, вкладка "Ноутбуки"
        private void buttonClearFilterNout_Click(object sender, EventArgs e)
        {
            textInvNomerNout.Clear();
            textNazvNout.Clear();
            textProcNout.Clear();
            textVideoNout.Clear();
            textDiagNout.Clear();
            textPamyatNout.Clear();
            textHDDNout.Clear();
            textTsenaNout.Clear();
            textDopInfNout.Clear();
        }

        //Клик по таблице, вкладка "Ноутбуки"
        private void dataGridVNout_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 12) || (e.ColumnIndex == 13))
                {
                    int indexRow = dataGridVNout.CurrentRow.Index;
                    int count = dataGridVNout.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        if (count < 9)
                        {
                            Rectangle curcell = dataGridVNout.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcNout.Location = p;
                            mcNout.Visible = true;
                            mcNout.Show();
                        }
                        else
                        {
                            Rectangle curcell = dataGridVNout.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcNout.Location = p;
                            mcNout.Visible = true;
                            mcNout.Show();
                        }
                    }
                    else
                    {
                        Rectangle curcell = dataGridVNout.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcNout.Location = p;
                        mcNout.Visible = true;
                        mcNout.Show();
                    }
                }
                else
                {
                    mcNout.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 9)
                {
                    Rectangle curcell1 = dataGridVNout.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusNout.Location = p1;
                    comboBoxDataGridStatusNout.Visible = true;
                    comboBoxDataGridStatusNout.Show();
                }
                else
                {
                    comboBoxDataGridStatusNout.Visible = false;
                }
            }
        }

        //Происходит при выборе даты, вкладка "Ноутбуки"
        private void mcNout_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVNout.CurrentCell.Value = e.Start;
                mcNout.Visible = false;
                dataGridVNout.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии на кнопку клавиатуры, вкладка "Ноутбуки"
        private void mcNout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcNout.Hide();
            }
        }

        //Происходит при нажатии на кнопку клавиатуры, вкладка "Ноутбуки"
        private void comboBoxDataGridStatusNout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusNout.Hide();
            }
        }

        //Происходит при выборе элемента списка, вкладка "Ноутбуки"
        private void comboBoxDataGridStatusNout_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVNout.CurrentCell.Value = comboBoxDataGridStatusNout.Text;
                comboBoxDataGridStatusNout.Visible = false;
                dataGridVNout.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Ноутбуки -------------------//
        
        //--------------------Вкладка Принтеры -------------------//

        public bool SaveTablePrint = true;
        public int ID_Param_print;

        //Кнопка "Очистка полей" Фильтрация, вкладка "Принтеры"
        private void buttonClearFilterPrint_Click(object sender, EventArgs e)
        {
            textInvNomerPrint.Clear();
            textNazvPrint.Clear();
            textMaxRazPrint.Clear();
            textRazmeriPrint.Clear();
            textPamyatPrint.Clear();
            textSkorPechPrint.Clear();
            textMaxFormatPrint.Clear();
            textCountStrPrint.Clear();
            textTsenaPrint.Clear();
            textDopInfPrint.Clear();
        }

        //Скрытие элементов управления, вкладка "Принтеры"
        private void splitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerPrint.Panel2.Width < 293)
            {
                splitContainerPrint.Panel2.Hide();
            }
            else
            {
                splitContainerPrint.Panel2.Show();
            }
        }

        //Кнопка "Показать всю таблицу", вкладка "Принтеры"
        private void buttonShowTablePrint_Click(object sender, EventArgs e)
        {
            SaveTablePrint = true;
            conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVNout, NavigatorNout);
        }

        //Показать дату ввода
        private void checkBoxDataSpisaniaPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaPrint.Checked == true)
            {
                textDataVvodaPrint.Visible = true;
            }
            else
            {
                textDataVvodaPrint.Visible = false;
            }
        }

        //Показать дату списания
        private void checkBoxDataVvodaPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaPrint.Checked == true)
            {
                textDataSpisaniaPrint.Visible = true;
            }
            else
            {
                textDataSpisaniaPrint.Visible = false;
            }
        }

        //Кнопка "Поиск", вкладка "Принтеры"
        private void buttonSearchPrint_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVPrint, textSearchComp);
        }

        //Кнопка "Очистка полей" Фильтрация, вкладка "Принтеры"
        private void buttonClearSearchPrint_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVPrint);
        }

        //Кнопка "Печать", вкладка "Принтеры"
        private void buttonPrintPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVPrint);
        }  

        //Кнопка "Очистка полей ввода", вкладка "Принтеры"
        private void buttonClearPrint_Click(object sender, EventArgs e)
        {
            ClearTextBoxPrint();
        }

        //Кнопка "ОК", вкладка "Принтеры"
        private void buttonAddRecordPrint_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerPrint.Text.Equals("")) ||
                (textAddNazvPrint.Text.Equals("")) || (textAddMaxRazPrint.Text.Equals("")) ||
                (textAddRazmeriPrint.Text.Equals("")) || (textAddPamyatPrint.Text.Equals("")) ||
                (textAddSkorPechPrint.Text.Equals("")) || (textAddMaxFormatPrint.Text.Equals("")) ||
                (textAddCountStrPrint.Text.Equals("")) || (textAddStatusPrint.Text.Equals("")) ||
                (textAddTsenaPrint.Text.Equals("")) || ((textAddAuditPrint.Text.Equals(""))))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_print`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_print`");
                        try
                        {
                            ID_Param_print = Convert.ToInt32(ID);
                            ID_Param_print++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_print = 1;
                    }

                    string queryString = "INSERT INTO `Param_print` VALUES (" + ID_Param_print + ",'" + textAddInvNomerPrint.Text +
                                        "','" + textAddNazvPrint.Text + "','" + textAddMaxRazPrint.Text +
                                        "','" + textAddRazmeriPrint.Text + "','" + textAddPamyatPrint.Text +
                                        "','" + textAddSkorPechPrint.Text + "','" + textAddMaxFormatPrint.Text +
                                        "','" + textAddCountStrPrint.Text + "','" + textAddStatusPrint.Text +
                                        "','" + textAddTsenaPrint.Text + "','" + textAddDopInfPrint.Text + 
                                        "','" + textAddDataVvodaPrint.Text + "','" + textAddDataSpisaniaPrint.Text + "','" + textAddAuditPrint.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVNout, NavigatorNout);
                    ClearTextBoxPrint();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrint.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string ID = Convert.ToString(dataGridVPrint[0, i].Value);
                    string queryString = "UPDATE `Param_print` SET `Inv_Nomer` = '"
                        + textAddInvNomerPrint.Text + "', `Nazvanie` = '"
                        + textAddNazvPrint.Text + "', `Max_raz` = '"
                        + textAddMaxRazPrint.Text + "', `Razmeri` = '"
                        + textAddRazmeriPrint.Text + "', `Pamyat` = '"
                        + textAddPamyatPrint.Text + "', `Skor_pechati` = '"
                        + textAddSkorPechPrint.Text + "', `Max_format` = '"
                        + textAddMaxFormatPrint.Text + "', `Kol_str` = '"
                        + textAddCountStrPrint.Text + "', `Status` = '"
                        + textAddStatusPrint.Text + "', `Tsena` = '"
                        + textAddTsenaPrint.Text + "', `Dop_Inf` = '"
                        + textAddDopInfPrint.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaPrint.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaPrint.Text + "', `Auditoria` = '"
                        + textAddAuditPrint.Text + "' WHERE `ID` = " + ID;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVNout, NavigatorNout);
                    ClearTextBoxPrint();
                    panelPrintAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Принтеры"
        private void buttonClosePanelPrint_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelPrintAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxPrint();
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxPrint()
        {
            textAddInvNomerPrint.Clear();
            textAddNazvPrint.Clear();
            textAddMaxRazPrint.Clear();
            textAddRazmeriPrint.Clear();
            textAddPamyatPrint.Clear();
            textAddSkorPechPrint.Clear();
            textAddMaxFormatPrint.Clear();
            textAddCountStrPrint.Clear();
            textAddTsenaPrint.Clear();
            textAddDopInfPrint.Clear();
        }

        //Кнопка "Сохранить запись", вкладка "Принтеры"
        private void buttonSaveRecordPrint_Click(object sender, EventArgs e)
        {
            if (SaveTablePrint == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVPrint, NavigatorPrint);
                    SaveTablePrint = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(11, dataGridVPrint) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrint.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_print`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_print` VALUES (" + 1 + ",'" + dataGridVPrint[1, i].Value +
                                            "','" + dataGridVPrint[2, i].Value + "','" + dataGridVPrint[3, i].Value +
                                            "','" + dataGridVPrint[4, i].Value + "','" + dataGridVPrint[5, i].Value +
                                            "','" + dataGridVPrint[6, i].Value + "','" + dataGridVPrint[7, i].Value +
                                            "','" + dataGridVPrint[8, i].Value + "','" + dataGridVPrint[9, i].Value +
                                            "','" + dataGridVPrint[10, i].Value + "','" + dataGridVPrint[11, i].Value +
                                            "','" + dataGridVPrint[12, i].Value + "','" + dataGridVPrint[13, i].Value + "','" + dataGridVPrint[14, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_print`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_print` SET `Inv_Nomer` = '"
                            + dataGridVPrint[1, i].Value + "', `Nazvanie` = '"
                            + dataGridVPrint[2, i].Value + "', `Max_raz` = '"
                            + dataGridVPrint[3, i].Value + "', `Razmeri` = '"
                            + dataGridVPrint[4, i].Value + "', `Pamyat` = '"
                            + dataGridVPrint[5, i].Value + "', `Skor_pechati` = '"
                            + dataGridVPrint[6, i].Value + "', `Max_format` = '"
                            + dataGridVPrint[7, i].Value + "', `Kol_str` = '"
                            + dataGridVPrint[8, i].Value + "', `Status` = '"
                            + dataGridVPrint[9, i].Value + "', `Tsena` = '"
                            + dataGridVPrint[10, i].Value + "', `Dop_Inf` = '"
                            + dataGridVPrint[11, i].Value + "', `Data_vvoda` = '"
                            + dataGridVPrint[12, i].Value + "', `Data_spisania` = '"
                            + dataGridVPrint[13, i].Value + "', `Auditoria` = '"
                            + dataGridVPrint[14, i].Value + "' WHERE `ID` = " + dataGridVPrint[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_print`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_print` VALUES (" + int_maxID + ",'" + dataGridVPrint[1, i].Value +
                                            "','" + dataGridVPrint[2, i].Value + "','" + dataGridVPrint[3, i].Value +
                                            "','" + dataGridVPrint[4, i].Value + "','" + dataGridVPrint[5, i].Value +
                                            "','" + dataGridVPrint[6, i].Value + "','" + dataGridVPrint[7, i].Value +
                                            "','" + dataGridVPrint[8, i].Value + "','" + dataGridVPrint[9, i].Value +
                                            "','" + dataGridVPrint[10, i].Value + "','" + dataGridVPrint[11, i].Value +
                                            "','" + dataGridVPrint[12, i].Value + "','" + dataGridVPrint[13, i].Value + "','" + dataGridVPrint[14, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", вкладка "Принтеры"
        private void textDeleteRecordPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_print`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVPrint.CurrentRow.Index;
                    string id_Print = Convert.ToString(dataGridVPrint[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_print` WHERE `ID` = " + id_Print);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourcePrint.RemoveAt(i);
                    conMySQL.LoadTable("Param_print", "SELECT * FROM `Param_print`", binSourcePrint, dataGridVPrint, NavigatorPrint);
                }
            }
        }

        //Кнопка "Фильтрация", вкладка "Принтеры"
        private void buttonFilterPrint_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaPrint.Checked == true)
            { DataVvoda = textDataVvodaPrint.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaPrint.Checked == true)
            { DataSpisania = textDataSpisaniaPrint.Text; }
            else
            { DataSpisania = String.Empty; }

            if (textInvNomerPrint.Text.Equals("") && textNazvPrint.Text.Equals("") &&
                textMaxRazPrint.Text.Equals("") && textRazmeriPrint.Text.Equals("") &&
                textPamyatPrint.Text.Equals("") && textSkorPechPrint.Text.Equals("") &&
                textMaxFormatPrint.Text.Equals("") && textCountStrPrint.Text.Equals("") &&
                textStatusPrint.Text.Equals("") && textDopInfPrint.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty && textAuditPrint.Text.Equals(""))
            {
                SaveTablePrint = true;
            }
            else
            {
                SaveTablePrint = false;
            }

            string queryFilterPrint = "SELECT * FROM `Param_print` WHERE (`Inv_nomer` LIKE '%" + textInvNomerPrint.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`Nazvanie` LIKE '%" + textNazvPrint.Text + "%' OR `Nazvanie` = '') and (`Max_raz` LIKE '%" + textMaxRazPrint.Text + "%' OR `Max_raz` = '') and "
                    + "(`Razmeri` LIKE '%" + textRazmeriPrint.Text + "%' OR `Razmeri` = '') and (`Pamyat` LIKE '%" + textPamyatPrint.Text + "%' OR `Pamyat` = '') and "
                    + "(`Skor_pechati` LIKE '%" + textSkorPechPrint.Text + "%' OR `Skor_pechati` = '') and (`Max_format` LIKE '%" + textMaxFormatPrint.Text + "%' OR `Max_format` = '') and"
                    + "(`Kol_str` LIKE '%" + textCountStrPrint.Text + "%' OR `Kol_str` = '') and (`Status` LIKE '%" + textStatusPrint.Text + "%' OR `Status` = '') and "
                    + "(`Tsena` LIKE '%" + textTsenaPrint.Text + "%' OR `Tsena` = '') and "
                    + "(`Dop_Inf` LIKE '%" + textDopInfPrint.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditPrint.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Param_print", queryFilterPrint, binSourcePrint, dataGridVPrint, NavigatorPrint);
        }

        //Клик по таблице, вкладка "Принтеры"
        private void dataGridVPrint_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 12) || (e.ColumnIndex == 13))
                {
                    int indexRow = dataGridVPrint.CurrentRow.Index;
                    int count = dataGridVPrint.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        if (count < 9)
                        {
                            Rectangle curcell = dataGridVPrint.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcPrint.Location = p;
                            mcPrint.Visible = true;
                            mcPrint.Show();
                        }
                        else
                        {
                            Rectangle curcell = dataGridVPrint.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                            Point p = new Point(curcell.Right, curcell.Bottom);
                            mcPrint.Location = p;
                            mcPrint.Visible = true;
                            mcPrint.Show();
                        }
                    }
                    else
                    {
                        Rectangle curcell = dataGridVPrint.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcPrint.Location = p;
                        mcPrint.Visible = true;
                        mcPrint.Show();
                    }
                }
                else
                {
                    mcPrint.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 9)
                {
                    Rectangle curcell1 = dataGridVPrint.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusPrint.Location = p1;
                    comboBoxDataGridStatusPrint.Visible = true;
                    comboBoxDataGridStatusPrint.Show();
                }
                else
                {
                    comboBoxDataGridStatusPrint.Visible = false;
                }
            }
        }

        //Происходит при выборе даты, вкладка "Принтеры"
        private void mcPrint_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVPrint.CurrentCell.Value = e.Start;
                mcPrint.Visible = false;
                dataGridVPrint.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии клавиши клавиатуры, вкладка "Принтеры"
        private void mcPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcPrint.Hide();
            }
        }

        //Происходит при нажатии клавиши клавиатуры, вкладка "Принтеры"
        private void comboBoxDataGridStatusPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusPrint.Hide();
            }
        }

        //Происходит при выборе элемента списка, вкладка "Принтеры"
        private void comboBoxDataGridStatusPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVPrint.CurrentCell.Value = comboBoxDataGridStatusPrint.Text;
                comboBoxDataGridStatusPrint.Visible = false;
                dataGridVPrint.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Принтеры -------------------//

        //--------------------Вкладка Мышки -------------------//

        public bool SaveTableMouse = true;
        public int ID_Param_mouse;

        //Скрытие элементов управления, вкладка "Мышки"
        private void splitContainerMouse_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerMouse.Panel2.Width < 293)
            {
                splitContainerMouse.Panel2.Hide();
            }
            else
            {
                splitContainerMouse.Panel2.Show();
            }
        }

        //Дата ввода в эксплуатацию, вкладка "Мышки"
        private void checkBoxDataVvodaMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaMouse.Checked == true)
            {
                textDataVvodaMouse.Visible = true;
            }
            else
            {
                textDataVvodaMouse.Visible = false;
            }
        }

        //Дата списания, вкладка "Мышки"
        private void checkBoxDataSpisaniaMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaMouse.Checked == true)
            {
                textDataSpisaniaMouse.Visible = true;
            }
            else
            {
                textDataSpisaniaMouse.Visible = false;
            }
        }

        //Кнопка "Поиск", вкладка "Мышки"
        private void buttonSearchMouse_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVMishek, textSearchMouse);
        }

        //Кнопка "Очистка результатов поиска", вкладка "Мышки"
        private void buttonClearSearchMouse_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVMishek);
        }

        //Кнопка "Печать", вкладка "Мышки"
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVMishek);
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Мышки"
        private void buttonSaveRecordMishek_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelMouseAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxMouse();
        }

        //Кнопка "OK", вкладка "Мышки"
        private void buttonAddRecordMouse_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerMouse.Text.Equals("")) ||
               (textAddTipPodMouse.Text.Equals("")) || (textAddStatusMouse.Text.Equals("")) ||
               (textAddTsenaMouse.Text.Equals("")) || ((textAddAuditMouse.Text.Equals(""))))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_mishek`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_mishek`");
                        try
                        {
                            ID_Param_mouse = Convert.ToInt32(ID);
                            ID_Param_mouse++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_mouse = 1;
                    }

                    string queryString = "INSERT INTO `Param_mishek` VALUES (" + ID_Param_mouse + ",'" + textAddInvNomerMouse.Text +
                                        "','" + textAddTipPodMouse.Text + "','" + textAddStatusMouse.Text +
                                        "','" + textAddTsenaMouse.Text + "','" + textAddDopInfMouse.Text + 
                                        "','" + textAddDataVvodaMouse.Text + "','" + textAddDataSpisaniaMouse.Text + "','" + textAddAuditMouse.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_mishek", "SELECT * FROM Param_mishek", binSourceMouse, dataGridVMishek, NavigatorMishek);
                    ClearTextBoxMouse();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMishek.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string ID = Convert.ToString(dataGridVMishek[0, i].Value);
                    string queryString = "UPDATE `Param_mishek` SET `Inv_Nomer` = '"
                        + textAddInvNomerMouse.Text + "', `Tip_podkl` = '"
                        + textAddTipPodMouse.Text + "', `Status` = '"
                        + textAddStatusMouse.Text + "', `Tsena` = '"
                        + textAddTsenaMouse.Text + "', `Dop_Inf` = '"
                        + textAddDopInfMouse.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaMouse.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaMouse.Text + "', `Auditoria` = '"
                        + textAddAuditMouse.Text + "' WHERE `ID` = " + ID ;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_mishek", "SELECT * FROM `Param_mishek`", binSourceMouse, dataGridVMishek, NavigatorMishek);
                    ClearTextBoxMouse();
                    panelMouseAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Процедура очистки полей ввода
        private void buttonClearMouse_Click(object sender, EventArgs e)
        {
            ClearTextBoxMouse();
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxMouse()
        {
            textAddInvNomerMouse.Clear();
            textAddTsenaMouse.Clear();
            textAddDopInfMouse.Clear();
        }

        //Кнопка "Сохранить данные", вкладка "Мышки"
        private void buttonSaveRecordMouse_Click(object sender, EventArgs e)
        {
            if (SaveTableMouse == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_mishek", "SELECT * FROM `Param_mishek`", binSourceMouse, dataGridVMishek, NavigatorMishek);
                    SaveTableMouse = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(5, dataGridVMishek) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMishek.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_mishek`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_mishek` VALUES (" + 1 + ",'" + dataGridVMishek[1, i].Value +
                                            "','" + dataGridVMishek[2, i].Value + "','" + dataGridVMishek[3, i].Value +
                                            "','" + dataGridVMishek[4, i].Value + "','" + dataGridVMishek[5, i].Value +
                                            "','" + dataGridVMishek[6, i].Value + "','" + dataGridVMishek[7, i].Value + "','" + dataGridVMishek[8, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_mishek`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_mishek` SET `Inv_Nomer` = '"
                            + dataGridVMishek[1, i].Value + "', `Tip_podkl` = '"
                            + dataGridVMishek[2, i].Value + "', `Status` = '"
                            + dataGridVMishek[3, i].Value + "', `Tsena` = '"
                            + dataGridVMishek[4, i].Value + "', `Dop_Inf` = '"
                            + dataGridVMishek[5, i].Value + "', `Data_vvoda` = '"
                            + dataGridVMishek[6, i].Value + "', `Data_spisania` = '"
                            + dataGridVMishek[7, i].Value + "', `Auditoria` = '"
                            + dataGridVMishek[8, i].Value + "' WHERE `ID` = " + dataGridVMishek[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_mishek`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_mishek` VALUES (" + int_maxID + ",'" + dataGridVMishek[1, i].Value +
                                            "','" + dataGridVMishek[2, i].Value + "','" + dataGridVMishek[3, i].Value +
                                            "','" + dataGridVMishek[4, i].Value + "','" + dataGridVMishek[5, i].Value +
                                            "','" + dataGridVMishek[6, i].Value + "','" + dataGridVMishek[7, i].Value + "','" + dataGridVMishek[8, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", вкладка "Мышки"
        private void buttonDeleteRecordMouse_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_mishek`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMishek.CurrentRow.Index;
                    string id_Mishek = Convert.ToString(dataGridVMishek[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_mishek` WHERE `ID` = " + id_Mishek);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceMouse.RemoveAt(i);
                    conMySQL.LoadTable("Param_mishek", "SELECT * FROM `Param_mishek`", binSourceMouse, dataGridVMishek, NavigatorMishek);
                }
            }
        }

        //Кнопка "Очиста полей" Фильтрация, вкладка "Мышки"
        private void buttonClearFilterMouse_Click(object sender, EventArgs e)
        {
            textInvNomerMouse.Clear();
            textTsenaMouse.Clear();
            textDopInfMouse.Clear();
        }

        //Кнопка "Показать всю таблицу", вкладка "Мышки"
        private void buttonShowTableMouse_Click(object sender, EventArgs e)
        {
            SaveTableMouse = true;
            conMySQL.LoadTable("Param_mishek", "SELECT * FROM `Param_mishek`", binSourceMouse, dataGridVMishek, NavigatorMishek);
        }

        //Кнопка "Фильтрация данных", вкладка "Мышки"
        private void buttonFilterMouse_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaMouse.Checked == true)
            { DataVvoda = textDataVvodaMouse.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaMouse.Checked == true)
            { DataSpisania = textDataSpisaniaMouse.Text; }
            else
            {
                DataSpisania = String.Empty;
            }

            if (textInvNomerMouse.Text.Equals("") && textTipPodMouse.Text.Equals("") &&
                textStatusMouse.Text.Equals("") && textTsenaMouse.Text.Equals("") &&
                textDopInfMouse.Text.Equals("") && textAuditMouse.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty)
            {
                SaveTableMouse = true;
            }
            else
            {
                SaveTableMouse = false;
            }


            string queryFilterMouse = "SELECT * FROM `Param_mishek` WHERE (`Inv_nomer` LIKE '%" + textInvNomerMouse.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`Tip_podkl` LIKE '%" + textTipPodMouse.Text + "%' OR `Tip_podkl` = '') and (`Status` LIKE '%" + textStatusMouse.Text + "%' OR `Status` = '') and "
                    + "(`Tsena` LIKE '%" + textTsenaMouse.Text + "%' OR `Tsena` = '') and "
                    + "(`Dop_Inf` LIKE '%" + textDopInfMouse.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditMouse.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Param_mishek", queryFilterMouse, binSourceMouse, dataGridVMishek, NavigatorMishek);
        }

        //Кнопка "Очистка", вкладка "Мышки"
        private void buttonClearSMouse_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVMishek);
        }

        //Клик по таблице, вкладка "Мышки"
        private void dataGridVMishek_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 6) || (e.ColumnIndex == 7))
                {
                    int indexRow = dataGridVMishek.CurrentRow.Index;
                    int count = dataGridVMishek.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        Rectangle curcell = dataGridVMishek.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcMouse.Location = p;
                        mcMouse.Visible = true;
                        mcMouse.Show();
                    }
                    else
                    {
                        Rectangle curcell = dataGridVMishek.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcMouse.Location = p;
                        mcMouse.Visible = true;
                        mcMouse.Show();
                    }
                }
                else
                {
                    mcMouse.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 3)
                {
                    Rectangle curcell1 = dataGridVMishek.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusMouse.Location = p1;
                    comboBoxDataGridStatusMouse.Visible = true;
                    comboBoxDataGridStatusMouse.Show();
                }
                else
                {
                    comboBoxDataGridStatusMouse.Visible = false;
                }

                //Для типа подключения
                if (e.ColumnIndex == 2)
                {
                    Rectangle curcell3 = dataGridVMishek.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p3 = new Point(curcell3.Right, curcell3.Bottom);
                    comboBoxDataGridTypePodMouse.Location = p3;
                    comboBoxDataGridTypePodMouse.Visible = true;
                    comboBoxDataGridTypePodMouse.Show();
                }
                else
                {
                    comboBoxDataGridTypePodMouse.Visible = false;
                }
            }
        }

        //Происходит при выборе даты, вкладка "Мышки"
        private void mcMouse_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVMishek.CurrentCell.Value = e.Start;
                mcMouse.Visible = false;
                dataGridVMishek.Enabled = true;
            }
            catch { }
        }

        //Происходит во время нажатия клавишы клавиатуры, вкладка "Мышки"
        private void mcMouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcMouse.Hide();
            }
        }

        //Происходит во время нажатия клавишы клавиатуры, вкладка "Мышки"
        private void comboBoxDataGridStatusMouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusMouse.Hide();
            }
        }

        //Происходит во время нажатия клавишы клавиатуры, вкладка "Мышки"
        private void comboBoxDataGridTypePodMouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridTypePodMouse.Hide();
            }
        }

        //Происходит при выборе элемента списка, вкладка "Мышки"
        private void comboBoxDataGridStatusMouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVMishek.CurrentCell.Value = comboBoxDataGridStatusMouse.Text;
                comboBoxDataGridStatusMouse.Visible = false;
                dataGridVMishek.Enabled = true;
            }
            catch { }
        }

        //Происходит при выборе элемента списка, вкладка "Мышки"
        private void comboBoxDataGridTypePodMouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVMishek.CurrentCell.Value = comboBoxDataGridTypePodMouse.Text;
                comboBoxDataGridTypePodMouse.Visible = false;
                dataGridVMishek.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Мышки -------------------//

        //--------------------Вкладка Мониторы -------------------//

        public bool SaveTableMon = true;
        public int ID_Param_Mon;

        //Дата ввода в эксплуатацию, вкладка "Мониторы"
        private void checkBoxDataVvodaMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaMonitor.Checked == true)
            {
                textDataVvodaMonitor.Visible = true;
            }
            else
            {
                textDataVvodaMonitor.Visible = false;
            }
        }

        //Дата списания, вкладка "Мониторы"
        private void checkBoxDataSpisaniaMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaMonitor.Checked == true)
            {
                textDataSpisaniaMonitor.Visible = true;
            }
            else
            {
                textDataSpisaniaMonitor.Visible = false;
            }
        }

        //Скрытие элементов управления, вкладка "Мышки"
        private void splitContainerMonitor_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerMonitor.Panel2.Width < 293)
            {
                splitContainerMonitor.Panel2.Hide();
            }
            else
            {
                splitContainerMonitor.Panel2.Show();
            }
        }

        //Кнопка "Фильтрация", вкладка "Мониторы"
        private void buttonFilterMonitor_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaMonitor.Checked == true)
            { DataVvoda = textDataVvodaMonitor.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaMonitor.Checked == true)
            { DataSpisania = textDataSpisaniaMonitor.Text; }
            else
            {
                DataSpisania = String.Empty;
            }

            if (textInvNomerMouse.Text.Equals("") && textTipPodMouse.Text.Equals("") &&
                textStatusMouse.Text.Equals("") && textTsenaMouse.Text.Equals("") &&
                textDopInfMouse.Text.Equals("") && textAuditMouse.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty)
            {
                SaveTableMon = true;
            }
            else
            {
                SaveTableMon = false;
            }

            string queryFilterMonitor = "SELECT * FROM `Param_monitor` WHERE (`Inv_nomer` LIKE '%" + textInvNomerMon.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`Razreshenie` LIKE '%" + textRazreshMon.Text + "%' OR `Razreshenie` = '') and (`Diag` LIKE '%" + textDiagMon.Text + "%' OR `Diag` = '') and" 
                    + "(`Status` LIKE '%" + textStatusMon.Text + "%' OR `Status` = '') and (`Tsena` LIKE '%" + textTsenaMon.Text + "%' OR `Tsena` = '') and "
                    + "(`Dop_Inf` LIKE '%" + textDopInfMon.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditMon.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Param_monitor", queryFilterMonitor, binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
        }

        //Кнопка "Очистка полей" Фильтрация, вкладка "Мониторы"
        private void buttonClearFilterMonitor_Click(object sender, EventArgs e)
        {
            textInvNomerMon.Clear();
            textDiagMon.Clear();
            textTsenaMon.Clear();
            textDopInfMon.Clear();
            textAuditMon.DataBindings.Clear();
        }

        //Кнопка "Показать всю таблицу", вкладка "Мониторы"
        private void buttonShowTableMonitor_Click(object sender, EventArgs e)
        {
            SaveTableMon = true;
            conMySQL.LoadTable("Param_monitor", "SELECT * FROM Param_monitor", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
        }

        //Кнопка "Поиск", таблицы "Монитор"
        private void buttonSearchMonitor_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVMonitor, textSearchMonitor);
        }

        //Кнопка "Очистка результатов поиска", вкладка "Монитор"
        private void buttonClearSearchMonitor_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVMonitor);
        }

        //Кнопка "Печать", вкладка "Монитор"
        private void buttonPrintMonitor_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVMonitor);
        }

        //Кнопка "OK", вкладка "Мониторы"
        private void buttonAddRecordMon_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerMon.Text.Equals("")) ||
               (textAddRazreshMon.Text.Equals("")) || (textAddDiagMon.Text.Equals("")) ||
               (textAddStatusMon.Text.Equals("")) || (textAddTsenaMon.Text.Equals("")) ||
               (textAddAuditMon.Text.Equals("")))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_monitor`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_monitor`");
                        try
                        {
                            ID_Param_Mon = Convert.ToInt32(ID);
                            ID_Param_Mon++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_Mon = 1;
                    }

                    string queryString = "INSERT INTO `Param_monitor` VALUES (" + ID_Param_Mon + ",'" + textAddInvNomerMon.Text +
                                        "','" + textAddRazreshMon.Text + "','" + textAddDiagMon.Text +
                                        "','" + textAddStatusMon.Text + "','" + textAddTsenaMon.Text +
                                        "','" +  textAddDopInfMon.Text + "','" + textAddDataVvodaMonitor.Text + 
                                        "','" + textAddDataSpisaniaMonitor.Text + "','" + textAddAuditMon.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_monitor", "SELECT * FROM `Param_monitor`", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
                    ClearTextBoxMon();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMonitor.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_Monitor = Convert.ToString(dataGridVMonitor[0, i].Value);
                    string queryString = "UPDATE `Param_monitor` SET `Inv_Nomer` = '"
                        + textAddInvNomerMon.Text + "', `Razreshenie` = '"
                        + textAddRazreshMon.Text + "', `Diag` = '"
                        + textAddDiagMon.Text + "', `Status` = '"
                        + textAddStatusMon.Text + "', `Tsena` = '"
                        + textAddTsenaMon.Text + "', `Dop_Inf` = '"
                        + textAddDopInfMon.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaMonitor.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaMonitor.Text + "', `Auditoria` = '"
                        + textAddAuditMon.Text + "' WHERE `ID` = " + id_Monitor;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_monitor", "SELECT * FROM `Param_monitor`", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
                    ClearTextBoxMon();
                    panelMonAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Кнопка "Очистка полей ввода", вкладка "Мониторы"
        private void buttonClearMon_Click(object sender, EventArgs e)
        {
            ClearTextBoxMon();
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Мониторы"
        private void buttonClosePanelMon_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelMonAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxMouse();
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxMon()
        {
            textAddInvNomerMon.Clear();
            textAddDiagMon.Clear();
            textAddTsenaMon.Clear();
            textAddDopInfMon.Clear();
        }

        //Кнопка "Сохранить данные", вкладка "Мониторы"
        private void buttonSaveRecordMon_Click(object sender, EventArgs e)
        {
            if (SaveTableMon == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_monitor", "SELECT * FROM `Param_monitor`", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
                    SaveTableMon = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(6, dataGridVMonitor) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMonitor.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_monitor`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_monitor` VALUES (" + 1 + ",'" + dataGridVMonitor[1, i].Value +
                                            "','" + dataGridVMonitor[2, i].Value + "','" + dataGridVMonitor[3, i].Value +
                                            "','" + dataGridVMonitor[4, i].Value + "','" + dataGridVMonitor[5, i].Value +
                                            "','" + dataGridVMonitor[6, i].Value + "','" + dataGridVMonitor[7, i].Value +
                                            "','" + dataGridVMonitor[8, i].Value + "','" + dataGridVMonitor[9, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {

                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_monitor`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_monitor` SET `Inv_Nomer` = '"
                            + dataGridVMonitor[1, i].Value + "', `Razreshenie` = '"
                            + dataGridVMonitor[2, i].Value + "', `Diag` = '"
                            + dataGridVMonitor[3, i].Value + "', `Status` = '"
                            + dataGridVMonitor[4, i].Value + "', `Tsena` = '"
                            + dataGridVMonitor[5, i].Value + "', `Dop_Inf` = '"
                            + dataGridVMonitor[6, i].Value + "', `Data_vvoda` = '"
                            + dataGridVMonitor[7, i].Value + "', `Data_spisania` = '"
                            + dataGridVMonitor[8, i].Value + "', `Auditoria` = '"
                            + dataGridVMonitor[9, i].Value + "' WHERE `ID` = " + dataGridVMonitor[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_monitor`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_monitor` VALUES (" + int_maxID + ",'" + dataGridVMonitor[1, i].Value +
                                            "','" + dataGridVMonitor[2, i].Value + "','" + dataGridVMonitor[3, i].Value +
                                            "','" + dataGridVMonitor[4, i].Value + "','" + dataGridVMonitor[5, i].Value +
                                            "','" + dataGridVMonitor[6, i].Value + "','" + dataGridVMonitor[7, i].Value +
                                            "','" + dataGridVMonitor[8, i].Value + "','" + dataGridVMonitor[9, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        
        //Кнопка "Удалить запись", вкладка "Мониторы"
        private void buttonDeleteRecordMon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Param_monitor`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVMonitor.CurrentRow.Index;
                    string id_Monitor = Convert.ToString(dataGridVMonitor[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_monitor` WHERE `ID` = " + id_Monitor);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceMonitor.RemoveAt(i);
                    conMySQL.LoadTable("Param_monitor", "SELECT * FROM Param_monitor", binSourceMonitor, dataGridVMonitor, NavigatorMonitor);
                }
            }
        }

        //Клик по таблице, вкладка "Мониторы"
        private void dataGridVMonitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 7) || (e.ColumnIndex == 8))
                {
                    int indexRow = dataGridVMonitor.CurrentRow.Index;
                    int count = dataGridVMonitor.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        Rectangle curcell = dataGridVMonitor.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcMon.Location = p;
                        mcMon.Visible = true;
                        mcMon.Show();
                    }
                    else
                    {
                        Rectangle curcell = dataGridVMonitor.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcMon.Location = p;
                        mcMon.Visible = true;
                        mcMon.Show();
                    }
                }
                else
                {
                    mcMon.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 4)
                {
                    Rectangle curcell1 = dataGridVMonitor.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusMon.Location = p1;
                    comboBoxDataGridStatusMon.Visible = true;
                    comboBoxDataGridStatusMon.Show();
                }
                else
                {
                    comboBoxDataGridStatusMon.Visible = false;
                }
            }
        }

        //Выбора даты, вкладка "Мониторы"
        private void mcMon_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVMonitor.CurrentCell.Value = e.Start;
                mcMon.Visible = false;
                dataGridVMonitor.Enabled = true;
            }
            catch { }
        }

        //Нажатие клавишы, вкладка "Мониторы"
        private void comboBoxDataGridStatusMon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusMon.Hide();
            }
        }

        //Нажатие клавишы, вкладка "Мониторы"
        private void mcMon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcMon.Hide();
            }
        }

        //Выбор элемента из списка, вкладка "Мониторы"
        private void comboBoxDataGridStatusMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVMonitor.CurrentCell.Value = comboBoxDataGridStatusMon.Text;
                comboBoxDataGridStatusMon.Visible = false;
                dataGridVMonitor.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Мониторы -------------------//

        //--------------------Вкладка Клавиатуры -------------------//

        public bool SaveTableKlava = true;
        public int ID_Param_Klava;

        //Кнопка "Очистка полей", вкладка "Клавиатуры"
        private void buttonClearFilterKlava_Click(object sender, EventArgs e)
        {
            textInvNomerKlava.Clear();
            textTipPodKlava.Clear();
            textCountKlavishKlava.Clear();
            textTsenaKlava.Clear();
            textDopInfKlava.Clear();
        }

        //Кнопка "Показать всю таблицу", вкладка "Клавиатуры"
        private void buttonShowTableKlava_Click(object sender, EventArgs e)
        {
            SaveTableKlava = true;
            conMySQL.LoadTable("Param_klava", "SELECT * FROM Param_klava", binSourceKlava, dataGridVKlava, NavigatorKlava);
        }

        //Скрытие элементов управления, вкладка "Клавиатуры"
        private void splitContainerKlava_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerKlava.Panel2.Width < 293)
            {
                splitContainerKlava.Panel2.Hide();
            }
            else
            {
                splitContainerKlava.Panel2.Show();
            }
        }

        //Дата введения в эксплуатацию, вкладка "Клавиатуры"
        private void checkBoxDataVvodaKlava_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaKlava.Checked == true)
            {
                textDataVvodaKlava.Visible = true;
            }
            else
            {
                textDataVvodaKlava.Visible = false;
            }
        }

        //Дата списания, вкладка "Клавиатуры"
        private void checkBoxDataSpisaniaKlava_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaKlava.Checked == true)
            {
                textDataSpisaniaKlava.Visible = true;
            }
            else
            {
                textDataSpisaniaKlava.Visible = false;
            }
        }

        //Кнопка "Поиск", вкладка "Клавиатуры"
        private void buttonSearchKlava_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVKlava, textSearchKlava);
        }

        //Кнопка "Очистка результатов поиска", вкладка "Клавиатуры"
        private void buttonClearSearchKlava_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVKlava);
        }

        //Кнопка "Печать", вкладка "Клавиатуры"
        private void buttonPrintKlava_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVKlava);
        }

        //Кнопка "Фильтрация данных", вкадка "Клавиатура"
        private void buttonFilterKlava_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaKlava.Checked == true)
            { DataVvoda = textDataVvodaKlava.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaKlava.Checked == true)
            { DataSpisania = textDataSpisaniaKlava.Text; }
            else
            {
                DataSpisania = String.Empty;
            }

            if (textInvNomerKlava.Text.Equals("") && textTipPodKlava.Text.Equals("") &&
                textCountKlavishKlava.Text.Equals("") && textStatusKlava.Text.Equals("") &&
                textTsenaKlava.Text.Equals("") && textDopInfKlava.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty && textAuditKlava.Text.Equals(""))
            {
                SaveTableKlava = true;
            }
            else
            {
                SaveTableKlava = false;
            }

            string queryFilterKlava = "SELECT * FROM `Param_klava` WHERE (`Inv_nomer` LIKE '%" + textInvNomerKlava.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`Tip_podkl` LIKE '%" + textTipPodKlava.Text + "%' OR `Tip_podkl` = '') and (`Kol_klavish` LIKE '%" + textCountKlavishKlava.Text + "%' OR `Kol_klavish` = '') and"
                    + "(`Status` LIKE '%" + textStatusKlava.Text + "%' OR `Status` = '') and (`Tsena` LIKE '%" + textTsenaKlava.Text + "%' OR `Tsena` = '') and "
                    + "(`Dop_Inf` LIKE '%" + textDopInfKlava.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditKlava.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Param_klava", queryFilterKlava, binSourceKlava, dataGridVKlava, NavigatorKlava);
        }

        //Кнопка "Очистка полей ввода", вкадка "Клавиатура"
        private void buttonClearKlava_Click(object sender, EventArgs e)
        {
            ClearTextBoxKlava();
        }

        //Кнопка "OK", вкадка "Клавиатура"
        private void buttonAddRecordKlava_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerKlava.Text.Equals("")) ||
                (textAddTipPodKlava.Text.Equals("")) || (textAddCountKlavishKlava.Text.Equals("")) ||
                (textAddStatusKlava.Text.Equals("")) || (textAddTsenaKlava.Text.Equals("")) ||
                (textAddAuditKlava.Text.Equals("")))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Param_klava`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_klava`");
                        try
                        {
                            ID_Param_Klava = Convert.ToInt32(ID);
                            ID_Param_Klava++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_Klava = 1;
                    }

                    string queryString = "INSERT INTO `Param_klava` VALUES (" + ID_Param_Klava + ",'" + textAddInvNomerKlava.Text +
                                        "','" + textAddTipPodKlava.Text + "','" + textAddCountKlavishKlava.Text +
                                        "','" + textAddStatusKlava.Text + "','" + textAddTsenaKlava.Text +
                                        "','" + textAddDopInfKlava.Text + "','" + textAddDataVvodaKlava.Text + 
                                        "','" + textAddDataSpisaniaKlava.Text + "','" + textAddAuditKlava.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_klava", "SELECT * FROM `Param_klava`", binSourceKlava, dataGridVKlava, NavigatorKlava);
                    ClearTextBoxKlava();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVKlava.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_Klava = Convert.ToString(dataGridVKlava[0, i].Value);
                    string queryString = "UPDATE `Param_klava` SET `Inv_Nomer` = '"
                        + textAddInvNomerKlava.Text + "', `Tip_podkl` = '"
                        + textAddTipPodKlava.Text + "', `Kol_klavish` = '"
                        + textAddCountKlavishKlava.Text + "', `Status` = '"
                        + textAddStatusKlava.Text + "', `Tsena` = '"
                        + textAddTsenaKlava.Text + "', `Dop_Inf` = '"
                        + textAddDopInfKlava.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaKlava.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaKlava.Text + "', `Auditoria` = "
                        + textAddAuditKlava.Text + " WHERE `ID` = " + id_Klava;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Param_klava", "SELECT * FROM `Param_klava`", binSourceKlava, dataGridVKlava, NavigatorKlava);
                    ClearTextBoxKlava();
                    panelKlavaAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Мониторы"
        private void buttonClosePanelKlava_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelKlavaAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxMouse();
        }   

        //Процедура очистки полей ввода
        public void ClearTextBoxKlava()
        {
            textAddInvNomerKlava.Clear();
            textAddTsenaKlava.Clear();
            textAddDopInfKlava.Clear();
        }

        //Кнопка "Сохранить данные", вкладка "Клавиатуры"
        private void buttonSaveRecordKlava_Click(object sender, EventArgs e)
        {
            if (SaveTableKlava == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Param_klava", "SELECT * FROM `Param_klava`", binSourceKlava, dataGridVKlava, NavigatorKlava);
                    SaveTableKlava = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(6, dataGridVKlava) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVKlava.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Param_klava`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Param_klava` VALUES (" + 1 + ",'" + dataGridVKlava[1, i].Value +
                                            "','" + dataGridVKlava[2, i].Value + "','" + dataGridVKlava[3, i].Value +
                                            "','" + dataGridVKlava[4, i].Value + "','" + dataGridVKlava[5, i].Value +
                                            "','" + dataGridVKlava[6, i].Value + "','" + dataGridVKlava[7, i].Value +
                                            "','" + dataGridVKlava[8, i].Value + "','" + dataGridVKlava[9, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Param_klava`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Param_klava` SET `Inv_Nomer` = '"
                            + dataGridVKlava[1, i].Value + "', `Tip_podkl` = '"
                            + dataGridVKlava[2, i].Value + "', `Kol_klavish` = '"
                            + dataGridVKlava[3, i].Value + "', `Status` = '"
                            + dataGridVKlava[4, i].Value + "', `Tsena` = '"
                            + dataGridVKlava[5, i].Value + "', `Dop_Inf` = '"
                            + dataGridVKlava[6, i].Value + "', `Data_vvoda` = '"
                            + dataGridVKlava[7, i].Value + "', `Data_spisania` = '"
                            + dataGridVKlava[8, i].Value + "', `Auditoria` = "
                            + dataGridVKlava[9, i].Value + " WHERE `ID` = " + dataGridVKlava[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Param_klava`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Param_klava` VALUES (" + int_maxID + ",'" + dataGridVKlava[1, i].Value +
                                            "','" + dataGridVKlava[2, i].Value + "','" + dataGridVKlava[3, i].Value +
                                            "','" + dataGridVKlava[4, i].Value + "','" + dataGridVKlava[5, i].Value +
                                            "','" + dataGridVKlava[6, i].Value + "','" + dataGridVKlava[7, i].Value +
                                            "','" + dataGridVKlava[8, i].Value + "','" + dataGridVKlava[9, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Кнопка "Удалить запись", вкладка "Клавиатура"
        private void buttonDeleteRecordKlava_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM Param_klava") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVKlava.CurrentRow.Index;
                    string ID = Convert.ToString(dataGridVKlava[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Param_klava` WHERE `ID` = " + ID);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceKlava.RemoveAt(i);
                    conMySQL.LoadTable("Param_klava", "SELECT * FROM Param_klava", binSourceKlava, dataGridVKlava, NavigatorKlava);
                }
            }
        }

        //Клик по таблице, вкладка "Клавиатуры"
        private void dataGridVKlava_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                //Для даты
                if ((e.ColumnIndex == 7) || (e.ColumnIndex == 8))
                {
                    int indexRow = dataGridVKlava.CurrentRow.Index;
                    int count = dataGridVKlava.RowCount;
                    int strong_Index = count - 9;
                    int Result = count - indexRow;

                    if (Result <= 9)
                    {
                        Rectangle curcell = dataGridVKlava.GetCellDisplayRectangle(e.ColumnIndex - 1, strong_Index, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcKlava.Location = p;
                        mcKlava.Visible = true;
                        mcKlava.Show();
                    }
                    else
                    {
                        Rectangle curcell = dataGridVKlava.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcKlava.Location = p;
                        mcKlava.Visible = true;
                        mcKlava.Show();
                    }
                }
                else
                {
                    mcKlava.Visible = false;
                }

                //Для статуса
                if (e.ColumnIndex == 4)
                {
                    Rectangle curcell1 = dataGridVKlava.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatusKlava.Location = p1;
                    comboBoxDataGridStatusKlava.Visible = true;
                    comboBoxDataGridStatusKlava.Show();
                }
                else
                {
                    comboBoxDataGridStatusKlava.Visible = false;
                }

                //Для типа подключения
                if (e.ColumnIndex == 2)
                {
                    Rectangle curcell3 = dataGridVKlava.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                    Point p3 = new Point(curcell3.Right, curcell3.Bottom);
                    comboBoxDataGridTypePodKlava.Location = p3;
                    comboBoxDataGridTypePodKlava.Visible = true;
                    comboBoxDataGridTypePodKlava.Show();
                }
                else
                {
                    comboBoxDataGridTypePodKlava.Visible = false;
                }
            }
        }

        //Происходит при выборе даты пользователем, вкладка "Клавиатуры"
        private void mcKlava_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVKlava.CurrentCell.Value = e.Start;
                mcKlava.Visible = false;
                dataGridVKlava.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии какой-либо клавишы пользователем, вкладка "Клавиатуры"
        private void mcKlava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcKlava.Hide();
            }
        }

        //Происходит при нажатии какой-либо клавишы пользователем, вкладка "Клавиатуры"
        private void comboBoxDataGridStatusKlava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridStatusKlava.Hide();
            }
        }

        //Происходит при выборе элемента комбо бокса, вкладка "Клавиатуры"
        private void comboBoxDataGridStatusKlava_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVKlava.CurrentCell.Value = comboBoxDataGridStatusKlava.Text;
                comboBoxDataGridStatusKlava.Visible = false;
                dataGridVKlava.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии какой-либо клавишы пользователем, вкладка "Клавиатуры"
        private void comboBoxDataGridTypePodKlava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                comboBoxDataGridTypePodKlava.Hide();
            }
        }

        //Происходит при выборе элемента комбо бокса, вкладка "Клавиатуры"
        private void comboBoxDataGridTypePodKlava_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVKlava.CurrentCell.Value = comboBoxDataGridTypePodKlava.Text;
                comboBoxDataGridTypePodKlava.Visible = false;
                dataGridVKlava.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Клавиатуры -------------------//

        //--------------------Вкладка Прочее -------------------//

        public bool SaveTableProchee = true;
        public int ID_Param_Prochee;

        //Процедура очистки полей ввода
        public void ClearTextBoxProchee()
        {
            textAddInvNomerProchee.Clear();
            textAddNazvanieProchee.Clear();
            textAddTsenaProchee.Clear();
            textAddDopInfProchee.Clear();
        }

        //Кнопка "Сохранить данные", вкладка "Прочее"
        private void buttonSaveRecordProchee_Click(object sender, EventArgs e)
        {
            if (SaveTableProchee == false)
            {
                if (MessageBox.Show("Прежде чем вносить какие либо изменения в таблицу, прогрузите все записи!\nПоказать все записи?", "Сообщение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conMySQL.LoadTable("Prochee", "SELECT * FROM Prochee", binSourceProchee, dataGridVProchee, NavigatorProchee);
                    SaveTableProchee = true;
                }
            }
            else
            {
                if (checkEmptyDataGridCell(5, dataGridVProchee) == true)
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVProchee.CurrentRow.Index;

                    if (conMySQL.QueryToBool("SELECT * FROM `Prochee`") == false)
                    {
                        string queryStringInsert = "INSERT INTO `Prochee` VALUES (" + 1 + ",'" + dataGridVProchee[1, i].Value +
                                            "','" + dataGridVProchee[2, i].Value + "','" + dataGridVProchee[3, i].Value +
                                            "','" + dataGridVProchee[4, i].Value + "','" + dataGridVProchee[5, i].Value +
                                            "','" + dataGridVProchee[6, i].Value + "','" + dataGridVProchee[7, i].Value + "','" + dataGridVProchee[8, i].Value + "')";

                        if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            i++;
                            MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        string Count = conMySQL.AgregateQueryToDataGrid("SELECT COUNT(*) FROM `Prochee`");

                        int chislo = Convert.ToInt32(Count);
                        chislo--;

                        if (i <= chislo)
                        {
                            string queryStringUpdate = "UPDATE `Prochee` SET `Inv_Nomer` = '"
                            + dataGridVProchee[1, i].Value + "', `Nazvanie` = '"
                            + dataGridVProchee[2, i].Value + "', `Status` = '"
                            + dataGridVProchee[3, i].Value + "', `Tsena` = '"
                            + dataGridVProchee[4, i].Value + "', `Dop_Inf` = '"
                            + dataGridVProchee[5, i].Value + "', `Data_vvoda` = '"
                            + dataGridVProchee[6, i].Value + "', `Data_spisania` = '"
                            + dataGridVProchee[7, i].Value + "', `Auditoria` = '"
                            + dataGridVProchee[8, i].Value + "' WHERE `ID` = " + dataGridVProchee[0, i].Value;

                            if (conMySQL.QueryToBool(queryStringUpdate) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не изменена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            string maxID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Prochee`");
                            int int_maxID = Convert.ToInt32(maxID);
                            int_maxID++;

                            string queryStringInsert = "INSERT INTO `Prochee` VALUES (" + int_maxID + ",'" + dataGridVProchee[1, i].Value +
                                            "','" + dataGridVProchee[2, i].Value + "','" + dataGridVProchee[3, i].Value +
                                            "','" + dataGridVProchee[4, i].Value + "','" + dataGridVProchee[5, i].Value +
                                            "','" + dataGridVProchee[6, i].Value + "','" + dataGridVProchee[7, i].Value + "','" + dataGridVProchee[8, i].Value + "')";

                            if (conMySQL.QueryToBool(queryStringInsert) == false & conMySQL.CheckException == false)
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " успешно сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                i++;
                                MessageBox.Show("Строка " + i + " не сохранена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ячейки таблицы\nСтрока: " + indexRow + ", ячейка: " + indexColumn, "Ошибка сохранения!",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }       
            }
        }

        //Кнопка "Удалить запись", вкладка "Прочее"
        private void buttonDeleteRecordProchee_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Prochee`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVProchee.CurrentRow.Index;
                    string id_Prochee = Convert.ToString(dataGridVProchee[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Prochee` WHERE `ID` = " + id_Prochee);
                    //Зачем здесь эта строка? Во славу Сатане конечно :3
                    binSourceProchee.RemoveAt(i);
                    conMySQL.LoadTable("Prochee", "SELECT * FROM `Prochee`", binSourceProchee, dataGridVProchee, NavigatorProchee);
                }
            }
        }
        
        //Кнопка "Поиск", вкладка "Прочее"
        private void buttonSearchProchee_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVProchee, textSearchProchee);
        }

        //Кнопка "Очистка результатов поиска", вкладка "Прочее"
        private void buttonClearSearchProchee_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVProchee);
        }

        //Кнопка "Печать", вкладка "Прочее"
        private void buttonPrintProchee_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVProchee);
        }

        //Кнопка "Крестик", вкладка "Прочее"
        private void buttonClosePanelProchee_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelProcheeAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxProchee();
        }

        //Кнопка "OK", вкладка "Прочее"
        private void buttonAddRecordProchee_Click(object sender, EventArgs e)
        {
            if ((textAddInvNomerProchee.Text.Equals("")) ||
                (textAddNazvanieProchee.Text.Equals("")) || (textAddStatusProchee.Text.Equals("")) ||
                (textAddTsenaProchee.Text.Equals("")))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    if (conMySQL.QueryToBool("SELECT * FROM `Prochee`") == true)
                    {
                        string ID = conMySQL.AgregateQueryToDataGrid("SELECT MAX(ID) FROM `Prochee`");
                        try
                        {
                            ID_Param_Prochee = Convert.ToInt32(ID);
                            ID_Param_Prochee++;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        ID_Param_Prochee = 1;
                    }

                    string queryString = "INSERT INTO `Prochee` VALUES (" + ID_Param_Prochee + ",'" + textAddInvNomerProchee.Text +
                                        "','" + textAddNazvanieProchee.Text + "','" + textAddStatusProchee.Text +
                                        "','" + textAddTsenaProchee.Text + "','" + textAddDopInfProchee.Text +
                                        "','" + textAddDataVvodaProchee.Text + "','" + textAddDataSpisaniaProchee.Text + "','" + textAddAuditProchee.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Prochee", "SELECT * FROM Prochee", binSourceProchee, dataGridVProchee, NavigatorProchee);
                    ClearTextBoxProchee();
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVProchee.CurrentRow.Index;
                    //Забор значения из 0 столбца i-тый строки
                    string id_Prochee = Convert.ToString(dataGridVProchee[0, i].Value);
                    string queryString = "UPDATE `Prochee` SET `Inv_Nomer` = '"
                        + textAddInvNomerProchee.Text + "', `Nazvanie` = '"
                        + textAddNazvanieProchee.Text + "', `Status` = '"
                        + textAddStatusProchee.Text + "', `Tsena` = '"
                        + textAddTsenaProchee.Text + "', `Dop_Inf` = '"
                        + textAddDopInfProchee.Text + "', `Data_vvoda` = '"
                        + textAddDataVvodaProchee.Text + "', `Data_spisania` = '"
                        + textAddDataSpisaniaProchee.Text + "', `Auditoria` = '"
                        + textAddAuditProchee.Text + "' WHERE `ID` = " + id_Prochee;
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Prochee", "SELECT * FROM Prochee", binSourceProchee, dataGridVProchee, NavigatorProchee);
                    ClearTextBoxProchee();
                    panelProcheeAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Кнопка "Очистка полей", вкладка "Прочее"
        private void buttonClearProchee_Click(object sender, EventArgs e)
        {
            ClearTextBoxProchee();
        }

        //Кнопка "Фильтрация", вкладка "Прочее"
        private void buttonFilterProchee_Click(object sender, EventArgs e)
        {
            string DataVvoda, DataSpisania;
            if (checkBoxDataVvodaProchee.Checked == true)
            { DataVvoda = textDataVvodaProchee.Text; }
            else
            { DataVvoda = String.Empty; }

            if (checkBoxDataSpisaniaProchee.Checked == true)
            { DataSpisania = textDataSpisaniaProchee.Text; }
            else
            {
                DataSpisania = String.Empty;
            }

            if (textInvNomerProchee.Text.Equals("") && textNazvanieProchee.Text.Equals("") &&
                textStatusProchee.Text.Equals("") && textTsenaProchee.Text.Equals("") &&
                textDopInfProchee.Text.Equals("") && textDopInfKlava.Text.Equals("") &&
                DataVvoda == String.Empty && DataSpisania == String.Empty && textAuditKlava.Text.Equals(""))
            {
                SaveTableProchee = true;
            }
            else
            {
                SaveTableProchee = false;
            }

            string queryFilterProchee = "SELECT * FROM `Prochee` WHERE (`Inv_nomer` LIKE '%" + textInvNomerProchee.Text + "%' OR `Inv_nomer` = '') and "
                    + "(`Nazvanie` LIKE '%" + textNazvanieProchee.Text + "%' OR `Nazvanie` = '') and (`Status` LIKE '%" + textStatusProchee.Text + 
                    "%' OR `Status` = '') and (`Tsena` LIKE '%" + textTsenaProchee.Text + "%' OR `Tsena` = '') and (`Dop_Inf` LIKE '%" + 
                    textDopInfProchee.Text + "%' OR `Dop_Inf` = '') and (`Data_vvoda` LIKE '%" + DataVvoda + "%' OR `Data_vvoda` = '') and "
                    + "(`Data_spisania` LIKE '%" + DataSpisania + "%' OR `Data_spisania` = '') and (`Auditoria` LIKE '%" + textAuditKlava.Text + "%' OR `Auditoria` = '')";

            conMySQL.LoadTable("Prochee", queryFilterProchee, binSourceProchee, dataGridVProchee, NavigatorProchee);
        }

        //Кнопка "Очистка полей" Фильтрация, вкладка "Прочее"
        private void buttonClearFilterProchee_Click(object sender, EventArgs e)
        {
            textAddInvNomerProchee.Clear();
            textAddNazvanieProchee.Clear();
            textAddTsenaProchee.Clear();
            textAddDopInfProchee.Clear();
        }

        //Кнопка "Показать всю таблицу", вкладка "Прочее"
        private void buttonShowTableProchee_Click(object sender, EventArgs e)
        {
            SaveTableProchee = true;
            conMySQL.LoadTable("Prochee", "SELECT * FROM `Prochee`", binSourceProchee, dataGridVProchee, NavigatorProchee);
        }

        //Скрытие элементов управления, вкладка "Прочее"
        private void splitContainerProchee_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainerProchee.Panel2.Width < 293)
            {
                splitContainerProchee.Panel2.Hide();
            }
            else
            {
                splitContainerProchee.Panel2.Show();
            }
        }

        //Дата ввода в эксплуатацию
        private void checkBoxDataVvodaProchee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataVvodaProchee.Checked == true)
            {
                textDataVvodaProchee.Visible = true;
            }
            else
            {
                textDataVvodaProchee.Visible = false;
            }
        }

        //Дата списания
        private void checkBoxDataSpisaniaProchee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDataSpisaniaProchee.Checked == true)
            {
                textDataSpisaniaProchee.Visible = true;
            }
            else
            {
                textDataSpisaniaProchee.Visible = false;
            }
        }

        //Клик по ячейке таблицы, вкладка "Прочее"
        private void dataGridVProchee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (admin_status == true)
            {
                if ((e.ColumnIndex == 6) || (e.ColumnIndex == 7))
                {
                    int indexRow = dataGridVProchee.CurrentRow.Index;
                    int count = dataGridVProchee.RowCount;
                    int Result = count - indexRow;
                    if (Result <= 9)
                    {
                        Rectangle curcell = dataGridVProchee.GetCellDisplayRectangle(e.ColumnIndex - 1, indexRow, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcProchee.Location = p;
                        mcProchee.Visible = true;
                        mcProchee.Show();
                    }
                    else
                    {
                        Rectangle curcell = dataGridVProchee.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, true);
                        Point p = new Point(curcell.Right, curcell.Bottom);
                        mcProchee.Location = p;
                        mcProchee.Visible = true;
                        mcProchee.Show();
                    }       
                }
                else
                {
                    mcProchee.Visible = false;
                }

                if (e.ColumnIndex == 3)
                {
                    Rectangle curcell1 = dataGridVProchee.GetCellDisplayRectangle(e.ColumnIndex-1, e.RowIndex, true);
                    Point p1 = new Point(curcell1.Right, curcell1.Bottom);
                    comboBoxDataGridStatus.Location = p1;
                    comboBoxDataGridStatus.Visible = true;
                    comboBoxDataGridStatus.Show();
                }
                else
                {
                    comboBoxDataGridStatus.Visible = false;
                }
            }            
        }

        //Происходит при выборе даты пользователем, вкладка "Прочее"
        private void mcProchee_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dataGridVProchee.CurrentCell.Value = e.Start;
                mcProchee.Visible = false;
                dataGridVProchee.Enabled = true;
            }
            catch { }
        }

        //Происходит при нажатии какой-либо клавишы пользователем, вкладка "Прочее"
        private void mcProchee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mcProchee.Hide();
            }
        }

        //Происходит при выборе элемента комбо бокса, вкладка "Прочее"
        private void comboBoxDataGridStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridVProchee.CurrentCell.Value = comboBoxDataGridStatus.Text;
                comboBoxDataGridStatus.Visible = false;
                dataGridVProchee.Enabled = true;
            }
            catch { }
        }

        //--------------------Конец Прочее -------------------//

        //--------------------Вкладка Аудитории -------------------//

        //Клик по таблице, вкладка "Аудитория"
        private void dataGridVAudit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Click_for_table(dataGridVAudit, pictureBoxAudit, "Audit");
        }

        //Смена изображения, вкладка "Аудитория"
        private void comboBoxImageAudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxImageAudit.SelectedIndex == 0)
            {
                Change_picture(dataGridVAudit, "Audit");                
            }
            else if (comboBoxImageAudit.SelectedIndex == 1)
            {
                Change_picture(dataGridVAudit, "Audit");                
            }
            else
            {
                Delete_Photo(dataGridVAudit, "Audit");                
            }
        }

        //Кнопка "Печать", вкладка "Аудитория"
        private void buttonPrintAudit_Click(object sender, EventArgs e)
        {
            ExportsTo.ExportToExcel(dataGridVAudit);
        }

        //Кнопка "Крестик", закрывает панель добавления, вкладка "Аудитория"
        private void buttonClosePanelAudit_Click(object sender, EventArgs e)
        {
            Check_Button = 2;
            panelAuditAdd.Visible = false;
            редактироватьЗаписьToolStripMenuItem.Enabled = true;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            ClearTextBoxAudit();
        }

        //Кнопка "Очистка полей ввода", вкладка "Аудитория"
        private void buttonClearAudit_Click(object sender, EventArgs e)
        {
            ClearTextBoxAudit();
        }

        //Кнопка "Открыть картинку в формате .BMP", вкладка "Аудитория"
        private void buttonAddPictureAudit_Click(object sender, EventArgs e)
        {
            string Name_File = textAddNomerAudit.Text;
            add_picture(Name_File, "Audit");
        }

        public string OldNameAudit;
        //Кнопка "OK", вкладка "Аудитория"
        private void buttonAddRecordAudit_Click(object sender, EventArgs e)
        {
            if ((textAddNomerAudit.Text.Equals("")) || (textAddOtvet.Text.Equals("")))
            {
                MessageBox.Show("Не все поля введены", "Ошибка!");
            }
            else
            {
                if (Check_Button == 0) //Была нажата кнопка "Добавить"
                {
                    string queryString = "INSERT INTO `Auditoria` VALUES ('" +
                        textAddNomerAudit.Text + "','" + textAddOtvet.Text + "')";

                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Auditoria", "SELECT * FROM `Auditoria`", binSourceAudit, dataGridVAudit, NavigatorAudit);
                    ClearTextBoxAudit();
                }
                else
                {

                    string queryString = "UPDATE `Auditoria` SET `Nomer` = '"
                        + textAddNomerAudit.Text + "', `Otvetstvenii` = '"
                        + textAddOtvet.Text + "' WHERE `Nomer` = '" + OldNameAudit + "'";
                    conMySQL.QueryToBool(queryString);
                    conMySQL.LoadTable("Auditoria", "SELECT * FROM `Auditoria`", binSourceAudit, dataGridVAudit, NavigatorAudit);
                    ClearTextBoxAudit();
                    panelAuditAdd.Visible = false;
                    добавитьЗаписьToolStripMenuItem.Enabled = true;
                }
            }
        }

        //Процедура очистки полей ввода
        public void ClearTextBoxAudit()
        {
            textAddNomerAudit.Clear();
            textAddOtvet.Clear();
        }        

        //Вкладка "Поиск по таблице", вкладка "Аудитория"
        private void buttonSearchAudit_Click(object sender, EventArgs e)
        {
            search_datagrid(dataGridVAudit, textSearchAudit);
        }

        //Вкладка "Очистка", вкладка "Аудитория"
        private void buttonClearSearchAudit_Click(object sender, EventArgs e)
        {
            clear_datagrid(dataGridVAudit);
        }

        //Кнопка "Удалить запись", вкладка "Аудитория"
        private void buttonDeleteRecordAudit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?",
                        "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conMySQL.QueryToBool("SELECT * FROM `Auditoria`") == false)
                {
                    MessageBox.Show("Все строки были удалены из базы", "Ошибка удаления!");
                }
                else
                {
                    //Определяем индекс выбранной строки
                    int i = dataGridVAudit.CurrentRow.Index;
                    string id_Audit = Convert.ToString(dataGridVAudit[0, i].Value);
                    //Удаление строки
                    conMySQL.QueryToBool("DELETE FROM `Auditoria` WHERE `Nomer` = '" + id_Audit + "'");
                    //Удаление картинки, привязанной к строке
                    Delete_Photo(dataGridVAudit, "Audit");
                    //Строка во славу Сатане...
                    binSourceAudit.RemoveAt(i);
                    conMySQL.LoadTable("Auditoria", "SELECT * FROM `Auditoria`", binSourceAudit, dataGridVAudit, NavigatorAudit);
                    //Очистка поля с изображением
                    pictureBoxAudit.Image = null;
                }
            }
        }

        //Кнопка "Первая запись" на навигаторе, вкладка "Аудитория"
        private void bindingNavigatorMoveFirstItem6_Click(object sender, EventArgs e)
        {
            Click_for_table(dataGridVAudit, pictureBoxAudit, "Audit");
        }

        //Кнопка "Предыдущая запись" на навигаторе, вкладка "Аудитория"
        private void bindingNavigatorMovePreviousItem6_Click(object sender, EventArgs e)
        {
            Click_for_table(dataGridVAudit, pictureBoxAudit, "Audit");
        }

        //Кнопка "Следующая запись" на навигаторе, вкладка "Аудитория"
        private void bindingNavigatorMoveNextItem6_Click(object sender, EventArgs e)
        {
            Click_for_table(dataGridVAudit, pictureBoxAudit, "Audit");
        }

        //Кнопка "Последняя запись" на навигаторе, вкладка "Аудитория"
        private void bindingNavigatorMoveLastItem6_Click(object sender, EventArgs e)
        {
            Click_for_table(dataGridVAudit, pictureBoxAudit, "Audit");
        }

        //--------------------Конец Аудитории -------------------//
    }
}