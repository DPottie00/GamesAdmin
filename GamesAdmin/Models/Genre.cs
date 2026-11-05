using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace GamesAdmin.Models
{
    public class Genre
    {
        public int Id { get; set; }

        //Nabigation
        [ValidateNever]
        public List<Game> Games { get; set; } = new List<Game>();

        [Display(Name = "Genre")]
        public string Title { get; set; } = string.Empty;
    }
}
