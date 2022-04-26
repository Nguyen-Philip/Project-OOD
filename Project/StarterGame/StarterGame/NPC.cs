using System;
namespace StarterGame
{
   
    public class NPC : IEntity
    {
        
        private string _Name;
        private Room _Location;
        private bool _CanTalk;
        private string _Dialog;
        
        public string Name { set { _Name = value; } get { return _Name; } }
        public Room Location { set { _Location = value; } get { return _Location; } }
        public bool CanTalk { set { _CanTalk = value; } get { return _CanTalk; } }
        public string Dialog { set { _Dialog = value; } get { return _Dialog; } }

        public NPC(Room location, string name)
        {
            _Location = location;
            _Name = name;
        }

        public NPC(Room location, string name, bool canTalk, string dialog)
        {
            _Location = location;
            _Name = name;
            _CanTalk = canTalk;
            _Dialog = dialog;
        }

        public static NPC CreateNPC(Room location, string name)
        {
            NPC npc = new NPC(location, name);
            location.SetEntity(name, npc);
            return npc;
        }

        public static NPC CreateNPC(Room location, string name, bool canTalk, string dialog)
        {
            NPC npc = new NPC(location, name, canTalk, dialog);
            location.SetEntity(name, npc);
            return npc;
        }
    }
}
