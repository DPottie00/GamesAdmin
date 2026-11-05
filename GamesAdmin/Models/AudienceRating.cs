using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace GamesAdmin.Models
{
    [Authorize]
    public class AudienceRating
    {
        public int Id { get; set; }

        //Nabigation
        [ValidateNever]
        public List<Game> Games { get; set; } = new List<Game>();

        [Display(Name = "ESRB Rating")]
        public string Title { get; set; } = string.Empty;
    }
}
