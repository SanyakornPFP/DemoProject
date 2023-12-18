namespace phoneBill.Models
{
    public class ReportRespose
    {
        public List<DataReportRespose> ListDataReport { get; set; }

    }

    public class DataReportRespose
    {
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public  string Phonenumber { get; set; }
        /// <summary>
        /// ชื่อผู้ใช้งาน
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// เดือน
        /// </summary>
        public string MonthBill { get; set; }
        /// <summary>
        /// ปี
        /// </summary>
        public string YearBill { get; set; }
        /// <summary>
        /// ค่าโปรโมชั่น
        /// </summary>
        public double PromotionCost { get; set; }
        /// <summary>
        /// ค่าส่วนเกิน
        /// </summary>
        public double ExcessCost { get; set; }
        /// <summary>
        /// ค่าโทรระหว่างประเทศ
        /// </summary>
        public double InterCallingCharge { get; set; }
        /// <summary>
        /// ค่าบริการเสริม
        /// </summary>
        public double AdditionalServiceFee { get; set; }
        /// <summary>
        /// VAT7%
        /// </summary>
        public double VAT { get; set; }
        /// <summary>
        /// รวมค่าบริการ
        /// </summary>
        public double TotalService { get; set; }

    }
}
