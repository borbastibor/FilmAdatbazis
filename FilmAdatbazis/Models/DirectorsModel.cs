using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmAdatbazis
{
    /// <summary>
    /// Model a Directors tábla kezeléséhez
    /// </summary>
    public partial class DirectorsModel
    {
        public DirectorsModel()
        {
            Movie = new HashSet<MovieModel>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Director { get; set; }

        public virtual ICollection<MovieModel> Movie { get; set; }
    }
}
