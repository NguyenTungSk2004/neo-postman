using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class ResponsePanel
    {
        [Parameter]
        public NeoResponseSnapshot? Response { get; set; }

        private string StatusLabel => Response?.StatusLabel ?? "-";

        private string TimeLabel => Response is { ElapsedMilliseconds: var ms } ? $"{ms} ms" : "-";

        private string SizeLabel => Response is { SizeKb: var kb }
            ? $"{kb.ToString("0.00", CultureInfo.InvariantCulture)} KB"
            : "-";

        private string PrettyContent => Response?.PrettyContent ?? "{ \"data\": [] }";

        private string RawContent => Response?.RawContent ?? "HTTP/1.1 200 OK";

        private string GetStatusChipClass() => Response switch
        {
            { IsSuccess: true } => "neo-chip neo-chip-ok",
            { IsSuccess: false } => "neo-chip neo-chip-error",
            _ => "neo-chip"
        };
    }
}