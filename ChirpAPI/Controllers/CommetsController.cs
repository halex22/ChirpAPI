using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChirpAPI.Models;

namespace ChirpAPI.Controllers
{
    public class CommetsController : Controller
    {
        private readonly CinguettioContext _context;

        public CommetsController(CinguettioContext context)
        {
            _context = context;
        }

        // GET: Commets
        public async Task<IActionResult> Index()
        {
            var cinguettioContext = _context.Commets.Include(c => c.Chirp).Include(c => c.Parent);
            return View(await cinguettioContext.ToListAsync());
        }

        // GET: Commets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commet = await _context.Commets
                .Include(c => c.Chirp)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commet == null)
            {
                return NotFound();
            }

            return View(commet);
        }

        // GET: Commets/Create
        public IActionResult Create()
        {
            ViewData["ChirpId"] = new SelectList(_context.Chirps, "Id", "Id");
            ViewData["ParentId"] = new SelectList(_context.Commets, "Id", "Id");
            return View();
        }

        // POST: Commets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChirpId,ParentId,Text,CreationDate")] Commet commet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChirpId"] = new SelectList(_context.Chirps, "Id", "Id", commet.ChirpId);
            ViewData["ParentId"] = new SelectList(_context.Commets, "Id", "Id", commet.ParentId);
            return View(commet);
        }

        // GET: Commets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commet = await _context.Commets.FindAsync(id);
            if (commet == null)
            {
                return NotFound();
            }
            ViewData["ChirpId"] = new SelectList(_context.Chirps, "Id", "Id", commet.ChirpId);
            ViewData["ParentId"] = new SelectList(_context.Commets, "Id", "Id", commet.ParentId);
            return View(commet);
        }

        // POST: Commets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChirpId,ParentId,Text,CreationDate")] Commet commet)
        {
            if (id != commet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommetExists(commet.Id))
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
            ViewData["ChirpId"] = new SelectList(_context.Chirps, "Id", "Id", commet.ChirpId);
            ViewData["ParentId"] = new SelectList(_context.Commets, "Id", "Id", commet.ParentId);
            return View(commet);
        }

        // GET: Commets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commet = await _context.Commets
                .Include(c => c.Chirp)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commet == null)
            {
                return NotFound();
            }

            return View(commet);
        }

        // POST: Commets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commet = await _context.Commets.FindAsync(id);
            if (commet != null)
            {
                _context.Commets.Remove(commet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommetExists(int id)
        {
            return _context.Commets.Any(e => e.Id == id);
        }
    }
}
