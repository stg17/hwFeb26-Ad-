using hwFeb26__Ad_.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using hwFeb26__Ad_.Data;

namespace hwFeb26__Ad_.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=AddSite; Integrated Security=true;";
        private static List<int> _myAds;
        
        public IActionResult Index()
        {
            _myAds = HttpContext.Session.Get<List<int>>("my-ads");
            var manager = new AdDBManager(_connectionString);
            var vm = new AdViewModel { Ads = manager.GetAds() };
            if (_myAds != null)
            {
                foreach (var ad in vm.Ads)
                {
                    if (_myAds.Any(id => id == ad.Id))
                    {
                        ad.MyAd = true;
                    }
                }
            }
            else
            {
                _myAds = new();
            }

            return View(vm);
        }

        public IActionResult NewAd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAd(Ad ad)
        {
            var manager = new AdDBManager(_connectionString);
            manager.AddAd(ad);
            _myAds.Add(ad.Id);
            HttpContext.Session.Set("my-ads", _myAds);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult DeleteAd(int Id)
        {
            var manager = new AdDBManager(_connectionString);
            manager.Delete(Id);
            return Redirect("/");
        }
    }
}
