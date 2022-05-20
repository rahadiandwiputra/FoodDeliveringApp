﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeleveryApp.Data.Models
{
    public class OrderData
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }

        public List<OrderDetailData> Details { get; set; }
    }
}
