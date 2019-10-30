using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain
{
    public class Document : Entity
    {
        public string DocumentNumber { get; set; }
        public Guid AccountId { get; set; }
    }
}
