using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUI.Models.DataContexts;
using WebApplicationUI.Models.Entities;

namespace WebApplicationUI.Controllers
{
    public class TodosController : Controller
    {
        readonly ToDoListDbConext db;

        public TodosController(ToDoListDbConext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Todo> todos = await db.Todos.Where(t => t.DeletedDate == null).ToListAsync();

            return View(todos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Todo todo = await db.Todos.FirstOrDefaultAsync(m => m.DeletedDate == null && m.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ResponsiblePeople,IsCompleted")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                db.Todos.Add(todo);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Todo todo = await db.Todos.FirstOrDefaultAsync(m => m.DeletedDate == null && m.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ResponsiblePeople,IsCompleted")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Todos.Update(todo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Todo todo = await db.Todos.FirstOrDefaultAsync(m => m.DeletedDate == null && m.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Todo todo = await db.Todos.FirstOrDefaultAsync(m => m.DeletedDate == null && m.Id == id);

            todo.DeletedDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return db.Todos.Any(e => e.DeletedDate == null && e.Id == id);
        }
    }
}
