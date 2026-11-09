using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace GamesAdmin.Models
{
    public class Game
    {
        //Primary Key
        [Required]
        public int Id { get; set; } //Auto Increment Unique ID

        //ForeignKey
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [ValidateNever]
        [Display(Name = "Genre")]
        public Genre Genre { get; set; } = null!;

        [Display(Name = "ESRB Rating")]
        public int AudienceRatingId { get; set; }

        [ValidateNever]
        [Display(Name = "ESRB Rating")]
        public AudienceRating AudienceRating { get; set; } = null!;

        public int PlatformId { get; set; }
        [ValidateNever]
        public Platform Platform { get; set; } = null!;

        public int DeveloperId { get; set; }
        [ValidateNever]
        public Developer Developer { get; set; } = null!;

        [ValidateNever]
        public List<CriticReview> CriticReviews { get; set; } = new List<CriticReview>();

        [Required]
        [StringLength(512)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2048)]
        public string Summary {  get; set; } = string.Empty;


        [Range(0, 100000)]
        [Display(Name = "Time to Beat in Minutes")]
        public int? TimeToBeat { get; set; } //Average In Minutes, Casual Play

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }  //Initial Release -- Think about Early Access titles in future?


        public string ImageUrl { get; set; } = string.Empty;

    }
}
