namespace ARM_System_Administrator.Forms
{
    partial class Sotrudnik
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sotrudnik));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.NavigatorSotr = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSaveRecord = new System.Windows.Forms.ToolStripButton();
            this.buttonDeleteRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textSearch = new System.Windows.Forms.ToolStripTextBox();
            this.buttonSearch = new System.Windows.Forms.ToolStripButton();
            this.buttonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.dataGridVSotr = new System.Windows.Forms.DataGridView();
            this.textDisPrepod = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.textOtchPrepod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textFamPrepod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textImyaPrepod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSotr = new System.Windows.Forms.Panel();
            this.buttonClosePanel = new System.Windows.Forms.Button();
            this.textAddDolgnSotr = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonClearSotr = new System.Windows.Forms.Button();
            this.buttonAddRecordSotr = new System.Windows.Forms.Button();
            this.textAddOtchSotr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textAddFamSotr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textAddImyaSotr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textAddIdSotr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonShowTable = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorSotr)).BeginInit();
            this.NavigatorSotr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVSotr)).BeginInit();
            this.panelSotr.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigatorSotr
            // 
            this.NavigatorSotr.AddNewItem = null;
            this.NavigatorSotr.CountItem = this.bindingNavigatorCountItem;
            this.NavigatorSotr.DeleteItem = null;
            this.NavigatorSotr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.buttonAdd,
            this.buttonEdit,
            this.toolStripSeparator1,
            this.buttonSaveRecord,
            this.buttonDeleteRecord,
            this.toolStripLabel1,
            this.textSearch,
            this.buttonSearch,
            this.buttonClear,
            this.toolStripSeparator2,
            this.buttonPrint});
            this.NavigatorSotr.Location = new System.Drawing.Point(0, 0);
            this.NavigatorSotr.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.NavigatorSotr.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.NavigatorSotr.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.NavigatorSotr.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.NavigatorSotr.Name = "NavigatorSotr";
            this.NavigatorSotr.PositionItem = this.bindingNavigatorPositionItem;
            this.NavigatorSotr.Size = new System.Drawing.Size(897, 25);
            this.NavigatorSotr.TabIndex = 0;
            this.NavigatorSotr.Text = "bindingNavigator1";
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
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(58, 23);
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
            // buttonAdd
            // 
            this.buttonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(23, 22);
            this.buttonAdd.Text = "Сохранить данные";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
            this.buttonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(23, 22);
            this.buttonEdit.Text = "Изменить данные";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonSaveRecord
            // 
            this.buttonSaveRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSaveRecord.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveRecord.Image")));
            this.buttonSaveRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSaveRecord.Name = "buttonSaveRecord";
            this.buttonSaveRecord.Size = new System.Drawing.Size(23, 22);
            this.buttonSaveRecord.Text = "Сохранить данные";
            this.buttonSaveRecord.Click += new System.EventHandler(this.buttonSaveRecord_Click);
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
            // textSearch
            // 
            this.textSearch.BackColor = System.Drawing.Color.CornflowerBlue;
            this.textSearch.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSearch.ForeColor = System.Drawing.Color.White;
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(100, 25);
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
            this.buttonClear.Text = "Очистить результаты поиска";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // dataGridVSotr
            // 
            this.dataGridVSotr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridVSotr.BackgroundColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridVSotr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVSotr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVSotr.Location = new System.Drawing.Point(0, 29);
            this.dataGridVSotr.Name = "dataGridVSotr";
            this.dataGridVSotr.Size = new System.Drawing.Size(648, 324);
            this.dataGridVSotr.TabIndex = 1;
            // 
            // textDisPrepod
            // 
            this.textDisPrepod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textDisPrepod.Location = new System.Drawing.Point(654, 238);
            this.textDisPrepod.Name = "textDisPrepod";
            this.textDisPrepod.ReadOnly = true;
            this.textDisPrepod.Size = new System.Drawing.Size(231, 21);
            this.textDisPrepod.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(651, 220);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(139, 15);
            this.label14.TabIndex = 94;
            this.label14.Text = "Должность сотрудника:";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFilter.Location = new System.Drawing.Point(654, 285);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(231, 27);
            this.buttonFilter.TabIndex = 13;
            this.buttonFilter.Text = "Фильтрация";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // textOtchPrepod
            // 
            this.textOtchPrepod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textOtchPrepod.Location = new System.Drawing.Point(654, 193);
            this.textOtchPrepod.MaxLength = 50;
            this.textOtchPrepod.Name = "textOtchPrepod";
            this.textOtchPrepod.Size = new System.Drawing.Size(231, 21);
            this.textOtchPrepod.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(651, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 15);
            this.label4.TabIndex = 83;
            this.label4.Text = "Отчество сотрудника:";
            // 
            // textFamPrepod
            // 
            this.textFamPrepod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textFamPrepod.Location = new System.Drawing.Point(654, 148);
            this.textFamPrepod.MaxLength = 50;
            this.textFamPrepod.Name = "textFamPrepod";
            this.textFamPrepod.Size = new System.Drawing.Size(231, 21);
            this.textFamPrepod.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(651, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 15);
            this.label3.TabIndex = 81;
            this.label3.Text = "Фамилия сотрудника:";
            // 
            // textImyaPrepod
            // 
            this.textImyaPrepod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textImyaPrepod.Location = new System.Drawing.Point(654, 103);
            this.textImyaPrepod.MaxLength = 50;
            this.textImyaPrepod.Name = "textImyaPrepod";
            this.textImyaPrepod.Size = new System.Drawing.Size(231, 21);
            this.textImyaPrepod.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(651, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 79;
            this.label2.Text = "Имя сотрудника:";
            // 
            // panelSotr
            // 
            this.panelSotr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSotr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSotr.Controls.Add(this.buttonClosePanel);
            this.panelSotr.Controls.Add(this.textAddDolgnSotr);
            this.panelSotr.Controls.Add(this.label13);
            this.panelSotr.Controls.Add(this.buttonClearSotr);
            this.panelSotr.Controls.Add(this.buttonAddRecordSotr);
            this.panelSotr.Controls.Add(this.textAddOtchSotr);
            this.panelSotr.Controls.Add(this.label8);
            this.panelSotr.Controls.Add(this.textAddFamSotr);
            this.panelSotr.Controls.Add(this.label9);
            this.panelSotr.Controls.Add(this.textAddImyaSotr);
            this.panelSotr.Controls.Add(this.label10);
            this.panelSotr.Controls.Add(this.textAddIdSotr);
            this.panelSotr.Controls.Add(this.label11);
            this.panelSotr.Controls.Add(this.label7);
            this.panelSotr.Location = new System.Drawing.Point(0, 29);
            this.panelSotr.Name = "panelSotr";
            this.panelSotr.Size = new System.Drawing.Size(648, 324);
            this.panelSotr.TabIndex = 96;
            this.panelSotr.Visible = false;
            // 
            // buttonClosePanel
            // 
            this.buttonClosePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClosePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClosePanel.BackgroundImage")));
            this.buttonClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClosePanel.Location = new System.Drawing.Point(614, 3);
            this.buttonClosePanel.Name = "buttonClosePanel";
            this.buttonClosePanel.Size = new System.Drawing.Size(27, 25);
            this.buttonClosePanel.TabIndex = 8;
            this.buttonClosePanel.UseVisualStyleBackColor = true;
            this.buttonClosePanel.Click += new System.EventHandler(this.buttonClosePanel_Click);
            // 
            // textAddDolgnSotr
            // 
            this.textAddDolgnSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textAddDolgnSotr.Location = new System.Drawing.Point(64, 162);
            this.textAddDolgnSotr.MaxLength = 50;
            this.textAddDolgnSotr.Name = "textAddDolgnSotr";
            this.textAddDolgnSotr.Size = new System.Drawing.Size(524, 21);
            this.textAddDolgnSotr.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(60, 144);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(158, 15);
            this.label13.TabIndex = 17;
            this.label13.Text = "Должность преподавателя:";
            // 
            // buttonClearSotr
            // 
            this.buttonClearSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonClearSotr.Location = new System.Drawing.Point(384, 203);
            this.buttonClearSotr.Name = "buttonClearSotr";
            this.buttonClearSotr.Size = new System.Drawing.Size(204, 27);
            this.buttonClearSotr.TabIndex = 6;
            this.buttonClearSotr.Text = "Очистка";
            this.buttonClearSotr.UseVisualStyleBackColor = true;
            this.buttonClearSotr.Click += new System.EventHandler(this.buttonClearSotr_Click);
            // 
            // buttonAddRecordSotr
            // 
            this.buttonAddRecordSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddRecordSotr.Location = new System.Drawing.Point(64, 203);
            this.buttonAddRecordSotr.Name = "buttonAddRecordSotr";
            this.buttonAddRecordSotr.Size = new System.Drawing.Size(204, 27);
            this.buttonAddRecordSotr.TabIndex = 7;
            this.buttonAddRecordSotr.Text = "OK";
            this.buttonAddRecordSotr.UseVisualStyleBackColor = true;
            this.buttonAddRecordSotr.Click += new System.EventHandler(this.buttonAddRecordSotr_Click);
            // 
            // textAddOtchSotr
            // 
            this.textAddOtchSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textAddOtchSotr.Location = new System.Drawing.Point(344, 117);
            this.textAddOtchSotr.MaxLength = 50;
            this.textAddOtchSotr.Name = "textAddOtchSotr";
            this.textAddOtchSotr.Size = new System.Drawing.Size(244, 21);
            this.textAddOtchSotr.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(340, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Отчество сотрудника:";
            // 
            // textAddFamSotr
            // 
            this.textAddFamSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textAddFamSotr.Location = new System.Drawing.Point(64, 118);
            this.textAddFamSotr.MaxLength = 50;
            this.textAddFamSotr.Name = "textAddFamSotr";
            this.textAddFamSotr.Size = new System.Drawing.Size(244, 21);
            this.textAddFamSotr.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(60, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "Фамилия сотрудника:";
            // 
            // textAddImyaSotr
            // 
            this.textAddImyaSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textAddImyaSotr.Location = new System.Drawing.Point(344, 72);
            this.textAddImyaSotr.MaxLength = 50;
            this.textAddImyaSotr.Name = "textAddImyaSotr";
            this.textAddImyaSotr.Size = new System.Drawing.Size(244, 21);
            this.textAddImyaSotr.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(340, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "Имя сотрудника:";
            // 
            // textAddIdSotr
            // 
            this.textAddIdSotr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textAddIdSotr.Location = new System.Drawing.Point(64, 72);
            this.textAddIdSotr.MaxLength = 50;
            this.textAddIdSotr.Name = "textAddIdSotr";
            this.textAddIdSotr.ReadOnly = true;
            this.textAddIdSotr.Size = new System.Drawing.Size(244, 21);
            this.textAddIdSotr.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(64, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Номер(Системный):";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(108, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(448, 33);
            this.label7.TabIndex = 3;
            this.label7.Text = "Добавление нового сотрудника";
            // 
            // buttonShowTable
            // 
            this.buttonShowTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowTable.Location = new System.Drawing.Point(654, 318);
            this.buttonShowTable.Name = "buttonShowTable";
            this.buttonShowTable.Size = new System.Drawing.Size(231, 27);
            this.buttonShowTable.TabIndex = 14;
            this.buttonShowTable.Text = "Показать всю таблицу";
            this.buttonShowTable.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(650, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(234, 22);
            this.label12.TabIndex = 98;
            this.label12.Text = "Критерий фильтрации:";
            // 
            // Sotrudnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(897, 353);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.buttonShowTable);
            this.Controls.Add(this.panelSotr);
            this.Controls.Add(this.textDisPrepod);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.textOtchPrepod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textFamPrepod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textImyaPrepod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridVSotr);
            this.Controls.Add(this.NavigatorSotr);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sotrudnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сотрудники";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Sotrudnik_FormClosed);
            this.Load += new System.EventHandler(this.Sotrudnik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NavigatorSotr)).EndInit();
            this.NavigatorSotr.ResumeLayout(false);
            this.NavigatorSotr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVSotr)).EndInit();
            this.panelSotr.ResumeLayout(false);
            this.panelSotr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator NavigatorSotr;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton buttonSaveRecord;
        private System.Windows.Forms.ToolStripButton buttonDeleteRecord;
        private System.Windows.Forms.DataGridView dataGridVSotr;
        private System.Windows.Forms.TextBox textDisPrepod;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.TextBox textOtchPrepod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textFamPrepod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textImyaPrepod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSotr;
        private System.Windows.Forms.Button buttonClosePanel;
        private System.Windows.Forms.TextBox textAddDolgnSotr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonClearSotr;
        private System.Windows.Forms.Button buttonAddRecordSotr;
        private System.Windows.Forms.TextBox textAddOtchSotr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textAddFamSotr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textAddImyaSotr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textAddIdSotr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.Button buttonShowTable;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox textSearch;
        private System.Windows.Forms.ToolStripButton buttonSearch;
        private System.Windows.Forms.ToolStripButton buttonClear;
        private System.Windows.Forms.ToolStripButton buttonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}