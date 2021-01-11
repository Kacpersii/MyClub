using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Temat")]
        public String Subject { get; set; }

        [Required]
        [Display(Name = "Treść")]
        public String Content { get; set; }

        public virtual User User { get; set; }
    }
}