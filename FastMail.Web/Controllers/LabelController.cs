using FastMail.Web.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.Versioning;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace FastMail.Web.Controllers
{
    [SupportedOSPlatform("windows")]
    public class LabelController : BaseController
    {
        public LabelController(IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
        }

        // GET: LabelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LabelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LabelController/Create
        public ActionResult Create()
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

        // POST: LabelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (collection["submit"] == "submit")
                {
                    return RedirectToAction(nameof(Review));
                }
                else
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: LabelController/Edit/5
        public ActionResult Review(int id)
        {
            return View();
        }

        // GET: LabelController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.MailTypes = GetMailTypes();

            return View();
        }

        // POST: LabelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LabelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LabelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Byte[] GenerateBarcode(string id, string deptName)
        {
            Byte[] byteArray;

            string filePath = string.Format(FileDefaults.BarcodeImagePath, id);
            //if (_fileProvider.FileExists(filePath))
            //    return string.Format(FileDefaults.BarcodeImageVirtualPath, id);

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = FileDefaults.BarcodeHeight,
                    Width = FileDefaults.BarcodeWidth,
                    PureBarcode = true
                }
            };

            var barcode = writer.Write(id);
            using (var bitmap = new Bitmap(FileDefaults.BarcodeWidth, FileDefaults.LabelHeight))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, FileDefaults.BarcodeWidth, FileDefaults.LabelHeight);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(barcode,
                             new Rectangle(0, FileDefaults.BarcodeTop, FileDefaults.BarcodeWidth, 80),
                             new Rectangle(0, 0, FileDefaults.BarcodeWidth, FileDefaults.BarcodeHeight),
                             GraphicsUnit.Pixel);

                    string topText = (deptName.Length < 40) ? deptName : string.Concat(deptName.Substring(0, 40), "...");
                    float topTextLeft = 15f;
                    string bottomText = id;
                    float bottomTextLeft = (FileDefaults.BarcodeWidth / 2) - bottomText.Length * 4.5f;
                    graphics.DrawString(topText, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new PointF(topTextLeft, 10f));
                    graphics.DrawString(bottomText, new Font("Arial", 11), Brushes.Black, new PointF(bottomTextLeft, 110f));

                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        byteArray = ms.ToArray();
                    }
                }
            }

            return byteArray;
            //return string.Format(FileDefaults.BarcodeImageVirtualPath, id);
        }

        public Byte[] GenerateQrCode(string location)
        {
            Byte[] byteArray;

            var margin = 0;
            var qrCodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = FileDefaults.QRHeight,
                    Width = FileDefaults.QRWidth,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write(location);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.Save(ms, ImageFormat.Png);
                    byteArray = ms.ToArray();
                }
            }

            return byteArray;
        }

        public JsonResult GetLookupList(string id)
        {

            var departments = new List<SelectListItem>();
            departments.Add(new SelectListItem("ISD", "IS"));
            departments.Add(new SelectListItem("Board of Supervisor", "BS"));
            ViewBag.Departments = departments;

            string html = "";
            if (id == "recipient")
            {
                var model = new List<string>();
                model.Add("Lincoln");
                model.Add("Obama");
                model.Add("Bush");
                model.Add("Washington");
                model.Add("Padmaja");

                html = RenderPartialViewToString("_RecipientLookup", model);
            }
            else
            {
                html = RenderPartialViewToString("_SenderLookup", string.Empty);
            }

            return Json(new { html });
        }

        public ActionResult GetAddresses(string id)
        {
            var addresses = new List<SelectListItem>();
            addresses.Add(new SelectListItem("- Select address -", ""));
            addresses.Add(new SelectListItem("Mailroom (if sending to the department)", ""));
            addresses.Add(new SelectListItem("1234 Main St., Downey", "1"));
            addresses.Add(new SelectListItem("6152 Eastern Ave.,  Los Angeles", "2"));

            return Json(addresses);
        }

        public ActionResult GetRecipients(string id)
        {
            var recipients = new List<string>();
            recipients.Add("Andrew");
            recipients.Add("Bey");
            recipients.Add("Padmaja");
            recipients.Add("Senen");

            string html = "<table class=\"table table-hover\"><thead><tr><th>Name</th><th>Email</th></tr></thead><tbody>";
            foreach (var r in recipients)
            {
                html += $"<tr onclick=\"showRecipient()\" data-bs-dismiss=\"offcanvas\"><td>{r}</td><td></td></tr>";
            }
            html += "</tbody></table>";

            string routing = "Delivery Schedule: Monday thru Thursday <br />";
            routing += "Same day delivery: Upon request<br />";
            routing += "Room: Front desk<br />";
            routing += "Program: Front desk";

            return Json(new { html, routing });
        }

        public ActionResult GetSenders(string id)
        {
            var recipients = new List<string>();
            recipients.Add("Andrew Sender");
            recipients.Add("Bey Sender");
            recipients.Add("Padmaja Sender");
            recipients.Add("Senen Sender");

            string html = "<table class=\"table table-hover\"><thead><tr><th>Name</th><th>Email</th></tr></thead><tbody>";
            foreach (var r in recipients)
            {
                html += $"<tr onclick=\"showSender()\" data-bs-dismiss=\"offcanvas\"><td>{r}</td><td></td></tr>";
            }
            html += "</tbody></table>";

            string routing = "Delivery Schedule: Monday thru Thursday <br />";
            routing += "Same day delivery: Upon request<br />";
            routing += "Room: Front desk<br />";
            routing += "Program: Front desk";

            return Json(new { html, routing });
        }

    }
}
