using System;

namespace Core.Entities
{
	public class MobileLogin : IEntity
	{
		public int Id { get; set; }
		/// <summary>
		/// Farklı sistemlerden login olunabildiği için
		/// provider type ve ExternalUserId alanları bir anahtar oluşturuyor.
		/// </summary>
		public AuthenticationProviderType Provider { get; set; }
		/// <summary>
		/// Bu alan orjinalde TCKimlikNo adındaydı. Fakat diğer providerlar TCKimlik veremeyebileceği
		/// için bu istem dönüştürülüp string türüne çevirildi.
		/// </summary>
		public string ExternalUserId { get; set; }
		public int Code { get; set; }
		public DateTime SendDate { get; set; }
		public bool IsSend { get; set; }
		public bool IsUsed { get; set; }
	}
}
