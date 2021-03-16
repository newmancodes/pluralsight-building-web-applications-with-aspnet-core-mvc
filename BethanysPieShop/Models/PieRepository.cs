using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext appDbContext;

        public IEnumerable<Pie> AllPies => this.appDbContext.Pies.Include(p => p.Category);

        public IEnumerable<Pie> PiesOfTheWeek => this.appDbContext.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);

        public PieRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Pie GetPieByPieId(int pieId)
        {
            return this.appDbContext.Pies.Include(p => p.Category).SingleOrDefault(p => p.PieId == pieId);
        }
    }
}
