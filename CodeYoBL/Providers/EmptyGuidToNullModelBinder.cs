using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace CodeYoBL.Services
{
    public class EmptyGuidToNullModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var modelType = bindingContext.ModelType;

            if (modelType != typeof(Guid) && modelType != typeof(Guid?))
            {
                return Task.CompletedTask;
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Empty or whitespace => treat differently for nullable vs non-nullable
            if (string.IsNullOrWhiteSpace(value))
            {
                if (modelType == typeof(Guid?))
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                }
                else
                {
                    // For non-nullable Guid, set Guid.Empty (so binder succeeds and ModelState is valid)
                    bindingContext.Result = ModelBindingResult.Success(Guid.Empty);
                }
                return Task.CompletedTask;
            }

            // try parse
            if (Guid.TryParse(value, out var parsedGuid))
            {
                bindingContext.Result = ModelBindingResult.Success(parsedGuid);
            }
            else
            {
                // invalid format -> register model error
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid GUID format.");
            }

            return Task.CompletedTask;
        }
    }
}
