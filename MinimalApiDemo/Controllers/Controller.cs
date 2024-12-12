using Microsoft.AspNetCore.Mvc;
using MinimalApiDemo.Interface;
using MinimalApiDemo.Contracts;
using MinimalApiDemo.Interface;

namespace MinimalApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }



// Creating new Todo Item
[HttpPost]
  public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }


      try
      {

          await _todoServices.CreateTodoAsync(request);
          return Ok(new { message = "Blog post successfully created" });

      }
      catch (Exception ex)
      {
          return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

      }
  }

// Get all Todo Items

  [HttpGet]
  public async Task<IActionResult> GetAllAsync()
  {
      try
      {
          var todo = await _todoServices.GetAllAsync();
          if (todo == null || !todo.Any())
          {
              return Ok(new { message = "No Todo Items  found" });
          }
          return Ok(new { message = "Successfully retrieved all blog posts", data = todo });

      }
      catch (Exception ex)
      {
          return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


      }
  }

// Update a Todo Item
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
  {
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    try
    {
        await _todoServices.UpdateTodoAsync(id, request);
        return Ok(new { message = "Todo Item successfully updated" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An error occurred while updating the Todo Item", error = ex.Message });
    }
  }

// Delete a Todo Item
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTodoAsync(Guid id)
  {
    try
    {
        await _todoServices.DeleteTodoAsync(id);
        return Ok(new { message = "Todo Item successfully deleted" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "An error occurred while deleting the Todo Item", error = ex.Message });
    }
  }

    }
}