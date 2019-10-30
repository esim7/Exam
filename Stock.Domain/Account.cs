using System;

namespace Stock.Domain
{
    public class Account : Entity
    {
        public bool isLoggined { get; set; }
        
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
