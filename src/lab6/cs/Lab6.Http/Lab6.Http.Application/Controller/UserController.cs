using Lab6.Http.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Http.Application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApi userApi;

        public UserController(IUserApi userApi)
        {
            this.userApi = userApi;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAsync(int id) 
        {
            var user = await userApi.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet()]
        public async Task<ActionResult<User>> GetAsync() 
        {
            var users = await userApi.GetAllAsync();

            if (users?.Any() != true)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]User user) 
        {
            var result = await userApi.AddAsync(user);
            if (!result) 
            {
                return BadRequest();
            }

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody]User user) 
        {
            var result = await userApi.UpdateAsync(id, user);
            if (!result) 
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) 
        {
            var result = await userApi.DeleteAsync(id);
            if (!result) 
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
