namespace phoneBill.Models
{
    public class DashboardResponse
    {
        public List<MemberResponse> ListCountMember { get; set; }
        public List<CampResponse> ListCamp { get; set; }
        public List<ServiceMonthReponse> ListService { get; set; }
        public List<SumServiceCostReponse> ListSumCost { get; set; }
    }

    public class MemberResponse
    {
        /// <summary>
        /// จำนวนผู้ที่ยังใช้งานเบอร์โทรศัพท์
        /// </summary>
        public int CountMember { get; set; }
        /// <summary>
        /// จำนวนผู้ที่ไม่ใช้งานเบอร์โทรศัพท์
        /// </summary>
        public int CountMemberD { get; set; }

    }


    public class CampResponse
    {
        /// <summary>
        /// ชื่อเครือข่าย
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวนผู้ใช้งานตามประเภทค่าย
        /// </summary>
        public int CountCamp { get; set; }
        /// <summary>
        /// ชื่อรูปเครือข่าย
        /// </summary>
        public string ImageCamp { get; set; }
        /// <summary>
        /// ค่าใช้จ่ายภายในปีปัจจุบัน
        /// </summary>
        public int SumServiceCamp { get; set; }

    }

    public class ServiceMonthReponse
    {
        /// <summary>
        /// ค่าบริการมในแต่ละเดือนในปีปัจจุบัน
        /// </summary>
        public int SumMonth { get; set; }
    }

    public class SumServiceCostReponse
    {
        /// <summary>
        /// ค่าโปรโมชั่น
        /// </summary>
        public int SumCostPromotion { get; set; }
        /// <summary>
        /// ค่าโปรโมชั่น
        /// </summary>
        public int SumCostExcess { get; set; }
        /// <summary>
        /// ค่าส่วนเกิน
        /// </summary>
        public int SumCostInterCalling { get; set; }
        /// <summary>
        /// ค่าโทรระหว่างประเทศ
        /// </summary>
        public int SumCostAdditinal { get; set; }
        /// <summary>
        /// VAT 7%
        /// </summary>
        public int SumCostVAT { get; set; }
    }
}
