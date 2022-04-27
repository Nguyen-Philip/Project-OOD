using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class SpeaktoCommand : Command
    {
        public SpeaktoCommand() : base()
        {
            this.Name = "speakto";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Speakto(this.SecondWord);
            }
            else
            {
                player.ErrorMessage("\nSpeak to who?");
                player.LocationMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }

    }
}
