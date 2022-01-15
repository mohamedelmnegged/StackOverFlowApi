using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.ViewsModel
{
    public class HomeQuestions
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public int QuestionDownvoted { get; set; }
        public int QuestionUpvoted { get; set; }
        public int QuestionViews { get; set; }
        public IEnumerable<TagView> tags { get; set; }
        //public int TagId { get; set; }
        //public string TagName { get; set; }

    }
}
