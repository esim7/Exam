using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Domain
{
    public class People
    {
        public bool IsWorker { get; set; }
        public string Name { get; set; }

        public People()
        {
            IsWorker = false;
        }
    }
}
