using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyClub.Models
{
    public class Participant
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Section")]
        public int SectionID { get; set; }

        [ForeignKey("SectionRole")]
        public int SectionRoleID { get; set; }

        public virtual User User { get; set; }
        public virtual SectionRole SectionRole { get; set; }
        public virtual Section Section { get; set; }

    }
}