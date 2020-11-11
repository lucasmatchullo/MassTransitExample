using System;

namespace Shared
{
    public interface EnderecoNotFound
    {
        string EnderecoId { get; }
        DateTime Timestamp { get; }
        short StatusCode { get; }
        string StatusText { get; }
    }
}