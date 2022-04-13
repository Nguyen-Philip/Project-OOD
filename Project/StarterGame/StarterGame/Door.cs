using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Door
    {
        private Room _roomA;
        private Room _roomB;

        public Door(Room roomA, Room roomB)
        {
            _roomA = roomA;
            _roomB = roomB;
        }

        public Room RoomOnTheOtherSide(Room ofThisRoom)
        {
            if(ofThisRoom == _roomA)
            {
                return _roomB;
            }
            else
            {
                return _roomA;
            }
        }
    }
}
