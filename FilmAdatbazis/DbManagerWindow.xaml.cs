using FilmAdatbazis.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FilmAdatbazis
{
    /// <summary>
    /// Az adatbáziskezelő ablak
    /// </summary>
    public partial class DbManagerWindow : Window
    {
        private readonly MovieCatalogContext context;

        // Konstruktor
        public DbManagerWindow()
        {
            InitializeComponent();
            context = new MovieCatalogContext();
        }

        // Eseménykezelő - A felhasználók fül betöltődött
        private void OnUsersLoaded(object sender, RoutedEventArgs e)
        {
            ReloadUsersListViewContent();
        }

        // Eseménykezelő - A kategóriák fül betöltődött
        private void OnGenreLoaded(object sender, RoutedEventArgs e)
        {
            ReloadGenreListViewContent();
        }

        // Eseménykezelő - A filmek fül betöltődött
        private void OnFilmsLoaded(object sender, RoutedEventArgs e)
        {
            ReloadFilmListViewContent();
        }

        // Eseménykezelő - A rendezők fül betöltődött
        private void OnDirectorsLoaded(object sender, RoutedEventArgs e)
        {
            ReloadDirListViewContent();
        }

        // Belső függvény, ami újratölti az felhasználók listview tartalmát
        private void ReloadUsersListViewContent()
        {
            List<UsersModel> userslist = context.Users.ToList();
            UsersListView.ItemsSource = userslist;
        }

        // Belső függvény, ami újratölti a kategóriák listview tartalmát
        private void ReloadGenreListViewContent()
        {
            List<GenreModel> genrelist = context.Genre.ToList();
            GenreListView.ItemsSource = genrelist;
        }

        // Belső függvény, ami újratölti a rendezők listview tartalmát
        private void ReloadDirListViewContent()
        {
            List<DirectorsModel> dirlist = context.Directors.ToList();
            DirListView.ItemsSource = dirlist;
        }

        // Belső függvény, ami újratölti a film listview tartalmát
        private void ReloadFilmListViewContent()
        {
            List<MovieModel> filmlist = context.Movie
                .Include(movie => movie.Genre)
                .Include(movie => movie.Director)
                .ToList();
            FilmListView.ItemsSource = filmlist;
        }

        // A felhasználók fülön szövegbevitel történt a keresőbe
        private void OnUsersTextInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Ha enter-t nyomtak, akkor keresés
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Keresés a username és password mezőkben
                string searchstr = "%" + UsersSearchText.Text + "%";
                List<UsersModel> userslist = context.Users
                    .Where(u => EF.Functions.Like(u.Username, searchstr) || EF.Functions.Like(u.Password, searchstr))
                    .ToList();
                UsersListView.ItemsSource = null;
                // Eredménylista átadása a ListView-nak
                UsersListView.ItemsSource = userslist;
            }
            
        }

        // A kategóriák fülön szövegbevitel történt a keresőbe
        private void OnGenreTextInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Ha enter-t nyomtak, akkor keresés
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Keresés a genre mezőben
                string searchstr = "%" + GenreSearchText.Text + "%";
                List<GenreModel> genrelist = context.Genre
                    .Where(g => EF.Functions.Like(g.Genre, searchstr))
                    .ToList();
                GenreListView.ItemsSource = null;
                // Eredménylista átadása a ListView-nak
                GenreListView.ItemsSource = genrelist;
            }
        }

        // A rendezők fülön szövegbevitel történt a keresőbe
        private void OnDirTextInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Ha enter-t nyomtak, akkor keresés
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Keresés a director mezőben
                string searchstr = "%" + DirSearchText.Text + "%";
                List<DirectorsModel> dirlist = context.Directors
                    .Where(d => EF.Functions.Like(d.Director, searchstr))
                    .ToList();
                DirListView.ItemsSource = null;
                // Eredménylista átadása a ListView-nak
                DirListView.ItemsSource = dirlist;
            }
        }

        // A filmek fülön szövegbevitel történt a keresőbe
        private void OnFilmTextInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Ha enter-t nyomtak, akkor keresés
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Keresés a title, releaseyear, genre, director, duration mezőkben
                string searchstr = "%" + FilmSearchText.Text + "%";
                List<MovieModel> movielist = context.Movie
                    .Include(m => m.Genre)
                    .Include(m => m.Director)
                    .Where(m => EF.Functions.Like(m.Title, searchstr) ||
                    EF.Functions.Like(m.ReleaseYear.ToString(), searchstr) || 
                    EF.Functions.Like(m.Genre.Genre, searchstr) ||
                    EF.Functions.Like(m.Director.Director, searchstr) ||
                    EF.Functions.Like(m.Duration.ToString(), searchstr))
                    .ToList();
                FilmListView.ItemsSource = null;
                // Eredménylista átadása a ListView-nak
                FilmListView.ItemsSource = movielist;
            }
        }

        // Felugró menü szerkesztés klikk - Felhasználó adatainak szerkesztése
        private void EditUserCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            EditUserRecord();
        }

        // Felugró menü törlés klikk - Felhasználó törlése
        private void DeleteUserCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteUserRecord();
        }

        // Felugró menü létrehozás klikk - Felhasználó létrehozása
        private void CreateUserCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            CreateUserRecord();
        }

        // Felugró menü szerkesztés klikk - Kategória adatainak szerkesztése
        private void EditGenreCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            EditGenreRecord();
        }

        // Felugró menü törlés klikk - Kategória törlése
        private void DeleteGenreCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteGenreRecord();
        }

        // Felugró menü létrehozás klikk - Kategória létrehozása
        private void CreateGenreCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            CreateGenreRecord();
        }

        // Felugró menü szerkesztés klikk - Rendező adatainak szerkesztése
        private void EditDirCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            EditDirRecord();
        }

        // Felugró menü törlés klikk - Rendező törlése
        private void DeleteDirCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteDirRecord();
        }

        // Felugró menü létrehozás klikk - Rendező létrehozása
        private void CreateDirCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            CreateDirRecord();
        }

        // Felugró menü szerkesztés klikk - Film adatainak szerkesztése
        private void EditFilmCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            EditFilmRecord();
        }

        // Felugró menü törlés klikk - Film törlése
        private void DeleteFilmCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteFilmRecord();
        }

        // Felugró menü létrehozás klikk - Új film létrehozása
        private void CreateFilmCMenu_OnClick(object sender, RoutedEventArgs e)
        {
            CreateFilmRecord();
        }

        // Szöveg kijelölése a keresőmezőben tabulátorra
        private void OnGotKbdFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            // Minden tabon megtalálható keresőmező ezt az eseménykezelőt használja
            TextBox tb = (TextBox)sender;
            switch (tb.Name)
            {
                case "FilmSearchText":
                    FilmSearchText.SelectAll();
                    break;

                case "DirSearchText":
                    DirSearchText.SelectAll();
                    break;

                case "GenreSearchText":
                    GenreSearchText.SelectAll();
                    break;

                case "UsersSearchText":
                    UsersSearchText.SelectAll();
                    break;
            }
        }

        // Szöveg kijelölése a keresőmezőben egér kattintásra
        private void OnGotMouseFocus(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Minden tabon megtalálható keresőmező ezt az eseménykezelőt használja
            TextBox tb = (TextBox)sender;
            switch (tb.Name)
            {
                case "FilmSearchText":
                    FilmSearchText.SelectAll();
                    break;

                case "DirSearchText":
                    DirSearchText.SelectAll();
                    break;

                case "GenreSearchText":
                    GenreSearchText.SelectAll();
                    break;

                case "UsersSearchText":
                    UsersSearchText.SelectAll();
                    break;
            }
        }

        // Klikk az Add ToolBar Button-ra
        private void OnClick_AddToolBtn(object sender, RoutedEventArgs e)
        {
            TabItem ti = TabCtrl.SelectedItem as TabItem;
            switch (ti.Name)
            {
                case "FilmTabItem":
                    CreateFilmRecord();
                    break;

                case "DirTabItem":
                    CreateDirRecord();
                    break;

                case "GenreTabItem":
                    CreateGenreRecord();
                    break;

                case "UsersTabItem":
                    CreateUserRecord();
                    break;
            }
        }

        // Klikk az Edit ToolBar Button-ra
        private void OnClick_EditToolBtn(object sender, RoutedEventArgs e)
        {
            TabItem ti = TabCtrl.SelectedItem as TabItem;
            switch (ti.Name)
            {
                case "FilmTabItem":
                    if (FilmListView.SelectedItem != null)
                    {
                        EditFilmRecord();
                    }
                    break;

                case "DirTabItem":
                    if (DirListView.SelectedItem != null)
                    {
                        EditDirRecord();
                    }
                    break;

                case "GenreTabItem":
                    if (GenreListView.SelectedItem != null)
                    {
                        EditGenreRecord();
                    }
                    break;

                case "UsersTabItem":
                    if (UsersListView.SelectedItem != null)
                    {
                        EditUserRecord();
                    }
                    break;
            }
        }

        // Klikk a Delete ToolBar Button-ra
        private void OnClick_DeleteToolBtn(object sender, RoutedEventArgs e)
        {
            TabItem ti = TabCtrl.SelectedItem as TabItem;
            switch (ti.Name)
            {
                case "FilmTabItem":
                    if (FilmListView.SelectedItem != null)
                    {
                        DeleteFilmRecord();
                    }
                    break;

                case "DirTabItem":
                    if (DirListView.SelectedItem != null)
                    {
                        DeleteDirRecord();
                    }
                    break;

                case "GenreTabItem":
                    if (GenreListView.SelectedItem != null)
                    {
                        DeleteGenreRecord();
                    }
                    break;

                case "UsersTabItem":
                    if (UsersListView.SelectedItem != null)
                    {
                        DeleteUserRecord();
                    }
                    break;
            }
        }

        // Új User rekord létrehozása
        private void CreateUserRecord()
        {
            // Dialog megjelenítése az adatok bekéréséhez
            UsersDialog usersdlg = new UsersDialog(null);
            usersdlg.Owner = this;
            usersdlg.ShowDialog();
            // Mentés esetén mentjük az adatokat az adatbázisba
            if ((bool)usersdlg.DialogResult)
            {
                UsersModel newuser = new UsersModel()
                {
                    Username = usersdlg.unameTextBox.Text,
                    Password = usersdlg.pswdTextBox.Text
                };
                context.Users.Add(newuser);
                context.SaveChanges();
            }
            ReloadUsersListViewContent();
        }

        // kiválasztott User rekord szerkesztése
        private void EditUserRecord()
        {
            // Dialog megjelenítése az adatok módosításához
            UsersModel selecteduser = (UsersModel)UsersListView.SelectedItem;
            UsersDialog usersdlg = new UsersDialog(selecteduser);
            usersdlg.Owner = this;
            usersdlg.ShowDialog();
            // Mentés esetén frissítjük az adatokat az adatbázisban
            if ((bool)usersdlg.DialogResult)
            {
                var upduser = context.Users.FirstOrDefault(u => u.Id == usersdlg.userid);
                upduser.Username = usersdlg.unameTextBox.Text;
                upduser.Password = usersdlg.pswdTextBox.Text;
                context.Users.Update(upduser);
                context.SaveChanges();
            }
            ReloadUsersListViewContent();
        }

        // Kiválasztott User rekord törlése
        private void DeleteUserRecord()
        {
            // Messagebox kérdéssel a törlés megerősítéséhez
            UsersModel selecteduser = (UsersModel)UsersListView.SelectedItem;
            string messageBoxText = "Biztos törölni akarja a felhasználót?";
            string caption = "Felhasználó törlése";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Ha igen, akkor törlés
            if (result == MessageBoxResult.Yes)
            {
                context.Users.Remove(selecteduser);
                context.SaveChanges();
            }
            ReloadUsersListViewContent();
        }

        // Új Director rekord létrehozása
        private void CreateDirRecord()
        {
            // Dialog megjelenítése az adatok bekéréséhez
            DirectorsDialog dirdlg = new DirectorsDialog(null);
            dirdlg.Owner = this;
            dirdlg.ShowDialog();
            // Mentés esetén mentjük az adatokat az adatbázisba
            if ((bool)dirdlg.DialogResult)
            {
                DirectorsModel newdir = new DirectorsModel()
                {
                    Director = dirdlg.directorTextBox.Text
                };
                context.Directors.Add(newdir);
                context.SaveChanges();
            }
            ReloadDirListViewContent();
        }

        // Kiválasztott Director rekord szerkesztése
        private void EditDirRecord()
        {
            // Dialog megjelenítése az adatok módosításához
            DirectorsModel selecteddir = (DirectorsModel)DirListView.SelectedItem;
            DirectorsDialog dirdlg = new DirectorsDialog(selecteddir);
            dirdlg.Owner = this;
            dirdlg.ShowDialog();
            // Mentés esetén frissítjük az adatokat az adatbázisban
            if ((bool)dirdlg.DialogResult)
            {
                var upddir = context.Directors.FirstOrDefault(d => d.Id == dirdlg.directorid);
                upddir.Director = dirdlg.directorTextBox.Text;
                context.Directors.Update(upddir);
                context.SaveChanges();
            }
            ReloadDirListViewContent();
        }

        // Kiválasztott Director rekord törlése
        private void DeleteDirRecord()
        {
            // Messagebox kérdéssel a törlés megerősítésére
            DirectorsModel selecteddir = (DirectorsModel)DirListView.SelectedItem;
            string messageBoxText = "Biztos törölni akarja ezt a rendezőt?";
            string caption = "Rendező törlése";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Ha igen, akkor törlés
            if (result == MessageBoxResult.Yes)
            {
                context.Directors.Remove(selecteddir);
                context.SaveChanges();
            }
            ReloadDirListViewContent();
        }

        // Új Genre rekord létrehozása
        private void CreateGenreRecord()
        {
            // Dialog megjelenítése az adatok bekéréséhez
            GenreDialog genredlg = new GenreDialog(null);
            genredlg.Owner = this;
            genredlg.ShowDialog();
            // Mentés esetén mentjük az adatokat az adatbázisba
            if ((bool)genredlg.DialogResult)
            {
                GenreModel newgenre = new GenreModel()
                {
                    Genre = genredlg.genreTextBox.Text
                };
                context.Genre.Add(newgenre);
                context.SaveChanges();
            }
            ReloadGenreListViewContent();
        }

        // Kiválasztott Genre rekord szerkesztése
        private void EditGenreRecord()
        {
            // Dialog megjelenítése az adatok módosításához
            GenreModel selectedgenre = (GenreModel)GenreListView.SelectedItem;
            GenreDialog genredlg = new GenreDialog(selectedgenre);
            genredlg.Owner = this;
            genredlg.ShowDialog();
            // Mentés esetén frissítjük az adatokat az adatbázisban
            if ((bool)genredlg.DialogResult)
            {
                var updgenre = context.Genre.FirstOrDefault(g => g.Id == genredlg.genreid);
                updgenre.Genre = genredlg.genreTextBox.Text;
                context.Genre.Update(updgenre);
                context.SaveChanges();
            }
            ReloadGenreListViewContent();
        }

        // Kiválasztott Genre rekord törlése
        private void DeleteGenreRecord()
        {
            // Messagebox kérdéssel a törlés megerősítéséhez
            GenreModel selectedgenre = (GenreModel)GenreListView.SelectedItem;
            string messageBoxText = "Biztos törölni akarja ezt a kategóriát?";
            string caption = "Kategória törlése";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Ha igen, akkor törlés
            if (result == MessageBoxResult.Yes)
            {
                context.Genre.Remove(selectedgenre);
                context.SaveChanges();
            }
            ReloadGenreListViewContent();
        }

        // Új Movie rekord létrehozása
        private void CreateFilmRecord()
        {
            // Dialog megjelenítése az adatok bekéréséhez
            List<DirectorsModel> dirlist = context.Directors.ToList();
            List<GenreModel> genrelist = context.Genre.ToList();
            MoviesDialog moviedlg = new MoviesDialog(dirlist, genrelist, null);
            moviedlg.Owner = this;
            moviedlg.ShowDialog();
            // Mentés esetén frissítjük az adatokat az adatbázisban
            if ((bool)moviedlg.DialogResult)
            {
                MovieModel newmovie = new MovieModel();
                newmovie.Title = moviedlg.titleTextBox.Text;
                if (moviedlg.yearTextBox.Text != "")
                {
                    newmovie.ReleaseYear = int.Parse(moviedlg.yearTextBox.Text);
                }
                else
                {
                    newmovie.ReleaseYear = null;
                }
                string genreselect = moviedlg.genreComboBox.SelectedItem != null ? (string)moviedlg.genreComboBox.SelectedItem : null;
                string dirselect = moviedlg.dirComboBox.SelectedItem != null ? (string)moviedlg.dirComboBox.SelectedItem : null;
                newmovie.GenreId = genreselect != null ? context.Genre.Where(g => g.Genre == genreselect).AsNoTracking().FirstOrDefault().Id : (int?)null;
                newmovie.DirectorId = dirselect != null ? context.Directors.Where(d => d.Director == dirselect).AsNoTracking().FirstOrDefault().Id : (int?)null;
                if (moviedlg.durationTextBox.Text != "")
                {
                    newmovie.Duration = int.Parse(moviedlg.durationTextBox.Text);
                }
                else
                {
                    newmovie.Duration = null;
                }
                context.Movie.Add(newmovie);
                context.SaveChanges();
            }
            ReloadFilmListViewContent();
        }

        // Kiválasztott Movie rekord szerkesztése
        private void EditFilmRecord()
        {
            // Dialog megjelenítése az adatok módosításához
            List<DirectorsModel> dirlist = context.Directors.ToList();
            List<GenreModel> genrelist = context.Genre.ToList();
            MovieModel selectedfilm = (MovieModel)FilmListView.SelectedItem;
            MoviesDialog moviedlg = new MoviesDialog(dirlist, genrelist, selectedfilm);
            moviedlg.Owner = this;
            moviedlg.ShowDialog();
            // Mentés esetén frissítjük az adatokat az adatbázisban
            if ((bool)moviedlg.DialogResult)
            {
                MovieModel updmovie = context.Movie.FirstOrDefault(m => m.Id == moviedlg.movieid);
                updmovie.Title = moviedlg.titleTextBox.Text;
                if (moviedlg.yearTextBox.Text != "")
                {
                    updmovie.ReleaseYear = int.Parse(moviedlg.yearTextBox.Text);
                }
                else
                {
                    updmovie.ReleaseYear = null;
                }
                string genreselect = moviedlg.genreComboBox.SelectedItem != null ? (string)moviedlg.genreComboBox.SelectedItem : null;
                string dirselect = moviedlg.dirComboBox.SelectedItem != null ? (string)moviedlg.dirComboBox.SelectedItem : null;
                updmovie.GenreId = genreselect != null ? context.Genre.Where(g => g.Genre == genreselect).AsNoTracking().FirstOrDefault().Id : (int?)null;
                updmovie.DirectorId = dirselect != null ? context.Directors.Where(d => d.Director == dirselect).AsNoTracking().FirstOrDefault().Id : (int?)null;
                if (moviedlg.durationTextBox.Text != "")
                {
                    updmovie.Duration = int.Parse(moviedlg.durationTextBox.Text);
                }
                else
                {
                    updmovie.Duration = null;
                }
                context.Movie.Update(updmovie);
                context.SaveChanges();
            }
            ReloadFilmListViewContent();
        }

        // Kiválasztott Movie rekord törlése
        private void DeleteFilmRecord()
        {
            // Messagebox kérdéssel a törlés megerősítésére
            MovieModel selectedfilm = (MovieModel)FilmListView.SelectedItem;
            string messageBoxText = "Biztos törölni akarja ezt a filmet?";
            string caption = "Film törlése";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            // Ha igen, akkor törlés
            if (result == MessageBoxResult.Yes)
            {
                context.Movie.Remove(selectedfilm);
                context.SaveChanges();
            }
            ReloadFilmListViewContent();
        }
    }
}
