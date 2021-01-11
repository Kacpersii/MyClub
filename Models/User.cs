using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public String UserName { get; set; }



        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "Imie")]
        public String Name { get; set; }



        [StringLength(50)]
        [Display(Name = "Nazwisko")]
        public String Surname { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Numer telefonu")]
        public int Phone { get; set; }


        public virtual List<Article> Articles { get; set; }
        public virtual List<Answer> Answers { get; set; }

    }
}