using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using Path = System.IO.Path; 
using StomatoloskaOrdinacijaKartoteka;
using System.Diagnostics; 

namespace DataBaseProgram
{
    public partial class Form5 : Form
    {
        string konekcioniString = Program.konekcioniString;
        private string idOsobe = "";
        private string idPosete;
        private Form1 roditeljskaForma;
        private string imeOsobe = "";
        private string prezimeOsobe = "";
        private string radjeniZubiPodaciIzBaze = "";

        public Form5(string idPosete, Form1 roditelj)
        {
            InitializeComponent();
            this.idPosete = idPosete;
            this.roditeljskaForma = roditelj;
            this.Load += Form5_Load;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            PopuniComboDate();
            UcitajPodatkePosete(); // Load data here
        }

        private void PopuniComboDate()
        {
            comboBoxDan.Items.Clear();
            for (int d = 1; d <= 31; d++)
                comboBoxDan.Items.Add(d.ToString("D2"));

            comboBoxMesec.Items.Clear();
            for (int m = 1; m <= 12; m++)
                comboBoxMesec.Items.Add(m.ToString("D2"));

            comboBoxGodina.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int y = currentYear; y >= currentYear - 100; y--)
                comboBoxGodina.Items.Add(y.ToString());

            // Set defaults
            comboBoxDan.SelectedIndex = 0;
            comboBoxMesec.SelectedIndex = 0;
            comboBoxGodina.SelectedIndex = 0;
        }

