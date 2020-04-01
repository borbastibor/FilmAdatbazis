using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmAdatbazis
{
    /// <summary>
    /// Model a Genre tábla kezeléséhez
    /// </summary>
    public partial class GenreModel
    {
        public GenreModel()
        {
            Movie = new HashSet<MovieModel>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }

        public virtual ICollection<MovieModel> Movie { get; set; }
    }
}
