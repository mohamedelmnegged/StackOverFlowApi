using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class Websites
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebsiteIcon { get; set; }
        public string WebsiteLink { get; set; }
        public string UserId{ get; set; }

    }
}
