using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class User : IdentityUser
    {
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public int Views { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public ICollection<Question> Questions { get; set; }
        public ICollection<UserQuestion> UserQuestions { get; set; }
        public ICollection<Websites> Websites { get; set; }
        public ICollection<Followers> Followers { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Actions> actions { get; set; }
        public ICollection<Tag> tags { get; set; }
    }
}
