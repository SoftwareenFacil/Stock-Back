﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Back.DAL.Models
{
    [Table("client")]
    public class Client
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; } 
        public int Phone { get; set; }
        [Required]
        public string TaxID { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}