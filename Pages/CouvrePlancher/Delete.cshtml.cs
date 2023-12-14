using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace first_proj.CouvrePlancher
{
	public class DeleteModel : PageModel
	{
		public string messageErreur = "";
		public CouvrePlancher couvre = new CouvrePlancher();
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
					String sql = "DELETE FROM couvreplancher WHERE id=@id;";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@id", id);
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
		public void OnPost()
		{

		}
	}
}