using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class NeoMainBar
    {
        [Parameter]
        public EventCallback OnShowHistory { get; set; }

        [Parameter]
        public bool HasHistoryEntries { get; set; }

        [Parameter]
        public bool IsHistoryMode { get; set; }

        private async Task HandleHistoryClick()
        {
            if (OnShowHistory.HasDelegate)
            {
                await OnShowHistory.InvokeAsync();
            }
        }
    }
}