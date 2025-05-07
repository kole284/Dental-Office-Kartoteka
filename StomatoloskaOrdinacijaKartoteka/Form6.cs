using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataBaseProgram; // Assuming Form1 is in this namespace
using Siticone.Desktop.UI.WinForms.Suite; // Keep if needed
using System.Diagnostics; // For Debug.WriteLine
using System.Data.OleDb; // Za rad sa bazom

namespace StomatoloskaOrdinacijaKartoteka // Keep your original namespace
{
    public partial class Form6 : Form
    {
        private string idOsobe;
        private Form1 roditelj;
        private ContextMenuStrip contextMenu;
        private Button trenutnoKliknutoDugme;
        private readonly List<string> _entries = new List<string>();
        private readonly Dictionary<string, Color> _intervencijaBoje = new Dictionary<string, Color>();

        public string RadjeniZubiString { get; private set; }

        public Form6(Form1 roditelj, string idOsobe)
        {
            InitializeComponent();
            InitializeContextMenu();
            InitializeButtons();
            InicijalizujMapuBojaIntervencija();
            this.roditelj = roditelj;
            this.idOsobe = idOsobe;

            Debug.WriteLine($"Form6 ctor: Primljen ID osobe = '{idOsobe}'");
            this.Load += Form6_Load;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            Debug.WriteLine("Form6_Load: UI inicijalizovan, počinjem učitavanje šeme");
            UcitajNajnovijuSemu();
        }

        private void InitializeContextMenu()
        {
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
        }

        private void InitializeButtons()
        {
            button1.Tag = "GL.8"; button2.Tag = "GL.7"; button3.Tag = "GL.6"; button4.Tag = "GL.5";
            button5.Tag = "GL.4"; button6.Tag = "GL.3"; button7.Tag = "GL.2"; button8.Tag = "GL.1";
            button9.Tag = "GD.1"; button10.Tag = "GD.2"; button11.Tag = "GD.3"; button12.Tag = "GD.4";
            button13.Tag = "GD.5"; button14.Tag = "GD.6"; button15.Tag = "GD.7"; button16.Tag = "GD.8";
            button17.Tag = "DL.8"; button18.Tag = "DL.7"; button19.Tag = "DL.6"; button20.Tag = "DL.5";
            button21.Tag = "DL.4"; button22.Tag = "DL.3"; button23.Tag = "DL.2"; button24.Tag = "DL.1";
            button25.Tag = "DD.1"; button26.Tag = "DD.2"; button27.Tag = "DD.3"; button28.Tag = "DD.4";
            button29.Tag = "DD.5"; button30.Tag = "DD.6"; button31.Tag = "DD.7"; button32.Tag = "DD.8";

            foreach (var btn in tableLayoutPanel1.Controls.OfType<Button>())
            {
                if (btn != null)
                    btn.MouseDown += Button_MouseDown;
            }
        }

        private void InicijalizujMapuBojaIntervencija()
        {
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
        }

