using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public abstract class Command
    {
        private string _name;

        private string _secondWord;

        public string Name { get { return _name; } set { _name = value; } }
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }

        public abstract bool Execute(Player player);

        public Command()
        {
            this.Name = "";
            this.SecondWord = null;
        }

        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }
    }
}
