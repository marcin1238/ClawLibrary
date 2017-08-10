using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClawLibrary.Web.Filters
{
    public class DefaultResponsesOperationFilter : IOperationFilter
    {
        private static readonly Dictionary<int, string> _defaultResponses = new Dictionary<int, string>
        {
            { 200, "Ok" },
            { 201, "Created" },
            { 400, "Model was wrong" },
            { 401, "Unauthorized" },
            { 404, "Not found" },
            { 410, "API endpoint or request is obsolete e.g. specific version of the message has been discontinued" },
            { 429, "Exception was thrown" },
            { 500, "Internal server error" },
            { 503, "Service is down for the maintanace or overloaded with the request" }
        };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (var response in _defaultResponses)
            {
                var code = response.Key.ToString();
                if (operation.Responses.ContainsKey(code)) continue;
                operation.Responses.Add(code, new Response() { Description = response.Value });
            }

            // Only POST should return '201 Created'
            if (context.ApiDescription.HttpMethod.ToUpper() != "POST")
            {
                operation.Responses.Remove("201");
            }
        }
    }
}
