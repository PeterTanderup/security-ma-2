using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MySensaiDk.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string PostalNumber { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
