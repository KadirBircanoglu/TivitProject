using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TivitProject_BL.InterfaceofManagers;
using TivitProject_EL.IdentityModels;
using TivitProject_EL.ViewModels;
using TivitProject_PL.Models;

namespace TivitProject_PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserTivitManager _userTivitManager;
        private readonly IMapper _mapper;
        private readonly ITivitPhotoManager _photoManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IUserTivitManager userTivitManager, IMapper mapper, ITivitPhotoManager photoManager)
        {
            _logger = logger;
            _userManager = userManager;
            _userTivitManager = userTivitManager;
            _mapper = mapper;
            _photoManager = photoManager;
        }

        public IActionResult Index()
        {
            var tivits = _userTivitManager.GetAll(x => !x.IsDeleted).Data.OrderByDescending(x => x.InsertedDate).ToList();
            var model = _mapper.Map<List<TivitIndexViewModel>>(tivits);
            foreach (var tivit in model)
            {
                var media = _photoManager.GetAll(x => x.TivitId == tivit.Id).Data;
                tivit.TivitPhotos = new List<TivitPhotoDTO>();
                foreach (var item in media)
                {
                    tivit.TivitPhotos.Add(item);
                }
            }

            return View(model);
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


        [Authorize]
        [HttpGet]
        public IActionResult TivitIndex()
        {
            //useridyi sayfaya gönderelim böylece adres eklemede useridyi metoda aktarabiliriz
            var username = User.Identity?.Name;
            var user = _userManager.FindByNameAsync(username).Result;
            TivitIndexViewModel model = new TivitIndexViewModel();
            model.SelectedPictures = new List<IFormFile>();
            model.UserId = user.Id;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult TivitIndex(TivitIndexViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                    ModelState.AddModelError("", "Lütfen bilgileri eksiksiz giriniz!");
                    return View(model);
                }

                var tivit = _mapper.Map<UserTivitDTO>(model);
                tivit.InsertedDate = DateTime.Now;
                var result = _userTivitManager.Add(tivit);
                if (!result.IsSuccess)
                {
                    _logger.LogError($"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                    ModelState.AddModelError("", "Tivit kaydedilemedi!");
                    return View(model);
                }




                // resim ekleme
                if (model.SelectedPictures != null)
                {
                    foreach (var item in model.SelectedPictures)
                    {
                        if (item.ContentType.Contains("image") && item.Length > 0)
                        {
                            string fileName = $"{item.FileName.Substring(0, item.FileName.IndexOf('.'))}-{Guid.NewGuid().ToString().Replace("-", "")}";

                            string uzanti = Path.GetExtension(item.FileName);

                            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/TivitPictures/{fileName}{uzanti}");

                            string directoryPath =
                               Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/TivitPictures/");

                            if (!Directory.Exists(directoryPath))
                                Directory.CreateDirectory(directoryPath);

                            using var stream = new FileStream(path, FileMode.Create);

                            item.CopyTo(stream);
                            TivitPhotoDTO p = new TivitPhotoDTO()
                            {
                                TivitId = result.Data.Id,
                                MediaPath = $"/TivitPictures/{fileName}{uzanti}"
                            };
                            _photoManager.Add(p);

                        }
                    }
                }

                TempData["TivitIndexSuccessMsg"] = "Tivit attınız!";
                _logger.LogInformation($"Home/TivitIndex atılan tivit: {JsonConvert.SerializeObject(model)}");
                return RedirectToAction("TivitIndex", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                ModelState.AddModelError("", "Beklenmedik bir sorun oluştu!");
                return View(model);
            }
        }


    }
}