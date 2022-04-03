using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class GameWorld
    {

        private static GameWorld _instance;
        private Room _town;

        public static GameWorld Instance()
        {
            if(_instance == null)
            {
                _instance = new GameWorld();
            }
            return _instance;
        }

        private GameWorld()
        {
            _town = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
        }

        public Room Town
        {
            get
            {
                return _town;
            }
        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            player.OutputMessage("\n*** The player is " + player.CurrentRoom.Tag + ", getting ready to leave ***");
        }

        public void PlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            player.OutputMessage("\n*** The player is " + player.CurrentRoom.Tag + " ***");
        }

        private Room CreateWorld()
        {
            Room town = new Room("in the town");
            Room entrance = new Room("in the entrance of the dungeon");

            Room room1_0 = new Room("in room 1_0");
            Room room1_1 = new Room("in room 1_1");
            Room room1_2 = new Room("in room 1_2");
            Room room1_3 = new Room("in room 1_3");
            Room room1_4 = new Room("in room 1_4");
            Room room1_5 = new Room("in room 1_5");
            Room room1_6 = new Room("in room 1_6");
            Room room1_7 = new Room("in room 1_7");
            Room room1_8 = new Room("in room 1_8");

            Room room2_0 = new Room("in room 2_0");
            Room room2_1 = new Room("in room 2_1");
            Room room2_2 = new Room("in room 2_2");
            Room room2_3 = new Room("in room 2_3");
            Room room2_4 = new Room("in room 2_4");
            Room room2_5 = new Room("in room 2_5");
            Room room2_6 = new Room("in room 2_6");
            Room room2_7 = new Room("in room 2_7");
            Room room2_8 = new Room("in room 2_8");

            Room room3_0 = new Room("in room 3_0");
            Room room3_1 = new Room("in room 3_1");
            Room room3_2 = new Room("in room 3_2");
            Room room3_3 = new Room("in room 3_3");
            Room room3_4 = new Room("in room 3_4");
            Room room3_5 = new Room("in room 3_5");
            Room room3_6 = new Room("in room 3_6");
            Room room3_7 = new Room("in room 3_7");
            Room room3_8 = new Room("in room 3_8");
            Room room3_9 = new Room("in room 3_9");


            town.SetExit("north", entrance);

            entrance.SetExit("north", room1_0);
            entrance.SetExit("west", room2_0);
            entrance.SetExit("east", room3_0);
            entrance.SetExit("south", town);

            room1_0.SetExit("north", room1_1);
            room1_0.SetExit("south", entrance);

            room1_1.SetExit("north", room1_2);
            room1_1.SetExit("south", room1_0);

            room1_2.SetExit("east", room1_3);
            room1_2.SetExit("south", room1_1);

            room1_3.SetExit("north", room1_5);
            room1_3.SetExit("west", room1_2);
            room1_3.SetExit("east", room1_4);

            room1_4.SetExit("west", room1_3);

            room1_5.SetExit("west", room1_7);
            room1_5.SetExit("east", room1_6);
            room1_5.SetExit("south", room1_3);

            room1_6.SetExit("west", room1_5);

            room1_7.SetExit("north", room1_8);
            room1_7.SetExit("east", room1_5);

            room1_8.SetExit("south", room1_7);

            room2_0.SetExit("north", room2_1);
            room2_0.SetExit("east", entrance);

            room2_1.SetExit("north", room2_3);
            room2_1.SetExit("west", room2_2);
            room2_1.SetExit("south", room2_0);

            room2_2.SetExit("east", room2_1);

            room2_3.SetExit("north", room2_4);
            room2_3.SetExit("south", room2_1);

            room2_4.SetExit("west", room2_5);
            room2_4.SetExit("south", room2_3);

            room2_5.SetExit("north", room2_7);
            room2_5.SetExit("east", room2_4);
            room2_5.SetExit("south", room2_6);

            room2_6.SetExit("north", room2_5);

            room2_7.SetExit("east", room2_8);
            room2_7.SetExit("south", room2_5);

            room2_8.SetExit("west", room2_7);

            room3_0.SetExit("north", room3_1);
            room3_0.SetExit("west", entrance);

            room3_1.SetExit("north", room3_2);
            room3_1.SetExit("east", room3_3);
            room3_1.SetExit("south", room3_0);

            //room3_2.SetExit("teleporter", );
            room3_2.SetExit("south", room3_1);

            room3_3.SetExit("north", room3_4);
            room3_3.SetExit("west", room3_1);

            room3_4.SetExit("east", room3_5);
            room3_4.SetExit("south", room3_3);

            room3_5.SetExit("north", room3_9);
            room3_5.SetExit("west", room3_4);
            room3_5.SetExit("south", room3_6);

            room3_6.SetExit("north", room3_5);
            room3_6.SetExit("south", room3_7);

            room3_7.SetExit("north", room3_6);
            room3_7.SetExit("west", room3_8);

            room3_8.SetExit("east", room3_7);

            room3_9.SetExit("south", room3_5);

            return town;
        }
    }
}