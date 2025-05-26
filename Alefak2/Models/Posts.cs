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
        
        public DateTime Date { get; set; }

        public string? Image { get; set; } = "https://i.pinimg.com/736x/1f/9f/2b/1f9f2ba580d5593237e7c1f7ab979ef8.jpg";// optional, could be a URL or base64 string
       
        [ForeignKey("AuthorID")]


        public User Author { get; set; }
    }
} 