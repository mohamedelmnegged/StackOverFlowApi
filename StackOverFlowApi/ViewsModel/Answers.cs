using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.ViewsModel
{
    public class Answers
    {
        public string AnswerBody { get; set; }
        public int AnswerUpvoted { get; set; }
        public int AnswerDownvoted { get; set; }
        public string UserId { get; set; }

    }
}
