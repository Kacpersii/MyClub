using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        [ForeignKey("Article")]
        public int ArticleID { get; set; }

        [Required]
        public string User_answer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data opublikowania")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}