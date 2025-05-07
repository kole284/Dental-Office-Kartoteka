using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DataBaseProgram
{
    static class Program
    {
        private static Mutex mutex = null;
        public static string konekcioniString;

        [STAThread]
        static void Main()

        {
            const string appName = "Stomatoloska Ordinacija Kartoteka";  // Unikatno ime tvoje aplikacije
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                // Ako već postoji instanca, samo izađi
                MessageBox.Show("Program je već pokrenut.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Putanja do baze koja se nalazi u istom folderu kao i .exe
            string bazaNaziv = "DataBase.accdb";
            string bazaPutanja = Path.Combine(Application.StartupPath, bazaNaziv);

            // Ako baza ne postoji u folderu aplikacije, obavesti korisnika
            if (!File.Exists(bazaPutanja))
            {
                MessageBox.Show("Baza podataka nije pronađena u aplikaciji.");
                return;
            }

            // Connection string za direktnu bazu
            konekcioniString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={bazaPutanja};Persist Security Info=False;";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
