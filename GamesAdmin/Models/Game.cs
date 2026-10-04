using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace GamesAdmin.Models
{
    public class Game
    {
        [Required]
        public int Id { get; set; } //Auto Increment Unique ID

        [Required]
        [StringLength(512)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2048)]
        public string Summary {  get; set; } = string.Empty;

        [Required]
        [StringLength(32)]
        public string Genre {  get; set; } = string.Empty;  //One Genre per game for now

        [StringLength(32)]
        public string Rating {  get; set; } = string.Empty;   //PEGI/ESRB??????

        [Range(0, 100000)]
        [Display(Name = "Time to Beat in Minutes")]
        public int TimeToBeat { get; set; } //Average In Minutes, Casual Play

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }  //Initial Release -- Think about Early Access titles in future?

        [Required]
        public string Developer {  get; set; } = string.Empty; //One Dev per game for now

        [Required]
        public string Platform {  get; set; } = string.Empty; //One Platform per game for now
    }
}
