#nullable enable
using System;

namespace SeaBattle
{
    public class BattlefieldBuilderException : Exception
    {
        public BattlefieldBuilderException() : base() { }

        public BattlefieldBuilderException(string message) : base(message) { }

        public BattlefieldBuilderException(string? paramName, string message) => ParamName = paramName;

        public string? ParamName { get; }
    }
}