        private void UcitajNajnovijuSemu()
        {
            Debug.WriteLine($"UcitajNajnovijuSemu: pokušavam za ID = '{idOsobe}'");
            if (string.IsNullOrEmpty(idOsobe))
            {
                Debug.WriteLine("UcitajNajnovijuSemu: ID osobe je prazan, prekidam");
                return;
            }

            try
            {
                string najnovijaSema = DohvatiNajnovijuSemuIzBaze();
                Debug.WriteLine($"UcitajNajnovijuSemu: rezultat iz baze = '{najnovijaSema}'");

                if (!string.IsNullOrEmpty(najnovijaSema))
                {
                    Debug.WriteLine($"UcitajNajnovijuSemu: primenjujem šemu: {najnovijaSema}");
                    PrimeniSemuNaDugmad(najnovijaSema);
                }
                else
                {
                    Debug.WriteLine("UcitajNajnovijuSemu: nema prethodnih šema za pacijenta.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UcitajNajnovijuSemu ERROR: {ex}");
            }
        }

        private string DohvatiNajnovijuSemuIzBaze()
        {
            string sema = string.Empty;
            string konekcioniString = Program.konekcioniString;
            string query = @"SELECT TOP 1 RadjeniZubi FROM Poseta
                                     WHERE IDOsobe = @IDOsobe AND RadjeniZubi IS NOT NULL
                                       AND RadjeniZubi <> ''
                                     ORDER BY DatumPosete DESC, IDPosete DESC";
            Debug.WriteLine($"DohvatiNajnovijuSemuIzBaze: Otvaram konekciju, query = {query}");
            try
            {
                using (OleDbConnection konekcija = new OleDbConnection(konekcioniString))
                using (OleDbCommand cmd = new OleDbCommand(query, konekcija))
                {
                    cmd.Parameters.AddWithValue("@IDOsobe", idOsobe);
                    Debug.WriteLine($"DohvatiNajnovijuSemuIzBaze: Parametar @IDOsobe = {idOsobe}");

                    konekcija.Open();
                    var rezultat = cmd.ExecuteScalar();
                    Debug.WriteLine($"DohvatiNajnovijuSemuIzBaze: ExecuteScalar vratio = '{rezultat}'");

                    if (rezultat != null && rezultat != DBNull.Value)
                        sema = rezultat.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DohvatiNajnovijuSemuIzBaze ERROR: {ex}");
            }

            return sema;
        }

        private void PrimeniSemuNaDugmad(string semaString)
        {
            Debug.WriteLine($"PrimeniSemuNaDugmad: primam semaString = '{semaString}'");
            ResetujBojeDugmadi();
            _entries.Clear();

            if (string.IsNullOrWhiteSpace(semaString))
            {
                Debug.WriteLine("PrimeniSemuNaDugmad: semaString je prazan, ništa za raditi");
                return;
            }

            var podaci = semaString.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Debug.WriteLine($"PrimeniSemuNaDugmad: splitovano na {podaci.Length} zapisa");

            foreach (var zapis in podaci)
            {
                Debug.WriteLine($"PrimeniSemuNaDugmad: procesiram zapis='{zapis}'");
                var delovi = zapis.Trim().Split('.');
                if (delovi.Length != 3)
                {
                    Debug.WriteLine($"PrimeniSemuNaDugmad: nevažeći zapis, preskačem: '{zapis}'");
                    continue;
                }

                string tag = $"{delovi[0]}.{delovi[1]}";
                string intervencija = delovi[2];
                Debug.WriteLine($"PrimeniSemuNaDugmad: tag={tag}, intervencija={intervencija}");

                var dugme = NadjiDugmePoTagu(tag);
                if (dugme == null)
                {
                    Debug.WriteLine($"PrimeniSemuNaDugmad: dugme za tag '{tag}' nije pronađeno");
                    continue;
                }

                if (_intervencijaBoje.TryGetValue(intervencija, out Color boja))
                {
                    dugme.BackColor = boja;
                    var entry = $"{tag}.{intervencija}";
                    Debug.WriteLine($"PrimeniSemuNaDugmad: dodajem entry='{entry}'");
                    if (!_entries.Contains(entry))
                        _entries.Add(entry);
                }
                else
                {
                    Debug.WriteLine($"PrimeniSemuNaDugmadi: boja za intervenciju '{intervencija}' nije pronađena");
                }
            }
        }

        private void ResetujBojeDugmadi()
        {
            Debug.WriteLine("ResetujBojeDugmadi: resetujem boje svih dugmadi");
            var defaultColor = _intervencijaBoje.TryGetValue("Zub", out Color dc) ? dc : Color.White;
            foreach (var btn in tableLayoutPanel1.Controls.OfType<Button>())
                if (btn.Tag != null) btn.BackColor = defaultColor;
        }

        private Button NadjiDugmePoTagu(string tag)
        {
            var dugme = tableLayoutPanel1.Controls.OfType<Button>()
                        .FirstOrDefault(btn => btn.Tag?.ToString().Equals(tag, StringComparison.OrdinalIgnoreCase) == true);
            Debug.WriteLine($"NadjiDugmePoTagu: tražim '{tag}', pronađeno: {dugme != null}");
            return dugme;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is Button btn)) return;
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            trenutnoKliknutoDugme = btn;
            if (btn.ContextMenuStrip == null) btn.ContextMenuStrip = contextMenu;
            contextMenu.Show(btn, new Point(0, btn.Height));
        }

        private void AddMenuItem(string text, string intervencija, int r, int g, int b)
        {
            var bmp = new Bitmap(16, 16);
            using (var gfx = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(Color.FromArgb(r, g, b)))
                gfx.FillRectangle(brush, 0, 0, 16, 16);
            var item = new ToolStripMenuItem(text, bmp);
            item.Click += (s, e) =>
            {
                Debug.WriteLine($"ContextMenu: klik na intervenciju '{intervencija}' za dugme '{trenutnoKliknutoDugme.Tag}'");
                trenutnoKliknutoDugme.BackColor = Color.FromArgb(r, g, b);
                var entryTag = trenutnoKliknutoDugme.Tag?.ToString() ?? string.Empty;
                var entry = $"{entryTag}.{intervencija}";
                Debug.WriteLine($"ContextMenu: entryTag={entryTag}, entry={entry}");
                _entries.RemoveAll(x => x.StartsWith(entryTag + "."));
                if (intervencija != "Zub" && intervencija != "Nema")
                {
                    Debug.WriteLine($"ContextMenu: dodajem entry '{entry}'");
                    _entries.Add(entry);
                }
            };
            contextMenu.Items.Add(item);
        }

        private void AddDefaultMenuItem()
        {
            var item = new ToolStripMenuItem("Vrati na default");
            item.Click += (s, e) =>
            {
                Debug.WriteLine($"ContextMenu: vraćam default za dugme '{trenutnoKliknutoDugme.Tag}'");
                var defaultColor = _intervencijaBoje.TryGetValue("Zub", out Color dc) ? dc : Color.White;
                trenutnoKliknutoDugme.BackColor = defaultColor;
                var entryTag = trenutnoKliknutoDugme.Tag?.ToString() ?? string.Empty;
                var removed = _entries.RemoveAll(x => x.StartsWith(entryTag + "."));
                Debug.WriteLine($"ContextMenu: uklonjeno {removed} entry-ja za tag {entryTag}");
            };
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(item);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("siticoneButton1_Click: korisnik završava izbor šeme");
            if (_entries != null && _entries.Count > 0)
            {
                RadjeniZubiString = string.Join(", ", _entries.Distinct());
                Debug.WriteLine($"siticoneButton1_Click: RadjeniZubiString = '{RadjeniZubiString}'");
            }
            else
            {
                RadjeniZubiString = string.Empty;
                Debug.WriteLine("siticioneButton1_Click: nema unosa, vraćam prazan string");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
