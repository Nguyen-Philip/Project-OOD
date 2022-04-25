using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public interface IRoomDelegate
    {
        Door GetExit(string exitName);
        string GetExits();
        string Description();
        Room ContainingRoom { set; get; }
        Dictionary<string, Door> ContainingRoomExits { set; get; }

    }

    public interface ILockable
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        bool CanClose { get; }
        void Lock();
        void Unlock();
    }

    public interface ICloseable : ILockable
    {
        bool IsOpen { get; }
        bool IsClosed { get;  }
        void Open();
        void Close();
    }

    
}
