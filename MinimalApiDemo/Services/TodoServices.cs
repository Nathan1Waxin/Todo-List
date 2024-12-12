using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.AppDataContext;
using MinimalApiDemo.Contracts;
using MinimalApiDemo.Interface;
using MinimalApiDemo.Models;

namespace TodoAPI.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }




        //  Create Todo for it be save in the datbase 

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the todo item.");
                throw new Exception("An error occurred while creating the todo item.");
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception(" No Todo items found");
            }
            return todo;

        }

        public async Task DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo item with ID {id} not found.");
                }

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Todo item with ID {id}");
                throw;
            }
        }

        // Get all TODO Items from the database 


        public async  Task<Todo> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo item with ID {id} not found.");
                }
                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching Todo item with ID {id}");
                throw;
            }
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo item with ID {id} not found.");
                }

                // Update fields
                todo.Title = request.Title;
                todo.Description = request.Description;
                todo.Priority = (int)request.Priority;
                todo.DueDate = (DateTime)request.DueDate;
                todo.IsComplete = (bool)request.IsComplete;
                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Todo item with ID {id}");
                throw;
            }
        }
    }
}