﻿using Business.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProductBody
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Product GetEntity()
        {
            return new Product()
            {
                Name = Name.Trim(),
                Description = Description.Trim(),
                Quantity = Quantity
            };
        }

    }
}
