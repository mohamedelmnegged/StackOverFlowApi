using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.ViewsModel
{
    public class Responces
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
    }
}
