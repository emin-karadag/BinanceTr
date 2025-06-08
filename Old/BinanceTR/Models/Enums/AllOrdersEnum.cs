using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum AllOrdersEnum
    {
        [Display(Name = "-1")]
        All,

        [Display(Name = "1")]
        Open,

        [Display(Name = "2")]
        History
    }
}
