using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum KLineIntervalEnum
    {
        [Display(Name = "1m")]
        Minute_1,

        [Display(Name = "3m")]
        Minute_3,

        [Display(Name = "5m")]
        Minute_5,

        [Display(Name = "15m")]
        Minute_15,

        [Display(Name = "30m")]
        Minute_30,

        [Display(Name = "1h")]
        Hour_1,

        [Display(Name = "2h")]
        Hour_2,

        [Display(Name = "4h")]
        Hour_4,

        [Display(Name = "6h")]
        Hour_6,

        [Display(Name = "8h")]
        Hour_8,

        [Display(Name = "12h")]
        Hour_12,

        [Display(Name = "1d")]
        Day_1,

        [Display(Name = "3d")]
        Day_3,

        [Display(Name = "1w")]
        Week_1,

        [Display(Name = "1M")]
        Mounth_1,
    }
}
