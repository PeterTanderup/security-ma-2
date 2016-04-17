using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySensaiDk.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int? MaxSpots { get; set; }
        public int? AddressId { get; set; }
        public string TeacherID { get; set; }

        public virtual Address Address { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<CourseTag> Tags { get; set; }
        public virtual ICollection<Enrollment> CourseEnrollments { get; set; }
    }
}
