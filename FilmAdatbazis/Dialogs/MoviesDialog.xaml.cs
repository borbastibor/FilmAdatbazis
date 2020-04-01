using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilmAdatbazis.Dialogs
{
    /// <summary>
    /// DialogBox film létrehozására vagy szerkesztésére
    /// </summary>
    public partial class MoviesDialog : Window
    {
        public int movieid;
        private List<DirectorsModel> directorlist;
        private List<GenreModel> genrelist;

        // Konstruktor
        public MoviesDialog(List<DirectorsModel> dirlist, List<GenreModel> genlist, MovieModel? movie)
        {
            InitializeComponent();
            directorlist = dirlist;
            genrelist = genlist;
            // Adatok előkészítése a combobox-okhoz
            List<string> dirnamelist = new List<string>();
            foreach (DirectorsModel item in directorlist)
            {
                dirnamelist.Add(item.Director);
            }
            List<string> genrenamelist = new List<string>();
            foreach (GenreModel item in genrelist)
            {
                genrenamelist.Add(item.Genre);
            }
            // Combobox-ok feltöltése adatokkal
            dirComboBox.ItemsSource = dirnamelist;
            genreComboBox.ItemsSource = genrenamelist;
            // Szerkesztés vagy új létrehozása
            if (movie != null)
            {
                // Dialog cím megváltoztatása és a megadott rekord adatainak betöltése a controlokba
                Title = "Film szerkesztése";
                movieid = movie.Id;
                titleTextBox.Text = movie.Title;
                if (movie.ReleaseYear.HasValue)
                {
                    yearTextBox.Text = movie.ReleaseYear.ToString();
                }
                if (movie.Genre != null)
                {
                    genreComboBox.SelectedItem = movie.Genre.Genre;
                }
                if (movie.Director != null)
                {
                    dirComboBox.SelectedItem = movie.Director.Director;
                }
                durationTextBox.Text = movie.Duration.ToString();
            }
            else
            {
                // Dialog cím megváltoztatása
                Title = "Új film";
                dirComboBox.SelectedIndex = 0;
                genreComboBox.SelectedIndex = 0;
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
