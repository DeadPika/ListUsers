using DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoWebApp.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<Models.User> listUsers = new List<Models.User>();
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
            DAL dal = new DAL();
            listUsers = dal.GetUsers(_configuration);
        }
    }
}
