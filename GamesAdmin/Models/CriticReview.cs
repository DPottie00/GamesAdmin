using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GamesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }

        //foreign key
        [Display(Name = "Game")]
        public int GameId { get; set; }

        [ValidateNever]
        [ScaffoldColumn(false)]
        public Game Game { get; set; } = null!;

        [Display(Name = "Review")]
        [MaxLength(2056)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0,5)]
        [Display(Name = "Rating (1 - 5)")]
        public int Rating { get; set; }

        [Display(Name = "Published?")]
        public bool isPublished { get; set; } = false;

        [Display(Name = "Author")]
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Name = "Review Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
