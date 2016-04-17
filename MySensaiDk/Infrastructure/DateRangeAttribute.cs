using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySensaiDk.Infrastructure
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(int maxAge = 120) : base (typeof(DateTime), DateTime.Today.AddYears(-maxAge).ToShortDateString(), DateTime.Today.ToShortDateString())
        {
            ErrorMessage = "Du skal være mellem 0 og " + maxAge + " år gammel";
        }
    }
}