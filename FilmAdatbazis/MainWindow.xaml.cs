using System.Linq;
using System.Windows;

namespace FilmAdatbazis
{
    /// <summary>
    /// Belépési ablak
    /// </summary>
    public partial class MainWindow : Window
    {
        // Referencia az adatbázishoz
        private readonly MovieCatalogContext context;

        // Konstruktor
        public MainWindow()
        {
            InitializeComponent();
            context = new MovieCatalogContext();
        }

        // Eseménykezelő a Belépés gombhoz
        private void OnClickButton(object sender, RoutedEventArgs e)
        {
            // Felhasználónév és jelszó ellenőrzése
            string pswd = password.Password;
            string usr = username.Text;
            var userRecord = context.Users.SingleOrDefault(u => u.Username == usr);
            if (userRecord != null && userRecord.Password == pswd)
            {
                DbManagerWindow dbManagerWindow = new DbManagerWindow();
                dbManagerWindow.Show();
                Close();
            } else
            {
                string messageBoxText = "Hibás felhasználónév vagy jelszó!";
                string caption = "Hiba";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
