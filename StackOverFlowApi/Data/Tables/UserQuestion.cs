using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class UserQuestion
    {
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public byte Type { get; set; }

    }
}
