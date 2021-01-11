using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class SectionRole
    {
        [Key]
        public int ID { get; set; }

        public string Role { get; set; }
    }
}