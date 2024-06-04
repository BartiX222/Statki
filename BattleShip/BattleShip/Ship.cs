using System;

namespace BattleShip
{
    public class Ship
    {
        public int Size { get; private set; }

        public Ship(int size)
        {
            Size = size;
        }
    }
}
