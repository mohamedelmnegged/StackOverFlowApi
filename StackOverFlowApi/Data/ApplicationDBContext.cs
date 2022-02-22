using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using StackOverFlowApi.Data.Tables;

namespace StackOverFlowApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        { 

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Followers>().HasKey(k => new {k.UserId, k.FollowingId });
            builder.Entity<UserQuestion>().HasKey(k => new {k.UserId, k.QuestionId });
            builder.Entity<QuestionTag>().HasKey(k => new {k.TagId, k.QuestionId });
            //builder.Entity<User>().HasData(new User { });
           
        }
        public DbSet<User> OwnUser { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserQuestion> UserQuestions { get; set; }
        public DbSet<Websites> Websites { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<QuestionTag> questionsTags { get; set; }
    }
}
