namespace SharedKernel.Contracts.Request
{
    public record LoginRequest(
        string Email,
        string Password,
        string Device,
        string Browser,
        string IpAddress
    );
}