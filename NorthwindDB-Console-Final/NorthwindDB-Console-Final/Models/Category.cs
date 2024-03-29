﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindDB_Console_Final.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A Category Name is required and can't be NULL for this entity")] 
        public string CategoryName { get; set; }

        [MaxLength(100, ErrorMessage = "The Category Description is too long. It can not be longer than 100 characters.")]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
