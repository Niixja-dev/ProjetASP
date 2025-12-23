using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetASP.Models;

namespace ProjetASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly GRHContext _context;

        public HomeController(GRHContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.TotalEmployes = _context.Employes.Count();
                ViewBag.EmployesActifs = _context.Employes.Where(e => e.EstActif).Count();
                ViewBag.CongesEnAttente = _context.Conges.Where(c => c.Statut == StatutConge.EnAttente).Count();
                ViewBag.PaiesNonPayees = _context.Paies.Where(p => !p.EstPaye).Count();
                
                var moisActuel = DateTime.Now.Month;
                var anneeActuelle = DateTime.Now.Year;
                
                var paiesDuMois = _context.Paies.Where(p => p.Mois == moisActuel && p.Annee == anneeActuelle).ToList();
                decimal masseSalariale = 0;
                foreach (var paie in paiesDuMois)
                {
                    masseSalariale = masseSalariale + paie.SalaireNet;
                }
                ViewBag.MasseSalariale = masseSalariale;
            }
            return View();
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
}
