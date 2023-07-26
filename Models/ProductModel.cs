using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public class ProductModel
    {
        public long IDProduct { get; set; } 
        public string Name { get; set; }
		public string Category { get; set; }
		public string Brand { get; set; }
        public long Pieces { get; set; }
        public string Version { get; set; }
        public string Serie { get; set; }
        public string Size { get; set; }
        public string Measure { get; set; }
        public DateTime EntryDate {  get; set; }
        public DateTime EgressDate { get; set; }
        public byte[] Image { get; set; }

    }
}
