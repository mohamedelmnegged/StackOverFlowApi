using StackOverFlowApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Upvoted { get; set; }
        public int Downvoted { get; set; }
        public bool Enable { get; set; }
        public bool Answered { get; set; }
        public status Status { get; set; }
        public int Views { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public User User { get; set; }
        public ICollection<UserQuestion> UserQuestions { get; set; }
        public ICollection<QuestionTag> questionsTags { get; set; }
    }
}
