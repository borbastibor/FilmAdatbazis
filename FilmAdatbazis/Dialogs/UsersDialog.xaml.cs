using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilmAdatbazis.Dialogs
{
    /// <summary>
    /// DialogBox felhasználó létrehozására vagy szerkesztésére
    /// </summary>
    public partial class UsersDialog : Window
    {
        public int userid;

        // Konstruktor
        public UsersDialog(UsersModel? user)
        {
            InitializeComponent();
            // Szerkesztés vagy új létrehozása
            if (user != null)
            {
                // Dialog cím megváltoztatása és adatok betöltése a controlokba
                Title = "Felhasználó szerkesztése";
                unameTextBox.Text = user.Username;
                pswdTextBox.Text = user.Password;
                userid = user.Id;
            } else
            {
                // Dialog cím megváltoztatása
                Title = "Új felhasználó";
            }
        }

        // Eseménykezelő a Mentés gombhoz
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            // Ha érvényes az adatbevitel, akkor true-t ad vissza és visszatér az adatbáziskezelő ablakhoz
            if (!IsValid(this)) return;
            else
            {
                this.DialogResult = true;
            }
        }

        // Segédfüggvény a bevit adatok validációjához
        bool IsValid(DependencyObject node)
        {
            // Végig járja a beviteli mezőket és validálja őket
            if (node != null)
            {
                bool isValid = !Validation.GetHasError(node);
                if (!isValid)
                {
                    if (node is IInputElement) Keyboard.Focus((IInputElement)node);
                    return false;
                }
            }

            foreach (object subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject)
                {
                    if (IsValid((DependencyObject)subnode) == false) return false;
                }
            }
            return true;
        }
    }
}
