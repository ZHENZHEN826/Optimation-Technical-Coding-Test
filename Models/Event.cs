namespace Optimation_Technical_Coding_Test.Models
{
    public class Event
    {
        public string Date { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string CostCentre { get; set; }
        public string PaymentMethod { get; set; }
        public float Gst { get; set; }
        public float TotalExcludingGst { get; set; }
        public float Total { get; set; }

        public Event(string a, string b, string c, string d, string e, float f, float g, float h)
        {
            Date = a;
            Vendor = b;
            Description = c;
            CostCentre = d;
            PaymentMethod = e;
            Total = f;
            Gst = g;
            TotalExcludingGst = h;
        }
    }
}