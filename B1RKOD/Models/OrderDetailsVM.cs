using System.Collections;
using System.Collections.Generic;

namespace B1RKOD.Models
{
    public class OrderDetailsVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OderDetails> OderDetails { get; set; }
    }
}
