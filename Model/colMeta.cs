using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatablesNetCoreApi.Model
{
    public class colMeta
    {
        public string Name { get; set; }
        public bool Editable { get; set; }
        public bool Searchable { get; set; }
        public string Class { get; set; }
    }
}
