using CicekSepetiTech.Case.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace CicekSepetiTech.Case.Api.Filters
{
    public class ValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorList = context.ModelState.ToDictionary(x => x.Key, y => y.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                var model = new ReturnModel<Dictionary<string, string[]>> { Data = errorList };
                model.Result.Status = ReturnStatus.Error;
                model.Result.ErrorCategory = "Validate";
                context.Result = new BadRequestObjectResult(model);
            }
        }
    }
}