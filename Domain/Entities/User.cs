namespace Domain.Entities
{
	public class User(string email, string password)
	{
		public int Id { get; set; }

		[Required]
		public string Email { get; set; } = email;

		[Required]
		public string Password { get; set; } = password;
	}
}
