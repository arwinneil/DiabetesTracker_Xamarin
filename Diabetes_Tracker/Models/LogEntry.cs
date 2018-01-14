using System;
using System.Collections.Generic;
using System.Text;

namespace Diabetes_Tracker.Models
{
    public class LogEntry
    {
        public double glucose { get; set; }
        public double activity { get; set; }
        public DateTime date { get; set; }
        public string stringDate { get; set; }
    }
}
