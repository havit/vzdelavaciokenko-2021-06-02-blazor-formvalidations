using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
	public class ExampleModelValidation : ComponentBase
	{
        private ValidationMessageStore messageStore;

        [CascadingParameter]
        private EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if ((CurrentEditContext == null) || (CurrentEditContext.Model == null) || (CurrentEditContext.Model is not Demo09.ExampleModel))
			{
                throw new InvalidOperationException();
            }
            
            messageStore = new ValidationMessageStore(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (sender, eventArgs) => Validate();
            CurrentEditContext.OnFieldChanged += (sender, eventArgs) => messageStore.Clear(eventArgs.FieldIdentifier);
        }

        public void Validate()
        {
            messageStore.Clear();

            Demo09.ExampleModel model = (Demo09.ExampleModel)CurrentEditContext.Model;

            if (String.IsNullOrEmpty(model.Name))
			{
                messageStore.Add(CurrentEditContext.Field(nameof(model.Name)), "Please fill the name (from custom validator).");
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
