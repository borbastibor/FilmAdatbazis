using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilmAdatbazis.Dialogs
{
    /// <summary>
    /// DialogBox kategória létrehozására vagy szerkesztésére
    /// </summary>
    public partial class GenreDialog : Window
    {
        public int genreid;

        // Konstruktor
        public GenreDialog(GenreModel? genre)
        {
            InitializeComponent();
            // Szerkesztés vagy új létrehozása
            if (genre != null)
            {
                // Dialog cím megváltoztatása és a megadott rekord adatainak betöltése a controlokba
                Title = "Kategória szerkesztése";
                genreTextBox.Text = genre.Genre;
                genreid = genre.Id;
            }
            else
            {
                // Dialog cím megváltoztatása
                Title = "Új kategória";
            }
        }

        // Eseménykezelő a Mentés gombhoz
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (genreTextBox.Text == null || genreTextBox.Text == "")
            {
                genreTextBox.Text = "  ";
            }
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
