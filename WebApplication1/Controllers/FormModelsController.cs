#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class FormModelsController : Controller
    {
        private readonly IServicesRepo _serviceRepo;
        private readonly ApplicationDbContext _context;

        public FormModelsController(ApplicationDbContext context,IServicesRepo servicesRepo)
        {
            _serviceRepo = servicesRepo;
            _context = context;
        }

        // GET: FormModels
        public async Task<IActionResult> Index()
        {
            ListOfForms temp = new();
            temp.FormList = _serviceRepo.FindAlltheList();
            foreach(var item in temp.FormList)
            {
                // puts mask for better view on phones
                try { item.Phone = String.Format("{0:(###) ###-####}", Int64.Parse(item.Phone));} catch { }
                

            }
            return View(temp);
        }

        // GET: FormModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formModel = await _context.FormModel
                .FirstOrDefaultAsync(m => m.TrIndex == id);
            if (formModel == null)
            {
                return NotFound();
            }

            return View(formModel);
        }

        // GET: FormModels/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: FormModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrIndex,TrDateTime,FirstName,LastName,Email,Phone,BirthDate")] FormModel formModel)
        {
            // remove mask from phone
            formModel.TrDateTime = DateTime.Now;
            try {formModel.Phone = formModel.Phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""); } catch { }
            
            if (formModel.Phone.Length > 10 || formModel.Phone.Length < 10)
            {
                if (formModel.Phone.Length > 10)
                {
                    TempData["Errormessage"] = "Phone number cannot be longer than 10 digits";
                   
                }
                else { TempData["Errormessage"] = "Phone number cannot be less than 10 digits"; }
                
                return View(formModel);
            }
            if (ModelState.IsValid)
            {
                _context.Add(formModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formModel);
        }

        // GET: FormModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formModel = await _context.FormModel.FindAsync(id);
            if (formModel == null)
            {
                return NotFound();
            }
            return View(formModel);
        }

        // POST: FormModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrIndex,TrDateTime,FirstName,LastName,Email,Phone,BirthDate")] FormModel formModel)
        {
            if (id != formModel.TrIndex)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormModelExists(formModel.TrIndex))
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
            return View(formModel);
        }

        // GET: FormModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formModel = await _context.FormModel
                .FirstOrDefaultAsync(m => m.TrIndex == id);
            if (formModel == null)
            {
                return NotFound();
            }

            return View(formModel);
        }

        // POST: FormModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formModel = await _context.FormModel.FindAsync(id);
            _context.FormModel.Remove(formModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormModelExists(int id)
        {
            return _context.FormModel.Any(e => e.TrIndex == id);
        }
    }
}
