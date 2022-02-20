using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatatablesNetCoreApi.Model
{
    public class Orders
    {
        public int ID { get; set; }
        public string ORD_NUM { get; set; }
        public string ORD_AMOUNT { get; set; }
        public string ADVANCE_AMOUNT { get; set; }
        public string ORD_DATE { get; set; }
        public string CUST_CODE { get; set; }
        public string AGENT_CODE { get; set; }
        public string ORD_DESCRIPTION { get; set; }
    }

    public class ServerDTOrderMap
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<Orders> data { get; set; }
    }
}