        private void UcitajPodatkePosete()
        {
            try
            {
                using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
                {
                    konekcija.Open();
                    string query = @"SELECT
                     Poseta.IDOsobe,
                     Poseta.DatumPosete,
                     Poseta.Usluga,
                     Poseta.Detaljno,
                     Poseta.Doktor,
                     Poseta.RadjeniZubi,
                     Osoba.Ime,
                     Osoba.Prezime
                 FROM Poseta
                 INNER JOIN Osoba ON Poseta.IDOsobe = Osoba.IDOsobe
                 WHERE Poseta.IDPosete = @id";

                    using (OleDbCommand cmd = new OleDbCommand(query, konekcija))
                    {
                        cmd.Parameters.Add("@id", OleDbType.Integer).Value = Convert.ToInt32(idPosete);

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idOsobe = reader["IDOsobe"].ToString();
                                DateTime dbDate = Convert.ToDateTime(reader["DatumPosete"]);
                                comboBoxDan.SelectedItem = dbDate.Day.ToString("D2");
                                comboBoxMesec.SelectedItem = dbDate.Month.ToString("D2");
                                comboBoxGodina.SelectedItem = dbDate.Year.ToString();

                                textBox1.Text = reader["Usluga"].ToString();
                                richTextBox1.Text = reader["Detaljno"].ToString();
                                textBox3.Text = reader["Doktor"].ToString();
                                imeOsobe = reader["Ime"].ToString();
                                prezimeOsobe = reader["Prezime"].ToString();
                                object radjeniZubiObj = reader["RadjeniZubi"]; 
                                radjeniZubiPodaciIzBaze = radjeniZubiObj != DBNull.Value ? radjeniZubiObj.ToString() : string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Nema podataka za ovu posetu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox1.Text = string.Empty;
                                richTextBox1.Text = string.Empty;
                                textBox3.Text = string.Empty;
                                radjeniZubiPodaciIzBaze = string.Empty;
                            }
                        }
                    }
                }
            }
            catch (FormatException fmtEx)
            {
                MessageBox.Show("Greška: ID posete nije u ispravnom formatu.\n" + fmtEx.Message, "Greška podataka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("RadjeniZubi"))
                {
                    MessageBox.Show("Greška pri učitavanju podataka posete: Kolona za šemu zuba ('RadjeniZubi') nije pronađena u bazi ili je pogrešno nazvana u kodu.\nProverite Access bazu i C# kod.\n\nDetalji: " + ex.Message, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Greška pri učitavanju podataka posete: " + ex.Message + "\n" + ex.StackTrace, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
        }

        private DateTime SastaviDatumIzComboboxova()
        {
            string day = comboBoxDan.SelectedItem.ToString();
            string month = comboBoxMesec.SelectedItem.ToString();
            string year = comboBoxGodina.SelectedItem.ToString();
            return DateTime.ParseExact($"{day}.{month}.{year}", "dd.MM.yyyy", null);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(radjeniZubiPodaciIzBaze))
            {
                MessageBox.Show("Za ovu posetu nisu zabeleženi podaci o šemi zuba u bazi.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (var formaZaPrikaz = new Form7(radjeniZubiPodaciIzBaze, idPosete, roditeljskaForma))
                {
                    if (formaZaPrikaz.ShowDialog(this) == DialogResult.OK)
                    {
                        Debug.WriteLine("Form5: Form7 je vratila OK, ponovo učitavam podatke posete...");
                        UcitajPodatkePosete();
                        Debug.WriteLine("Form5: Podaci posete ponovo učitani.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom otvaranja forme za prikaz/izmenu šeme: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Error creating/showing Form7 from Form5: {ex}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = SastaviDatumIzComboboxova();
            using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
            {
                try
                {
                    konekcija.Open();
                    string updateQuery = @"UPDATE Poseta
                                           SET DatumPosete = @datumPosete,
                                               Usluga = @usluga,
                                               Detaljno = @detaljno,
                                               Doktor = @doktor
                                           WHERE IDPosete = @idPosete";
                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, konekcija))
                    {
                        cmd.Parameters.Add("@datumPosete", OleDbType.Date).Value = selectedDate;
                        cmd.Parameters.Add("@usluga", OleDbType.VarWChar, 255).Value = (object)textBox1.Text ?? DBNull.Value;
                        cmd.Parameters.Add("@detaljno", OleDbType.LongVarWChar).Value = (object)richTextBox1.Text ?? DBNull.Value;
                        cmd.Parameters.Add("@doktor", OleDbType.VarWChar, 255).Value = (object)textBox3.Text ?? DBNull.Value;
                        cmd.Parameters.Add("@idPosete", OleDbType.Integer).Value = Convert.ToInt32(idPosete);

                        int redovaIzmenjeno = cmd.ExecuteNonQuery();
                        if (redovaIzmenjeno > 0)
                        {
                            MessageBox.Show("Podaci su uspešno ažurirani!", "Ažurirano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            roditeljskaForma?.UcitajDetaljeIzOsobe(idOsobe);
                        }
                        else
                        {
                            MessageBox.Show("Nije bilo moguće ažurirati podatke (poseta nije pronađena?).", "Neuspešno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri ažuriranju posete: " + ex.Message, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine($"Error updating visit {idPosete}: {ex}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = SastaviDatumIzComboboxova();
            string safeIme = string.Join("_", imeOsobe.Split(Path.GetInvalidFileNameChars()));
            string safePrezime = string.Join("_", prezimeOsobe.Split(Path.GetInvalidFileNameChars()));
            string safeDatum = selectedDate.ToString("yyyy-MM-dd");

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF fajlovi (*.pdf)|*.pdf";
            saveDialog.FileName = $"Izvestaj_{safeIme}_{safePrezime}_{safeDatum}.pdf";
            saveDialog.Title = "Sačuvaj PDF izveštaj";

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                ExportToPdf(saveDialog.FileName, selectedDate);
            }
        }

        private void ExportToPdf(string putanjaFajla, DateTime selectedDate)
        {
            try
            {
                using (FileStream fs = new FileStream(putanjaFajla, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    WriterProperties writerProps = new WriterProperties();
                    PdfWriter writer = new PdfWriter(fs, writerProps);
                    using (PdfDocument pdfDoc = new PdfDocument(writer))
                    using (Document document = new Document(pdfDoc, PageSize.A4))
                    {
                        document.SetMargins(50, 50, 50, 50);

                        PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                        PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                        document.Add(new Paragraph("NALAZ-UPUTNICA")
                            .SetFont(boldFont)
                            .SetFontSize(18)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetMarginBottom(20));

                        document.Add(new Paragraph($"Ime i prezime: {imeOsobe} {prezimeOsobe}")
                            .SetFont(regularFont).SetFontSize(12));
                        document.Add(new Paragraph($"Datum posete: {selectedDate:dd.MM.yyyy}")
                            .SetFont(regularFont).SetFontSize(12));
                        document.Add(new Paragraph($"Usluga: {textBox1.Text}")
                            .SetFont(regularFont).SetFontSize(12));
                        document.Add(new Paragraph($"Detaljno: {richTextBox1.Text}")
                            .SetFont(regularFont).SetFontSize(12));
                        document.Add(new Paragraph($"Doktor: {textBox3.Text}")
                            .SetFont(regularFont).SetFontSize(12));
                        if (!string.IsNullOrWhiteSpace(radjeniZubiPodaciIzBaze))
                        {
                            document.Add(new Paragraph("\n"));
                            document.Add(new Paragraph("Radjeni zubi:")
                               .SetFont(regularFont).SetFontSize(12));
                            document.Add(new Paragraph(radjeniZubiPodaciIzBaze)
                               .SetFont(regularFont).SetFontSize(10));
                        }
                    }
                }

                MessageBox.Show("PDF uspešno sačuvan!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Greška pri pristupu fajlu PDF: {ioEx.Message}\nProverite da li fajl '{Path.GetFileName(putanjaFajla)}' već otvoren.", "Greška Fajla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri kreiranju PDF-a: {ex.Message}\n\nTip greške: {ex.GetType().FullName}\n\nInnerException: {ex.InnerException?.Message}", "Greška PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"PDF Export Error: {ex}");
            }
        }
    }
}
