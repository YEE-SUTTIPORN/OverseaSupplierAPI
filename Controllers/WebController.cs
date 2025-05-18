using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OverseaSupplierAPI.DTOs;

namespace OverseaSupplierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        [HttpGet("GetWebs")]
        public ActionResult GetWebs()
        {
            // Simulate fetching data from a database
            var webs = new List<WebDto>();

            for (int i = 1; i <= 10; i++)
            {
                webs.Add(new WebDto
                {
                    Web_ID = i,
                    Web_Name = $"Web Name {i}",
                    Web_Description = $"Web Description {i}",
                    Web_Icon = $"https://picsum.photos/200"
                });
            }

            return Ok(webs);
        }
    }
}
