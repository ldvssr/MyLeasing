using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;

        public LesseesController(ILesseeRepository lesseeRepository,
            IUserHelper userHelper, IBlobHelper blobHelper,
            IConverterHelper converterHelper)
        {
            _lesseeRepository = lesseeRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        public IActionResult Index()
        {
            return View(_lesseeRepository.GetAll());
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null) return NotFound();

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                    imageId =
                        await _blobHelper.UploadBlobAsync(model.ImageFile,
                            "lessees");

                var lessee = _converterHelper.ToLessee(model, imageId, true);

                lessee.User =
                    await _userHelper.GetUserByEmailAsync(
                        "rubinhodavid@gmail.com");
                await _lesseeRepository.CreateAsync(lessee);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null) return NotFound();

            var view = _converterHelper.ToLesseeViewModel(lessee);
            return View(view);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                        imageId =
                            await _blobHelper.UploadBlobAsync(model.ImageFile,
                                "lessees");

                    var lessee =
                        _converterHelper.ToLessee(model, imageId, false);

                    lessee.User =
                        await _userHelper.GetUserByEmailAsync(
                            "rubinhodavid@gmail.com");
                    await _lesseeRepository.UpdateAsync(lessee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lesseeRepository.ExistAsync(model.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null) return NotFound();

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _lesseeRepository.GetByIdAsync(id);
            await _lesseeRepository.DeleteAsync(lessee);
            return RedirectToAction(nameof(Index));
        }
    }
}