using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GamesAdmin.Models
{
    public class AudienceRating
    {
        public int Id { get; set; }

        //Nabigation
        [ValidateNever]
        public List<Game> Games { get; set; } = new List<Game>();
        
        public string Title { get; set; } = string.Empty;
    }
}
