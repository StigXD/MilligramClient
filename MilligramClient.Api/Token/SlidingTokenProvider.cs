using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using MilligramClient.Api.Clients.Account;
using MilligramClient.Domain.Dtos;
using Nito.AsyncEx;

namespace MilligramClient.Api.Token;

public class SlidingTokenProvider : ITokenProvider
{
    private readonly IAccountClient _accountClient;
    private readonly int _refreshTokenBeforeExpirationInPercent;

    private readonly AsyncLock _tokenAsyncLock = new();
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    private string? _token;

    public SlidingTokenProvider(
        IAccountClient accountClient,
        int refreshTokenBeforeExpirationInPercent)
    {
        _accountClient = accountClient;
        _refreshTokenBeforeExpirationInPercent = refreshTokenBeforeExpirationInPercent;
    }

    public async Task LoginAsync(LoginDto login)
    {
        using var _ = await _tokenAsyncLock.LockAsync();

        var token = await _accountClient.LoginAsync(login);
        _token = token.Token ?? throw new ArgumentNullException(nameof(token.Token), "Token is empty");
    }

    public async Task<TResult> ExecuteWithToken<TResult>(Func<string, Task<TResult>> action)
    {
        return await action(await GetTokenAsync());
    }

    private async Task<string> GetTokenAsync()
    {
        var token = _token;
        if (!IsNeedRefreshToken(token))
            return token;

        using var _ = await _tokenAsyncLock.LockAsync();

        if (!IsNeedRefreshToken(_token))
            return _token;

        var refreshResult = await _accountClient.RefreshTokenAsync(_token);
        _token = refreshResult.Token ?? throw new ArgumentNullException(nameof(refreshResult.Token), "Token is empty");

        return _token;
    }

    private bool IsNeedRefreshToken([NotNull] string? token)
    {
        if (token == null)
            throw new InvalidOperationException("You must login before using the token");

        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

        var refreshTokenBeforeTicks =
            (jwtSecurityToken.ValidTo - jwtSecurityToken.ValidFrom).Ticks
            / 100 * _refreshTokenBeforeExpirationInPercent;

        var refreshTokenAfter = jwtSecurityToken.ValidTo.AddTicks(-refreshTokenBeforeTicks);

        var utcNow = DateTime.UtcNow;
        return refreshTokenAfter < utcNow && utcNow < jwtSecurityToken.ValidTo;
    }
}