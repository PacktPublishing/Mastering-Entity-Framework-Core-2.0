using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.RawSql.Starter.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MasteringEFCore.RawSql.Starter.Attributes
{
    public class FutureOnlyAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var post = (Post) validationContext.ObjectInstance;
            return post.PublishedDateTime.CompareTo(DateTime.Now) < 0
                ? new ValidationResult("Publishing Date cannot be in past, kindly provide a future date")
                : ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-futureonly"] = "Publishing Date cannot be in past, kindly provide a future date";
        }
    }
}
