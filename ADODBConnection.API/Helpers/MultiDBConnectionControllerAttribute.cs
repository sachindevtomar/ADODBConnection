using ADODBConnection.Contracts.MultiDBConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ADODBConnection.API.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MultiDBConnectionControllerAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // get account data store type from header
            string dbConnectionHeader;
            IHeaderDictionary headers = context.HttpContext.Request?.Headers;

            if (!(context.Controller is IMultiDBConnectionSetter controller)) { return; }

            if (!headers.ContainsKey("DBConnectionType")) { return; }

            dbConnectionHeader = headers["DBConnectionType"];
            if (Enum.TryParse(dbConnectionHeader, out DBConnectionType dbConnectionType))
            {
                controller.SetDBConnection(dbConnectionType);
            }
        }
    }
}
