#nullable enable
using System;

namespace SeaBattle.Implementations
{
    public class BattlefieldBuilderException : Exception
    {
        public BattlefieldBuilderException()
        {
        }

        public BattlefieldBuilderException(string message) : base(message)
        {
        }

        public BattlefieldBuilderException(string? paramName, string message)
        {
            ParamName = paramName;
        }

        public string? ParamName { get; }
    }
}