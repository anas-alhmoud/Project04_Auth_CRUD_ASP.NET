﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project04_Auth_CRUD_ASP.NET.Data;

namespace Project04_Auth_CRUD_ASP.NET.Models
{
    [Authorize]
    public class PricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PriceModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prices.ToListAsync());
        }

        // GET: PriceModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Prices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceModel == null)
            {
                return NotFound();
            }

            return View(priceModel);
        }

        // GET: PriceModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Time")] PriceModel priceModel)
        {
            if (ModelState.IsValid)
            {
                priceModel.Id = Guid.NewGuid();
                _context.Add(priceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceModel);
        }

        // GET: PriceModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Prices.FindAsync(id);
            if (priceModel == null)
            {
                return NotFound();
            }
            return View(priceModel);
        }

        // POST: PriceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Time")] PriceModel priceModel)
        {
            if (id != priceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceModelExists(priceModel.Id))
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
            return View(priceModel);
        }

        // GET: PriceModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceModel = await _context.Prices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceModel == null)
            {
                return NotFound();
            }

            return View(priceModel);
        }

        // POST: PriceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var priceModel = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(priceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceModelExists(Guid id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }
    }
}