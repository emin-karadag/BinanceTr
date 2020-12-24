using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum OrderTypeEnum
    {
        [Display(Name = "1")]
        LIMIT,

        [Display(Name = "2")]
        MARKET,

        [Display(Name = "3")]
        STOP_LOSS,

        [Display(Name = "4")]
        STOP_LOSS_LIMIT,

        [Display(Name = "5")]
        TAKE_PROFIT,

        [Display(Name = "6")]
        TAKE_PROFIT_LIMIT,

        [Display(Name = "7")]
        LIMIT_MAKER
    }
}
