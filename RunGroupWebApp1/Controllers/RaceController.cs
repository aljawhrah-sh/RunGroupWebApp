using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp1.Models;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Data;
using RunGroupWebApp1.ViewModels;
using System.Diagnostics;
using System.Net;

namespace RunGroupWebApp1.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoServices _photoServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IRaceRepository _raceRepository;
        public RaceController(IPhotoServices photoServices, IRaceRepository raceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _photoServices = photoServices;
            _raceRepository = raceRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            Race race = await _raceRepository.GetByIdAsyn(id);
            return View(race);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //use ClaimPrincipalExtensions to get AppUserId
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createRaceViewModel = new CreateRaceViewModel { AppUserId = curUserId };

            return View(createRaceViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {

            if (ModelState.IsValid)
            {
                var result = await _photoServices.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        Street = raceVM.Address.Street,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload error");

            }
            return View(raceVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsyn(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Could not Delete photo");
                return View("Error");
            }
            var userRace = await _raceRepository.GetByIdAsyn(id);
            if (userRace != null)
            {
                try
                {
                    await _photoServices.DeletePhotoAsync(userRace.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View("Error");
                }
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address
                };
                _raceRepository.update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsyn(id);
            if (raceDetails == null)
                return View("Error");
            else
                return View(raceDetails);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsyn(id);
            if (raceDetails == null)
                return View("Error");
            _raceRepository.delete(raceDetails);
            return RedirectToAction("Index");
        }
    }
}
