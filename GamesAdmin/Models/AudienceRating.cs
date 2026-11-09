using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace GamesAdmin.Models
{

    // FROM https://code-maze.com/efcore-add-unique-constraints-to-a-property-code-first/
    [Index(nameof(Order), IsUnique = true)]
    public class AudienceRating
    {
        public int Id { get; set; }

        //Nabigation
        [ValidateNever]
        public List<Game> Games { get; set; } = new List<Game>();

        [Display(Name = "ESRB Rating")]
        public string Title { get; set; } = string.Empty;

        public int Order { get; set; }
    }
}
