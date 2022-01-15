using StackOverFlowApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Tables
{
    public class Actions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public status Status { get; set; }
        public ActionName ActionName { get; set; }
        public string ActionLink { get; set; }
        public string UserId { get; set; }
    }
}
