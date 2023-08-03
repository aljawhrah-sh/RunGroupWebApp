using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Models;
using RunGroupWebApp1.Repsitoery;
using RunGroupWebApp1.ViewModels;

namespace RunGroupWebApp1.Controllers
{
    public class ClubController : Controller
    {

        private readonly IPhotoServices _photoServices;
        private readonly IClubReposiroty _clubReposiroty;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IPhotoServices photoServices, IClubReposiroty clubReposiroty, IHttpContextAccessor httpContextAccessor)
        {
            _clubReposiroty = clubReposiroty;
            _photoServices = photoServices;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //to bring back all the data
            var clubs = await _clubReposiroty.GetAll();
            return View(clubs);
        }
        [HttpGet]
        //return detail page
        public async Task<IActionResult> Detail(int Id)
        {

            var club = await _clubReposiroty.GetByIdAsync(Id);
            return View(club);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //using ClaimPrincipalExtensions to get the user id
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View(createClubViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            

            if (ModelState.IsValid)
            {
                var result = await _photoServices.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId =clubVM.AppUserId,
                    creationDate = DateTime.Now,
                    Address = new Address
                    {
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                    }
                };
                _clubReposiroty.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");

            }
            return View(clubVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubReposiroty.GetByIdAsync(id);
            if (club == null) {
            return View("error", new ErrorViewModel { });
            }
            
             //creating a vm object from a Club instance
             // we can use automapper here.
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory,
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit club");
                return View("Error");
            }
            var userClub = await _clubReposiroty.GetByIdAsync(id);
            if (userClub != null)
            {
                try
                {
                    await _photoServices.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View("Error");
                }
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                    LastUpdateDate = DateTime.Now
                };
                _clubReposiroty.update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubReposiroty.GetByIdAsync(id);
            if (clubDetails == null)
                return View("Error");
            else
                return View(clubDetails);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubReposiroty.GetByIdAsync(id);
            if (clubDetails == null)
                return View("Error");
            _clubReposiroty.delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}
