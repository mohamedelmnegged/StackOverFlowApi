using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string UserId { get; set; }
        public ICollection<QuestionTag> questionsTags { get; set; }
    }
}
