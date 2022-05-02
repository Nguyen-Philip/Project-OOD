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
        private Chest chest;
        private Gem gem;
        private Armor armor;
        private Enemy enemy;
        private NPC npc;
        private Potion potion;
        private Weapon weapon;
        private Key key;
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
            if(player.CurrentRoom == _exit)
            {
                door = player.CurrentRoom.GetExit("portal");
                if(door != null)
                {
                    player.CurrentRoom.SetExit("portal", null);
                    _trigger = null;
                    player.NotificationMessage("\n*** The portal collapses behind you. ***");
                }
            }
            //player.OutputMessage("\n*** The player is " + player.CurrentRoom.Tag + ", getting ready to leave ***");
        }

        public void PlayerDidEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.CurrentRoom == _trigger)
            {
                door = Door.CreatePortal(_exit, _portalExit, "portal");
                player.NotificationMessage("\n*** You hear a loud noise. A portal has been created nearby. ***");
            }
            player.NotificationMessage("\n*** The player is " + player.CurrentRoom.Tag + " ***");
        }

        private Room CreateWorld()
        {
            Room room = new Room();
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

            //town
            door = Door.CreateLockedDoor(town, entrance, "north", "south", "dungeonkey");

            npc = NPC.CreateNPC(town, "Merchant", true, "Hello, would you like to look at my wares");
            npc = NPC.CreateNPC(town, "Guard", true, "Hello, there! Spend some time in town!");

            chest = Chest.CreateLockedChest(town, "chest", "townchestkey");
            armor = Armor.CreateArmor(town, "bronzechestplate", 10, 5, 5);
            potion = Potion.CreatePotion(town, "smallhealingpotion", 10, 2, 20, "HP");
            weapon = Weapon.CreateWeapon(town, "bronzedagger", 10, 5, 2);
            chest.Add(armor);
            chest.Add(weapon);
            chest.Add(potion);

            //entrance
            door = Door.CreateLockedDoor(entrance, room1_0, "north", "south", "northkey");
            door = Door.CreateLockedDoor(entrance, room2_0, "west", "east", "westkey");
            door = Door.CreateLockedDoor(entrance, room3_0, "east", "west", "eastkey");

            enemy = Enemy.CreateEnemy(entrance, "Skeleton", 10, 2, 2, 10, 10);
            key = Key.CreateKey(entrance, "northkey", 0, "Key that opens the room north of the entrance");
            enemy.AddKeyItems(key);

            //room1_0
            door = Door.CreateDoor(room1_0, room1_1, "north", "south");


            //room1_1
            door = Door.CreateDoor(room1_1, room1_2, "north", "south");

            
            //room1_2
            door = Door.CreateDoor(room1_2, room1_3, "east", "west");


            //room1_3 - room1_4
            door = Door.CreateDoor(room1_3, room1_4, "east", "west");
            door = Door.CreateDoor(room1_3, room1_5, "north", "south");


            //room1_5 - room1_6
            door = Door.CreateDoor(room1_5, room1_6, "east", "west");
            door = Door.CreateDoor(room1_5, room1_7, "west", "east");

            //room1_7 - room1_8
            door = Door.CreateDoor(room1_7, room1_8, "north", "south");

            enemy = Enemy.CreateEnemy(room1_8, "Wizard", 50, 5, 1, 100, 100);
            key = Key.CreateKey(room1_8, "westkey", 0, "Key that opens the room west of the entrance");
            armor = Armor.CreateArmor(room1_8, "ironchestplate", 10, 5, 7);
            potion = Potion.CreatePotion(room1_8, "mediumhealingpotion", 10, 2, 40, "HP");
            weapon = Weapon.CreateWeapon(room1_8, "irondagger", 10, 5, 4);
            enemy.AddKeyItems(key);
            enemy.Add(armor);
            enemy.Add(potion);
            enemy.Add(weapon);

            //room2_0
            door = Door.CreateDoor(room2_0, room2_1, "north", "south");

            //room2_1 - room2_2
            door = Door.CreateDoor(room2_1, room2_2, "west", "east");
            door = Door.CreateDoor(room2_1, room2_3, "north", "south");

            //room2_3
            door = Door.CreateDoor(room2_3, room2_4, "north", "south");

            //room2_4
            door = Door.CreateDoor(room2_4, room2_5, "west", "east");

            //room2_5 - room2_6
            door = Door.CreateDoor(room2_5, room2_6, "south", "north");
            door = Door.CreateDoor(room2_5, room2_7, "north", "south");

            //room2_7 - room2_8
            door = Door.CreateDoor(room2_7, room2_8, "east", "west");

            enemy = Enemy.CreateEnemy(room2_8, "Werewolf", 100, 10, 1, 150, 150);
            key = Key.CreateKey(room2_8, "eastkey", 0, "Key that opens the room east of the entrance");
            enemy.AddKeyItems(key);

            //room3_0
            door = Door.CreateDoor(room3_0, room3_1, "north", "south");

            //room3_1 - room3_2
            door = Door.CreateDoor(room3_1, room3_2, "north", "south");
            door = Door.CreateDoor(room3_1, room3_3, "east", "west");

            //room3_3
            door = Door.CreateDoor(room3_3, room3_4, "north", "south");

            //room3_4
            door = Door.CreateDoor(room3_4, room3_5, "east", "west");

            //room3_5 - room3_9
            door = Door.CreateDoor(room3_5, room3_6, "south", "north");
            door = Door.CreateLockedDoor(room3_5, room3_9, "north", "south", "bosskey");
            door = Door.CreateDoor(room2_7, room2_8, "east", "west");

            enemy = Enemy.CreateEnemy(room3_9, "Dragon", 400, 20, 0, 450, 450);

            //objective


            //room3_6
            door = Door.CreateDoor(room3_6, room3_7, "south", "north");

            //room3_7 - room3_8
            door = Door.CreateDoor(room3_7, room3_8, "west", "east");

            enemy = Enemy.CreateEnemy(room3_8, "Wyvern", 300, 15, 0, 350, 350);
            key = Key.CreateKey(room3_8, "bosskey", 0, "Key that opens the room north of room3_5");
            enemy.AddKeyItems(key);

            _exit = entrance;
            _trigger = entrance;
            _portalExit = room1_4;
            _entrance = town;

            //set the Delegate Rooms
            room1_4.Delegate = new TrapRoom("help");
            room1_6.Delegate = new EchoRoom();
            town.Delegate = new TownRoom("ready", town);

            return town;
        }
    }
}