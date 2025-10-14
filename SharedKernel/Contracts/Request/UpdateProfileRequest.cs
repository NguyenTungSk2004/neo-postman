namespace SharedKernel.Contracts.Request
{
    public record UpdateProfileRequest(
        string Name,
        string? UrlAvatar
    );
}