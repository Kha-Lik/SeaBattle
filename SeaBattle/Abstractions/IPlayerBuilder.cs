﻿namespace SeaBattle.Abstractions
{
    public interface IPlayerBuilder
    {
        IPlayer ConstructPlayer(string name);
    }
}