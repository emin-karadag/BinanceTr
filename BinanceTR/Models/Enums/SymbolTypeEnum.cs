using System.ComponentModel.DataAnnotations;

namespace BinanceTR.Models.Enums
{
    public enum SymbolTypeEnum
    {
        [Display(Name = "1")]
        MAIN,

        [Display(Name = "2")]
        NEXT
    }
}
