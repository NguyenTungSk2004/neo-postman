using System;

namespace BlazorWebApp.Components.Features.NeoPostman.Main
{
    public enum NeoWorkspaceMode
    {
        Builder,
        History
    }

    public record NeoHistoryEntry(DateTime Timestamp, string User, string Method, string Url, int StatusCode);

    public record NeoResponseSnapshot(string StatusLabel, bool IsSuccess, int StatusCode, int ElapsedMilliseconds, double SizeKb, string PrettyContent, string RawContent);
}