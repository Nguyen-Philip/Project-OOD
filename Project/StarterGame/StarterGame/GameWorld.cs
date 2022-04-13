using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class GameWorld
    {

        private static GameWorld _instance;
        private Room _entrance;
        private Room _exit;
        private Room _trigger;
        private Room _portalExit;
        private Door door;

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
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            NotificationCenter.Instance.AddObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
        }

        public Room Entrance
        {
            get
            {
                return _entrance;
            }
        }

        public Room Trigger
        {
            get
            {
                return _trigger;
            }
        }

        public Room PortalExit
        {
            get
            {
                return _portalExit;
            }
        }
        public Room Exit
        {
            get
            {
                return _exit;
            }
        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            /*if(player.CurrentRoom == _exit)
            {
                Room room = player.CurrentRoom.GetExit("teleporter");
                if(room != null)
                {
                    player.CurrentRoom.SetExit("teleporter", null);
                }
            }*/
            player.OutputMessage("\n*** The player is " + player.CurrentRoom.Tag + ", getting ready to leave ***");
        }

        public void PlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.CurrentRoom == _trigger)
            {
                door = Door.CreateTeleporter(_exit, _portalExit, "teleporter");
                player.OutputMessage("\n*** You hear a loud noise. A teleporter has been created nearby. ***");
            }
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

            door = Door.CreateDoor(town, entrance, "north", "south");

            door = Door.CreateDoor(entrance, room1_0, "north", "south");
            door = Door.CreateDoor(room1_0, room1_1, "north", "south");
            door = Door.CreateDoor(room1_1, room1_2, "north", "south");
            door = Door.CreateDoor(room1_2, room1_3, "east", "west");
            door = Door.CreateDoor(room1_3, room1_4, "east", "west");
            door = Door.CreateDoor(room1_3, room1_5, "north", "south");
            door = Door.CreateDoor(room1_5, room1_6, "east", "west");
            door = Door.CreateDoor(room1_5, room1_7, "west", "east");
            door = Door.CreateDoor(room1_7, room1_8, "north", "south");

            door = Door.CreateDoor(entrance, room2_0, "west", "east");
            door = Door.CreateDoor(room2_0, room2_1, "north", "south");
            door = Door.CreateDoor(room2_1, room2_2, "west", "east");
            door = Door.CreateDoor(room2_1, room2_3, "north", "south");
            door = Door.CreateDoor(room2_3, room2_4, "north", "south");
            door = Door.CreateDoor(room2_4, room2_5, "west", "east");
            door = Door.CreateDoor(room2_5, room2_6, "south", "north");
            door = Door.CreateDoor(room2_5, room2_7, "north", "south");
            door = Door.CreateDoor(room2_7, room2_8, "east", "west");

            door = Door.CreateDoor(entrance, room3_0, "east", "west");

            door = Door.CreateDoor(room3_0, room3_1, "north", "south");
            door = Door.CreateDoor(room3_1, room3_2, "north", "south");
            door = Door.CreateDoor(room3_1, room3_3, "east", "west");
            door = Door.CreateDoor(room3_3, room3_4, "north", "south");
            door = Door.CreateDoor(room3_4, room3_5, "east", "west");
            door = Door.CreateDoor(room3_5, room3_6, "south", "north");
            door = Door.CreateDoor(room3_5, room3_9, "north", "south");
            door = Door.CreateDoor(room3_6, room3_7, "south", "north");

            door = Door.CreateDoor(room3_7, room3_8, "west", "east");

            _exit = entrance;

            _trigger = entrance;

            _portalExit = room1_4;

            _entrance = town;

            //set the Delegate Rooms
            room1_4.Delegate = new TrapRoom("help");
            room1_6.Delegate = new EchoRoom();

            return town;
        }
    }
}