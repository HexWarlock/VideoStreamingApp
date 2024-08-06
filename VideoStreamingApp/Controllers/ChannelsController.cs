using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoStreamingApp.Data;
using VideoStreamingApp.Models;

namespace VideoStreamingApp.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChannelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Channels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Channels.Include(c => c.ContentProvider);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Channels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .Include(c => c.ContentProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        // GET: Channels/Create
        public IActionResult Create()
        {
            ViewData["ContentProviderId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContentProviderId")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentProviderId"] = new SelectList(_context.Users, "Id", "Id", channel.ContentProviderId);
            return View(channel);
        }

        // GET: Channels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }
            ViewData["ContentProviderId"] = new SelectList(_context.Users, "Id", "Id", channel.ContentProviderId);
            return View(channel);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContentProviderId")] Channel channel)
        {
            if (id != channel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelExists(channel.Id))
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
            ViewData["ContentProviderId"] = new SelectList(_context.Users, "Id", "Id", channel.ContentProviderId);
            return View(channel);
        }

        // GET: Channels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .Include(c => c.ContentProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel != null)
            {
                _context.Channels.Remove(channel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}
