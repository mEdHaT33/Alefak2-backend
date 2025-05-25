using System;
using System.ComponentModel.DataAnnotations;

namespace Alefak2.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
        public string UserName { get; set; }
        public int Phone { get; set; }
        public string? Country { get; set; } = null;
        public string? City { get; set; } = null;
    }
}