namespace SharedKernel.Contracts.Request
{
    public record VerifyTokenRequest(
        string Token,
        int Type
    );
}