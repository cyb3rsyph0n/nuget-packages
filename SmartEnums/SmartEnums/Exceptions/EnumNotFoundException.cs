﻿using System.Diagnostics.CodeAnalysis;

namespace SmartEnums.Exceptions;

/// <summary>
///     Enum not found exception
/// </summary>
[ExcludeFromCodeCoverage]
public class EnumNotFoundException : Exception
{
    /// <inheritdoc />
    public EnumNotFoundException(string message)
        : base(message)
    {
    }
}