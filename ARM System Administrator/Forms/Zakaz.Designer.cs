namespace ARM_System_Administrator.Forms
{
    partial class Zakaz
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zakaz));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonWordGO = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textDopInf = new System.Windows.Forms.RichTextBox();
            this.dataGridVZakaz = new System.Windows.Forms.DataGridView();
            this.textFIO = new System.Windows.Forms.ComboBox();
            this.textDolgnost = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVZakaz)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите ваше ФИО:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Должность:";
            // 
            // buttonWordGO
            // 
            this.buttonWordGO.Location = new System.Drawing.Point(17, 331);
            this.buttonWordGO.Name = "buttonWordGO";
            this.buttonWordGO.Size = new System.Drawing.Size(262, 27);
            this.buttonWordGO.TabIndex = 9;
            this.buttonWordGO.Text = "Создать заявку";
            this.buttonWordGO.UseVisualStyleBackColor = true;
            this.buttonWordGO.Click += new System.EventHandler(this.buttonWordGO_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(14, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Описание:";
            // 
            // textDopInf
            // 
            this.textDopInf.Location = new System.Drawing.Point(17, 120);
            this.textDopInf.Name = "textDopInf";
            this.textDopInf.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textDopInf.Size = new System.Drawing.Size(262, 204);
            this.textDopInf.TabIndex = 8;
            this.textDopInf.Text = "";
            // 
            // dataGridVZakaz
            // 
            this.dataGridVZakaz.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.dataGridVZakaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridVZakaz.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridVZakaz.Location = new System.Drawing.Point(287, 29);
            this.dataGridVZakaz.Name = "dataGridVZakaz";
            this.dataGridVZakaz.Size = new System.Drawing.Size(315, 329);
            this.dataGridVZakaz.TabIndex = 10;
            // 
            // textFIO
            // 
            this.textFIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textFIO.FormattingEnabled = true;
            this.textFIO.Location = new System.Drawing.Point(17, 29);
            this.textFIO.Name = "textFIO";
            this.textFIO.Size = new System.Drawing.Size(262, 23);
            this.textFIO.TabIndex = 11;
            this.textFIO.SelectedIndexChanged += new System.EventHandler(this.textFIO_SelectedIndexChanged);
            // 
            // textDolgnost
            // 
            this.textDolgnost.Location = new System.Drawing.Point(17, 75);
            this.textDolgnost.Name = "textDolgnost";
            this.textDolgnost.ReadOnly = true;
            this.textDolgnost.Size = new System.Drawing.Size(262, 21);
            this.textDolgnost.TabIndex = 12;
            // 
            // Zakaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(616, 377);
            this.Controls.Add(this.textDolgnost);
            this.Controls.Add(this.textFIO);
            this.Controls.Add(this.dataGridVZakaz);
            this.Controls.Add(this.buttonWordGO);
            this.Controls.Add(this.textDopInf);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Zakaz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Оформить заказ";
            this.Load += new System.EventHandler(this.Zakaz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVZakaz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonWordGO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox textDopInf;
        private System.Windows.Forms.DataGridView dataGridVZakaz;
        private System.Windows.Forms.ComboBox textFIO;
        private System.Windows.Forms.TextBox textDolgnost;
    }
}