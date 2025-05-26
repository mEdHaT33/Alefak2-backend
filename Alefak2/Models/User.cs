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
        public string? Image { get; set; } = "https://i.pinimg.com/736x/1f/9f/2b/1f9f2ba580d5593237e7c1f7ab979ef8.jpg";// optional, could be a URL or base64 string


    }
}