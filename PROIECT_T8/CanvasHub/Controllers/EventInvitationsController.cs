using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanvasHub.Models;

namespace CanvasHub.Controllers
{
    public class EventInvitationsController : Controller
    {
        private readonly CanvasHubContext _context;

        public EventInvitationsController(CanvasHubContext context)
        {
            _context = context;
        }

        // GET: EventInvitations
        public async Task<IActionResult> Index()
        {
            var canvasHubContext = _context.EventInvitations.Include(e => e.Event).Include(e => e.Message);
            return View(await canvasHubContext.ToListAsync());
        }

        // GET: EventInvitations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInvitation = await _context.EventInvitations
                .Include(e => e.Event)
                .Include(e => e.Message)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventInvitation == null)
            {
                return NotFound();
            }

            return View(eventInvitation);
        }

        // GET: EventInvitations/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["MessageId"] = new SelectList(_context.Messages, "MessageId", "MessageId");
            return View();
        }

        // POST: EventInvitations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventInvitationId,StatusInvitation,EventId,MessageId,Id")] EventInvitation eventInvitation)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(eventInvitation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventInvitation.EventId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "MessageId", "MessageId", eventInvitation.MessageId);
            return View(eventInvitation);
        }

        // GET: EventInvitations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInvitation = await _context.EventInvitations.FindAsync(id);
            if (eventInvitation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventInvitation.EventId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "MessageId", "MessageId", eventInvitation.MessageId);
            return View(eventInvitation);
        }

        // POST: EventInvitations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventInvitationId,StatusInvitation,EventId,MessageId,Id")] EventInvitation eventInvitation)
        {
            if (id != eventInvitation.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(eventInvitation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventInvitationExists(eventInvitation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventInvitation.EventId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "MessageId", "MessageId", eventInvitation.MessageId);
            return View(eventInvitation);
        }

        // GET: EventInvitations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventInvitation = await _context.EventInvitations
                .Include(e => e.Event)
                .Include(e => e.Message)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventInvitation == null)
            {
                return NotFound();
            }

            return View(eventInvitation);
        }

        // POST: EventInvitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventInvitation = await _context.EventInvitations.FindAsync(id);
            if (eventInvitation != null)
            {
                _context.EventInvitations.Remove(eventInvitation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventInvitationExists(int id)
        {
            return _context.EventInvitations.Any(e => e.Id == id);
        }
    }
}
