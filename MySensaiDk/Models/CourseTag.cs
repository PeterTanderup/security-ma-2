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
    public class CourseTag
    {
        public int CourseTagId { get; set; }
        public int CourseId { get; set; }
        public int TagId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
