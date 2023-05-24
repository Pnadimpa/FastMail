using FastMail.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FastMail.Web.Controllers
{
    public class RecipientController : BaseController
    {
       private readonly FastMailContext _context;

        public RecipientController(FastMailContext context,
                 IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = _context.Recipients.ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //code for submit button
        public ActionResult Create(Recipient model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //throw new Exception("Please Enter All Required Fields.");
                    //This process adds data to the database table Recipients.
                    _context.Recipients.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Recipient));
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Recipients.Find(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //code for submit button
        public ActionResult Edit(Recipient model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //throw new Exception("Please Enter All Required Fields.");
                    //This process adds data to the database table Recipients.
                    _context.Recipients.Update(model);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    return RedirectToAction(nameof(Recipient));
                    // return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            var recipientId = collection["recipientId"].ToString();
            var recipientName = collection["recipientName"].ToString();

            var model = _context.Recipients.ToList();
            if (!string.IsNullOrEmpty(recipientId))
                model = model.Where(x => x.RecipientId.ToLower().Contains(recipientId.ToLower())).ToList();
            if (!string.IsNullOrEmpty(recipientName))
                model = model.Where(x => x.RecipientName.ToUpper().Contains(recipientName.ToUpper())).ToList();

            return View(model);
        }
    }
}