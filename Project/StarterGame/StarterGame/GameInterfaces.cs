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

    public interface IEntity
    {
        string Name { set; get; }
        Room Location { set; get; }

    }

    public interface Item : IEntity
    {
        int Value { set; get; }
        int Weight { set; get; }
        bool CanBeHeld { get; }
        bool IsUsable { get; }
    }

    public interface KeyItem : IEntity
    {
        int Weight { set; get; }
        string Description { set; get; }
        bool CanBeHeld { get; }
        bool IsUsable { get; }
        bool CanBeDropped { get; }
    }
}
