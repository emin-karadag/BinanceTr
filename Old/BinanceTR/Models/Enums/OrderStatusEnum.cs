using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum OrderStatusEnum
    {
        [Display(Name = "-2")]
        SYSTEM_PROCESSING,

        [Display(Name = "0")]
        NEW,

        [Display(Name = "1")]
        PARTIALLY_FILLED,

        [Display(Name = "2")]
        FILLED,

        [Display(Name = "3")]
        CANCELED,

        [Display(Name = "4")]
        PENDING_CANCEL,

        [Display(Name = "5")]
        REJECTED,

        [Display(Name = "6")]
        EXPIRED
    }
}
