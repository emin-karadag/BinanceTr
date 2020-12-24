using BinanceTR.Core.Models;
using System.Collections.Generic;

namespace BinanceTR.Models.Common
{
    public class KLinesModel : IBinanceTrModel
    {
        public List<List<object>> MyArray { get; set; }
    }
}
