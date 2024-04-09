using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoList.Mvc.Controllers
{
    public class TodoBaseController : Controller
    {
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
