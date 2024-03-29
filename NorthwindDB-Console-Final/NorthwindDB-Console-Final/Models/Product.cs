﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindDB_Console_Final.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "A Product Name is required and can't be NULL for this entity")]
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
