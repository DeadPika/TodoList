using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Entities;

namespace TodoList.Mvc.Models.Home
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно!")]
        public string TaskName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Данное поле обязательно!")]
        public DateTime DateTime { get; set; }
        public IEnumerable<TaskApp>? Tasks { get; set; }
    }
}
