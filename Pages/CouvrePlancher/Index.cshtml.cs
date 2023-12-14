using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace first_proj.CouvrePlancher
{
	public class IndexModel : PageModel
	{
		//La liste des couvre planchers
		public List<CouvrePlancher> CouvreList = new List<CouvrePlancher>();
		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-V6N4EFN;Initial Catalog=dotnetdb;Integrated Security=True;Encrypt=False";

				using (SqlConnection connection = new(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM couvreplancher;";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								CouvrePlancher couvre = new CouvrePlancher();
								couvre.id = reader.GetInt32(0);
								couvre.typec = reader.GetString(1);
								couvre.materiaux = reader.GetString(2);
								couvre.main_ouevre = reader.GetString(3);

								CouvreList.Add(couvre);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
	}
	public class CouvrePlancher
	{
		public int id;
		public string typec = "";
		public string materiaux = "";
		public string main_ouevre = "";
	}
	public class Client
	{
		public int idC;
		public string nomC = "";
		public string prenomC = "";
		public string email = "";
		public string adresse = "";
	}
	public class Demande
	{
		public int idD;
		public string nomC = "";
		public string langueur = "";
		public string largeur = "";
		public string couvrep = "";
	}
}