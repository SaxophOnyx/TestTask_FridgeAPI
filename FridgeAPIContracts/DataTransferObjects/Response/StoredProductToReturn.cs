﻿using System;

namespace FridgeAPI.DataTransferObjects.DataTransferObjects.Response
{
    public class StoredProductToReturn
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
