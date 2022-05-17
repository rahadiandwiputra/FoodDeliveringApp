using System;
using System.Collections.Generic;

namespace FoodDeleveryApp.Data.Models
{
    public partial class Courier
    {
        public Courier()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Completed { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
