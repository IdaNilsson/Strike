using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Strike.Data;
using Strike.Models;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Hosting;

namespace Strike.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly StrikeContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdvertisementsController(StrikeContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
           _hostingEnvironment = hostingEnvironment;
        }

        // GET: Advertisements
        public async Task<IActionResult> Index(string search = null)
        {
            var strikeContext = _context.Advertisements.Include(a => a.User).Include(a => a.AdvertisementImages);
            if (search == null)
            {
                return View(await strikeContext.ToListAsync());
            }
            else
            {
                return View(await strikeContext.Where(a => a.Name.Contains(search) || a.Description.Contains(search) || search == null).ToListAsync());
            }
            
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Price,Description,Phone,CreatedDate,County,Area")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                //Handle image upload
                IFormFileCollection files = this.Request.Form.Files;

                var identity = (ClaimsIdentity)User.Identity;
                int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
                advertisement.UserId = userId;
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                SaveFileUploads(files, advertisement.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        private async void SaveFileUploads(IFormFileCollection files, int advertisementId)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //Places uploaded file in Uploads, generate UUID for filename + file extension
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Guid.NewGuid() + fileExtension;
                    string filePath = _hostingEnvironment.WebRootPath + "\\uploads\\" + fileName;
                    //Copy file from user to server
                    using (var inputStream = new FileStream(filePath, FileMode.Create))
                    {
                        Stream stream = file.OpenReadStream();
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(inputStream);
                    }

                    AdvertisementImage advertisementImage = new AdvertisementImage(file.Length, fileName, advertisementId);
                    _context.AdvertisementImages.Add(advertisementImage);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // GET: Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Price,Description,Phone,CreatedDate,UserId")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", advertisement.UserId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
