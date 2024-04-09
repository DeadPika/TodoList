using System.ComponentModel.DataAnnotations;

namespace TodoList.Mvc.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно!")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Данное поле обязательно!")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Данное поле обязательно!")]
        [Compare("Password", ErrorMessage = "Пароль не совпадает!")]
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
