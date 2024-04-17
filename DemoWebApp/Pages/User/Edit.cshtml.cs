using DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoWebApp.Pages.User
{
    public class EditModel : PageModel
    {
        public Models.User user = new Models.User();
        public string successMessage = string.Empty;
        public string errorMessage = string.Empty;
        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                DAL dal = new DAL();
                user = dal.GetUser(id, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }
        public void OnPost()
        {
            Request.ContentType = "text/plain;charset=utf-8";
            user.Id = Request.Form["hiddenId"];
            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];

            if (user.FirstName.Length == 0 || user.LastName.Length == 0)
            {
                errorMessage = "Все поля обязательны!";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int i = dal.UpdateUser(user, _configuration);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            successMessage = "Пользователь обновлен.";
            Response.Redirect("/User/Index");
        }
    }
}
