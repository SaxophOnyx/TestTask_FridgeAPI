﻿using System;

namespace FridgeAPI.Entities.Models
{
    public class FridgeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}
