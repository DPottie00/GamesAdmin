namespace GamesAdmin.Models
{
    public class Developer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
