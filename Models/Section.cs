using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class Section
    {
        [Key]
        public int ID { get; set; }

        public string NameAC { get; set; }
    }
}