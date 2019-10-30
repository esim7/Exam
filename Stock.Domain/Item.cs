using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Guid DocumentId { get; set; }
    }
}
