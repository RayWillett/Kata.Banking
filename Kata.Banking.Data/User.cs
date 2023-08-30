using System;

namespace Kata.Banking.Data
{
    public class User
    {
        public Guid ID { get; private set; }
        
        public User()
        {
            ID = Guid.NewGuid();
        }
    }
}