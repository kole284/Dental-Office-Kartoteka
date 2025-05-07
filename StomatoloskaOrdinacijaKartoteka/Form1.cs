using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Siticone.Desktop.UI.WinForms;
using StomatoloskaOrdinacijaKartoteka;

namespace DataBaseProgram
{

    public partial class Form1 : Form
    {
        string konekcioniString = Program.konekcioniString;
        OleDbConnection konekcija;
        private DataTable osobeTabela = new DataTable();
        private DataTable poseteTabela = new DataTable();
        private System.Windows.Forms.Timer searchTimer; 
        
        public Form1()
        {
            InitializeComponent();
            // Timer za odloženu pretragu
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 400; // milisekundi
            searchTimer.Tick += SearchTimer_Tick;

            this.WindowState = FormWindowState.Maximized;
            dataGridViewOsobe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOsobe.MultiSelect = false;
            dataGridViewOsobe.ReadOnly = true;
            dataGridViewPosete.ColumnHeadersDefaultCellStyle.Font = new Font("Bahnschrift", 9, FontStyle.Regular);
            // Konfiguracija dataGridViewPosete (nova kontrola umesto ListView2)
            dataGridViewPosete.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPosete.MultiSelect = false;
            dataGridViewPosete.ReadOnly = true;
            // Dodavanje događaja za selekciju reda
            dataGridViewPosete.CellDoubleClick += DataGridViewPosete_CellDoubleClick;
        }

