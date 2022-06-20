using CST.Data;
using CST.Models.Master;
using System.Collections.Generic;
using System.Linq;

namespace CST.Repository
{
    public class M_Jabatan_Repository
    {
        private readonly AppDbContext _context;

        public M_Jabatan_Repository(AppDbContext context)
        {
            _context = context;
        }
        public List<M_Jabatan> GetAll()
        {
            var result = _context.M_Jabatan.ToList();
            return result;
        }

    }
}
