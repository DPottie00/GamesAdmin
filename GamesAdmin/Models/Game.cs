namespace GamesAdmin.Models
{
    public class Game
    {

        public int Id { get; set; } //Auto Increment Unique ID

        public string Title { get; set; } = string.Empty;

        public string Summary {  get; set; } = string.Empty;

        public string Genre {  get; set; } = string.Empty;  //One Genre per game for now

        public string Rating {  get; set; } = string.Empty;   //PEGI/ESRB??????

        public int TimeToBeat { get; set; } //Average In Minutes, Casual Play

        public DateTime ReleaseDate { get; set; }  //Initial Release -- Think about Early Access titles in future?

        public string Developer {  get; set; } = string.Empty; //One Dev per game for now

        public string Platform {  get; set; } = string.Empty; //One Platform per game for now


    }
}
