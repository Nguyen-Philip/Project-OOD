using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Door
    {
        private Room _roomA;
        private Room _roomB;
        private bool _open;

        public bool IsOpen { get { return _open; } }

        public bool IsClosed { get { return !_open; } }

        public Door(Room roomA, Room roomB)
        {
            _roomA = roomA;
            _roomB = roomB;
            _open = true;
        }

        public void Open()
        {
            _open = true;
        }

        public void Close()
        {
            _open = false;
        }

        public Room GetRoomOnTheOtherSide(Room ofThisRoom)
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

        public static Door CreateDoor(Room room1, Room room2, string label1, string label2)
        {
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            room2.SetExit(label2, door);
            return door;
        }

        public static Door CreatePortal(Room room1, Room room2, string label1)
        {
            Door door = new Door(room1, room2);
            room1.SetExit(label1, door);
            return door;
        }
    }
}
