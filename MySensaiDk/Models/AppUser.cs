using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MySensaiDk.Models
{
    public enum Gender
    {
        Mand, Kvinde
    }

    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Enrollment> UserEnrollments { get; set; }
    }
}
