using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatablesNetCoreApi.Model
{
    public class tableMeta
    {
        public string Name { get; set; }
        public bool Deletable { get; set; }
        public bool Insertable { get; set; }
        public List<colMeta> Column;
    }
}
