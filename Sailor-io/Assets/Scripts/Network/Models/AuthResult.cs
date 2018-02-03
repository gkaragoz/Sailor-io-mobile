using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.ApiModels
{
	public class Token
	{
		public string tokenType { get; set; }
		public string accessToken { get; set; }
		public string refreshToken { get; set; }
		public DateTime expiresIn { get; set; }
	}

	public class User
	{
		public string id { get; set; }
		public string email { get; set; }
		public string role { get; set; }
		public DateTime createdAt { get; set; }
	}

	public class AuthResult
	{
		public Token token { get; set; }
		public User user { get; set; }
	}
}
