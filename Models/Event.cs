using System.Collections.Generic;

namespace Optimation_Technical_Coding_Test.Models
{
    public class Event
    {
        public string Date { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string CostCentre { get; set; }
        public string PaymentMethod { get; set; }
        public double Total { get; set; }
        public double Gst { get; set; }
        public double TotalExcludingGst { get; set; }

        public string[] openingTags = new string[] { "<date>", "<vendor>", "<description>", "<cost_centre>", "<payment_method>", "<total>" };
        public string[] closingTags = new string[] { "</date>", "</vendor>", "</description>", "</cost_centre>", "</payment_method>", "</total>" };

        public Event() { }

        public void SetFields(List<string> strings, List<double> numbers)
        {
            this.Date = strings[0];
            Vendor = strings[1];
            Description = strings[2];
            CostCentre = strings[3];
            PaymentMethod = strings[4];
            Total = numbers[0];
            Gst = numbers[1];
            TotalExcludingGst = numbers[2];
            System.Console.WriteLine(strings[0]);
            System.Console.WriteLine(numbers[0]);
        }
    }
}