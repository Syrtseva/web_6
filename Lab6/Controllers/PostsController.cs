using Lab6.Models;
using Lab6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly HttpClientService _httpClientService;

        public PostsController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _httpClientService.GetAsync("posts");

            if (result.StatusCode == 200 && result.Data != null)
            {
                return Ok(result.Data);
            }

            return StatusCode(result.StatusCode, new { message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostData postData)
        {
            var result = await _httpClientService.PostAsync("posts", postData);

            if (result.StatusCode == 201)
            {
                return CreatedAtAction(
                    nameof(GetAllPosts),
                    new { id = result.Data[0].Id },
                    result.Data[0]
                );
            }

            return StatusCode(result.StatusCode, new { message = result.Message });
        }
    }
}
