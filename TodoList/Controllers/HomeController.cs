using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TodoList.Domain;
using TodoList.Domain.Entities;
using TodoList.Models;
using TodoList.Mvc.Controllers;
using TodoList.Mvc.Models.Home;

namespace TodoList.Controllers
{
    [Authorize]
    public class HomeController : TodoBaseController
    {
        private readonly TodoListContext _context;
        public HomeController(TodoListContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeViewModel
            {
                Tasks = await GetTasksCurrentUserAsync()
            });
        }
        public async Task<IActionResult> Create(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Tasks = await GetTasksCurrentUserAsync();
                return View("Index", model);
            }

            var task = _context.Tasks.FirstOrDefault(t => t.Name.ToLower() == model.TaskName.ToLower()
            && t.UserId == CurrentUserId
            && t.ExpiredDate == model.DateTime);

            if(task != null)
            {
                model.Tasks = await GetTasksCurrentUserAsync();
                ViewBag.Error = "Данная задача уже существует!";

                return View("Index", model);
            }

            await _context.Tasks.AddAsync(new Domain.Entities.TaskApp
            {
                Name = model.TaskName,
                ExpiredDate = model.DateTime,
                UserId = CurrentUserId
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if(task != null)
            {
                task.IsCompleted = true;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateName(int id, string name)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if(task != null)
            {
                if(task.Name != name)
                {
                    task.Name = name;
                    _context.Tasks.Update(task);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IEnumerable<TaskApp>> GetTasksCurrentUserAsync()
        {
            return await _context.Tasks
                .Where(t => t.UserId == CurrentUserId)
                .ToListAsync();
        }

        [AllowAnonymous]
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}