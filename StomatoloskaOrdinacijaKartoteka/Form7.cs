using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // Potrebno za Debug.WriteLine
using System.Data.OleDb;  // Potrebno za rad sa bazom
using DataBaseProgram;   // Potrebno (ili gde ti je Form1)

// Ensure this namespace matches your project structure
namespace StomatoloskaOrdinacijaKartoteka
{
    public partial class Form7 : Form
    {
        // --- Podaci potrebni za rad ---
        private readonly string _pocetnaSema;          // Originalna šema učitana pri otvaranju
        private string _trenutnaSema => GenerisiStringIzListe(); // Dinamički generiše trenutnu šemu
        private readonly string _idPoseteZaUpdate;     // ID posete koju ažuriramo
        private readonly Form1 _glavnaFormaRef;         // Referenca na Form1 za osvežavanje
        private readonly string _konekcioniString = Program.konekcioniString; // Konekcija

        // --- Komponente za UI i logiku iz Form6 ---
        private ContextMenuStrip contextMenu;
        private Button trenutnoKliknutoDugme;
        private readonly List<string> _entries = new List<string>(); // Lista koja prati TRENUTNO stanje editora
        private readonly Dictionary<string, Color> _intervencijaBoje = new Dictionary<string, Color>();

        // --- Konstruktor za Prikaz i AŽURIRANJE ---
        public Form7(string pocetnaSema, string idPosete, Form1 glavnaForma)
        {
            // MessageBox.Show($"Form7: Constructor (EDIT) called. Sema Length: {pocetnaSema?.Length ?? 0}, IDPosete: {idPosete}", "Form7 Debug"); // UKLONJENO
            Debug.WriteLine($"--- Form7 Constructor START (EDIT Version) ---");
            Debug.WriteLine($"Form7: Received pocetnaSema: '{pocetnaSema ?? "NULL"}'");
            Debug.WriteLine($"Form7: Received idPosete: '{idPosete ?? "NULL"}'");

            InitializeComponent(); // Inicijalizacija kontrola sa dizajnera

            // Čuvanje prosleđenih podataka
            this._pocetnaSema = pocetnaSema ?? "";
            this._idPoseteZaUpdate = idPosete;
            this._glavnaFormaRef = glavnaForma;

            // Validacija ključnih podataka
            if (string.IsNullOrEmpty(_idPoseteZaUpdate))
            {
                Debug.WriteLine("Form7 CRITICAL ERROR: ID Posete nije prosleđen!");
                MessageBox.Show("Interna greška: ID posete nije dostupan za ažuriranje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
                if (this.siticoneButton1 != null) this.siticoneButton1.Enabled = false; // Onemogući čuvanje
            }
            if (_glavnaFormaRef == null)
            {
                Debug.WriteLine("Form7 CRITICAL ERROR: Referenca na Form1 nije prosleđena!");
                MessageBox.Show("Interna greška: Glavna forma nije dostupna za osvežavanje.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
            }

            // Inicijalizacija UI elemenata i stanja
            InitializeButtonsAndTags();     // Postavi Tag-ove i MouseDown event na dugmad
            InicijalizujMapuBojaIntervencija(); // Definiši boje
            InitializeContextMenu();        // Kreiraj context meni za izmene
            PrimeniPocetnuSemu(_pocetnaSema);// Postavi početne boje I popuni _entries listu

            Debug.WriteLine($"--- Form7 Constructor END (EDIT Version) ---");
            // MessageBox.Show("Form7: Constructor finished.", "Form7 Debug"); // UKLONJENO

            this.Load += Form7_Load;

            // Poveži click event za dugme za čuvanje/ažuriranje
            if (this.siticoneButton1 != null)
            {
                this.siticoneButton1.Click += new System.EventHandler(this.siticoneButton1_SacuvajIzmene_Click);
                this.siticoneButton1.Text = "Sačuvaj Izmene"; // Postavi tekst dugmeta
            }
            else
            {
                Debug.WriteLine("Form7 CRITICAL ERROR: Dugme 'siticoneButton1' nije pronađeno na formi!");
                MessageBox.Show("Greška dizajnera: Dugme za čuvanje nije pronađeno.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
            }
        }

        // --- Metode za inicijalizaciju i UI interakciju ---

        private void InitializeButtonsAndTags()
        {
            Debug.WriteLine("Form7: Inicijalizujem Tag-ove i MouseDown event za dugmad...");
            button1.Tag = "GL.8"; button2.Tag = "GL.7"; button3.Tag = "GL.6"; button4.Tag = "GL.5";
            button5.Tag = "GL.4"; button6.Tag = "GL.3"; button7.Tag = "GL.2"; button8.Tag = "GL.1";
            button9.Tag = "GD.1"; button10.Tag = "GD.2"; button11.Tag = "GD.3"; button12.Tag = "GD.4";
            button13.Tag = "GD.5"; button14.Tag = "GD.6"; button15.Tag = "GD.7"; button16.Tag = "GD.8";
            button17.Tag = "DL.8"; button18.Tag = "DL.7"; button19.Tag = "DL.6"; button20.Tag = "DL.5";
            button21.Tag = "DL.4"; button22.Tag = "DL.3"; button23.Tag = "DL.2"; button24.Tag = "DL.1";
            button25.Tag = "DD.1"; button26.Tag = "DD.2"; button27.Tag = "DD.3"; button28.Tag = "DD.4";
            button29.Tag = "DD.5"; button30.Tag = "DD.6"; button31.Tag = "DD.7"; button32.Tag = "DD.8";

            TableLayoutPanel mainLayout = this.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
            if (mainLayout != null)
            {
                int count = 0;
                foreach (Button btn in mainLayout.Controls.OfType<Button>())
                {
                    if (btn.Tag != null)
                    {
                        btn.MouseDown += Button_MouseDown;
                        count++;
                    }
                }
                Debug.WriteLine($"Form7: MouseDown event dodat na {count} dugmadi.");
            }
            else
            {
                Debug.WriteLine("Form7 WARNING: Glavni TableLayoutPanel nije pronađen!");
            }
        }

        private void InicijalizujMapuBojaIntervencija()
        {
            Debug.WriteLine("Form7: Inicijalizujem mapu boja intervencija...");
            _intervencijaBoje.Clear();
            _intervencijaBoje["Nema"] = Color.FromArgb(122, 152, 153);
            _intervencijaBoje["Zub"] = Color.White;
            _intervencijaBoje["Ektrakcija"] = Color.FromArgb(1, 1, 1);
            _intervencijaBoje["Karijes"] = Color.FromArgb(173, 20, 1);
            _intervencijaBoje["Plomba"] = Color.FromArgb(3, 180, 10);
            _intervencijaBoje["Lijek"] = Color.FromArgb(25, 38, 203);
            _intervencijaBoje["Navlaka"] = Color.FromArgb(221, 221, 59);
            _intervencijaBoje["Apendix"] = Color.FromArgb(226, 105, 6);
            _intervencijaBoje["Most"] = Color.FromArgb(89, 197, 216);
            _intervencijaBoje["Parcijalna"] = Color.FromArgb(26, 84, 88);
            _intervencijaBoje["Totalna gornja"] = Color.FromArgb(228, 28, 214);
            _intervencijaBoje["Totalna donja"] = Color.FromArgb(88, 4, 119);
            Debug.WriteLine($"Form7: Mapa boja inicijalizovana sa {_intervencijaBoje.Count} unosa.");
        }

        private void InitializeContextMenu()
        {
            Debug.WriteLine("Form7: Inicijalizujem ContextMenu...");
            contextMenu = new ContextMenuStrip();
            AddMenuItem("Nema", "Nema", 122, 152, 153);
            AddMenuItem("Zub", "Zub", 255, 255, 255);
            AddMenuItem("Ektrakcija", "Ektrakcija", 1, 1, 1);
            AddMenuItem("Karijes", "Karijes", 173, 20, 1);
            AddMenuItem("Plomba", "Plomba", 3, 180, 10);
            AddMenuItem("Lijek", "Lijek", 25, 38, 203);
            AddMenuItem("Navlaka", "Navlaka", 221, 221, 59);
            AddMenuItem("Apendix", "Apendix", 226, 105, 6);
            AddMenuItem("Most", "Most", 89, 197, 216);
            AddMenuItem("Parcijalna", "Parcijalna", 26, 84, 88);
            AddMenuItem("Totalna gornja", "Totalna gornja", 228, 28, 214);
            AddMenuItem("Totalna donja", "Totalna donja", 88, 4, 119);
            AddDefaultMenuItem();
            Debug.WriteLine($"Form7: ContextMenu inicijalizovan sa {contextMenu.Items.Count} stavki.");
        }

        private void AddMenuItem(string text, string intervencija, int r, int g, int b)
        {
            var bmp = new Bitmap(16, 16);
            using (var gfx = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(r, g, b)))
            {
                gfx.FillRectangle(brush, 0, 0, 16, 16);
            }

            var item = new ToolStripMenuItem(text, bmp);
            item.Click += (s, e) => {
                if (trenutnoKliknutoDugme == null) return;
                Debug.WriteLine($"Form7: Meni '{text}' kliknut za dugme '{trenutnoKliknutoDugme.Tag}'");
                trenutnoKliknutoDugme.BackColor = Color.FromArgb(r, g, b);
                var entryTag = trenutnoKliknutoDugme.Tag?.ToString();
                if (string.IsNullOrEmpty(entryTag)) return;
                _entries.RemoveAll(x => x.StartsWith(entryTag + "."));
                if (intervencija != "Zub")
                {
                    var newEntry = $"{entryTag}.{intervencija}";
                    _entries.Add(newEntry);
                    Debug.WriteLine($"    Novi unos '{newEntry}' dodat u _entries.");
                    // MessageBox.Show($"Dodato/Ažurirano: {newEntry}\nUkupno unosa: {_entries.Count}\nSadržaj: {GenerisiStringIzListe()}", "DEBUG: _entries Update (Add)"); // UKLONJENO
                }
                else
                {
                    Debug.WriteLine($"    Intervencija je 'Zub', ne dodaje se u _entries.");
                    // MessageBox.Show($"Resetovano na 'Zub' za: {entryTag}\nUkupno unosa: {_entries.Count}\nSadržaj: {GenerisiStringIzListe()}", "DEBUG: _entries Update (Reset)"); // UKLONJENO
                }
                Debug.WriteLine($"    Trenutno stanje _entries: {GenerisiStringIzListe()}");
            };
            contextMenu.Items.Add(item);
        }

        private void AddDefaultMenuItem()
        {
            contextMenu.Items.Add(new ToolStripSeparator());
            var item = new ToolStripMenuItem("Vrati na default");
            item.Click += (s, e) => {
                if (trenutnoKliknutoDugme == null) return;
                Debug.WriteLine($"Form7: Meni 'Vrati na default' kliknut za dugme '{trenutnoKliknutoDugme.Tag}'");
                if (_intervencijaBoje.TryGetValue("Zub", out Color defaultColor))
                {
                    trenutnoKliknutoDugme.BackColor = defaultColor;
                }
                else
                {
                    trenutnoKliknutoDugme.BackColor = SystemColors.Control;
                }
                var entryTag = trenutnoKliknutoDugme.Tag?.ToString();
                if (!string.IsNullOrEmpty(entryTag))
                {
                    int removedCount = _entries.RemoveAll(x => x.StartsWith(entryTag + "."));
                    if (removedCount > 0)
                    {
                        Debug.WriteLine($"    Unos za '{entryTag}' uklonjen iz _entries.");
                        // MessageBox.Show($"Uklonjen unos za: {entryTag}\nUkupno unosa: {_entries.Count}\nSadržaj: {GenerisiStringIzListe()}", "DEBUG: _entries Update (Remove Default)"); // UKLONJENO
                    }
                }
                Debug.WriteLine($"    Trenutno stanje _entries: {GenerisiStringIzListe()}");
            };
            contextMenu.Items.Add(item);
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is Button btn)) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            trenutnoKliknutoDugme = btn;
            Debug.WriteLine($"Form7: MouseDown na dugme '{btn.Tag}'");
            if (btn.ContextMenuStrip == null)
            {
                btn.ContextMenuStrip = contextMenu;
            }
            contextMenu.Show(btn, new Point(0, btn.Height));
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Debug.WriteLine($"--- Form7 Form_Load START (EDIT Version) ---");
            this.Text = "Prikaz i Izmena Šeme Zuba";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            Debug.WriteLine($"--- Form7 Form_Load END (EDIT Version) ---");
        }

