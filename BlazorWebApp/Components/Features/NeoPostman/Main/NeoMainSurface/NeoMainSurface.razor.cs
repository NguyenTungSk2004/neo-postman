using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public partial class NeoMainSurface : ComponentBase
    {
        private const string DefaultBody = "{\n  \"name\": \"Webcam 1080p\",\n  \"price\": 890000,\n  \"inStock\": true\n}";
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
        private static readonly Random Rng = new();

        private readonly List<NeoHistoryEntry> _history = new();

        private NeoWorkspaceMode _mode = NeoWorkspaceMode.Builder;
        private string _method = "POST";
        private string _url = "https://api.example.com/v1/products";
        private string _body = DefaultBody;
        private NeoResponseSnapshot? _response;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SeedHistory();
        }

        private void SeedHistory()
        {
            var now = DateTime.Now;
            _history.Clear();
            _history.AddRange(new[]
            {
                new NeoHistoryEntry(now - TimeSpan.FromMinutes(2), "You", "GET", "/api/products?limit=20", 200),
                new NeoHistoryEntry(now - TimeSpan.FromMinutes(9), "You", "POST", "/api/auth/login", 200),
                new NeoHistoryEntry(now - TimeSpan.FromMinutes(33), "Teammate A", "PUT", "/api/products/123", 204),
                new NeoHistoryEntry(now - TimeSpan.FromMinutes(90), "Teammate B", "GET", "/api/auth/profile", 401),
            });
        }

        private Task ShowHistory()
        {
            _mode = NeoWorkspaceMode.History;
            return Task.CompletedTask;
        }

        private Task ShowBuilder()
        {
            _mode = NeoWorkspaceMode.Builder;
            return Task.CompletedTask;
        }

        private Task UpdateMethodAsync(string? method)
        {
            if (!string.IsNullOrWhiteSpace(method))
            {
                _method = method.ToUpperInvariant();
            }

            return Task.CompletedTask;
        }

        private Task UpdateUrlAsync(string? url)
        {
            _url = string.IsNullOrWhiteSpace(url) ? string.Empty : url.Trim();
            return Task.CompletedTask;
        }

        private Task UpdateBodyAsync(string? body)
        {
            _body = body ?? string.Empty;
            return Task.CompletedTask;
        }

        private Task SendAsync()
        {
            var elapsedMs = Rng.Next(30, 151);
            var sizeKb = Math.Round(0.8 + (Rng.NextDouble() * 3.0), 2);
            var isSuccess = Rng.NextDouble() > 0.08;
            var statusCode = isSuccess ? 200 : 500;
            var statusLabel = isSuccess ? "200 OK" : "500 ERROR";

            var prettyPayload = JsonSerializer.Serialize(new
            {
                method = _method,
                url = _url,
                body = string.IsNullOrWhiteSpace(_body) ? null : _body,
                took_ms = elapsedMs,
                ok = isSuccess
            }, JsonOptions);

            var contentLength = Math.Max(128, (int)Math.Round(sizeKb * 1024, MidpointRounding.AwayFromZero));
            var rawBuilder = new StringBuilder();
            rawBuilder.AppendLine($"HTTP/1.1 {statusCode} {(isSuccess ? "OK" : "ERROR")}");
            rawBuilder.AppendLine("Content-Type: application/json");
            rawBuilder.AppendLine($"Content-Length: {contentLength}");
            rawBuilder.AppendLine();
            rawBuilder.Append(JsonSerializer.Serialize(new
            {
                message = isSuccess ? "Success" : "Failure",
                at = DateTime.UtcNow
            }, JsonOptions));

            _response = new NeoResponseSnapshot(statusLabel, isSuccess, statusCode, elapsedMs, sizeKb, prettyPayload, rawBuilder.ToString());
            _history.Insert(0, new NeoHistoryEntry(DateTime.Now, "You", _method, _url, statusCode));
            _mode = NeoWorkspaceMode.Builder;

            return Task.CompletedTask;
        }
    }
}
