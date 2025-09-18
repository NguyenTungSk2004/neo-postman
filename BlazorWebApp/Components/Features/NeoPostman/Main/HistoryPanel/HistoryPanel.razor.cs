using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class HistoryPanel
    {
        [Parameter]
        public IReadOnlyList<NeoHistoryEntry> Entries { get; set; } = Array.Empty<NeoHistoryEntry>();

        [Parameter]
        public EventCallback OnBack { get; set; }

        private async Task HandleBackClick()
        {
            if (OnBack.HasDelegate)
            {
                await OnBack.InvokeAsync();
            }
        }

        private static string GetStatusClass(int statusCode)
        {
            return statusCode >= 200 && statusCode < 400 ? "status-ok" : "status-error";
        }
    }
}