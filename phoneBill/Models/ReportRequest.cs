namespace phoneBill.Models
{
    public class ReportRequest
    {
        public string Phonenumber { get; set; }
        public string YearBill { get; set; }
        public string MonthBill { get; set; }
        public string TypeService { get; set; }
        public string TypeReport { get; set; }
    }
}
