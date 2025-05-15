using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Alefak2.Models
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int AuthorID { get; set; }
        
        public string Text { get; set; }
        
        public int Likes { get; set; }
        
        public List<string> Comments { get; set; } = new List<string>();
        
        public DateTime Date { get; set; }
        
        public string? Image { get; set; } // optional, could be a URL or base64 string
    }
} 