using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Model;

namespace ChirpAPI.Controllers
{
    public class ChirpsController : Controller
    {
        private readonly ChirpContext _context;

        public ChirpsController(ChirpContext context)
        {
            _context = context;
        }

        // GET: Chirps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chirps.ToListAsync());
        }

        // GET: Chirps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chirp = await _context.Chirps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chirp == null)
            {
                return NotFound();
            }

            return View(chirp);
        }

        // GET: Chirps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chirps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,ExtUrl,CreationTime,Lat,Lng")] Chirp chirp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chirp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chirp);
        }

        // GET: Chirps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chirp = await _context.Chirps.FindAsync(id);
            if (chirp == null)
            {
                return NotFound();
            }
            return View(chirp);
        }

        // POST: Chirps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,ExtUrl,CreationTime,Lat,Lng")] Chirp chirp)
        {
            if (id != chirp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chirp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChirpExists(chirp.Id))
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
            return View(chirp);
        }

        // GET: Chirps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chirp = await _context.Chirps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chirp == null)
            {
                return NotFound();
            }

            return View(chirp);
        }

        // POST: Chirps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chirp = await _context.Chirps.FindAsync(id);
            if (chirp != null)
            {
                _context.Chirps.Remove(chirp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChirpExists(int id)
        {
            return _context.Chirps.Any(e => e.Id == id);
        }
    }
}
