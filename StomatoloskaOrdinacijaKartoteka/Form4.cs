using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;
using StomatoloskaOrdinacijaKartoteka;

namespace DataBaseProgram
{
    public partial class Form4 : Form
    {
        private readonly string konekcioniString = Program.konekcioniString;
        private readonly string idOsobe;
        private readonly Form1 roditeljskaForma;
        private string radjeniZubiShema;

        public Form4(string idOsobe, Form1 roditelj)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(idOsobe))
            {
                MessageBox.Show("Interna greška: ID osobe nije prosleđen.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => { this.Close(); };
                return;
            }
            if (!long.TryParse(idOsobe, out _))
            {
                MessageBox.Show($"Interna greška: Prosleđeni ID osobe '{idOsobe}' nije validan broj.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => { this.Close(); };
                return;
            }

            this.idOsobe = idOsobe;
            this.roditeljskaForma = roditelj;
            this.Load += Form4_Load;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (this.IsDisposed || this.Disposing) return;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            PopuniComboDate();
        }

        // Popunjava comboBoxDan, comboBoxMesec, comboBoxGodina
        private void PopuniComboDate()
        {
            comboBoxDan.Items.Clear();
            for (int d = 1; d <= 31; d++)
                comboBoxDan.Items.Add(d.ToString("D2"));

            comboBoxMesec.Items.Clear();
            for (int m = 1; m <= 12; m++)
                comboBoxMesec.Items.Add(m.ToString("D2"));

            comboBoxGodina.Items.Clear();
            int godinaSada = DateTime.Now.Year;
            for (int y = godinaSada; y >= godinaSada - 100; y--)
                comboBoxGodina.Items.Add(y.ToString());

            comboBoxDan.SelectedIndex = 0;
            comboBoxMesec.SelectedIndex = 0;
            comboBoxGodina.SelectedIndex = 0;
        }

        // Sastavlja DateTime iz comboboxova
        private DateTime SastaviDatumIzComboboxova()
        {
            string dan = comboBoxDan.SelectedItem.ToString();
            string mesec = comboBoxMesec.SelectedItem.ToString();
            string godina = comboBoxGodina.SelectedItem.ToString();
            return DateTime.ParseExact($"{dan}.{mesec}.{godina}", "dd.MM.yyyy", null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var usluga = textBox1.Text.Trim();
            var doktor = textBox3.Text.Trim();
            var detaljno = richTextBox1.Text;
            var datumPosete = SastaviDatumIzComboboxova();

            if (SacuvajPosetu(
                idOsobe,
                datumPosete,
                usluga,
                detaljno,
                doktor,
                radjeniZubiShema))
            {
                MessageBox.Show("Poseta je uspešno dodata!", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                roditeljskaForma?.UcitajDetaljeIzOsobe(idOsobe);
                Close();
            }
            else
            {
                MessageBox.Show("Problem u dodavanju!");
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idOsobe))
            {
                MessageBox.Show("Nije pronađen ID osobe.");
                return;
            }

            try
            {
                using (var zubnaSemaForm = new Form6(roditeljskaForma, idOsobe))
                {
                    DialogResult result = zubnaSemaForm.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        radjeniZubiShema = zubnaSemaForm.RadjeniZubiString;
                    }
                    this.ActiveControl = button1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri otvaranju šeme zuba: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Exception in siticoneButton1_Click: {ex}");
            }
        }

        private bool SacuvajPosetu(string idOsobeStr, DateTime datumPosete, string usluga, string detaljno, string doktor, string radjeniZubiShema)
        {
            int idOsobeInt;
            Debug.WriteLine($"Početak upisa posete za osobu ID: {idOsobeStr}");

            if (!int.TryParse(idOsobeStr, out idOsobeInt))
            {
                MessageBox.Show($"Interna greška: ID osobe '{idOsobeStr}' ne može biti konvertovan u broj pre upisa u bazu.", "Greška konverzije", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Greška pri konverziji IDOsobe: '{idOsobeStr}' u broj.");
                return false;
            }

            const int MAX_SCHEMA_LENGTH = 30000;
            if (radjeniZubiShema != null && radjeniZubiShema.Length > MAX_SCHEMA_LENGTH)
            {
                MessageBox.Show($"Greška: Podaci o šemi zuba su predugački ({radjeniZubiShema.Length} karaktera). Maksimalno dozvoljeno: {MAX_SCHEMA_LENGTH}.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radjeniZubiShema = radjeniZubiShema.Substring(0, MAX_SCHEMA_LENGTH);
            }

            string query = "INSERT INTO Poseta (IDOsobe, DatumPosete, Usluga, Detaljno, Doktor, RadjeniZubi) VALUES (@IDOsobe, @DatumPosete, @Usluga, @Detaljno, @Doktor, @RadjeniZubi)";
            Debug.WriteLine($"SQL upit: {query}");

            try
            {
                using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
                {
                    konekcija.Open();
                    using (OleDbCommand cmd = new OleDbCommand(query, konekcija))
                    {
                        cmd.Parameters.Add("@IDOsobe", OleDbType.Integer).Value = idOsobeInt;
                        cmd.Parameters.Add("@DatumPosete", OleDbType.Date).Value = datumPosete;
                        cmd.Parameters.Add("@Usluga", OleDbType.VarWChar, 255).Value = (object)usluga ?? DBNull.Value;

                        var detaljnoParam = new OleDbParameter("@Detaljno", OleDbType.LongVarWChar)
                        {
                            Value = string.IsNullOrEmpty(detaljno) ? DBNull.Value : (object)detaljno
                        };
                        cmd.Parameters.Add(detaljnoParam);

                        cmd.Parameters.Add("@Doktor", OleDbType.VarWChar, 255).Value = string.IsNullOrEmpty(doktor) ? (object)DBNull.Value : doktor;

                        var zubiParam = new OleDbParameter("@RadjeniZubi", OleDbType.LongVarWChar)
                        {
                            Value = string.IsNullOrEmpty(radjeniZubiShema) ? DBNull.Value : (object)radjeniZubiShema
                        };
                        cmd.Parameters.Add(zubiParam);

                        int result = cmd.ExecuteNonQuery();
                        Debug.WriteLine($"Broj pogođenih redova: {result}");
                        return result > 0;
                    }
                }
            }
            catch (OleDbException oleEx)
            {
                MessageBox.Show($"Greška pri upisu: {oleEx.Message}", "Greška baze podataka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"OleDb Error: {oleEx}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Opšta greška: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"General Error u SacuvajPosetu: {ex}");
                return false;
            }
        }
    }
}
