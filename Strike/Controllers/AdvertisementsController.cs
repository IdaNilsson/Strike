﻿using System;
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
using System.Collections.Generic;

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
        public async Task<IActionResult> Index(string search = null, List<int> categoryIds = null, string county = null)
        {
            List<Advertisement> advertisements;
            ViewData["Categories"] = await _context.Categories.ToListAsync();
            if (search == null && categoryIds == null && county == null)
            {
                advertisements = await _context.Advertisements
                    .Include(a => a.User)
                    .Include(a => a.AdvertisementImages)
                    .Include(a => a.AdvertisementCategories)
                        .ThenInclude(ac => ac.Category)
                    .ToListAsync();
            }
            else
            {
                advertisements = await _context.Advertisements
                    .Include(a => a.User)
                    .Include(a => a.AdvertisementImages)
                    .Include(a => a.AdvertisementCategories)
                        .ThenInclude(ac => ac.Category)
                    .ToListAsync();

                if (search != null)
                {
                    advertisements = advertisements
                        .Where(a => a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) || a.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (categoryIds != null && categoryIds.Count > 0)
                {
                    advertisements = advertisements
                       .Where(a => categoryIds == null || a.AdvertisementCategories.Any(ac => categoryIds.Contains(ac.CategoryId)))
                       .ToList();
                }

                if (county != null && !county.Equals("Välj"))
                {
                    advertisements = advertisements
                        .Where(a => county == null || a.County.Contains(county, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
            }

            return View(advertisements.OrderBy(a => a.CreatedDate).Reverse());
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
                .Include(a => a.AdvertisementImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Categories"] = _context.Categories.ToList();
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Price,Description,Phone,CreatedDate,County,Area,CategoryIds")] Advertisement advertisement)
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

                foreach (int id in advertisement.CategoryIds)
                {
                    _context.AdvertisementCategories.Add(new AdvertisementCategory(advertisement.Id, id));
                }
                await _context.SaveChangesAsync();

                SaveFileUploads(files, advertisement.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.AdvertisementCategories)
                .Include(a => a.AdvertisementImages)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = _context.Categories.ToList();
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Name, string Title, double Price, string Description, string Phone, List<int> CategoryIds)
        {
            Advertisement advertisement = await _context.Advertisements.FindAsync(id);
            List<AdvertisementCategory> advertisementCategories = await _context.AdvertisementCategories.Where(ac => ac.AdvertisementId == id).ToListAsync();
            IFormFileCollection files = this.Request.Form.Files;

            bool hasChanged =
                !Name.Equals(advertisement.Name) ||
                !Title.Equals(advertisement.Title) ||
                !Description.Equals(advertisement.Description) ||
                !Phone.Equals(advertisement.Phone) ||
                Price != advertisement.Price;

            if (hasChanged)
            {
                advertisement.Name = Name;
                advertisement.Title = Title;
                advertisement.Description = Description;
                advertisement.Phone = Phone;
                advertisement.Price = Price;
            }
            _context.Update(advertisement);
            await _context.SaveChangesAsync();

            foreach (AdvertisementCategory advertisementCategory in advertisementCategories)
            {
                _context.AdvertisementCategories.Remove(advertisementCategory);
            }

            await _context.SaveChangesAsync();

            foreach (int categoryId in CategoryIds)
            {
                _context.AdvertisementCategories.Add(new AdvertisementCategory(id, categoryId));
            }

            await _context.SaveChangesAsync();

            SaveFileUploads(files, id);

            ViewData.Add("changed", "Dina inställningar är nu uppdaterade!");

            return RedirectToAction(nameof(Index));
        }


        // GET: Advertisements/Delete/5
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyAdvertisements));
        }

        [Authorize]
        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }

        // GET: MyAdvertisements
        [Authorize]
        public async Task<IActionResult> MyAdvertisements()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);

            List<Advertisement> advertisements = await _context.Advertisements
                .Where(a => a.UserId == userId)
                .Include(a => a.AdvertisementImages)
                .ToListAsync();

            return View(advertisements);
        }

        [Authorize]
        public async Task<IActionResult> DeleteImage(int? id)   
        {
            var img = await _context.AdvertisementImages.FindAsync(id);
            _context.AdvertisementImages.Remove(img);
            await _context.SaveChangesAsync();
            ViewData["Categories"] = _context.Categories.ToList();
            return RedirectToAction("Edit", new { id =  img.AdvertisementId});
        }
    }
}
