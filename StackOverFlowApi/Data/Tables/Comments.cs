using StackOverFlowApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class Comments
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int Upvoted { get; set; } = 0;
        public int Downvoted { get; set; } = 0;
        public Flag Flag { get; set; } = Flag.comment;
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public int QuestionId { get; set; }
        public string UserId { get; set; }

    }
}
