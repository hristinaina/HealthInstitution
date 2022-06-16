using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Utilis
{
    class DateRange
    {
        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set { _startDate = value; } }

        private DateTime _endDate;
        public DateTime EndDate { get => _endDate; set { _endDate = value; } }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public bool InRange(DateTime date)
        {
            if (date >= _startDate && date <= _endDate) return true;
            return false;
        }

        public bool Overlaps(DateRange range)
        {
            if (InRange(range.StartDate) || InRange(range.EndDate)) return true;
            return false;
        }
    }
}
