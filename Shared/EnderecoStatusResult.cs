using System;

namespace Shared
{
    public interface EnderecoStatusResult
    {
        string EnderecoId { get; }
        DateTime Timestamp { get; }
        short StatusCode { get; }
        string StatusText { get; }
    }
}