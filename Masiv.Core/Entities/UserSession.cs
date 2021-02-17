namespace Masiv.Core.Entities
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }  
    }
}
