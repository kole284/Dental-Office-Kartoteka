using System.Drawing;
using System.Windows.Forms;

namespace DataBaseProgram
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxPretraga = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.button2 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.dataGridViewOsobe = new System.Windows.Forms.DataGridView();
            this.dataGridViewPosete = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.siticoneButton1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOsobe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPosete)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPretraga
            // 
            this.textBoxPretraga.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPretraga.Location = new System.Drawing.Point(21, 29);
            this.textBoxPretraga.Multiline = true;
            this.textBoxPretraga.Name = "textBoxPretraga";
            this.textBoxPretraga.Size = new System.Drawing.Size(285, 29);
            this.textBoxPretraga.TabIndex = 1;
            this.textBoxPretraga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPretraga_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ime:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(494, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Prezime:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(902, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 59);
            this.button1.TabIndex = 5;
            this.button1.Text = "Dodaj pacijenta\n";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(902, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 51);
            this.button2.TabIndex = 6;
            this.button2.Text = "Prikaži detalje\n";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewOsobe
            // 
            this.dataGridViewOsobe.AllowUserToDeleteRows = false;
            this.dataGridViewOsobe.AllowUserToResizeColumns = false;
            this.dataGridViewOsobe.AllowUserToResizeRows = false;
            this.dataGridViewOsobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOsobe.ColumnHeadersVisible = false;
            this.dataGridViewOsobe.Location = new System.Drawing.Point(21, 65);
            this.dataGridViewOsobe.Name = "dataGridViewOsobe";
            this.dataGridViewOsobe.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bahnschrift Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewOsobe.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOsobe.Size = new System.Drawing.Size(285, 433);
            this.dataGridViewOsobe.TabIndex = 8;
            // 
            // dataGridViewPosete
            // 
            this.dataGridViewPosete.AllowUserToResizeColumns = false;
            this.dataGridViewPosete.AllowUserToResizeRows = false;
            this.dataGridViewPosete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPosete.Location = new System.Drawing.Point(333, 65);
            this.dataGridViewPosete.Name = "dataGridViewPosete";
            this.dataGridViewPosete.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bahnschrift Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPosete.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPosete.Size = new System.Drawing.Size(544, 433);
            this.dataGridViewPosete.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(339, 501);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Datum rodjenja:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // siticoneButton1
            // 
            this.siticoneButton1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.siticoneButton1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siticoneButton1.ForeColor = System.Drawing.Color.White;
            this.siticoneButton1.Location = new System.Drawing.Point(902, 152);
            this.siticoneButton1.Name = "siticoneButton1";
            this.siticoneButton1.Size = new System.Drawing.Size(121, 51);
            this.siticoneButton1.TabIndex = 11;
            this.siticoneButton1.Text = "Exportuj listu";
            this.siticoneButton1.Click += new System.EventHandler(this.siticoneButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1035, 551);
            this.Controls.Add(this.siticoneButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewPosete);
            this.Controls.Add(this.dataGridViewOsobe);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPretraga);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1051, 590);
            this.Name = "Form1";
            this.Text = "STOMATOLOŠKA ORDINACIJA KARTOTEKA";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOsobe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPosete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox textBoxPretraga;
        private Label label1;
        private Label label2;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button2;
        private DataGridView dataGridViewOsobe;
        private DataGridView dataGridViewPosete;
        private Label label3;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton1;
    }
}
