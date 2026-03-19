namespace GameExchange.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }

}
