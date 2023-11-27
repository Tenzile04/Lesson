﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PustokBookCrud.Models
{
    public class Slider 
    {
        public int Id {  get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Description { get; set; }
        [Required]

        public string RedirectUrl { get; set; }
        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string RedirecText { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
