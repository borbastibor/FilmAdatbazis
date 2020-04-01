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

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsValid(this)) return;
            else
            {
                this.DialogResult = true;
            }
        }

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
