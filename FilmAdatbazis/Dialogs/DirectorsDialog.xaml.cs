using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilmAdatbazis.Dialogs
{
    /// <summary>
    /// DialogBox rendező létrehozására vagy szerkesztésére
    /// </summary>
    public partial class DirectorsDialog : Window
    {
        public int directorid;

        // Konstruktor
        public DirectorsDialog(DirectorsModel? director)
        {
            InitializeComponent();
            // Szerkesztés vagy új létrehozása
            if (director != null)
            {
                // Dialog cím megváltoztatása és a megadott rekord adatainak betöltése a controlokba
                Title = "Rendező szerkesztése";
                directorTextBox.Text = director.Director;
                directorid = director.Id;
            }
            else
            {
                // Dialog cím megváltoztatása
                Title = "Új rendező";
            }
        }

        // Eseménykezelő a mentés gombhoz
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            // Validációs eljárás meghívása a bevitt adatok ellenőrzésére
            // A rendezőnév textbox null értékének ellenőrzése
            if (directorTextBox.Text == null || directorTextBox.Text == "")
            {
                directorTextBox.Text = "  ";
            }
            // Ha rossz adatok akkor kilép
            if (!IsValid(this)) return;
            else
            {
                // Ha jók a bevitt adatok, akkor vissza az adatbáziskezelő ablakba true eredménnyel
                DialogResult = true;
            }
        }

        // Segédfüggvény a beviteli mezők végigjárására és validálására
        bool IsValid(DependencyObject node)
        {
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
