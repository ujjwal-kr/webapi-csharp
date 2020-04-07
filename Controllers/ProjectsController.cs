using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly Context _context;

        public ProjectsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return NotFound();

            return project;
        }

        [HttpGet("{id}/todos")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodo(long id)
        {
            var todos = await _context.TodoItems
                .Where(t => t.ProjectId == id)
                .ToListAsync();

            if (todos == null) return NotFound(new {message ="Todos not found or server error"});
            return todos; 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> PutProject(long id, [FromBody]Project proj)
        {
            var project = await _context.Projects.FindAsync(id);
            if (proj == null) return NotFound();

            project.Name = proj.Name;
            project.Description = proj.Description;

            await _context.SaveChangesAsync();
            return proj;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            await deleteTodos(id);

            return project;
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        private async Task<bool> deleteTodos(long id)
        {
            var todos = await _context.TodoItems
                .Where(t => t.ProjectId == id)
                .ToListAsync(); 

            foreach(TodoItem todo in todos)
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
            }

            return true;
        }

    }
}