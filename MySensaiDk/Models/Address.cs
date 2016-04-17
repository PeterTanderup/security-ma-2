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
    public class Address
    {
        public int AddressId { get; set; }
        public int CityId { get; set; }
        public string FullAddress { get; set; }
        public string AddressName { get; set; }
        public string UserId { get; set; }
        public virtual City City { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}