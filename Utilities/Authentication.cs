﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace proyectoWeb_GYM.Utilities
{
    public class Authentication: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if(filterContext.HttpContext.Session.GetString("Usuario") == null)
            {
                filterContext.Result = new RedirectToRouteResult(

                    new RouteValueDictionary
                    {
                        {"Controller", "Home" },
                        {"Action", "Login" }
                    }

                    );
            }

        }


    }
}
