namespace SharedKernel.Contracts.Request
{
    public readonly record struct UpdateProfileRequest(
        string Name,
        string? UrlAvatar
    );
}