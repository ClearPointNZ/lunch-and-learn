using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ILogger<TodoItemsController> logger)
        {
            _logger = logger; 
        } 

        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            // save the item to the DB and call
            // await _messagingService.SendServiceBusMessageAsync(message); 

            throw new NotImplementedException();
        } 
    }
}
