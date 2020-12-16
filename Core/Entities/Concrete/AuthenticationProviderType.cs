namespace Core.Entities
{
	public enum AuthenticationProviderType
	{
		/// <summary>
		/// Olmaması gereken durumdur. 0 koduna karşılık gelir. Hataları tespit etmek için eklenmiştir.
		/// </summary>
		Unknown,
		/// <summary>
		/// Kişiler için giriş.
		/// </summary>
		Person,
		/// <summary>
		/// Kurum personeli için giriş.
		/// </summary>
		Staff,
		/// <summary>
		/// Anlaşmalı hizmet personeli için giriş.
		/// </summary>
		Agent
	}

}
