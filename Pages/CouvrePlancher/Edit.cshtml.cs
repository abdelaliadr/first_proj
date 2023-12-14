using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace first_proj.CouvrePlancher
{
	public class EditModel : PageModel
	{
		public CouvrePlancher couvre = new CouvrePlancher();
		public string messageErreur = "";

		public void OnGet()
		{
			String id = Request.Query["id"];
			try
			{
				//connection string
				string connectionString = "Data Source=DESKTOP-V6N4EFN;Initial Catalog=dotnetdb;Integrated Security=True; Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM couvreplancher WHERE id=@id;";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read()) //Lire les données (non vides)
							{
								couvre.id = reader.GetInt32(0);
								couvre.typec = reader.GetString(1);
								couvre.materiaux = reader.GetString(2);
								couvre.main_ouevre = reader.GetString(3);
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
		public void OnPost()
		{
			couvre.id = Convert.ToInt32(Request.Form["id"]);
			couvre.typec = Request.Form["typec"];
			couvre.materiaux = Request.Form["materiaux"];
			couvre.main_ouevre = Request.Form["main_ouevre"];

			try
			{
				//connection string
				string connectionString = "Data Source=DESKTOP-V6N4EFN;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE couvreplancher SET typec=@typec,materiaux=@materiaux, main_ouevre=@main_ouevre WHERE id=@id;";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						Console.WriteLine("Modifier le couvre n: " + Convert.ToString(couvre.id));
						cmd.Parameters.AddWithValue("@id", Convert.ToString(couvre.id));
						cmd.Parameters.AddWithValue("@typec", couvre.typec);
						cmd.Parameters.AddWithValue("@materiaux", couvre.materiaux);
						cmd.Parameters.AddWithValue("@main_oeuvre", couvre.main_ouevre);
						Console.WriteLine("la requête");

						cmd.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
			Response.Redirect("/CouvrePlancher/Index");

		}
	}
}

