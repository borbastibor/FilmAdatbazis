using System.ComponentModel.DataAnnotations;

namespace FilmAdatbazis
{
    /// <summary>
    /// Model a Movie tábla kezeléséhez
    /// </summary>
    public partial class MovieModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public int? GenreId { get; set; }
        public int? DirectorId { get; set; }
        public int? Duration { get; set; }

        public virtual DirectorsModel Director { get; set; }
        public virtual GenreModel Genre { get; set; }
    }
}
