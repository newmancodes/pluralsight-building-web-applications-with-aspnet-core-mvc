using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            var piesListViewModel = new PiesListViewModel
            {
                CurrentCategory = "Cheesecakes",
                Pies = this.pieRepository.AllPies,
            };

            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = this.pieRepository.GetPieByPieId(id);
            
            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }
    }
}
