using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DatatablesNetCoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DatatablesNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class dtexample : Controller
    {
       
        private readonly IConfiguration _config;
        private  DataUtility dt;
        public dtexample(IConfiguration configuration)
        {
            _config = configuration;

            dt = new DataUtility(_config.GetConnectionString("SqlConnectionString"));
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost("tableMeta")]
        public string GetTableMeta()
        {
            
            List<string> cols = new List<string>();
            List<List<string>> cols2 = new List<List<string>>();
            tableMeta Tbl = new tableMeta();
            List<colMeta> ColumnList = new List<colMeta>();

            string c = @"select top 1 *  from Orders";
            
            colMeta col2 = new colMeta();
            col2.Name = "Hist";
            col2.Searchable = true;
            // ColumnList.Add(col2);
            Tbl.Deletable = true;
            Tbl.Insertable = true;
            foreach (DataColumn dc in dt.getDataset(c).Tables[0].Columns)
            {
                colMeta col = new colMeta();
                col.Searchable = false;
                col.Editable = false;
                col.Name = dc.ColumnName;
                if (dc.ColumnName != "ID" && dc.ColumnName != "DateCreated")
                    col.Searchable = true;
                if (dc.ColumnName != "ID" && dc.ColumnName != "DateCreated" && dc.ColumnName != "ORD_DATE")
                    col.Editable = true;
                col.Class = dc.ColumnName;
                ColumnList.Add(col);

            }
            Tbl.Column = ColumnList;
            return JsonConvert.SerializeObject(Tbl);

        }


        [HttpPost("tableData")]
        public string GetTableData(IFormCollection formData)
        {
            int draw = Convert.ToInt32(formData["draw"]);
            string sort = formData["sort"].ToString();
            string search = formData["search"].ToString();
            int start = Convert.ToInt32(formData["start"]);
            int length = Convert.ToInt32(formData["length"]);

            string c = @"select count(*) count from Orders";

            int tblerowcount = Convert.ToInt32(dt.getDataset(c).Tables[0].Rows[0]["count"]);
            NameValueCollection forw = new NameValueCollection();
            foreach (string key in formData.Keys)
            {
                forw.Add(key,formData[key]);
            }
            forw.Remove("draw");
            forw.Remove("sort");
            forw.Remove("start");
            forw.Remove("empty");
            forw.Remove("length");
            forw.Remove("search");
            string whereclause = "";
            string filterclause = "";
            int filterrowcount;

            foreach (string key in forw)
            {
                if (forw[key] != "")
                {
                    filterclause += " and " + key + " like '%" + forw[key] + "%' ";
                }
                else if(search.Length > 2)
                {
                    filterclause += " and " + key + " like '%" + search + "%' ";
                }

            }
            whereclause = "where 1=1 " + filterclause;
            c = @"select count(*) count from Orders " + whereclause;
            if (string.IsNullOrEmpty(sort))
                sort = "ID";
            if (length == 0)
                length = 10;
            string q = @"select * from Orders " + whereclause + "  order by " + sort + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
            filterrowcount = Convert.ToInt32(dt.getDataset(c).Tables[0].Rows[0]["count"]);
            DataTable tb = dt.getDataset(q).Tables[0];


            List<Orders> orders = new List<Orders>();
            
            foreach (DataRow dr in tb.Rows)
            { 
                var order = new Orders
                {
                    ID = Convert.ToInt16(dr["ID"]),
                    ORD_NUM = dr["ORD_NUM"].ToString(),
                    ORD_AMOUNT = dr["ORD_AMOUNT"].ToString(),
                    ADVANCE_AMOUNT = dr["ADVANCE_AMOUNT"].ToString(),
                    ORD_DATE = dr["ORD_DATE"].ToString(),
                    CUST_CODE = dr["CUST_CODE"].ToString(),
                    AGENT_CODE = dr["AGENT_CODE"].ToString(),
                    ORD_DESCRIPTION = dr["ORD_DESCRIPTION"].ToString()
                };
                orders.Add(order);
            }

            var sdt = new ServerDTOrderMap
            {
                draw = draw,
                recordsTotal = tblerowcount,
                recordsFiltered = filterrowcount,
                data = orders
            };
            return JsonConvert.SerializeObject(sdt);
        }
         
    }
}
