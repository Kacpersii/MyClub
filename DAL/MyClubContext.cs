using MyClub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyClub.DAL
{
    public class MyClubContext : DbContext
    {
        public MyClubContext() : base("MySprotsClubContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<SectionRole> SectionRoles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<IndividualTraining> IndividualTrainings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Article>().HasRequired<User>(a => a.User)
                .WithMany(u => u.Articles).HasForeignKey(a => a.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IndividualTraining>().HasRequired<User>(i => i.User)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}