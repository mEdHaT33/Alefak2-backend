namespace Alefak2.Models
{
    public class Comments

    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
    }
}
