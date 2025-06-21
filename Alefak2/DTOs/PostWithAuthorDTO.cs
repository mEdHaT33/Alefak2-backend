namespace Alefak2.DTOs
{
    public class PostWithAuthorDTO
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}