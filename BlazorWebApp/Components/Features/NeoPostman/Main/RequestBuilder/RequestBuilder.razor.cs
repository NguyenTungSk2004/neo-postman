using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class RequestBuilder
    {
        [Parameter]
        public string Method { get; set; } = "GET";

        [Parameter]
        public EventCallback<string> OnMethodChanged { get; set; }

        [Parameter]
        public string Url { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<string> OnUrlChanged { get; set; }

        [Parameter]
        public EventCallback OnSend { get; set; }

        private async Task HandleMethodChange(ChangeEventArgs args)
        {
            if (OnMethodChanged.HasDelegate)
            {
                await OnMethodChanged.InvokeAsync(args?.Value?.ToString() ?? string.Empty);
            }
        }

        private async Task HandleUrlInput(ChangeEventArgs args)
        {
            if (OnUrlChanged.HasDelegate)
            {
                await OnUrlChanged.InvokeAsync(args?.Value?.ToString() ?? string.Empty);
            }
        }

        private async Task HandleSendClick()
        {
            if (OnSend.HasDelegate)
            {
                await OnSend.InvokeAsync();
            }
        }
    }
}