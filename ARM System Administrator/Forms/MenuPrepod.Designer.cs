namespace ARM_System_Administrator.Forms
{
    partial class MenuPrepod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrepod));
            this.buttonZayavka = new System.Windows.Forms.Button();
            this.buttonUchet = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureZayavka = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureZayavka)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonZayavka
            // 
            this.buttonZayavka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonZayavka.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonZayavka.ForeColor = System.Drawing.Color.Black;
            this.buttonZayavka.Location = new System.Drawing.Point(12, 140);
            this.buttonZayavka.Name = "buttonZayavka";
            this.buttonZayavka.Size = new System.Drawing.Size(174, 50);
            this.buttonZayavka.TabIndex = 0;
            this.buttonZayavka.Text = "Сделать заявку";
            this.buttonZayavka.UseVisualStyleBackColor = true;
            this.buttonZayavka.Click += new System.EventHandler(this.buttonZayavka_Click);
            // 
            // buttonUchet
            // 
            this.buttonUchet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonUchet.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonUchet.ForeColor = System.Drawing.Color.Black;
            this.buttonUchet.Location = new System.Drawing.Point(192, 140);
            this.buttonUchet.Name = "buttonUchet";
            this.buttonUchet.Size = new System.Drawing.Size(174, 50);
            this.buttonUchet.TabIndex = 1;
            this.buttonUchet.Text = "Проверить заявку";
            this.buttonUchet.UseVisualStyleBackColor = true;
            this.buttonUchet.Click += new System.EventHandler(this.buttonUchet_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(192, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 122);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureZayavka
            // 
            this.pictureZayavka.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureZayavka.BackgroundImage")));
            this.pictureZayavka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureZayavka.Location = new System.Drawing.Point(12, 12);
            this.pictureZayavka.Name = "pictureZayavka";
            this.pictureZayavka.Size = new System.Drawing.Size(174, 122);
            this.pictureZayavka.TabIndex = 3;
            this.pictureZayavka.TabStop = false;
            // 
            // MenuPrepod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(378, 201);
            this.Controls.Add(this.pictureZayavka);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonUchet);
            this.Controls.Add(this.buttonZayavka);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MenuPrepod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выберите желаемую функцию";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuPrepod_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureZayavka)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonZayavka;
        private System.Windows.Forms.Button buttonUchet;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureZayavka;
    }
}