using System.Drawing;
using System.Windows.Forms;

namespace DataBaseProgram
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxMesec = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxGodina = new Guna.UI2.WinForms.Guna2ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox1.Location = new System.Drawing.Point(24, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(293, 30);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox2.Location = new System.Drawing.Point(24, 129);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(293, 30);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(29, 437);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 56);
            this.button1.TabIndex = 6;
            this.button1.Text = "Dodaj pacijenta";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label1.Location = new System.Drawing.Point(24, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ime";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label2.Location = new System.Drawing.Point(24, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Prezime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label3.Location = new System.Drawing.Point(24, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Datum rođenja\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label4.Location = new System.Drawing.Point(24, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Broj telefona";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox3.Location = new System.Drawing.Point(24, 248);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(293, 30);
            this.textBox3.TabIndex = 13;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox4.Location = new System.Drawing.Point(24, 376);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(293, 30);
            this.textBox4.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label5.Location = new System.Drawing.Point(24, 349);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Adresa";
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
            this.comboBoxDan.Location = new System.Drawing.Point(24, 190);
            this.comboBoxDan.Name = "comboBoxDan";
            this.comboBoxDan.Size = new System.Drawing.Size(101, 36);
            this.comboBoxDan.TabIndex = 16;
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
            this.comboBoxMesec.Location = new System.Drawing.Point(121, 190);
            this.comboBoxMesec.Name = "comboBoxMesec";
            this.comboBoxMesec.Size = new System.Drawing.Size(101, 36);
            this.comboBoxMesec.TabIndex = 17;
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
            this.comboBoxGodina.Location = new System.Drawing.Point(216, 190);
            this.comboBoxGodina.Name = "comboBoxGodina";
            this.comboBoxGodina.Size = new System.Drawing.Size(101, 36);
            this.comboBoxGodina.TabIndex = 18;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.richTextBox1.Location = new System.Drawing.Point(334, 70);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(625, 355);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox5.Location = new System.Drawing.Point(24, 305);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(293, 30);
            this.textBox5.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label6.Location = new System.Drawing.Point(24, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 25);
            this.label6.TabIndex = 21;
            this.label6.Text = "Grad";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(989, 510);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBoxGodina);
            this.Controls.Add(this.comboBoxMesec);
            this.Controls.Add(this.comboBoxDan);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "STOMATOLOŠKA ORDINACIJA KARTOTEKA";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label5;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxDan;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxMesec;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxGodina;
        private RichTextBox richTextBox1;
        private TextBox textBox5;
        private Label label6;
    }
}