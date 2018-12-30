using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Strike.Data;
using Strike.Models;

namespace Strike.Controllers
{
    public class MessagesController : Controller
    {
        private readonly StrikeContext _context;

        public MessagesController(StrikeContext context)
        {
            _context = context;
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.MessageSender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            if (message.IsRead == false)
            {
                message.IsRead = true;
                _context.Update(message);
                await _context.SaveChangesAsync();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create(int? receiverUserId, int? advertisementId)
        {
            ViewData["receiverUserId"] = receiverUserId;
            ViewData["advertisementId"] = advertisementId;
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int receiverUserId, int? advertisementId, string subject, string text)
        {
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(text))
            {
                return View();
            }

            Message message = new Message(subject, text);
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            MessageSender messageSender = new MessageSender(message.Id, userId);
            _context.MessageSenders.Add(messageSender);

            MessageReceiver messageReceiver = new MessageReceiver(message.Id, receiverUserId);
            _context.MessageReceivers.Add(messageReceiver);

            await _context.SaveChangesAsync();

            if (advertisementId == null)
            {
                return RedirectToAction("ReceivedMessages", "Messages");
            }
            else
            {
                return RedirectToAction("Details", "Advertisements", new { id = advertisementId });
            }
            
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Delete));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ReceivedMessages()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            User loggedInUser = await _context.Users
                .Include(u => u.ReceivedMessages)
                    .ThenInclude(rm => rm.Message)
                .Include(u => u.ReceivedMessages)
                    .ThenInclude(rm => rm.User)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return View(loggedInUser.ReceivedMessages.OrderBy(rm => rm.Message.Created).Reverse());
        }

        public async Task<IActionResult> SentMessages()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            User loggedInUser = await _context.Users
                .Include(u => u.SentMessages)
                    .ThenInclude(sm => sm.Message)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return View(loggedInUser.SentMessages);
        }

        //Returns count of unread messages
        [HttpGet]
        public JsonResult GetUnreadCount()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            User loggedInUser = _context.Users
                .Include(u => u.ReceivedMessages)
                    .ThenInclude(rm => rm.Message)
                .FirstOrDefault(u => u.Id == userId);
            int unreadMessagesCount = loggedInUser.ReceivedMessages.Where(rm => !rm.Message.IsRead).Count();

            return Json(new { count = unreadMessagesCount });
        }
    }
}
