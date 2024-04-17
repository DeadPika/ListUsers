using DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoWebApp.Pages.User
{
    public class CreatecshtmlModel : PageModel
    {
        public Models.User user = new Models.User();
        public string successMessage = string.Empty;
        public string errorMessage = string.Empty;

        private readonly IConfiguration _configuration;
        public CreatecshtmlModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Request.ContentType = "text/plain;charset=utf-8";
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];

            if(user.FirstName.Length == 0 || user.LastName.Length == 0)
            {
                errorMessage = "Все поля обязательны!";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.AddUser(user, _configuration);
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            user.FirstName = ""; user.LastName = "";
            successMessage = "Пользователь добавлен";
            Response.Redirect("/User/Index");
        }
    }
}
