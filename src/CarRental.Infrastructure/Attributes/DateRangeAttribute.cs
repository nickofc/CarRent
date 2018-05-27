using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Infrastructure.Attributes
{
    public class DateRangeAttribute : RangeAttribute
    {
        public DateRangeAttribute(int maxDaysForward) : base(typeof(DateTime), DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(maxDaysForward).ToShortDateString())
        {

        }
    }
}