        private void DataGridViewPosete_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string idPosete = dataGridViewPosete.Rows[e.RowIndex].Cells["IDPosete"].Value.ToString();
                Form5 formaPosete = new Form5(idPosete, this);
                formaPosete.ShowDialog();
            }
        }


        private void PostaviKolone()
        {
            if (poseteTabela.Columns.Count == 0)
            {
                poseteTabela.Columns.Add("IDPosete", typeof(int));
                poseteTabela.Columns.Add("DatumPosete", typeof(string));
                poseteTabela.Columns.Add("Usluga", typeof(string));
                poseteTabela.Columns.Add("Detaljno", typeof(string));
                poseteTabela.Columns.Add("Doktor", typeof(string));
            }

            // Povežemo DataGridView sa tabelom
            dataGridViewPosete.DataSource = poseteTabela;

            // Sakrijemo ID kolonu
            if (dataGridViewPosete.Columns.Contains("IDPosete"))
            {
                dataGridViewPosete.Columns["IDPosete"].Visible = false;
            }

            // Podešavamo širine kolona
            dataGridViewPosete.Columns["DatumPosete"].Width = 105;
            dataGridViewPosete.Columns["Usluga"].Width = 150;
            dataGridViewPosete.Columns["Detaljno"].Width = 150;
            dataGridViewPosete.Columns["Doktor"].Width = 136;

            // Podešavamo nazive kolona
            dataGridViewPosete.Columns["DatumPosete"].HeaderText = "Datum posjete";
            dataGridViewPosete.Columns["Detaljno"].HeaderText = "Opis";
        }

        private void FiltrirajOsobe(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                osobeTabela.DefaultView.RowFilter = "";
            }
            else
            {
                // Escape ' for OleDb filtering
                string escaped = filter.Replace("'", "''");
                osobeTabela.DefaultView.RowFilter = $"Ime LIKE '%{escaped}%' OR Prezime LIKE '%{escaped}%'";
            }

            // Ažuriramo redne brojeve nakon filtriranja
            for (int i = 0; i < osobeTabela.DefaultView.Count; i++)
            {
                osobeTabela.DefaultView[i]["RedniBroj"] = i + 1;
            }
        }

        private void DataGridViewOsobe_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewOsobe.SelectedRows.Count > 0)
            {
                DataGridViewRow red = dataGridViewOsobe.SelectedRows[0];
                string idOsobe = red.Cells["IDOsobe"].Value.ToString();

                // Prikazuje ime i prezime
                label1.Text = "Ime: " + red.Cells["Ime"].Value.ToString();
                label2.Text = "Prezime: " + red.Cells["Prezime"].Value.ToString();

                // Učitava datum rođenja sa baze
                string datumRodjenja = UcitajDatumRodjenja(idOsobe);
                label3.Text = "Datum rođenja: " + datumRodjenja;

                // Poziva metodu za učitavanje detalja
                UcitajDetaljeIzOsobe(idOsobe);

                // Prikazuje dugme
                button2.Visible = true;
            }
        }
        private string UcitajDatumRodjenja(string idOsobe)
        {
            string datumRodjenja = string.Empty;

            try
            {
                using (OleDbConnection lokalnaKonekcija = new OleDbConnection(konekcioniString))
                {
                    lokalnaKonekcija.Open();

                    string query = "SELECT DatumRodjenja FROM Osoba WHERE IDOsobe = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, lokalnaKonekcija))
                    {
                        cmd.Parameters.AddWithValue("?", idOsobe);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            datumRodjenja = Convert.ToDateTime(result).ToString("dd/MM/yyyy"); // Formatiraj datum kako želiš
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju datuma rođenja, proverite da li je dodat. " + ex.Message);
            }

            return datumRodjenja;
        }

        public void UcitajDetaljeIzOsobe(string idOsobe)
        {
            poseteTabela.Clear();

            try
            {
                using (OleDbConnection lokalnaKonekcija = new OleDbConnection(konekcioniString))
                {
                    lokalnaKonekcija.Open();

                    string posetaQuery = @"
                SELECT IDPosete, DatumPosete, Usluga, Detaljno, Doktor
                FROM Poseta 
                WHERE IDOsobe = @id
                ORDER BY DatumPosete DESC";

                    using (OleDbCommand cmdPoseta = new OleDbCommand(posetaQuery, lokalnaKonekcija))
                    {
                        cmdPoseta.Parameters.AddWithValue("@id", idOsobe);
                        using (OleDbDataReader reader = cmdPoseta.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataRow noviRed = poseteTabela.NewRow();
                                noviRed["IDPosete"] = reader["IDPosete"];

                                // Format datuma
                                DateTime datumPosete = Convert.ToDateTime(reader["DatumPosete"]);
                                noviRed["DatumPosete"] = datumPosete.ToShortDateString();

                                noviRed["Usluga"] = reader["Usluga"];

                                // Skraćivanje opisa ako je predugačak
                                string detaljno = reader["Detaljno"].ToString();
                                noviRed["Detaljno"] = detaljno.Length > 30 ? detaljno.Substring(0, 30) + "..." : detaljno;

                                noviRed["Doktor"] = reader["Doktor"];
                                poseteTabela.Rows.Add(noviRed);
                            }
                        }
                    }
                }
                dataGridViewPosete.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju poseta: " + ex.Message);
            }
        }

        public string GetSelectedIdOsobe()
        {
            if (dataGridViewOsobe.SelectedRows.Count > 0)
            {
                return dataGridViewOsobe.SelectedRows[0].Cells["IDOsobe"].Value.ToString();
            }
            return null;
        }
        public void UcitajSveOsobe()
        {
            osobeTabela.Clear();

            try
            {
                using (OleDbConnection lokalnaKonekcija = new OleDbConnection(konekcioniString))
                {
                    lokalnaKonekcija.Open();

                    // Zadržavamo IDOsobe u podacima, ali ga nećemo prikazivati direktno
                    string query = "SELECT IDOsobe, Ime, Prezime FROM Osoba ORDER BY IDOsobe";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, lokalnaKonekcija))
                    {
                        // Proverite da li postoji kolona RedniBroj pre učitavanja podataka
                        if (!osobeTabela.Columns.Contains("RedniBroj"))
                        {
                            DataColumn redniBrojKolona = new DataColumn("RedniBroj", typeof(int));
                            osobeTabela.Columns.Add(redniBrojKolona);
                        }

                        adapter.Fill(osobeTabela);

                        // Popunjavamo redne brojeve
                        for (int i = 0; i < osobeTabela.Rows.Count; i++)
                        {
                            osobeTabela.Rows[i]["RedniBroj"] = i + 1;
                        }

                        // Postavite da je DataSource null pre ponovnog povezivanja
                        dataGridViewOsobe.DataSource = null;
                        dataGridViewOsobe.DataSource = osobeTabela;

                        // Sakrivamo IDOsobe kolonu
                        if (dataGridViewOsobe.Columns.Contains("IDOsobe"))
                        {
                            dataGridViewOsobe.Columns["IDOsobe"].Visible = false;
                        }

                        // Postavljamo redosled kolona
                        dataGridViewOsobe.Columns["RedniBroj"].DisplayIndex = 0;
                        dataGridViewOsobe.Columns["Ime"].DisplayIndex = 1;
                        dataGridViewOsobe.Columns["Prezime"].DisplayIndex = 2;

                        // Postavljamo fiksnu širinu za RedniBroj kolonu (50 piksela)
                        dataGridViewOsobe.Columns["RedniBroj"].Width = 50;
                        dataGridViewOsobe.Columns["Ime"].Width = 120;
                        dataGridViewOsobe.Columns["Prezime"].Width = 112;

                        // Sprečavamo korisniku da menja širinu kolone
                        dataGridViewOsobe.Columns["RedniBroj"].Resizable = DataGridViewTriState.False;

                        // Opcionalno: Preimenovanje zaglavlja kolone
                        dataGridViewOsobe.Columns["RedniBroj"].HeaderText = " ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri učitavanju osoba: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 novaForma = new Form2(this);
            novaForma.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewOsobe.SelectedRows.Count == 0)
                return;

            string idOsobe = dataGridViewOsobe.SelectedRows[0].Cells["IDOsobe"].Value.ToString();

            Form3 formaZaPregled = new Form3(idOsobe, this);
            formaZaPregled.ShowDialog();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            konekcija = new OleDbConnection(konekcioniString);
            PostaviKolone();
            UcitajSveOsobe();
            textBoxPretraga.TextChanged += textBoxPretraga_TextChanged;
            dataGridViewOsobe.SelectionChanged += DataGridViewOsobe_SelectionChanged;
            button2.Visible = false;
            dataGridViewOsobe.ClearSelection();
        }
        private void textBoxPretraga_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ako je pritisnut Enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; // Sprečava dalje procesiranje Enter tastera

                // Pokreni pretragu sa trenutnim tekstom u TextBox-u
                FiltrirajOsobe(textBoxPretraga.Text.Trim());

                // Postavljamo ReadOnly svojstvo na false da bi korisnik mogao da koristi Backspace i Delete
                // Ali ćemo koristiti drugim načinom da blokiramo unos novih karaktera
                textBoxPretraga.Tag = "SearchLocked"; // Koristimo Tag da označimo stanje
            }
            else if (textBoxPretraga.Tag?.ToString() == "SearchLocked")
            {
                // Ako je u zaključanom režimu, ali nije pritisnut Backspace ili Delete
                if (e.KeyChar != (char)8 && e.KeyChar != (char)127)
                {
                    e.Handled = true; // Blokira sve osim Backspace i Delete
                }
            }
        }

        private void textBoxPretraga_TextChanged(object sender, EventArgs e)
        {
            // Kada se tekst izbriše, ponovo omogućava unos
            if (string.IsNullOrEmpty(textBoxPretraga.Text) && textBoxPretraga.Tag?.ToString() == "SearchLocked")
            {
                textBoxPretraga.Tag = null; // Vraćamo na normalan režim
            }

            // Resetuj tajmer za odloženu pretragu samo ako nismo u zaključanom režimu
            if (textBoxPretraga.Tag?.ToString() != "SearchLocked")
            {
                searchTimer.Stop();
                searchTimer.Start();
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            FiltrirajOsobe(textBoxPretraga.Text.Trim());
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kreiranje SaveFileDialog za izbor lokacije za čuvanje PDF fajla
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    Title = "Sačuvaj listu pacijenata kao PDF",
                    FileName = "Lista_Pacijenata.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Putanja do PDF fajla koji će biti kreiran
                    string putanja = saveFileDialog.FileName;

                    // Izvozimo u PDF
                    ExportToPdf(putanja);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom kreiranja PDF fajla: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToPdf(string putanjaFajla)
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
                        PdfFont italicFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

                        // Dodavanje naslova
                        document.Add(new Paragraph("LISTA PACIJENATA")
                            .SetFont(boldFont)
                            .SetFontSize(18)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetMarginBottom(10));

                        // Dodavanje datuma
                        document.Add(new Paragraph($"Datum: {DateTime.Now:dd.MM.yyyy}")
                            .SetFont(italicFont)
                            .SetFontSize(10)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetMarginBottom(20));

                        float[] sirine = new float[] { 15f, 42.5f, 42.5f };
                        Table tabela = new Table(UnitValue.CreatePercentArray(sirine))
                            .UseAllAvailableWidth();


                        // Dodavanje zaglavlja tabele
                        Cell rbCell = new Cell()
                            .Add(new Paragraph("Rbr.").SetFont(boldFont).SetFontSize(12))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBackgroundColor(new iText.Kernel.Colors.DeviceRgb(220, 220, 220));

                        Cell imeCell = new Cell()
                            .Add(new Paragraph("Ime").SetFont(boldFont).SetFontSize(12))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBackgroundColor(new iText.Kernel.Colors.DeviceRgb(220, 220, 220));

                        Cell prezimeCell = new Cell()
                            .Add(new Paragraph("Prezime").SetFont(boldFont).SetFontSize(12))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBackgroundColor(new iText.Kernel.Colors.DeviceRgb(220, 220, 220));

                        tabela.AddHeaderCell(rbCell);
                        tabela.AddHeaderCell(imeCell);
                        tabela.AddHeaderCell(prezimeCell);

                        // Učitaj podatke iz datagrida
                        DataView dv = osobeTabela.DefaultView;
                        // Dodajemo redove
                        for (int i = 0; i < dv.Count; i++)
                        {
                            // Redni broj
                            Cell redniBrojCell = new Cell()
                                .Add(new Paragraph((i + 1).ToString()).SetFont(regularFont).SetFontSize(10))
                                .SetTextAlignment(TextAlignment.CENTER);
                            tabela.AddCell(redniBrojCell);

                            // Ime
                            Cell imeValueCell = new Cell()
                                .Add(new Paragraph(dv[i]["Ime"].ToString()).SetFont(regularFont).SetFontSize(10))
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetPaddingLeft(5);
                            tabela.AddCell(imeValueCell);

                            // Prezime
                            Cell prezimeValueCell = new Cell()
                                .Add(new Paragraph(dv[i]["Prezime"].ToString()).SetFont(regularFont).SetFontSize(10))
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetPaddingLeft(5);
                            tabela.AddCell(prezimeValueCell);
                        }

                        // Dodajemo tabelu u dokument
                        document.Add(tabela);

                        // Dodajemo informacije o broju pacijenata na dnu stranice
                        document.Add(new Paragraph($"\nUkupan broj pacijenata: {dv.Count}")
                            .SetFont(regularFont)
                            .SetFontSize(10)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetMarginTop(10));
                    }
                }

                MessageBox.Show("Lista pacijenata je uspešno sačuvana kao PDF.", "Uspešno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(putanjaFajla);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Greška pri pristupu fajlu PDF: {ioEx.Message}\nProverite da li je fajl '{System.IO.Path.GetFileName(putanjaFajla)}' već otvoren.", "Greška Fajla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri kreiranju PDF-a: {ex.Message}", "Greška PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}

 