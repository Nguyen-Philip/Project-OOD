using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base()
        {
            this.Name = "quit";
        }

        override
        public bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.ErrorMessage("\nQuit does not contain " + this.SecondWord);
                player.LocationMessage("\n" + player.CurrentRoom.Description());
                answer = false;
            }
            return answer;
        }
    }
}