        private void PrimeniPocetnuSemu(string semaString)
        {
            Debug.WriteLine($"--- Form7 PrimeniPocetnuSemu START ---");
            Debug.WriteLine($"Form7: Primenjujem početnu šemu i punim _entries: '{semaString}'");

            ResetujBojeDugmadi();
            _entries.Clear();

            if (string.IsNullOrWhiteSpace(semaString))
            {
                Debug.WriteLine("Form7: Početna šema je prazna.");
                Debug.WriteLine($"--- Form7 PrimeniPocetnuSemu END (prazan string) ---");
                return;
            }

            var podaci = semaString.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Debug.WriteLine($"Form7: Početna šema podeljena na {podaci.Length} zapisa.");

            int primenjenoBoja = 0;
            foreach (var zapis in podaci)
            {
                var delovi = zapis.Trim().Split('.');
                if (delovi.Length != 3)
                {
                    Debug.WriteLine($"    ERR (Početna): Preskačem zapis '{zapis}'.");
                    continue;
                }
                string tag = $"{delovi[0]}.{delovi[1]}";
                string intervencija = delovi[2];
                Button dugme = NadjiDugmePoTagu(tag);
                if (dugme != null)
                {
                    if (_intervencijaBoje.TryGetValue(intervencija, out Color boja))
                    {
                        dugme.BackColor = boja;
                        string entry = $"{tag}.{intervencija}";
                        if (!_entries.Contains(entry))
                        {
                            _entries.Add(entry);
                        }
                        primenjenoBoja++;
                    }
                    else
                    {
                        Debug.WriteLine($"    WARN (Početna): Boja za '{intervencija}' nije nađena!");
                    }
                }
                else
                {
                    Debug.WriteLine($"    WARN (Početna): Dugme za tag '{tag}' nije nađeno!");
                }
            }
            Debug.WriteLine($"Form7: Završena primena početne šeme. Postavljeno boja: {primenjenoBoja}. Početno stanje _entries: {GenerisiStringIzListe()}");
            Debug.WriteLine($"--- Form7 PrimeniPocetnuSemu END ---");
        }

