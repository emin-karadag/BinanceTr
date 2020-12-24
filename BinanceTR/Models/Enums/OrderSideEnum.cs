using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum OrderSideEnum
    {
        [Display(Name = "0")]
        BUY,

        [Display(Name = "1")]
        SELL
    }
}
