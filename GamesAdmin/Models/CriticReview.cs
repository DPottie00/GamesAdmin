using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GamesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }

        //foreign key
        public int GameId { get; set; }

        [ValidateNever]
        public Game Game { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        public bool isPublished { get; set; } = false;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
