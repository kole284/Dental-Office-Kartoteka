using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq; // Added for validation check
using System.Text;
using System.Windows.Forms;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;

// Removed using static System.Windows.Forms.VisualStyles.VisualStyleElement; - Likely unused based on Form2 code
using StomatoloskaOrdinacijaKartoteka; // Assuming this namespace exists

namespace DataBaseProgram // Assuming this is your namespace
{
    public partial class Form3 : Form
    {
        string konekcioniString = Program.konekcioniString;
        private string idOsobe;
        private Form1 roditeljskaForma;

        // Assuming you have textBox4 added in the designer for Address
        // If not, add it:
        // private System.Windows.Forms.TextBox textBox4;
        // And initialize it in InitializeComponent()

        // Assuming you need to add a RichTextBox for DetaljiPacijenta
        // Add this to your Designer.cs:
        // private System.Windows.Forms.RichTextBox richTextBoxDetalji;
        // And initialize it in InitializeComponent()

        public Form3(string id, Form1 roditelj)
        {
            InitializeComponent();
            this.idOsobe = id;
            this.roditeljskaForma = roditelj;
            // No need to add Load event handler here, it's usually done in the Designer.cs
            // this.Load += Form3_Load;
        }

        private void UcitajPodatkeOsobe()
        {
            // Proverite da li postoji textBox4 pre nego što pokušate da je koristite
            if (this.Controls.Find("textBox4", true).FirstOrDefault() == null)
            {
                MessageBox.Show("Greška: Kontrola textBox4 za adresu nije pronađena na formi.", "Greška Dizajna", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Prestanite sa učitavanjem ako UI element nije pronađen
            }

            // Proverite da li postoji textBox5 za grad
            if (this.Controls.Find("textBox5", true).FirstOrDefault() == null)
            {
                MessageBox.Show("Greška: Kontrola textBox5 za grad nije pronađena na formi.", "Greška Dizajna", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Proverite da li postoji richTextBoxDetalji
            if (this.Controls.Find("richTextBox1", true).FirstOrDefault() == null)
            {
                MessageBox.Show("Greška: Kontrola richTextBoxDetalji za detalje pacijenta nije pronađena na formi.", "Greška Dizajna", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 1; i <= 31; i++)
            {
                comboBoxDan.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 12; i++)
            {
                comboBoxMesec.Items.Add(i.ToString());
            }
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                comboBoxGodina.Items.Add(i.ToString());
            }

            using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
            {
                try
                {
                    konekcija.Open();
                    // Upit koji se koristi za učitavanje podataka o korisniku - dodat Grad
                    string query = "SELECT Ime, Prezime, BrojTelefona, DatumRodjenja, Adresa, Grad, DetaljiPacijenta FROM Osoba WHERE IDOsobe = @id";
                    OleDbCommand cmd = new OleDbCommand(query, konekcija);
                    cmd.Parameters.Add("@id", OleDbType.VarWChar).Value = idOsobe;

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Učitavanje osnovnih podataka
                            textBox1.Text = reader["Ime"]?.ToString() ?? "";
                            textBox2.Text = reader["Prezime"]?.ToString() ?? "";
                            textBox3.Text = reader["BrojTelefona"]?.ToString() ?? "";

                            // Učitavanje datuma rođenja
                            object datumObj = reader["DatumRodjenja"];
                            if (datumObj != null && datumObj != DBNull.Value && DateTime.TryParse(datumObj.ToString(), out DateTime datum))
                            {
                                // Postavljanje datuma u ComboBox-e
                                comboBoxDan.SelectedItem = datum.Day.ToString();
                                comboBoxMesec.SelectedItem = datum.Month.ToString();
                                comboBoxGodina.SelectedItem = datum.Year.ToString();
                            }
                            else
                            {
                                // Ako datum nije validan, postavite trenutni datum kao podrazumevani
                                comboBoxDan.SelectedItem = DateTime.Today.Day.ToString();
                                comboBoxMesec.SelectedItem = DateTime.Today.Month.ToString();
                                comboBoxGodina.SelectedItem = DateTime.Today.Year.ToString();
                            }

                            // Učitavanje adrese
                            textBox4.Text = reader["Adresa"]?.ToString() ?? "";

                            // Učitavanje grada
                            textBox5.Text = reader["Grad"]?.ToString() ?? "";

                            // Učitavanje detalja pacijenta
                            richTextBox1.Text = reader["DetaljiPacijenta"]?.ToString() ?? "";
                        }
                        else
                        {
                            MessageBox.Show("Nije pronađen korisnik sa ID: " + idOsobe, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri učitavanju podataka: " + ex.Message, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            UcitajPodatkeOsobe();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }

        // Capitalize function (optional, but good for consistency if needed for update)
        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            if (input.Length == 1) return input.ToUpper();
            return char.ToUpper(input[0]) + input.Substring(1);
        }


        private void button1_Click(object sender, EventArgs e) // Delete Button
        {
            DialogResult result = MessageBox.Show(
                "Da li ste sigurni da želite da obrišete ovog korisnika i sve njegove posete?",
                "Potvrda brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
                {
                    OleDbTransaction transakcija = null; // Use a transaction
                    try
                    {
                        konekcija.Open();
                        transakcija = konekcija.BeginTransaction();

                        // Delete related records (Poseta) first
                        string deletePosetaQuery = "DELETE FROM Poseta WHERE IDOsobe = @id";
                        OleDbCommand cmdPoseta = new OleDbCommand(deletePosetaQuery, konekcija, transakcija);
                        cmdPoseta.Parameters.Add("@id", OleDbType.VarWChar).Value = idOsobe; // Adjust OleDbType if needed
                        cmdPoseta.ExecuteNonQuery();

                        // Delete the main record (Osoba)
                        string deleteOsobaQuery = "DELETE FROM Osoba WHERE IDOsobe = @id";
                        OleDbCommand cmdOsoba = new OleDbCommand(deleteOsobaQuery, konekcija, transakcija);
                        cmdOsoba.Parameters.Add("@id", OleDbType.VarWChar).Value = idOsobe; // Adjust OleDbType if needed
                        int redovaObrisano = cmdOsoba.ExecuteNonQuery();

                        transakcija.Commit(); // Commit transaction if both deletes succeed

                        if (redovaObrisano > 0)
                        {
                            MessageBox.Show("Korisnik i njegove posete su uspešno obrisani!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            roditeljskaForma.UcitajSveOsobe(); // Refresh parent form
                            this.Close();
                        }
                        else
                        {
                            // This might happen if the user was deleted between load and delete click
                            MessageBox.Show("Korisnik nije pronađen za brisanje (moguće da je već obrisan).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            roditeljskaForma.UcitajSveOsobe(); // Refresh anyway
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        transakcija?.Rollback(); // Rollback on error
                        MessageBox.Show("Greška pri brisanju korisnika: " + ex.Message, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } // Connection disposed
            }
        }

        private void button2_Click(object sender, EventArgs e) // Update Button
        {
            // Dohvatite vrednosti sa ComboBox-ova
            string imeRaw = textBox1.Text.Trim();
            string prezimeRaw = textBox2.Text.Trim();
            string brojTelefona = textBox3.Text.Trim();
            string adresa = textBox4.Text.Trim(); // Adresa iz textBox4
            string grad = textBox5.Text.Trim(); // Grad iz textBox5
            string detaljiPacijenta = richTextBox1.Text.Trim(); // Detalji iz richTextBoxDetalji
            DateTime datumRodjenja = new DateTime(
                int.Parse(comboBoxGodina.SelectedItem.ToString()),
                int.Parse(comboBoxMesec.SelectedItem.ToString()),
                int.Parse(comboBoxDan.SelectedItem.ToString())
            );

            // Validacija i ostale provere (isto kao što ste već radili)
            if (string.IsNullOrWhiteSpace(imeRaw) || string.IsNullOrWhiteSpace(prezimeRaw) /*|| string.IsNullOrWhiteSpace(brojTelefona) || string.IsNullOrWhiteSpace(adresa)*/)
            {
                MessageBox.Show("Sva polja (Ime, Prezime, Broj Telefona, Adresa) moraju biti popunjena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(imeRaw)) textBox1.Focus();
                else if (string.IsNullOrWhiteSpace(prezimeRaw)) textBox2.Focus();
                else if (string.IsNullOrWhiteSpace(brojTelefona)) textBox3.Focus();
                else if (string.IsNullOrWhiteSpace(adresa)) textBox4.Focus();
                return;
            }

            // Ako je sve u redu, izvrši ažuriranje u bazi:
            using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
            {
                try
                {
                    konekcija.Open();
                    string query = @"UPDATE Osoba
                            SET Ime = @ime,
                                Prezime = @prezime,
                                BrojTelefona = @telefon,
                                DatumRodjenja = @datum,
                                Adresa = @adresa,
                                Grad = @grad,
                                DetaljiPacijenta = @detalji
                            WHERE IDOsobe = @id";

                    OleDbCommand cmd = new OleDbCommand(query, konekcija);
                    cmd.Parameters.Add("@ime", OleDbType.VarWChar).Value = CapitalizeFirstLetter(imeRaw);
                    cmd.Parameters.Add("@prezime", OleDbType.VarWChar).Value = CapitalizeFirstLetter(prezimeRaw);
                    cmd.Parameters.Add("@telefon", OleDbType.VarWChar).Value = brojTelefona;
                    cmd.Parameters.Add("@datum", OleDbType.Date).Value = datumRodjenja; // Slanje datuma kao DateTime
                    cmd.Parameters.Add("@adresa", OleDbType.VarWChar).Value = adresa;
                    cmd.Parameters.Add("@grad", OleDbType.VarWChar).Value = grad; // Dodato grad
                    cmd.Parameters.Add("@detalji", OleDbType.LongVarWChar).Value = detaljiPacijenta; // Dugi tekstualni podatak
                    cmd.Parameters.Add("@id", OleDbType.VarWChar).Value = idOsobe;

                    int redovaIzmenjeno = cmd.ExecuteNonQuery();

                    if (redovaIzmenjeno > 0)
                    {
                        MessageBox.Show("Podaci su uspešno ažurirani!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        roditeljskaForma.UcitajSveOsobe(); // Osvježavanje podataka na roditeljskoj formi
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nije bilo moguće ažurirati podatke.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri ažuriranju korisnika: " + ex.Message, "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e) // Button to open Form4 (Posete)
        {
            //Form1 ownerForm = this.roditeljskaForma; // Already have roditeljskaForma field

            // Ensure roditeljskaForma is not null before using it
            if (this.roditeljskaForma == null || this.roditeljskaForma.IsDisposed)
            {
                MessageBox.Show("Greška: Veza ka glavnoj formi nije dostupna.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form4 novaForma = new Form4(idOsobe, this.roditeljskaForma);
            this.Close();
            novaForma.Show(this.roditeljskaForma);

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            // Izvoz podataka o posetama pacijenta
            try
            {
                // Add necessary imports at the top of your file:
                // using System.IO;
                // using System.Text;
                // using iText.Kernel.Pdf;
                // using iText.Layout;
                // using iText.Layout.Element;
                // using iText.Kernel.Font;
                // using iText.IO.Font.Constants;
                // using iText.Kernel.Geom;

                // Get patient data for filename and header
                string patientName = $"{textBox1.Text} {textBox2.Text}";
                string patientPhone = textBox3.Text;

                // Create SaveFileDialog to let user choose where to save the export
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    Title = "Izvoz poseta pacijenta",
                    FileName = $"Posete_{patientName.Replace(" ", "_")}_{DateTime.Now:yyyy-MM-dd}",
                    DefaultExt = "pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveDialog.FileName;
                    string fileType = Path.GetExtension(fileName).ToLower();

                    // Load all visits data for this patient
                    List<VisitData> visits = LoadPatientVisits();

                    if (visits.Count == 0)
                    {
                        MessageBox.Show("Pacijent nema evidentiranih poseta za izvoz.",
                            "Nema podataka",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    // Export based on file type
                    if (fileType == ".pdf")
                    {
                        ExportToPdf(fileName, patientName, visits);
                    }
                    else // Default to text export
                    {
                        ExportToText(fileName, patientName, visits);
                    }

                    MessageBox.Show($"Uspešno izvezene posete pacijenta {patientName}.",
                        "Uspešan izvoz",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Ask if the user wants to open the exported file
                    if (MessageBox.Show("Da li želite da otvorite izvezeni fajl?",
                        "Otvoriti fajl",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom izvoza poseta: {ex.Message}",
                    "Greška",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Class to store visit data
        private class VisitData
        {
            public DateTime DateOfVisit { get; set; }
            public string Service { get; set; }     // Usluga
            public string Details { get; set; }     // Detaljno
            public string Doctor { get; set; }      // Doktor
            public string TeethWorkedOn { get; set; } // RadjeniZubi
        }

        // Method to load all visits for the current patient
        private List<VisitData> LoadPatientVisits()
        {
            List<VisitData> visits = new List<VisitData>();

            using (OleDbConnection connection = new OleDbConnection(konekcioniString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT DatumPosete, Usluga, Detaljno, Doktor, RadjeniZubi 
                         FROM Poseta 
                         WHERE IDOsobe = @id 
                         ORDER BY DatumPosete DESC";

                    OleDbCommand cmd = new OleDbCommand(query, connection);
                    cmd.Parameters.Add("@id", OleDbType.VarWChar).Value = idOsobe;

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            visits.Add(new VisitData
                            {
                                DateOfVisit = reader["DatumPosete"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["DatumPosete"])
                                    : DateTime.MinValue,
                                Service = reader["Usluga"]?.ToString() ?? "",
                                Details = reader["Detaljno"]?.ToString() ?? "",
                                Doctor = reader["Doktor"]?.ToString() ?? "",
                                TeethWorkedOn = reader["RadjeniZubi"]?.ToString() ?? ""
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška pri učitavanju poseta: {ex.Message}",
                        "Greška Baze",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            return visits;
        }

        // Method to export to text file
        private void ExportToText(string fileName, string patientName, List<VisitData> visits)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                // Write header
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine($"IZVEŠTAJ POSETA ZA PACIJENTA: {patientName}");
                writer.WriteLine($"GRAD: {textBox5.Text}"); // Dodato grad
                writer.WriteLine($"Datum izvoza: {DateTime.Now:dd.MM.yyyy HH:mm}");
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine();

                // Write visits
                for (int i = 0; i < visits.Count; i++)
                {
                    var visit = visits[i];

                    writer.WriteLine($"POSETA #{i + 1} - {visit.DateOfVisit:dd.MM.yyyy}");
                    writer.WriteLine("-".PadRight(80, '-'));

                    writer.WriteLine("USLUGA:");
                    writer.WriteLine(FormatLongText(visit.Service));
                    writer.WriteLine();

                    writer.WriteLine("DETALJI:");
                    writer.WriteLine(FormatLongText(visit.Details));
                    writer.WriteLine();

                    writer.WriteLine("DOKTOR:");
                    writer.WriteLine(FormatLongText(visit.Doctor));
                    writer.WriteLine();

                    writer.WriteLine("RAĐENI ZUBI:");
                    writer.WriteLine(FormatLongText(visit.TeethWorkedOn));

                    // Add separator between visits
                    if (i < visits.Count - 1)
                    {
                        writer.WriteLine();
                        writer.WriteLine("=".PadRight(80, '='));
                        writer.WriteLine();
                    }
                }
            }
        }

        // Format long text for better readability
        private string FormatLongText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "(Nema podataka)";

            // If text is already well-formatted with line breaks, keep it as is
            if (text.Contains("\r\n") || text.Contains("\n"))
                return text;

            // Otherwise add line breaks for readability (around 80 chars per line)
            StringBuilder result = new StringBuilder();
            int lineLength = 0;
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                if (lineLength + word.Length + 1 > 80)
                {
                    result.AppendLine();
                    lineLength = 0;
                }

                if (lineLength > 0)
                {
                    result.Append(' ');
                    lineLength++;
                }

                result.Append(word);
                lineLength += word.Length;
            }

            return result.ToString();
        }

        // Method to export to PDF file
        private void ExportToPdf(string fileName, string patientName, List<VisitData> visits)
        {
            try
            {
                // Create a PDF document
                using (var pdfWriter = new iText.Kernel.Pdf.PdfWriter(fileName))
                using (var pdf = new iText.Kernel.Pdf.PdfDocument(pdfWriter))
                {
                    var document = new iText.Layout.Document(pdf, iText.Kernel.Geom.PageSize.A4);

                    // Add document properties
                    pdf.GetDocumentInfo().SetTitle($"Izveštaj poseta za {patientName}");
                    pdf.GetDocumentInfo().SetAuthor("Stomatološka Ordinacija Kartoteka");
                    pdf.GetDocumentInfo().SetCreator("Stomatološka Ordinacija Kartoteka");

                    // Define styles
                    var fontRegular = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_ROMAN);
                    var fontBold = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_BOLD);

                    // Title
                    var title = new iText.Layout.Element.Paragraph($"IZVEŠTAJ POSETA ZA PACIJENTA: {patientName}")
                        .SetFont(fontBold)
                        .SetFontSize(16)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    document.Add(title);

                    // Add City/Grad
                    var cityInfo = new iText.Layout.Element.Paragraph($"GRAD: {textBox5.Text}")
                        .SetFont(fontRegular)
                        .SetFontSize(12)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    document.Add(cityInfo);

                    // Header info (removed phone number)
                    var exportInfo = new iText.Layout.Element.Paragraph($"Datum izvoza: {DateTime.Now:dd.MM.yyyy HH:mm}")
                        .SetFont(fontRegular)
                        .SetFontSize(12)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    document.Add(exportInfo);

                    // Add separator
                    var separator = new iText.Layout.Element.LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine());
                    document.Add(separator);
                    document.Add(new iText.Layout.Element.Paragraph("\n"));

                    // Add visits
                    for (int i = 0; i < visits.Count; i++)
                    {
                        var visit = visits[i];

                        // Visit header
                        var visitHeader = new iText.Layout.Element.Paragraph($"POSETA #{i + 1} - {visit.DateOfVisit:dd.MM.yyyy}")
                            .SetFont(fontBold)
                            .SetFontSize(14)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                        document.Add(visitHeader);

                        document.Add(new iText.Layout.Element.LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.DottedLine()));

                        // Service (Usluga)
                        var serviceTitle = new iText.Layout.Element.Paragraph("USLUGA:")
                            .SetFont(fontBold)
                            .SetFontSize(12);
                        document.Add(serviceTitle);

                        var serviceText = new iText.Layout.Element.Paragraph(string.IsNullOrEmpty(visit.Service) ? "(Nema podataka)" : visit.Service)
                            .SetFont(fontRegular)
                            .SetFontSize(11);
                        document.Add(serviceText);
                        document.Add(new iText.Layout.Element.Paragraph("\n"));

                        // Details (Detaljno)
                        var detailsTitle = new iText.Layout.Element.Paragraph("DETALJI:")
                            .SetFont(fontBold)
                            .SetFontSize(12);
                        document.Add(detailsTitle);

                        var detailsText = new iText.Layout.Element.Paragraph(string.IsNullOrEmpty(visit.Details) ? "(Nema podataka)" : visit.Details)
                            .SetFont(fontRegular)
                            .SetFontSize(11);
                        document.Add(detailsText);
                        document.Add(new iText.Layout.Element.Paragraph("\n"));

                        // Doctor (Doktor)
                        var doctorTitle = new iText.Layout.Element.Paragraph("DOKTOR:")
                            .SetFont(fontBold)
                            .SetFontSize(12);
                        document.Add(doctorTitle);

                        var doctorText = new iText.Layout.Element.Paragraph(string.IsNullOrEmpty(visit.Doctor) ? "(Nema podataka)" : visit.Doctor)
                            .SetFont(fontRegular)
                            .SetFontSize(11);
                        document.Add(doctorText);
                        document.Add(new iText.Layout.Element.Paragraph("\n"));

                        // Teeth Worked On (RadjeniZubi)
                        var teethTitle = new iText.Layout.Element.Paragraph("RAĐENI ZUBI:")
                            .SetFont(fontBold)
                            .SetFontSize(12);
                        document.Add(teethTitle);

                        var teethText = new iText.Layout.Element.Paragraph(string.IsNullOrEmpty(visit.TeethWorkedOn) ? "(Nema podataka)" : visit.TeethWorkedOn)
                            .SetFont(fontRegular)
                            .SetFontSize(11);
                        document.Add(teethText);

                        // Add separator between visits (except after the last one)
                        if (i < visits.Count - 1)
                        {
                            document.Add(new iText.Layout.Element.Paragraph("\n"));
                            document.Add(separator);
                            document.Add(new iText.Layout.Element.Paragraph("\n"));
                        }
                    }

                    // Close the document
                    document.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri kreiranju PDF-a: {ex.Message}\nFajl će biti sačuvan kao tekstualni dokument.",
                    "PDF greška",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Change extension to .txt
                fileName = Path.ChangeExtension(fileName, ".txt");
                ExportToText(fileName, patientName, visits);
            }
        }


        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            // Kreiraj PDF dokument
            string pdfFilePath = "Osobe_Podaci.pdf";

            // Kreiraj PdfWriter
            using (PdfWriter writer = new PdfWriter(pdfFilePath))
            {
                // Kreiraj PdfDocument
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Kreiraj Document koji koristi PdfDocument
                    Document doc = new Document(pdf);

                    // Kreiraj font za stilizovane naslove i tekst
                    var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // Dodaj naslov
                    Paragraph naslov = new Paragraph("Podaci o pacijentu")
                        .SetFont(font)
                        .SetFontSize(18)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginBottom(20);
                    doc.Add(naslov);

                    // Dodaj liniju ispod naslova
                    doc.Add(new LineSeparator(new SolidLine()));

                    // Učitaj podatke iz baze
                    List<Pacijent> pacijenti = GetPacijentiFromDatabase();

                    // Dodaj podatke u PDF
                    foreach (var pacijent in pacijenti)
                    {
                        // Dodaj ime i prezime u podebljanom fontu
                        doc.Add(new Paragraph($"Ime: {pacijent.Ime} {pacijent.Prezime}")
                            .SetFont(regularFont)
                            .SetFontSize(12)
                            .SetMarginTop(10));

                        // Dodaj ostale informacije sa regularnim fontom
                        doc.Add(new Paragraph($"Datum Rodjenja: {pacijent.DatumRodjenja.ToString("dd/MM/yyyy")}")
                            .SetFont(regularFont)
                            .SetFontSize(12));

                        doc.Add(new Paragraph($"Broj Telefona: {pacijent.BrojTelefona}")
                            .SetFont(regularFont)
                            .SetFontSize(12));

                        doc.Add(new Paragraph($"Grad: {pacijent.Grad}")
                            .SetFont(regularFont)
                            .SetFontSize(12));


                        doc.Add(new Paragraph($"Adresa: {pacijent.Adresa}")
                            .SetFont(regularFont)
                            .SetFontSize(12));

                        // Dodaj detalje pacijenta
                        doc.Add(new Paragraph($"Detalji: {pacijent.DetaljiPacijenta}")
                            .SetFont(regularFont)
                            .SetFontSize(12));

                        // Dodaj liniju ispod svakog pacijenta
                        doc.Add(new LineSeparator(new SolidLine()).SetMarginTop(10));
                    }
                }
            }

            // Obavesti korisnika o uspešnom eksportu
            MessageBox.Show("Podaci su uspešno eksportovani u PDF.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Otvori PDF odmah nakon kreiranja
            System.Diagnostics.Process.Start(pdfFilePath);
        }

        // Model za pacijenta
        public class Pacijent
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string BrojTelefona { get; set; }
            public string Grad { get; set; }
            public string Adresa { get; set; }
            public string DetaljiPacijenta { get; set; } // Dodato polje za detalje pacijenta
        }

        // Funkcija koja učitava pacijente iz baze
        private List<Pacijent> GetPacijentiFromDatabase()
        {
            List<Pacijent> pacijenti = new List<Pacijent>();
            string konekcioniString = Program.konekcioniString; // Podesi svoj konekcioni string

            using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
            {
                try
                {
                    konekcija.Open();

                    // Dodajemo Grad u SELECT upit - ovo je bilo nedostajalo
                    string query = "SELECT Ime, Prezime, DatumRodjenja, BrojTelefona, Adresa, Grad, DetaljiPacijenta FROM Osoba";
                    OleDbCommand cmd = new OleDbCommand(query, konekcija);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pacijent pacijent = new Pacijent
                        {
                            Ime = reader["Ime"].ToString(),
                            Prezime = reader["Prezime"].ToString(),
                            DatumRodjenja = Convert.ToDateTime(reader["DatumRodjenja"]),
                            BrojTelefona = reader["BrojTelefona"].ToString(),
                            Adresa = reader["Adresa"].ToString(),
                            // Pravilno čitamo polje Grad iz baze i postavljamo ga sa proverom za null
                            Grad = reader["Grad"] != DBNull.Value ? reader["Grad"].ToString() : "",
                            DetaljiPacijenta = reader["DetaljiPacijenta"].ToString() // Učitaj detalje pacijenta
                        };
                        pacijenti.Add(pacijent);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri radu sa bazom podataka: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return pacijenti;
        }
    }
}