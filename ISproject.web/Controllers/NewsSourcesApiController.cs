using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Service.Implementation;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ISproject.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsSourcesApiController : ControllerBase
    {
        private readonly INewsSourceService newsSourceService;

        public NewsSourcesApiController(INewsSourceService newsSourceService)
        {
            this.newsSourceService = newsSourceService;
        }



        // GET: api/NewsSourcesAPI
        [HttpGet("all")]
        public ActionResult<IEnumerable<NewsSource>> GetNewsSources()
        {
            return Ok(newsSourceService.GetAll());
        }

        // GET: api/NewsSourcesAPI/5
        [HttpGet("{id}")]
        public ActionResult<NewsSource> GetNewsSource(Guid id)
        {
            

            var newsSource = newsSourceService.GetById(id);
            if (newsSource == null)
            {
                return NotFound();
            }

            return Ok(newsSource);
        }

        // PUT: api/NewsSourcesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<NewsSource> PutNewsSource(Guid id, [FromBody] NewsSource newsSource)
        {
            if (id != newsSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedSource = newsSourceService.Update(newsSource);
                return Ok(updatedSource);
            }

            return BadRequest();
        }

        // POST: api/NewsSourcesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<NewsSource> PostNewsSource([FromBody] NewsSource newsSource)
        {
            if (ModelState.IsValid)
            {
                newsSourceService.Add(newsSource);
                return CreatedAtAction("GetNewsSource", new { id = newsSource.Id }, newsSource);
            }

            return BadRequest();
        }

        // DELETE: api/NewsSourcesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNewsSource(Guid id)
        {
            if (NewsSourceExists(id)) {
                NewsSource deletedSource = newsSourceService.Delete(id);
                return Ok(deletedSource);

            }
            return BadRequest();
        }

        private bool NewsSourceExists(Guid id)
        {
            return newsSourceService.ExistsById(id);
        }
    }
}
