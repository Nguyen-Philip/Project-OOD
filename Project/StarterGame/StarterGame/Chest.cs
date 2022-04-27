using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Chest : ICloseable
    {
        private Room _roomA;
        private bool _open;
        private ILockable _lock;

        public bool IsOpen { get { return _open; } }

        public bool IsClosed { get { return !_open; } }

        public bool CanClose { get { return _lock == null ? true : _lock.CanClose; } }

        public bool IsLocked { get { return _lock == null ? false : _lock.IsLocked; } }

        public bool IsUnlocked { get { return _lock == null ? true : _lock.IsUnlocked; } }

        public Chest(Room roomA)
        {
            _roomA = roomA;
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

        public static Chest CreateChest(Room room1, string label1)
        {
            Chest chest = new Chest(room1);
            room1.SetChest(label1, chest);
            return chest;
        }

        public static Chest CreateLockedChest(Room room1, string label1)
        {
            Chest chest = new Chest(room1);
            room1.SetChest(label1, chest);
            return chest;
        }
    }
}
