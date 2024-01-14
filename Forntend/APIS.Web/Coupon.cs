using System.ComponentModel.DataAnnotations;

namespace APIS.Web
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public decimal MinAmount { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
    }
}
