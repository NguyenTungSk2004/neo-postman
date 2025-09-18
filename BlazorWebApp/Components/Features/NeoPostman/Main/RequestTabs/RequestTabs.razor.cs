using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class RequestTabs : ComponentBase
    {
        private string _currentBody = string.Empty;

        [Parameter]
        public string Body { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<string> BodyChanged { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _currentBody = Body ?? string.Empty;
        }

        private async Task HandleBodyInput(ChangeEventArgs args)
        {
            _currentBody = args?.Value?.ToString() ?? string.Empty;
            if (BodyChanged.HasDelegate)
            {
                await BodyChanged.InvokeAsync(_currentBody);
            }
        }
    }
}
