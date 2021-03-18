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

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory = category;

            if (string.IsNullOrEmpty(category))
            {
                pies = this.pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = this.pieRepository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
                currentCategory = this.categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            var piesListViewModel = new PiesListViewModel
            {
                CurrentCategory = currentCategory,
                Pies = pies,
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
