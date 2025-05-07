using System.Drawing;
using System.Windows.Forms;

namespace DataBaseProgram
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.button2 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.button3 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.siticoneButton1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.comboBoxGodina = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxMesec = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxDan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label4.Location = new System.Drawing.Point(305, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Detaljnije";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label3.Location = new System.Drawing.Point(27, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "Doktor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label2.Location = new System.Drawing.Point(25, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Usluga";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label1.Location = new System.Drawing.Point(27, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Datum pojsete";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox3.Location = new System.Drawing.Point(25, 190);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(263, 30);
            this.textBox3.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox1.Location = new System.Drawing.Point(23, 129);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 30);
            this.textBox1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(23, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 29);
            this.button1.TabIndex = 16;
            this.button1.Text = "Obriši";
            // 
            // button2
            // 
            this.button2.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(23, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 29);
            this.button2.TabIndex = 17;
            this.button2.Text = "Ažuriraj posetu";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button3.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(158, 283);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 64);
            this.button3.TabIndex = 18;
            this.button3.Text = "Preuzmi PDF";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.richTextBox1.Location = new System.Drawing.Point(305, 36);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 482);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            // 
            // siticoneButton1
            // 
            this.siticoneButton1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.siticoneButton1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.siticoneButton1.ForeColor = System.Drawing.Color.White;
            this.siticoneButton1.Location = new System.Drawing.Point(23, 353);
            this.siticoneButton1.Name = "siticoneButton1";
            this.siticoneButton1.Size = new System.Drawing.Size(263, 47);
            this.siticoneButton1.TabIndex = 20;
            this.siticoneButton1.Text = "Zubna šema";
            this.siticoneButton1.Click += new System.EventHandler(this.siticoneButton1_Click);
            // 
            // comboBoxGodina
            // 
            this.comboBoxGodina.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxGodina.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxGodina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGodina.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxGodina.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxGodina.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxGodina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxGodina.IntegralHeight = false;
            this.comboBoxGodina.ItemHeight = 30;
            this.comboBoxGodina.Location = new System.Drawing.Point(190, 64);
            this.comboBoxGodina.Name = "comboBoxGodina";
            this.comboBoxGodina.Size = new System.Drawing.Size(98, 36);
            this.comboBoxGodina.TabIndex = 32;
            // 
            // comboBoxMesec
            // 
            this.comboBoxMesec.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxMesec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxMesec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMesec.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxMesec.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxMesec.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxMesec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxMesec.IntegralHeight = false;
            this.comboBoxMesec.ItemHeight = 30;
            this.comboBoxMesec.Location = new System.Drawing.Point(108, 64);
            this.comboBoxMesec.Name = "comboBoxMesec";
            this.comboBoxMesec.Size = new System.Drawing.Size(88, 36);
            this.comboBoxMesec.TabIndex = 31;
            // 
            // comboBoxDan
            // 
            this.comboBoxDan.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxDan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxDan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxDan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comboBoxDan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboBoxDan.IntegralHeight = false;
            this.comboBoxDan.ItemHeight = 30;
            this.comboBoxDan.Location = new System.Drawing.Point(25, 64);
            this.comboBoxDan.Name = "comboBoxDan";
            this.comboBoxDan.Size = new System.Drawing.Size(86, 36);
            this.comboBoxDan.TabIndex = 30;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(951, 544);
            this.Controls.Add(this.comboBoxGodina);
            this.Controls.Add(this.comboBoxMesec);
            this.Controls.Add(this.comboBoxDan);
            this.Controls.Add(this.siticoneButton1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form5";
            this.Text = "STOMATOLOŠKA ORDINACIJA KARTOTEKA";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox3;
        private TextBox textBox1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button2;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button3;
        private RichTextBox richTextBox1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton1;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxGodina;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxMesec;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxDan;
    }
}