using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace PontoPlus.Manager.Services.Filters
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public string Departamento { get; set; }
        public AutorizacaoFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            object usuarioId = context.HttpContext.Session.GetString("UserId");
            object usuarioDepartamento = context.HttpContext.Session.GetString("UserDepartamento");

            if (usuarioId == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { Controller = "Home", action = "Login" }
                    )
                );
                return;
            }

            if (Departamento != null)
            {
                if (usuarioDepartamento.ToString() != Departamento.ToString())
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { Controller = "Home", action = "Login" }
                        )
                    );
                }
            }
        }
    }
}