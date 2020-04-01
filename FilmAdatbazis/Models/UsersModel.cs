using System.ComponentModel.DataAnnotations;

namespace FilmAdatbazis
{
    /// <summary>
    /// Model a Users tábla kezeléséhez
    /// </summary>
    public partial class UsersModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
