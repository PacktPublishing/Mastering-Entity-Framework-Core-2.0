namespace MasteringEFCore.Validations.Starter.Models
{
    public class TagPost
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}