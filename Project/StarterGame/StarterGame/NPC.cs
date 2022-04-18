using System;
namespace StarterGame
{
   
    public class NPC : IEntity
    {
        
        private string _Name;
        private string _Location;
        private bool _CanTalk;
        private string _Dialog;
        
        public string Name { set { _Name = value; } get { return _Name; } }
        public string Location { set { _Location = value; } get { return _Location; } }
        public bool CanTalk { set { _CanTalk = value; } get { return _CanTalk; } }
        public string Dialog { set { _Dialog = value; } get { return _Dialog; } }
        public NPC(string name) : this(name, "NO LOCATION") { }
        public NPC(string name, string location) : this(name, location, false, null) { }
        public NPC(string name, string location, bool canTalk, string dialog)
        {
            _Name = name;
            _Location = location;
            _CanTalk = canTalk;
            _Dialog = dialog;
        }
       
    }
}
