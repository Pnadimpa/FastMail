using FastMail.Core.Domain;
using FastMail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FastMail.Web.Controllers
{
    public class SpecialPackageController : BaseController
    {
        private readonly FastMailContext _context;

        // public SpecialPackageController(FastMailContext Context)
        public SpecialPackageController(FastMailContext context,
                  IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
            //return RedirectToAction("Index", "Home");

        }

        public IActionResult Create()
        {
            ViewBag.MailTypes = GetMailTypes();
            return View();
        }
        private IList<SelectListItem> GetMailTypes()
        {
            var mailTypes = new List<SelectListItem>();
            mailTypes.Add(new SelectListItem("Small Envelope", "se"));
            mailTypes.Add(new SelectListItem("Large Envelope", "le"));
            mailTypes.Add(new SelectListItem("Box", "bx"));
            mailTypes.Add(new SelectListItem("Package", "pk"));
            mailTypes.Add(new SelectListItem("Locked Pouch", "lp"));
            mailTypes.Add(new SelectListItem("Unlocked Pouch", "up"));
            mailTypes.Add(new SelectListItem("Payroll Bag", "pb"));
            mailTypes.Add(new SelectListItem("Other", "ot"));

            return mailTypes;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //code for submit button
        public ActionResult Create(SpecialPackage model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   //throw new Exception("Please Enter All Required Fields.");
                    _context.SpecialPackages.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Create));
                }
                else
                {
                    ViewBag.MailTypes = GetSpecialPackageMailTypes();
                    return RedirectToAction(nameof(Create));
                   // return View(model);
                }
            }
            catch (Exception ex)
            {
               TempData["Message"]=ex.Message;
                ViewBag.MailTypes = GetMailTypes();
                return View(model);
            }
        }
       
        private dynamic GetSpecialPackageMailTypes()
        {
            throw new NotImplementedException();
        }
    }
}



