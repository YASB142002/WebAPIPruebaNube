using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIPruebaModels
{
    public class Customer
    {
        public int customer_id {  get; set; }
        public int store_id { get; set; }
        public string? firt_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? address_id { get; set; }
        public bool active { get; set; }
    }
}
