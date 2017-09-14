using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClawLibrary.Core.Models
{
    public class QueryData
    {
        public int? Count { get; set; }
        public int? Offset { get; set; }
        public string OrderField { get; set; }
        public string OrderDirection { get; set; }
        public string SearchString { get; set; }

        public string OrderBy => $"{OrderField}_{OrderDirection}";

        public override string ToString()
        {
            return $"Count : {Count?.ToString() ?? "null"}, Offset : {Offset?.ToString() ?? "null"}, OrderBy: {OrderBy}, SearchString : {SearchString} ";
        }
    }
}
