using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotByePOC.Models
{
    public class Policy
    {
        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public float PremiumAmount { get; set; }
        public string InsuredName { get; set; }
    }

    public class PolicyLocation
    {
        public int PolicyID { get; set; }
        public int PolicyLocationID { get; set; }
        public int LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
    }

    public class Outage
    {
        public int OutageID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Application { get; set; }
        public string Description { get; set; }
    }
}
