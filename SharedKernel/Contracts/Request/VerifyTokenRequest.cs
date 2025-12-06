namespace SharedKernel.Contracts.Request
{
    public readonly record struct VerifyTokenRequest(
        string Token,
        int Type
    );
}