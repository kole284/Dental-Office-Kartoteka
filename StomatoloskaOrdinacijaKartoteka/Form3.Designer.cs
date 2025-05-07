using System.Drawing;
using System.Windows.Forms;

namespace DataBaseProgram
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.button2 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.button3 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxGodina = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxMesec = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comboBoxDan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.siticoneButton1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.siticoneButton2 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox3.Location = new System.Drawing.Point(61, 246);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(294, 27);
            this.textBox3.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label4.Location = new System.Drawing.Point(61, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Broj telefona";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label3.Location = new System.Drawing.Point(61, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Datum rođenja\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label2.Location = new System.Drawing.Point(61, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Prezime";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label1.Location = new System.Drawing.Point(61, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Ime";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox2.Location = new System.Drawing.Point(61, 127);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(294, 27);
            this.textBox2.TabIndex = 15;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox1.Location = new System.Drawing.Point(61, 68);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 27);
            this.textBox1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(61, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 67);
            this.button1.TabIndex = 22;
            this.button1.Text = "Obriši";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(61, 478);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 67);
            this.button2.TabIndex = 23;
            this.button2.Text = "Ažuriraj";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.button3.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(175, 405);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(181, 67);
            this.button3.TabIndex = 24;
            this.button3.Text = "Dodaj posjetu";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox4.Location = new System.Drawing.Point(61, 369);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(294, 30);
            this.textBox4.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label5.Location = new System.Drawing.Point(61, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 25);
            this.label5.TabIndex = 25;
            this.label5.Text = "Adresa";
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
            this.comboBoxGodina.Location = new System.Drawing.Point(254, 185);
            this.comboBoxGodina.Name = "comboBoxGodina";
            this.comboBoxGodina.Size = new System.Drawing.Size(101, 36);
            this.comboBoxGodina.TabIndex = 29;
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
            this.comboBoxMesec.Location = new System.Drawing.Point(158, 185);
            this.comboBoxMesec.Name = "comboBoxMesec";
            this.comboBoxMesec.Size = new System.Drawing.Size(101, 36);
            this.comboBoxMesec.TabIndex = 28;
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
            this.comboBoxDan.Location = new System.Drawing.Point(61, 185);
            this.comboBoxDan.Name = "comboBoxDan";
            this.comboBoxDan.Size = new System.Drawing.Size(101, 36);
            this.comboBoxDan.TabIndex = 27;
            // 
            // siticoneButton1
            // 
            this.siticoneButton1.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.siticoneButton1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.siticoneButton1.ForeColor = System.Drawing.Color.White;
            this.siticoneButton1.Location = new System.Drawing.Point(175, 478);
            this.siticoneButton1.Name = "siticoneButton1";
            this.siticoneButton1.Size = new System.Drawing.Size(181, 67);
            this.siticoneButton1.TabIndex = 30;
            this.siticoneButton1.Text = "Exportuj sve nalaze";
            this.siticoneButton1.Click += new System.EventHandler(this.siticoneButton1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.richTextBox1.Location = new System.Drawing.Point(383, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 524);
            this.richTextBox1.TabIndex = 31;
            this.richTextBox1.Text = "";
            // 
            // siticoneButton2
            // 
            this.siticoneButton2.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.siticoneButton2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.siticoneButton2.ForeColor = System.Drawing.Color.White;
            this.siticoneButton2.Location = new System.Drawing.Point(61, 551);
            this.siticoneButton2.Name = "siticoneButton2";
            this.siticoneButton2.Size = new System.Drawing.Size(295, 41);
            this.siticoneButton2.TabIndex = 32;
            this.siticoneButton2.Text = "Exportuj podatke pacijenta";
            this.siticoneButton2.Click += new System.EventHandler(this.siticoneButton2_Click);
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F);
            this.textBox5.Location = new System.Drawing.Point(61, 309);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(294, 30);
            this.textBox5.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.label6.Location = new System.Drawing.Point(61, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 25);
            this.label6.TabIndex = 33;
            this.label6.Text = "Grad";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1041, 627);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.siticoneButton2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.siticoneButton1);
            this.Controls.Add(this.comboBoxGodina);
            this.Controls.Add(this.comboBoxMesec);
            this.Controls.Add(this.comboBoxDan);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "STOMATOLOŠKA ORDINACIJA KARTOTEKA";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox3;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button2;
        private Siticone.Desktop.UI.WinForms.SiticoneButton button3;
        private TextBox textBox4;
        private Label label5;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxGodina;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxMesec;
        private Guna.UI2.WinForms.Guna2ComboBox comboBoxDan;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton1;
        private RichTextBox richTextBox1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton2;
        private TextBox textBox5;
        private Label label6;
    }
}