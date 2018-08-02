using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPExamples.EntityFramework.Abstractions.Entities
{
    public class User
    {
        public User()
        {

        }

        public User(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
