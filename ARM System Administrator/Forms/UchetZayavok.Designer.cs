namespace ARM_System_Administrator.Forms
{
    partial class UchetZayavok
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UchetZayavok));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.NavigatorUchet = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonDeleteRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textSearchUchet = new System.Windows.Forms.ToolStripTextBox();
            this.buttonSearch = new System.Windows.Forms.ToolStripButton();
            this.buttonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.dataGridVUchet = new System.Windows.Forms.DataGridView();
            this.textStatusZayavki = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textTselZayavki = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textInvNomer = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textNomerAudit = new System.Windows.Forms.TextBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxNewStatus = new System.Windows.Forms.ComboBox();
            this.textFIO = new System.Windows.Forms.ComboBox();
            this.textTipUser = new System.Windows.Forms.ComboBox();
            this.textTipOborud = new System.Windows.Forms.ComboBox();
            this.checkBoxDataZayavki = new System.Windows.Forms.CheckBox();
            this.checkBoxDataSdachi = new System.Windows.Forms.CheckBox();
            this.checkBoxFIO = new System.Windows.Forms.CheckBox();
            this.checkBoxTypeUser = new System.Windows.Forms.CheckBox();
            this.checkBoxTypeOborud = new System.Windows.Forms.CheckBox();
            this.textDataZayavki = new System.Windows.Forms.DateTimePicker();
            this.textDataSdachi = new System.Windows.Forms.DateTimePicker();
            this.buttonShowTable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorUchet)).BeginInit();
            this.NavigatorUchet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVUchet)).BeginInit();
            this.SuspendLayout();
            // 
            // NavigatorUchet
            // 
            this.NavigatorUchet.AddNewItem = null;
            this.NavigatorUchet.CountItem = this.bindingNavigatorCountItem;
            this.NavigatorUchet.DeleteItem = null;
            this.NavigatorUchet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.buttonDeleteRecord,
            this.toolStripLabel1,
            this.textSearchUchet,
            this.buttonSearch,
            this.buttonClear,
            this.toolStripSeparator1,
            this.buttonPrint});
            this.NavigatorUchet.Location = new System.Drawing.Point(0, 0);
            this.NavigatorUchet.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.NavigatorUchet.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.NavigatorUchet.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.NavigatorUchet.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.NavigatorUchet.Name = "NavigatorUchet";
            this.NavigatorUchet.PositionItem = this.bindingNavigatorPositionItem;
            this.NavigatorUchet.Size = new System.Drawing.Size(1026, 25);
            this.NavigatorUchet.TabIndex = 0;
            this.NavigatorUchet.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
            this.bindingNavigatorCountItem.Text = "для {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonDeleteRecord
            // 
            this.buttonDeleteRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDeleteRecord.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteRecord.Image")));
            this.buttonDeleteRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDeleteRecord.Name = "buttonDeleteRecord";
            this.buttonDeleteRecord.Size = new System.Drawing.Size(23, 22);
            this.buttonDeleteRecord.Text = "Удалить запись";
            this.buttonDeleteRecord.Click += new System.EventHandler(this.buttonDeleteRecord_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Поиск:";
            // 
            // textSearchUchet
            // 
            this.textSearchUchet.BackColor = System.Drawing.Color.CornflowerBlue;
            this.textSearchUchet.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSearchUchet.ForeColor = System.Drawing.Color.White;
            this.textSearchUchet.Name = "textSearchUchet";
            this.textSearchUchet.Size = new System.Drawing.Size(100, 25);
            // 
            // buttonSearch
            // 
            this.buttonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            this.buttonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(23, 22);
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(23, 22);
            this.buttonClear.Text = "Очистка результатов поиска";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonPrint
            // 
            this.buttonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(23, 22);
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // dataGridVUchet
            // 
            this.dataGridVUchet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVUchet.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.dataGridVUchet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridVUchet.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVUchet.Location = new System.Drawing.Point(0, 25);
            this.dataGridVUchet.Name = "dataGridVUchet";
            this.dataGridVUchet.Size = new System.Drawing.Size(774, 532);
            this.dataGridVUchet.TabIndex = 1;
            // 
            // textStatusZayavki
            // 
            this.textStatusZayavki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textStatusZayavki.Location = new System.Drawing.Point(780, 412);
            this.textStatusZayavki.MaxLength = 50;
            this.textStatusZayavki.Name = "textStatusZayavki";
            this.textStatusZayavki.Size = new System.Drawing.Size(230, 21);
            this.textStatusZayavki.TabIndex = 114;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(777, 394);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 15);
            this.label12.TabIndex = 42;
            this.label12.Text = "Статус заявки:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(777, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 15);
            this.label14.TabIndex = 40;
            this.label14.Text = "Дата сдачи:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(777, 149);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 15);
            this.label16.TabIndex = 38;
            this.label16.Text = "Дата заявки:";
            // 
            // textTselZayavki
            // 
            this.textTselZayavki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTselZayavki.Location = new System.Drawing.Point(780, 125);
            this.textTselZayavki.MaxLength = 50;
            this.textTselZayavki.Name = "textTselZayavki";
            this.textTselZayavki.Size = new System.Drawing.Size(230, 21);
            this.textTselZayavki.TabIndex = 102;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(777, 107);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 15);
            this.label19.TabIndex = 36;
            this.label19.Text = "Цель заявки:";
            // 
            // textInvNomer
            // 
            this.textInvNomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textInvNomer.Location = new System.Drawing.Point(780, 83);
            this.textInvNomer.MaxLength = 50;
            this.textInvNomer.Name = "textInvNomer";
            this.textInvNomer.Size = new System.Drawing.Size(230, 21);
            this.textInvNomer.TabIndex = 101;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(777, 231);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(112, 15);
            this.label20.TabIndex = 32;
            this.label20.Text = "Номер аудитории:";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(777, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 15);
            this.label21.TabIndex = 34;
            this.label21.Text = "Инв. номер:";
            // 
            // textNomerAudit
            // 
            this.textNomerAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textNomerAudit.Location = new System.Drawing.Point(780, 249);
            this.textNomerAudit.MaxLength = 50;
            this.textNomerAudit.Name = "textNomerAudit";
            this.textNomerAudit.Size = new System.Drawing.Size(230, 21);
            this.textNomerAudit.TabIndex = 107;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFilter.Location = new System.Drawing.Point(780, 439);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(230, 23);
            this.buttonFilter.TabIndex = 115;
            this.buttonFilter.Text = "Фильтрация данных";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(780, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(234, 22);
            this.label22.TabIndex = 84;
            this.label22.Text = "Критерий фильтрации:";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(777, 273);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(125, 15);
            this.label25.TabIndex = 87;
            this.label25.Text = "ФИО преподавателя:";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(777, 313);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(104, 15);
            this.label26.TabIndex = 89;
            this.label26.Text = "Тип пользователя:";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(777, 352);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(112, 15);
            this.label27.TabIndex = 91;
            this.label27.Text = "Тип оборудования:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(781, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 95;
            this.label2.Text = "Новый статус:";
            // 
            // comboBoxNewStatus
            // 
            this.comboBoxNewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNewStatus.FormattingEnabled = true;
            this.comboBoxNewStatus.Items.AddRange(new object[] {
            "В процессе",
            "В ожидании",
            "Готово",
            "Отказ"});
            this.comboBoxNewStatus.Location = new System.Drawing.Point(784, 522);
            this.comboBoxNewStatus.Name = "comboBoxNewStatus";
            this.comboBoxNewStatus.Size = new System.Drawing.Size(230, 23);
            this.comboBoxNewStatus.TabIndex = 116;
            this.comboBoxNewStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxNewStatus_SelectedIndexChanged);
            // 
            // textFIO
            // 
            this.textFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textFIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textFIO.Font = new System.Drawing.Font("Century Gothic", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFIO.FormattingEnabled = true;
            this.textFIO.Location = new System.Drawing.Point(833, 290);
            this.textFIO.Name = "textFIO";
            this.textFIO.Size = new System.Drawing.Size(177, 21);
            this.textFIO.TabIndex = 109;
            // 
            // textTipUser
            // 
            this.textTipUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTipUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textTipUser.Font = new System.Drawing.Font("Century Gothic", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTipUser.FormattingEnabled = true;
            this.textTipUser.Items.AddRange(new object[] {
            "Администратор",
            "Преподаватель"});
            this.textTipUser.Location = new System.Drawing.Point(833, 329);
            this.textTipUser.Name = "textTipUser";
            this.textTipUser.Size = new System.Drawing.Size(177, 21);
            this.textTipUser.TabIndex = 111;
            // 
            // textTipOborud
            // 
            this.textTipOborud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTipOborud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textTipOborud.Font = new System.Drawing.Font("Century Gothic", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTipOborud.FormattingEnabled = true;
            this.textTipOborud.Items.AddRange(new object[] {
            "Компьютер",
            "Ноутбук",
            "Принтер",
            "Мышь",
            "Монитор",
            "Клавиатура"});
            this.textTipOborud.Location = new System.Drawing.Point(833, 370);
            this.textTipOborud.Name = "textTipOborud";
            this.textTipOborud.Size = new System.Drawing.Size(177, 21);
            this.textTipOborud.TabIndex = 113;
            // 
            // checkBoxDataZayavki
            // 
            this.checkBoxDataZayavki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDataZayavki.AutoSize = true;
            this.checkBoxDataZayavki.ForeColor = System.Drawing.Color.White;
            this.checkBoxDataZayavki.Location = new System.Drawing.Point(780, 169);
            this.checkBoxDataZayavki.Name = "checkBoxDataZayavki";
            this.checkBoxDataZayavki.Size = new System.Drawing.Size(47, 19);
            this.checkBoxDataZayavki.TabIndex = 103;
            this.checkBoxDataZayavki.Text = "Вкл.";
            this.checkBoxDataZayavki.UseVisualStyleBackColor = true;
            // 
            // checkBoxDataSdachi
            // 
            this.checkBoxDataSdachi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDataSdachi.AutoSize = true;
            this.checkBoxDataSdachi.ForeColor = System.Drawing.Color.White;
            this.checkBoxDataSdachi.Location = new System.Drawing.Point(780, 209);
            this.checkBoxDataSdachi.Name = "checkBoxDataSdachi";
            this.checkBoxDataSdachi.Size = new System.Drawing.Size(47, 19);
            this.checkBoxDataSdachi.TabIndex = 105;
            this.checkBoxDataSdachi.Text = "Вкл.";
            this.checkBoxDataSdachi.UseVisualStyleBackColor = true;
            // 
            // checkBoxFIO
            // 
            this.checkBoxFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFIO.AutoSize = true;
            this.checkBoxFIO.ForeColor = System.Drawing.Color.White;
            this.checkBoxFIO.Location = new System.Drawing.Point(780, 291);
            this.checkBoxFIO.Name = "checkBoxFIO";
            this.checkBoxFIO.Size = new System.Drawing.Size(47, 19);
            this.checkBoxFIO.TabIndex = 108;
            this.checkBoxFIO.Text = "Вкл.";
            this.checkBoxFIO.UseVisualStyleBackColor = true;
            // 
            // checkBoxTypeUser
            // 
            this.checkBoxTypeUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTypeUser.AutoSize = true;
            this.checkBoxTypeUser.ForeColor = System.Drawing.Color.White;
            this.checkBoxTypeUser.Location = new System.Drawing.Point(780, 331);
            this.checkBoxTypeUser.Name = "checkBoxTypeUser";
            this.checkBoxTypeUser.Size = new System.Drawing.Size(47, 19);
            this.checkBoxTypeUser.TabIndex = 110;
            this.checkBoxTypeUser.Text = "Вкл.";
            this.checkBoxTypeUser.UseVisualStyleBackColor = true;
            // 
            // checkBoxTypeOborud
            // 
            this.checkBoxTypeOborud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTypeOborud.AutoSize = true;
            this.checkBoxTypeOborud.ForeColor = System.Drawing.Color.White;
            this.checkBoxTypeOborud.Location = new System.Drawing.Point(780, 372);
            this.checkBoxTypeOborud.Name = "checkBoxTypeOborud";
            this.checkBoxTypeOborud.Size = new System.Drawing.Size(47, 19);
            this.checkBoxTypeOborud.TabIndex = 112;
            this.checkBoxTypeOborud.Text = "Вкл.";
            this.checkBoxTypeOborud.UseVisualStyleBackColor = true;
            // 
            // textDataZayavki
            // 
            this.textDataZayavki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textDataZayavki.Location = new System.Drawing.Point(833, 165);
            this.textDataZayavki.Name = "textDataZayavki";
            this.textDataZayavki.Size = new System.Drawing.Size(177, 21);
            this.textDataZayavki.TabIndex = 104;
            // 
            // textDataSdachi
            // 
            this.textDataSdachi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textDataSdachi.Location = new System.Drawing.Point(833, 207);
            this.textDataSdachi.Name = "textDataSdachi";
            this.textDataSdachi.Size = new System.Drawing.Size(177, 21);
            this.textDataSdachi.TabIndex = 106;
            // 
            // buttonShowTable
            // 
            this.buttonShowTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowTable.Location = new System.Drawing.Point(780, 468);
            this.buttonShowTable.Name = "buttonShowTable";
            this.buttonShowTable.Size = new System.Drawing.Size(230, 23);
            this.buttonShowTable.TabIndex = 117;
            this.buttonShowTable.Text = "Показать все записи";
            this.buttonShowTable.UseVisualStyleBackColor = true;
            this.buttonShowTable.Click += new System.EventHandler(this.buttonShowTable_Click);
            // 
            // UchetZayavok
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1026, 557);
            this.Controls.Add(this.buttonShowTable);
            this.Controls.Add(this.textDataSdachi);
            this.Controls.Add(this.textDataZayavki);
            this.Controls.Add(this.checkBoxTypeOborud);
            this.Controls.Add(this.checkBoxTypeUser);
            this.Controls.Add(this.checkBoxFIO);
            this.Controls.Add(this.checkBoxDataSdachi);
            this.Controls.Add(this.checkBoxDataZayavki);
            this.Controls.Add(this.textTipOborud);
            this.Controls.Add(this.textTipUser);
            this.Controls.Add(this.textFIO);
            this.Controls.Add(this.comboBoxNewStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.textStatusZayavki);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textTselZayavki);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textInvNomer);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textNomerAudit);
            this.Controls.Add(this.dataGridVUchet);
            this.Controls.Add(this.NavigatorUchet);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UchetZayavok";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учет заявок";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UchetZayavok_FormClosing);
            this.Load += new System.EventHandler(this.UchetZayavok_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorUchet)).EndInit();
            this.NavigatorUchet.ResumeLayout(false);
            this.NavigatorUchet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVUchet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator NavigatorUchet;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView dataGridVUchet;
        private System.Windows.Forms.ToolStripButton buttonDeleteRecord;
        private System.Windows.Forms.TextBox textStatusZayavki;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textTselZayavki;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textInvNomer;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textNomerAudit;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxNewStatus;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox textSearchUchet;
        private System.Windows.Forms.ToolStripButton buttonSearch;
        private System.Windows.Forms.ToolStripButton buttonClear;
        private System.Windows.Forms.ToolStripButton buttonPrint;
        private System.Windows.Forms.ComboBox textFIO;
        private System.Windows.Forms.ComboBox textTipUser;
        private System.Windows.Forms.ComboBox textTipOborud;
        private System.Windows.Forms.CheckBox checkBoxDataZayavki;
        private System.Windows.Forms.CheckBox checkBoxDataSdachi;
        private System.Windows.Forms.CheckBox checkBoxFIO;
        private System.Windows.Forms.CheckBox checkBoxTypeUser;
        private System.Windows.Forms.CheckBox checkBoxTypeOborud;
        private System.Windows.Forms.DateTimePicker textDataZayavki;
        private System.Windows.Forms.DateTimePicker textDataSdachi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button buttonShowTable;
    }
}