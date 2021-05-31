using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class CustomValidation : ComponentBase
    {
        private ValidationMessageStore messageStore;

        [CascadingParameter]
		private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CustomValidation)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(CustomValidation)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            messageStore = new(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (s, e) => messageStore.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) => messageStore.Clear(e.FieldIdentifier);
        }

        public void ClearErrors()
        {
            messageStore.Clear();
            CurrentEditContext.NotifyValidationStateChanged();
        }

        public void AddError(string fieldName, string error)
        {
            messageStore.Add(CurrentEditContext.Field(fieldName), error);
            CurrentEditContext.NotifyValidationStateChanged();
        }

    }
}
