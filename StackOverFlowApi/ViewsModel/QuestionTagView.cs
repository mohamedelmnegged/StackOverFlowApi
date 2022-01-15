using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.ViewsModel
{
    public class QuestionTagView
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public User QuestionUser { get; set; }
    }
}
