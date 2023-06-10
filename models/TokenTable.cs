namespace WebApplication3.models
{
    public class TokenTable
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
