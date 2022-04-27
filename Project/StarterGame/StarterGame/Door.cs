using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Door : ICloseable
    {
        private Room _roomA;
        private Room _roomB;
        private bool _open;
        private ILockable _lock;

        public bool IsOpen { get { return _open; } }

        public bool IsClosed { get { return !_open; } }

        public bool CanClose { get { return _lock == null ? true : _lock.CanClose; } }

        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } }

        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }

        public Door(Room roomA, Room roomB)
        {
            _roomA = roomA;
            _roomB = roomB;
            _open = true;
            _lock = null;
        }

        public void Open()
        {
            if (IsUnlocked)
            {
                _open = true;
            }
        }

        public void Close()
        {
            if(IsOpen && CanClose)
            {
                _open = false;
            }
        }

        public void Lock()
        {
            if(_lock != null)
            {
                _lock.Lock();
            }
        }

        public void Unlock()
        {
            if(_lock != null)
            {
                _lock.Unlock();
            }
        }

        public ILockable InstallLock(ILockable theLock)
        {
            ILockable oldLock = _lock;
            _lock = theLock;
            return oldLock;
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
