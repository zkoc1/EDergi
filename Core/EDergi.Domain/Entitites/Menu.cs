using System;
using System.Collections.Generic;

namespace EDergi.Domain.Entitites
{
	public class Menu
	{
		public Menu()
		{
			Endpoints = new HashSet<Endpoint>();
		}

		public Guid Id { get; set; } = Guid.NewGuid(); // Benzersiz ID
		public string Name { get; set; } // Menü adı, örneğin "Kullanıcı Yönetimi"
		public string? Icon { get; set; } // İsteğe bağlı, UI için ikon bilgisi (örneğin: "user", "settings")
		public string? Path { get; set; } // Frontend'de yönlendirme adresi (örn: "/admin/users")

		// Her menü birçok endpoint barındırabilir (actionlar gibi)
		public ICollection<Endpoint> Endpoints { get; set; }
	}
}
