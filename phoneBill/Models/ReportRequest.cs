using System.ComponentModel.DataAnnotations;

namespace phoneBill.Models
{
    public class ReportRequest
    {
        
        public string Phonenumber { get; set; }
        [Required]
        [Display(Name = "กรุณาเลือกปีรายงาน")]
        public string YearBill { get; set; }
        public string MonthID { get; set; }
        public string TypeReport { get; set; }
    }
}
