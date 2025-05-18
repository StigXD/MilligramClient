using MilligramClient.Domain.Dtos;

namespace MilligramClient.Api.Token;

public interface ITokenProvider
{
    Task LoginAsync(LoginDto login);

    Task<TResult> ExecuteWithToken<TResult>(Func<string, Task<TResult>> action);
}