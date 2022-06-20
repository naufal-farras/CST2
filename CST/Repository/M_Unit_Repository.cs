using CST.Data;
using CST.Models.Master;
using System.Collections.Generic;
using System.Linq;

namespace CST.Repository
{
    public class M_Unit_Repository
    {
        private readonly AppDbContext _context;

        public M_Unit_Repository(AppDbContext context)
        {
            _context = context;
        }

        public List<M_Unit> GetAll()
        {
            return _context.M_Unit.ToList();
        }
    }
}
