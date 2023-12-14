using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace first_proj.Pages;

public class NouvelleDemandeModel : PageModel
{
    public bool hasData = false;
    public string nom = "";
    public string prenom = "";
    public string email = "";
    public string adressep = "";
    public string numTel = "";
    public string langueur = "";
    public string largeur = "";
    public string typecouvre = "";
    public List<CouvrePlancher> CouvreList = new List<CouvrePlancher>();
    public void OnGet()
    {
        try
        {
            string connectionString = "Data Source=DESKTOP-V6N4EFN;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                String sql = "SELECT typec FROM couvreplancher;";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CouvrePlancher couvre = new CouvrePlancher();
                            couvre.id = reader.GetInt32(0);
                            couvre.typec = reader.GetString(1);

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
    public void OnPost()
    {
        hasData = true;
        nom = Request.Form["nom"];
        prenom = Request.Form["prenom"];
        numTel = Request.Form["numTel"];
        email = Request.Form["email"];
        adressep = Request.Form["adressep"];
        langueur = Request.Form["langueur"];
        largeur = Request.Form["largeur"];
        typecouvre = Request.Form["typecouvre"];
    }
    public class CouvrePlancher
    {
        public int id;
        public string typec = "";
        public string materiaux = "";
        public string main_ouevre = "";
    }
}
