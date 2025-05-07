using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using StomatoloskaOrdinacijaKartoteka; // Assuming this namespace exists from your context
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock; // Assuming this is needed

namespace DataBaseProgram // Assuming this is your namespace
{
    public partial class Form2 : Form
    {
        string konekcioniString = Program.konekcioniString;
        private Form1 roditeljskaForma;

        public Form2(Form1 roditelj)
        {
            InitializeComponent();
            this.roditeljskaForma = roditelj;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            for (int i = 1; i <= 31; i++)
            {
                comboBoxDan.Items.Add(i.ToString("00"));
            }

            // Popuni mesece 01-12
            for (int i = 1; i <= 12; i++)
            {
                comboBoxMesec.Items.Add(i.ToString("00"));
            }

            // Popuni godine od 1900 do 2025
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                comboBoxGodina.Items.Add(i.ToString());
            }

            // Postavi podrazumevane vrednosti
            comboBoxDan.SelectedIndex = 0;
            comboBoxMesec.SelectedIndex = 0;
            comboBoxGodina.SelectedIndex = comboBoxGodina.Items.Count - 1;
        }

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            if (input.Length == 1)
            {
                return input.ToUpper();
            }
            // Ensure only the first letter is capitalized, rest are lower (optional, but common)
            // return char.ToUpper(input[0]) + input.Substring(1).ToLower(); 
            // Or just capitalize the first letter, leaving the rest as is:
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // --- Get input values ---
            string imeRaw = textBox1.Text.Trim();
            string prezimeRaw = textBox2.Text.Trim();
            string brojTelefona = textBox3.Text.Trim();
            string adresa = textBox4.Text.Trim();
            string grad = textBox5.Text.Trim();
            string dan = comboBoxDan.SelectedItem.ToString();
            string mesec = comboBoxMesec.SelectedItem.ToString();
            string godina = comboBoxGodina.SelectedItem.ToString();
            string detaljiPacijenta = richTextBox1.Text.Trim();
            string datumRodjenjaStr = $"{dan}/{mesec}/{godina}";
            DateTime datumRodjenja;
            if (!DateTime.TryParseExact(datumRodjenjaStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out datumRodjenja))
            {
                MessageBox.Show("Neispravan datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Capitalize Names ---
            string ime = CapitalizeFirstLetter(imeRaw);
            string prezime = CapitalizeFirstLetter(prezimeRaw);

            // --- Input Validation ---
            // Added adresa to the check
            if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prezime) /*|| string.IsNullOrWhiteSpace(brojTelefona) || string.IsNullOrWhiteSpace(adresa)*/)
            {
                // Updated error message
                MessageBox.Show("Sva polja (Ime, Prezime, Broj Telefona, Adresa) moraju biti popunjena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Optionally focus the first empty field
                if (string.IsNullOrWhiteSpace(ime)) textBox1.Focus();
                else if (string.IsNullOrWhiteSpace(prezime)) textBox2.Focus();
                else if (string.IsNullOrWhiteSpace(brojTelefona)) textBox3.Focus();
                else if (string.IsNullOrWhiteSpace(adresa)) textBox4.Focus();
                return;
            }

            if (ime.Any(char.IsDigit))
            {
                MessageBox.Show("Ime ne sme sadržati brojeve.", "Greška Unosa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (prezime.Any(char.IsDigit))
            {
                MessageBox.Show("Prezime ne sme sadržati brojeve.", "Greška Unosa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            /* Basic phone number validation (allows digits and optional leading '+', length 6-15)
            if (!System.Text.RegularExpressions.Regex.IsMatch(brojTelefona, @"^\+?\d{6,15}$"))
            {
                MessageBox.Show("Broj telefona mora sadržati samo cifre (opciono '+' na početku) i imati između 6 i 15 cifara.", "Greška Formata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }
            */

            // --- Database Interaction ---
            using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
            {
                try
                {
                    konekcija.Open();

                    string proveraQuery = "SELECT COUNT(*) FROM Osoba WHERE UCase(Ime) = UCase(@Ime) AND UCase(Prezime) = UCase(@Prezime) AND DatumRodjenja = @DatumRodjenja";
                    OleDbCommand proveraCmd = new OleDbCommand(proveraQuery, konekcija);
                    proveraCmd.Parameters.Add("@Ime", OleDbType.VarWChar).Value = ime;
                    proveraCmd.Parameters.Add("@Prezime", OleDbType.VarWChar).Value = prezime;
                    proveraCmd.Parameters.Add("@DatumRodjenja", OleDbType.VarWChar).Value = datumRodjenjaStr; 


                    int brojPostojecih = (int)proveraCmd.ExecuteScalar();

                    if (brojPostojecih > 0)
                    {
                        MessageBox.Show("Korisnik sa istim imenom, prezimenom i datumom rođenja već postoji u bazi.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Stop execution if duplicate found
                    }


                    string insertQuery = "INSERT INTO Osoba (Ime, Prezime, DatumRodjenja, BrojTelefona, Grad, Adresa, DetaljiPacijenta) VALUES (@Ime, @Prezime, @DatumRodjenja, @BrojTelefona, @Grad, @Adresa, @DetaljiPacijenta)";
                    OleDbCommand insertCmd = new OleDbCommand(insertQuery, konekcija);

                    // Add parameters (including the new address parameter)
                    insertCmd.Parameters.Add("@Ime", OleDbType.VarWChar).Value = ime;
                    insertCmd.Parameters.Add("@Prezime", OleDbType.VarWChar).Value = prezime;
                    insertCmd.Parameters.Add("@DatumRodjenja", OleDbType.VarWChar).Value = datumRodjenjaStr; // Match type used in check
                    insertCmd.Parameters.Add("@BrojTelefona", OleDbType.VarWChar).Value = brojTelefona;
                    insertCmd.Parameters.Add("@Grad", OleDbType.VarWChar).Value = grad;
                    insertCmd.Parameters.Add("@Adresa", OleDbType.VarWChar).Value = adresa; 
                    insertCmd.Parameters.Add("@DetaljiPacijenta", OleDbType.LongVarWChar).Value = detaljiPacijenta;

                    insertCmd.ExecuteNonQuery(); // Execute the insert command

                    MessageBox.Show("Korisnik je uspešno dodat.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the parent form's data grid and close this form
                    roditeljskaForma.UcitajSveOsobe();
                    this.Close();
                }
                catch (OleDbException oleEx) // Catch specific OleDb exceptions
                {
                    MessageBox.Show("Greška pri radu sa bazom podataka: " + oleEx.Message + "\nError Code: " + oleEx.ErrorCode, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Catch general exceptions
                {
                    MessageBox.Show("Došlo je do greške: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // No finally block needed as 'using' handles connection disposal
            }
        }
    }
}