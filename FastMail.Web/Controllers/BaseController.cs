using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace FastMail.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IRazorViewEngine _razorViewEngine;

        public BaseController(IRazorViewEngine razorViewEngine)
        {
            _razorViewEngine = razorViewEngine;
        }

        protected virtual string RenderPartialViewToString(string viewName, object model)
        {
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor, ModelState);

            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            //set model
            ViewData.Model = model;

            //try to get view by the name
            var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
            {
                //try to get a view by the path
                viewResult = _razorViewEngine.GetView(null, viewName, false);
                if (viewResult.View == null)
                    throw new ArgumentNullException($"{viewName} view was not found");
            }
            using (var stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext, viewResult.View, ViewData, TempData, stringWriter, new HtmlHelperOptions());

                var t = viewResult.View.RenderAsync(viewContext);
                t.Wait();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        protected virtual string RenderViewComponentToString(string componentName, object arguments = null)
        {
            if (string.IsNullOrEmpty(componentName))
                throw new ArgumentNullException(nameof(componentName));

            var actionContextAccessor = HttpContext.RequestServices.GetService(typeof(IActionContextAccessor)) as IActionContextAccessor;
            if (actionContextAccessor == null)
                throw new Exception("IActionContextAccessor cannot be resolved");

            var context = actionContextAccessor.ActionContext;

            var viewComponentResult = ViewComponent(componentName, arguments);

            var viewData = ViewData;
            if (viewData == null)
            {
                throw new NotImplementedException();
            }

            var tempData = TempData;
            if (tempData == null)
            {
                throw new NotImplementedException();
            }

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(context, NullView.Instance, viewData, tempData, writer, new HtmlHelperOptions());

                var viewComponentHelper = context.HttpContext.RequestServices.GetRequiredService<IViewComponentHelper>();
                (viewComponentHelper as IViewContextAware)?.Contextualize(viewContext);

                var result = viewComponentResult.ViewComponentType == null ?
                    viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentName, viewComponentResult.Arguments) :
                    viewComponentHelper.InvokeAsync(viewComponentResult.ViewComponentType, viewComponentResult.Arguments);

                result.Result.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        /// <summary>
        /// NullView implementation
        /// </summary>
        internal class NullView : IView
        {
            public static readonly NullView Instance = new NullView();

            public string Path => string.Empty;

            public Task RenderAsync(ViewContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                return Task.CompletedTask;
            }
        }
    }

}
