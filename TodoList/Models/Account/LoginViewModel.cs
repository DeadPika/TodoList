using System.ComponentModel.DataAnnotations;

namespace TodoList.Mvc.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно!")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Данное поле обязательно!")]
        public string Password { get; set; } = string.Empty;
    }
}
