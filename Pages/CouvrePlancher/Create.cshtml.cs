using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace first_proj.CouvrePlancher
{
	public class CreateModel : PageModel
	{
		public CouvrePlancher couvre = new CouvrePlancher();
		public string messageErreur { get; set; } = "";
		public void OnGet()
		{
		}
		public void OnPost()
		{
			couvre.typec = Request.Form["typec"];
			couvre.materiaux = Request.Form["materiaux"];
			couvre.main_ouevre = Request.Form["main_ouevre"];

			if (couvre.typec == "")
			{
				messageErreur = "Veuillez saisir le type de couvre plancher!";
				return;
			}

			try
			{
				string connectionString = "Data Source=DESKTOP-V6N4EFN;Initial Catalog=dotnetdb;Integrated Security=True; Encrypt=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO couvreplancher(typec,materiaux, main_ouevre)" + "(@typec,@materiaux,@main_ouevre);";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
                        cmd.Parameters.AddWithValue("@typec", couvre.typec);
						cmd.Parameters.AddWithValue("@materiaux", couvre.materiaux);
						cmd.Parameters.AddWithValue("@main_oeuvre", couvre.main_ouevre);
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}

			//Reinitialiser les données
			couvre.typec = "";
			couvre.materiaux = "";
			couvre.main_ouevre = "";

			Response.Redirect("/CouvrePlancher/Index");
		}

	}
}