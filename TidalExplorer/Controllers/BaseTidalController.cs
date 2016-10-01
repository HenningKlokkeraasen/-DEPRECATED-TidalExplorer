using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using OpenTidl.Methods;
using TidalExplorer.TidalIntegration;

namespace TidalExplorer.Controllers
{
    public abstract class BaseTidalController : Controller
    {
        protected async Task<ActionResult> ViewWithDataOrEmptyResult<T>(string viewName, Func<OpenTidlSession, Task<T>> dlgt)
        {
            var x = await InvokeMethodWithTidalSessionFromClaimsIdentity(dlgt);

            if (x == null)
                return new EmptyResult();

            return View(viewName, x);
        }

        protected async Task<T> InvokeMethodWithTidalSessionFromClaimsIdentity<T>(Func<OpenTidlSession, Task<T>> dlgt)
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(User.Identity);
            if (session == null)
                return default(T);
            var t = await InvokeAsyncMethod(() => dlgt.Invoke(session));
            return t;
        }

        protected static async Task<T> InvokeAsyncMethod<T>(Func<Task<T>> func)
        {
            return await func();
        }
    }
}