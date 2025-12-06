namespace SharedKernel.Contracts.Request
{
    public readonly record struct LoginRequest(
        string Email,
        string Password,
        string Device,
        string Browser,
        string IpAddress
    );
}