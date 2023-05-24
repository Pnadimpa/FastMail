using FastMail.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Runtime.InteropServices;

namespace FastMail.Web.Controllers
{
    public class UserController : BaseController
    {
        // private readonly ILogger<UsersController> _logger;
        private readonly FastMailContext _context;

        public UserController(FastMailContext context,
                 IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            // var model = _context.Users.ToList();
            var model = _context.Users.ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
            //return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //code for submit button
        public ActionResult Create(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //throw new Exception("Please Enter All Required Fields.");
                    //This process adds data to the database table users.
                    _context.Users.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                    // return base.RedirectToAction(nameof(Core.Domain.User));
                }
                else
                {
                    //return base.RedirectToAction(nameof(Core.Domain.User));
                    return base.RedirectToAction(nameof(User));
                    //  return View(model);
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
            var UserId = collection["UserId"].ToString();
            var UserName = collection["UserName"].ToString();

            var model = _context.Users.ToList();
            if (!string.IsNullOrEmpty(UserId))
                model = model.Where(x => x.UserId.ToLower().Contains(UserId.ToLower())).ToList();
            if (!string.IsNullOrEmpty(UserName))
                model = model.Where(x => x.UserName.ToUpper().Contains(UserName.ToUpper())).ToList();

            return View(model);
        }

        // GET: LabelController/Delete/5
        public ActionResult Delete(int id)
        {
            var User = _context.Users.Find(id);
            //in case we use GET action
            //if (label != null)
            //{
            //    _context.Labels.Remove(label);
            //    _context.SaveChanges();
            //}
            //return RedirectToAction(nameof(Index));

            return View(User);
        }

        // POST: LabelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var User = _context.Users.Find(id);
                if (User != null)
                {
                    _context.Users.Remove(User);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LabelController/Delete/5
        public ActionResult Edit(int id)
        {
            var User = _context.Users.Find(id);
            //in case we use GET action
            //if (label != null)
            //{
            //    _context.Labels.Remove(label);
            //    _context.SaveChanges();
            //}
            //return RedirectToAction(nameof(Index));

            return View(User);
        }

        // POST: LabelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var User = _context.Users.Find(id);
                if (User != null)
                {
                    _context.Users.Remove(User);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }

}