        private string GenerisiStringIzListe()
        {
            _entries.Sort(); // Sortiraj pre spajanja za konzistentnost
            return string.Join(", ", _entries.Distinct());
        }

        // Event handler za dugme "Sačuvaj Izmene"
        private void siticoneButton1_SacuvajIzmene_Click(object sender, EventArgs e)
        {
            // --- Početak eventa ---
            Debug.WriteLine($"--- Form7 siticoneButton1_SacuvajIzmene_Click START ---");
            // MessageBox.Show("Sacuvaj Izmene - START", "Form7 Debug"); // UKLONJENO

            // --- Provera ID Posete ---
            if (string.IsNullOrEmpty(_idPoseteZaUpdate))
            {
                Debug.WriteLine("Form7 ERROR: Nemoguće sačuvati, ID Posete je nepoznat.");
                MessageBox.Show("Interna greška: Nije moguće sačuvati šemu jer ID posete nije poznat.", "Greška Čuvanja", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
                Debug.WriteLine($"--- Form7 siticoneButton1_SacuvajIzmene_Click END (ERROR - No ID) ---");
                return;
            }
            Debug.WriteLine($"Form7: ID Posete za ažuriranje: {_idPoseteZaUpdate}");

            // --- Generisanje i ispis šema ---
            string generisanaTrenutnaSema = this._trenutnaSema; // Poziva GenerisiStringIzListe()
            Debug.WriteLine($"Form7: Početna (originalna) šema: '{_pocetnaSema}'");
            Debug.WriteLine($"Form7: Trenutno generisana šema (iz _entries): '{generisanaTrenutnaSema}'");

            // --- Poređenje šema ---
            var pocetnaLista = _pocetnaSema.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            pocetnaLista.Sort();
            var trenutnaLista = _entries.ToList();
            trenutnaLista.Sort();

            // Debug ispis pre poređenja
            Debug.WriteLine($"--- Poređenje Šema ---");
            Debug.WriteLine($"Početna (sortirana): [{string.Join(" | ", pocetnaLista)}]");
            Debug.WriteLine($"Trenutna (sortirana): [{string.Join(" | ", trenutnaLista)}]");
            bool jednake = pocetnaLista.SequenceEqual(trenutnaLista);
            Debug.WriteLine($"Da li su jednake? {jednake}");
            // MessageBox.Show($"Početna: [{string.Join(", ", pocetnaLista)}]\n" +
            //                   $"Trenutna: [{string.Join(", ", trenutnaLista)}]\n" +
            //                   $"Jednake: {jednake}", "DEBUG: Poređenje Šema"); // UKLONJENO

            // --- Logika na osnovu poređenja ---
            if (jednake)
            {
                // Nema izmena
                Debug.WriteLine("Form7: Nema izmena u šemi. Ništa za čuvanje.");
                MessageBox.Show("Niste napravili nikakve izmene u šemi zuba.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); // Ostavljeno - info za korisnika
            }
            else
            {
                // Ima izmena, pokušaj čuvanja
                Debug.WriteLine("Form7: Detektovane izmene. Pokušavam da sačuvam...");
                // MessageBox.Show("Detektovane izmene, pokušavam sačuvati...", "Form7 Debug"); // UKLONJENO

                if (SacuvajIzmeneUBazu(generisanaTrenutnaSema))
                {
                    // Čuvanje uspešno
                    MessageBox.Show("Izmene su uspešno sačuvane!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information); // Ostavljeno - potvrda za korisnika
                    Debug.WriteLine("Form7: Izmene uspešno sačuvane. Zatvaram formu...");

                    // Osvežavanje Form1
                    if (_glavnaFormaRef != null)
                    {
                        string idOsobeZaOsvezavanje = _glavnaFormaRef.GetSelectedIdOsobe();
                        if (!string.IsNullOrEmpty(idOsobeZaOsvezavanje))
                        {
                            Debug.WriteLine($"Form7: Osvežavam prikaz u Form1 za osobu ID: {idOsobeZaOsvezavanje}");
                            try
                            {
                                _glavnaFormaRef.UcitajDetaljeIzOsobe(idOsobeZaOsvezavanje);
                                Debug.WriteLine($"Form7: Poziv UcitajDetaljeIzOsobe završen.");
                            }
                            catch (Exception exRef)
                            {
                                Debug.WriteLine($"Form7 ERROR: Greška prilikom poziva UcitajDetaljeIzOsobe na Form1: {exRef.Message}");
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Form7 WARN: GetSelectedIdOsobe vratio null/prazno, ne mogu osvežiti Form1.");
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Form7 WARN: Referenca na Form1 je null, ne mogu osvežiti.");
                    }

                    // Postavljanje DialogResult i zatvaranje
                    // MessageBox.Show("Postavljam DialogResult na OK i zatvaram Form7.", "DEBUG: Closing Form7"); // UKLONJENO
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Čuvanje nije uspelo (poruka prikazana iz SacuvajIzmeneUBazu)
                    Debug.WriteLine("Form7: Čuvanje izmena nije uspelo. Ostajem na formi.");
                    // MessageBox.Show("Čuvanje izmena nije uspelo. Proverite Debug Output.", "Form7 Debug - Greška Čuvanja"); // UKLONJENO
                }
            }
            // --- Kraj eventa ---
            Debug.WriteLine($"--- Form7 siticoneButton1_SacuvajIzmene_Click END ---");
        }


        // Metoda za čuvanje izmena u bazu
        private bool SacuvajIzmeneUBazu(string novaSema)
        {
            Debug.WriteLine($"--- Form7 SacuvajIzmeneUBazu START ---");
            Debug.WriteLine($"Form7: Ažuriram posetu ID: {_idPoseteZaUpdate} sa šemom: '{novaSema}'");

            if (!int.TryParse(_idPoseteZaUpdate, out int idPoseteNumericki))
            {
                Debug.WriteLine($"Form7 ERROR: ID Posete '{_idPoseteZaUpdate}' nije validan broj za SQL.");
                MessageBox.Show($"Interna greška pri čuvanju: ID posete '{_idPoseteZaUpdate}' nije validan broj.", "Greška Konverzije", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
                return false;
            }

            string query = "UPDATE Poseta SET RadjeniZubi = @NovaSema WHERE IDPosete = @IDPosete";

            try
            {
                using (OleDbConnection konekcija = new OleDbConnection(_konekcioniString))
                using (OleDbCommand cmd = new OleDbCommand(query, konekcija))
                {
                    OleDbParameter semaParam = new OleDbParameter("@NovaSema", OleDbType.LongVarWChar);
                    semaParam.Value = string.IsNullOrEmpty(novaSema) ? DBNull.Value : (object)novaSema;
                    cmd.Parameters.Add(semaParam);
                    cmd.Parameters.Add("@IDPosete", OleDbType.Integer).Value = idPoseteNumericki;

                    konekcija.Open();
                    Debug.WriteLine("Form7: Izvršavam UPDATE komandu...");
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Debug.WriteLine($"Form7: UPDATE komanda izvršena. Broj ažuriranih redova: {rowsAffected}");

                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Form7: Ažuriranje baze uspešno.");
                        Debug.WriteLine($"--- Form7 SacuvajIzmeneUBazu END (Uspeh) ---");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Form7 WARN: UPDATE nije ažurirao nijedan red (IDPosete možda ne postoji?).");
                        MessageBox.Show("Nije bilo moguće ažurirati šemu. Poseta sa datim ID-jem možda više ne postoji.", "Greška Ažuriranja", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Ostavljeno - info/greška za korisnika
                        Debug.WriteLine($"--- Form7 SacuvajIzmeneUBazu END (Nije nađeno) ---");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Form7 ERROR prilikom čuvanja šeme u bazu: {ex}");
                MessageBox.Show($"Greška prilikom čuvanja šeme u bazu podataka:\n{ex.Message}", "Greška Baze", MessageBoxButtons.OK, MessageBoxIcon.Error); // Ostavljeno - greška za korisnika
                Debug.WriteLine($"--- Form7 SacuvajIzmeneUBazu END (Greška) ---");
                return false;
            }
        }

        // Pomoćna metoda za pronalaženje dugmeta
        private Button NadjiDugmePoTagu(string tag)
        {
            foreach (TableLayoutPanel tlp in this.Controls.OfType<TableLayoutPanel>())
            {
                foreach (Button btn in tlp.Controls.OfType<Button>())
                {
                    if (btn.Tag != null && btn.Tag.ToString().Equals(tag, StringComparison.OrdinalIgnoreCase))
                    {
                        return btn;
                    }
                }
            }
            Debug.WriteLine($"    -> UPOZORENJE: Dugme sa tagom '{tag}' NIJE pronađeno na Form7!");
            return null;
        }

        // Pomoćna metoda za resetovanje boja
        private void ResetujBojeDugmadi()
        {
            int resetovano = 0;
            if (!_intervencijaBoje.TryGetValue("Zub", out Color defaultColor))
            {
                defaultColor = SystemColors.Control;
            }
            foreach (TableLayoutPanel tlp in this.Controls.OfType<TableLayoutPanel>())
            {
                foreach (Button btn in tlp.Controls.OfType<Button>())
                {
                    if (btn.Tag != null)
                    {
                        btn.BackColor = defaultColor;
                        resetovano++;
                    }
                }
            }
        }
    }
}