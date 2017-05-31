using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BotByePOC.Models;

namespace BotByePOC.Models
{
    public class BotByePOCContext : DbContext
    {
        public BotByePOCContext (DbContextOptions<BotByePOCContext> options)
            : base(options)
        {
        }

        public DbSet<BotByePOC.Models.Policy> Policy { get; set; }

        public DbSet<BotByePOC.Models.PolicyLocation> PolicyLocation { get; set; }

        public DbSet<BotByePOC.Models.Outage> Outage { get; set; }
    }
}
