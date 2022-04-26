using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class Regularlock : ILockable
    {
        private bool _locked;

        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }

        public bool CanClose { get { return true; } }

        public Regularlock()
        {
            _locked = true;
        }

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
        }
    }
}
