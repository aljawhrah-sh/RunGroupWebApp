using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupWebApp1.Helpers;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Models;
using RunGroupWebApp1.Repsitoery;
using RunGroupWebApp1.ViewModels;

namespace RunGroupWebApp1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IClubReposiroty _clubRepository;

    public HomeController(ILogger<HomeController> logger, IClubReposiroty clubReposiroty)
    {
        _logger = logger;
        _clubRepository = clubReposiroty;
    }

    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();
        try
        {
            string url = "https://ipinfo.io/86.60.117.26/json?token=b6246ad7edbc35";
            var info = new WebClient().DownloadString(url);
            ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
            RegionInfo myRT1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRT1.EnglishName;
            homeViewModel.City = ipInfo.City;
            homeViewModel.State = ipInfo.Region;
            if(homeViewModel.City != null)
            {
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
            }
            else
            {
                homeViewModel.Clubs = null;
            }
            return View(homeViewModel);
        }
        catch(Exception ex)
        {
            homeViewModel.Clubs = null;
        }
        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

