using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Data
{
    public class PaginatedListDto<T>
    {
        public bool Success { get; set; }
        public Paginate Paginate { get; set; }
        public IEnumerable<T> Items { get; set; }
    }

    public class Paginate
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
    }
}
