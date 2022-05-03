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
        private Room _treasureRoom;
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

        public Room TreasureRoom
        {
            get
            {
                return _treasureRoom;
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
            else if (player.CurrentRoom == _treasureRoom)
            {
                player.NotificationMessage("\n*** You have reached the treasure room, and won the game! ***");
                player.NotificationMessage("\nFinal Stats ");
                player.Stats();
                System.Environment.Exit(0);
            }
            player.NotificationMessage("\n*** The player is " + player.CurrentRoom.Tag + " ***");
        }

        private Room CreateWorld()
        {
            Room room = new Room();
            Room itemRoom = new Room("Purpose of creating items");
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
            Room treasureRoom = new Room("in the treasure room");

            List<Room> rooms = new List<Room>();
            rooms.Add(room);
            rooms.Add(itemRoom);
            rooms.Add(town);
            rooms.Add(entrance);
            rooms.Add(room1_0);
            rooms.Add(room1_1);
            rooms.Add(room1_2);
            rooms.Add(room1_3);
            rooms.Add(room1_4);
            rooms.Add(room1_5);
            rooms.Add(room1_6);
            rooms.Add(room1_7);
            rooms.Add(room1_8);
            rooms.Add(room2_0);
            rooms.Add(room2_1);
            rooms.Add(room2_2);
            rooms.Add(room2_3);
            rooms.Add(room2_4);
            rooms.Add(room2_5);
            rooms.Add(room2_6);
            rooms.Add(room2_7);
            rooms.Add(room2_8);
            rooms.Add(room3_0);
            rooms.Add(room3_1);
            rooms.Add(room3_2);
            rooms.Add(room3_3);
            rooms.Add(room3_4);
            rooms.Add(room3_5);
            rooms.Add(room3_6);
            rooms.Add(room3_7);
            rooms.Add(room3_8);
            rooms.Add(room3_9);
            rooms.Add(treasureRoom);
            //town
            //door = Door.CreateLockedDoor(town, entrance, "north", "south", "dungeonkey");
            door = Door.CreateDoor(town, entrance, "north", "south");
            npc = NPC.CreateNPC(town, "merchant", true, "Hello, would you like to look at my wares");
            npc = NPC.CreateNPC(town, "guard", true, "Hello, there! Spend some time in town!");
            gem = Gem.CreateGem(town, "ruby", 5, 1, 5);
            Gem gem2 = Gem.CreateGem(town, "ruby", 5, 1, 5);
            Gem gem3 = Gem.CreateGem(town, "ruby", 5, 1, 5);
            Gem gem4 = Gem.CreateGem(town, "onyx", 5, 1, 5);
            Gem gem5 = Gem.CreateGem(town, "onyx", 5, 1, 5);
            chest = Chest.CreateLockedChest(town, "chest", "chestkey");
            potion = Potion.CreatePotion(itemRoom, "shealingpotion", 10, 1, 10, "HP", 2);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "mhealingpotion", 25, 1, 35, "HP", 3);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "lhealingpotion", 50, 1, 80, "HP", 4);
            chest.Add(potion);

            //entrance
            door = Door.CreateLockedDoor(entrance, room1_0, "north", "south", "northkey");
            door = Door.CreateLockedDoor(entrance, room2_0, "west", "east", "westkey");
            door = Door.CreateLockedDoor(entrance, room3_0, "east", "west", "eastkey");
            enemy = Enemy.CreateEnemy(entrance, "Skeleton", 10, 2, 2, 10, 10);
            key = Key.CreateKey(itemRoom, "northkey", 0, "Key that opens the room north of the entrance");
            enemy.AddKeyItems(key);
            armor = Armor.CreateArmor(itemRoom, "bronzearmor", 10, 5, 5, 1);
            weapon = Weapon.CreateWeapon(itemRoom, "bronzesword", 10, 5, 5, 1);
            enemy.Add(armor);
            enemy.Add(weapon);

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
            enemy = Enemy.CreateEnemy(room1_5, "Ghoul", 20, 8, 2, 15, 20);
            key = Key.CreateKey(itemRoom, "chestkey1", 0, "Key that open a chest");
            enemy.AddKeyItems(key);
            chest = Chest.CreateLockedChest(room1_6, "chest", "chestkey1");
            gem = Gem.CreateGem(itemRoom, "ruby", 5, 1, 1);
            chest.Add(gem);
            potion = Potion.CreatePotion(itemRoom, "shealingpotion", 10, 1, 10, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "mhealingpotion", 25, 1, 35, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "lhealingpotion", 50, 1, 80, "HP", 1);
            chest.Add(potion);

            //room1_7 - room1_8
            door = Door.CreateDoor(room1_7, room1_8, "north", "south");
            enemy = Enemy.CreateEnemy(room1_8, "Wizard", 250, 10, 1, 50, 50);
            key = Key.CreateKey(itemRoom, "westkey", 0, "Key that opens the room west of the entrance");
            enemy.AddKeyItems(key);
            armor = Armor.CreateArmor(itemRoom, "ironarmor", 20, 8, 7, 1);
            weapon = Weapon.CreateWeapon(itemRoom, "ironsword", 20, 8, 10, 1);
            enemy.Add(armor);
            enemy.Add(weapon);

            //room2_0
            door = Door.CreateDoor(room2_0, room2_1, "north", "south");








            //room2_1 - room2_2
            door = Door.CreateDoor(room2_1, room2_2, "west", "east");
            door = Door.CreateDoor(room2_1, room2_3, "north", "south");
            enemy = Enemy.CreateEnemy(room2_1, "Wolf", 40, 10, 2, 30, 30);
            key = Key.CreateKey(itemRoom, "chestkey2", 0, "Key that open a chest");
            enemy.AddKeyItems(key);
            chest = Chest.CreateLockedChest(room2_2, "chest", "chestkey2");
            gem = Gem.CreateGem(itemRoom, "ruby", 5, 1, 1);
            chest.Add(gem);
            potion = Potion.CreatePotion(itemRoom, "shealingpotion", 10, 1, 10, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "mhealingpotion", 25, 1, 35, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "lhealingpotion", 50, 1, 80, "HP", 1);
            chest.Add(potion);

            //room2_3
            door = Door.CreateDoor(room2_3, room2_4, "north", "south");







            //room2_4
            door = Door.CreateDoor(room2_4, room2_5, "west", "east");







            //room2_5 - room2_6
            door = Door.CreateDoor(room2_5, room2_6, "south", "north");
            door = Door.CreateDoor(room2_5, room2_7, "north", "south");
            enemy = Enemy.CreateEnemy(room2_5, "Bear", 80, 13, 2, 80, 80);
            key = Key.CreateKey(itemRoom, "chestkey3", 0, "Key that open a chest");
            enemy.AddKeyItems(key);
            chest = Chest.CreateLockedChest(room2_6, "chest", "chestkey3");
            gem = Gem.CreateGem(itemRoom, "ruby", 5, 1, 1);
            chest.Add(gem);
            potion = Potion.CreatePotion(itemRoom, "shealingpotion", 10, 1, 10, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "mhealingpotion", 25, 1, 35, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "lhealingpotion", 50, 1, 80, "HP", 1);
            chest.Add(potion);

            //room2_7 - room2_8
            door = Door.CreateDoor(room2_7, room2_8, "east", "west");
            enemy = Enemy.CreateEnemy(room2_8, "Werewolf", 500, 15, 1, 150, 150);
            key = Key.CreateKey(room2_8, "eastkey", 0, "Key that opens the room east of the entrance");
            enemy.AddKeyItems(key);
            armor = Armor.CreateArmor(itemRoom, "steelarmor", 40, 11, 9, 1);
            weapon = Weapon.CreateWeapon(itemRoom, "steelsword", 40, 11, 15, 1);
            enemy.Add(armor);
            enemy.Add(weapon);

            //room3_0
            door = Door.CreateDoor(room3_0, room3_1, "north", "south");






            //room3_1 - room3_2
            door = Door.CreateDoor(room3_1, room3_2, "north", "south");
            door = Door.CreateDoor(room3_1, room3_3, "east", "west");
            enemy = Enemy.CreateEnemy(room3_1, "Knight", 160, 17, 2, 160, 160);
            key = Key.CreateKey(itemRoom, "chestkey4", 0, "Key that open a chest");
            enemy.AddKeyItems(key);
            chest = Chest.CreateLockedChest(room3_2, "chest", "chestkey4");
            gem = Gem.CreateGem(itemRoom, "ruby", 5, 1, 1);
            chest.Add(gem);
            potion = Potion.CreatePotion(itemRoom, "shealingpotion", 10, 1, 10, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "mhealingpotion", 25, 1, 35, "HP", 1);
            chest.Add(potion);
            potion = Potion.CreatePotion(itemRoom, "lhealingpotion", 50, 1, 80, "HP", 1);
            chest.Add(potion);

            //room3_3
            door = Door.CreateDoor(room3_3, room3_4, "north", "south");






            //room3_4
            door = Door.CreateDoor(room3_4, room3_5, "east", "west");







            //room3_5 - room3_9 - treasureRoom
            door = Door.CreateDoor(room3_5, room3_6, "south", "north");
            door = Door.CreateLockedDoor(room3_5, room3_9, "north", "south", "bosskey");
            door = Door.CreateLockedDoor(room3_9, treasureRoom, "north", "south", "treasurekey");
            door = Door.CreateDoor(room2_7, room2_8, "east", "west");
            enemy = Enemy.CreateEnemy(room3_9, "Dragon", 1000, 25, 0, 450, 450);
            key = Key.CreateKey(itemRoom, "treasurekey", 0, "Key that opens the room north of room3_9");
            enemy.AddKeyItems(key);

            //room3_6
            door = Door.CreateDoor(room3_6, room3_7, "south", "north");







            //room3_7 - room3_8
            door = Door.CreateDoor(room3_7, room3_8, "west", "east");
            enemy = Enemy.CreateEnemy(room3_8, "Wyvern", 750, 21, 1, 350, 350);
            key = Key.CreateKey(room3_8, "bosskey", 0, "Key that opens the room north of room3_5");
            enemy.AddKeyItems(key);
            armor = Armor.CreateArmor(itemRoom, "scalearmor", 60, 14, 11, 1);
            weapon = Weapon.CreateWeapon(itemRoom, "dragonbonesword", 60, 14, 20, 1);
            enemy.Add(armor);
            enemy.Add(weapon);




            _exit = room1_0;
            _trigger = room1_0;
            _portalExit = room1_4;
            _entrance = town;
            _treasureRoom = treasureRoom;


            //set the Delegate Rooms
            room1_4.Delegate = new TrapRoom("help");
            room1_6.Delegate = new EchoRoom();
            town.Delegate = new TownRoom("ready", town);

            return town;
        }
    }
}