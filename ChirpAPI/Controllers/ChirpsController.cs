using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Model;
using ChirpAPI.services.ViewModels;
using ChirpAPI.services.Services.Interfaces;

namespace ChirpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirpsController : ControllerBase
    {
        private readonly IChirpsService _chirpsService;
        private readonly ILogger<ChirpsController> _logger;

        public ChirpsController(IChirpsService chirpsService, ILogger<ChirpsController> logger)
        {
            _chirpsService = chirpsService;
            _logger = logger;
        }

        // GET: api/Chirps
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChirps()
        {
            Console.WriteLine("Sto cercando i chirps");
            var result = await _chirpsService.GetAllChirps();
            return Ok(result);
        }

        // GET: api/Chirps?
        [HttpGet]
        public async Task<IActionResult> GetChirpsByFilter([FromQuery] ChirpFilter filter)
        {
            _logger.LogInformation("Received request to get chirps with filter: {@Filter}", filter);
            var result = await _chirpsService.GetChirpsByFilter(filter);

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No chirps found for the given filter: {@Filter}", filter);
                return NoContent();
            }
            _logger.LogInformation("Found {Count} chirps for the filter: {@Filter}", result.Count, filter);
            return Ok(result);
            
        }

        // GET: api/Chirps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChirp([FromRoute] int id)
        {
            // var chirp = await _context.Chirps.FindAsync(id);
            var chirp = await _chirpsService.GetChirpById(id);

            if (chirp == null)
            {
                return NotFound();
            }

            return Ok(chirp);
        }

        // PUT: api/Chirps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChirp([FromRoute]int id, [FromBody]Chirp chirp)
        {
            if (id != chirp.Id) return BadRequest();

            try
            { 
                await _chirpsService.UpdateChirp(id, chirp);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return NoContent();

        }

        // POST: api/Chirps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chirp>> PostChirp([FromBody]Chirp chirp)
        {
            await _chirpsService.CreateChirp(chirp);

            return CreatedAtAction("GetChirp", new { id = chirp.Id }, chirp);
        }

        // DELETE: api/Chirps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirp(int id)
        {
            try
            {
                await _chirpsService.DeleteChirp(id);
            }
            catch
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
