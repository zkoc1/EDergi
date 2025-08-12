using System;
using System.Collections.Generic;

namespace EDergi.Domain.Entitites
{
	public class Endpoint
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Endpoint’in gerçekleştirdiği işlem tipi (örneğin: Read, Add, Delete, Update)
		/// </summary>
		public string ActionType { get; set; }

		/// <summary>
		/// HTTP method tipi (örneğin: GET, POST, PUT, DELETE)
		/// </summary>
		public string HttpType { get; set; }

		/// <summary>
		/// Bu endpoint’in yaptığı işin açıklaması
		/// </summary>
		public string Definition { get; set; }
		public Menu Menu { get; set; }
		/// <summary>
		/// Her bir action için, parametrelerden türetilen benzersiz kod.
		/// Bu kod yetkilendirme kontrolünde kullanılır.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Bu endpoint’e erişim yetkisi olan roller
		/// </summary>
		public ICollection<AppRole> Roles { get; set; } = new HashSet<AppRole>();
	}
}
