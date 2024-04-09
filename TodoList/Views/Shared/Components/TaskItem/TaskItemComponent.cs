using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entities;

namespace TodoList.Mvc.Views.Shared.Components.TaskItem
{
    public class TaskItemComponent : ViewComponent
    {
        public IViewComponentResult Ivoke(TaskApp task)
        {
            return View(task);
        }
    }
}
