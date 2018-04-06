using System;
using GameFramework;
using Hra.characters;
using Hra.commands;
using Hra.items;
using Hra.rooms;

namespace Hra
{
    internal class MaharanthCaveGame : Game
    {
        private readonly DatabaseModel _database;

        public MaharanthCaveGame()
        {
            _parser.AddCommand(new HelpCommand(), GameState.New | GameState.Running | GameState.End);
            _database = new DatabaseModel();
        }

        protected override void InicializeNewGame()
        {
            var startRoom = new Room("Hlavná jaskyna");
            var miestnostDraka = new DraciaJaskina();
            var rozcestie = new Rozcestie("Rozcestie");
            var skladisko = new Room("Skladisko");
            var veza = new VyhliadkovaVeza("Trblietavá veža");
            var chram = new Room("Chánm slnka");
            Rooms.Add(startRoom);
            Rooms.Add(miestnostDraka);
            Rooms.Add(skladisko);
            Rooms.Add(veza);
            Rooms.Add(chram);
            //pospajanie miestnosti
            startRoom.ConnectTo("sever", miestnostDraka);
            miestnostDraka.ConnectTo("von", startRoom);

            startRoom.ConnectTo("juh", chram);
            chram.ConnectTo("von", startRoom);

            startRoom.ConnectTo("vychod", rozcestie);
            rozcestie.ConnectTo("zapad", startRoom);

            rozcestie.ConnectTo("sever", skladisko);
            skladisko.ConnectTo("juh", rozcestie);

            rozcestie.ConnectTo("juh", veza);
            veza.ConnectTo("von", rozcestie);

            //pridanie predmetov
            skladisko.AddItem(new Dynamit());
            veza.AddItem(new PrirucnaCiernaDiera(veza));
            chram.AddItem(new Oltar(chram));

            //pridanie npc
            rozcestie.AddCharacter(new Gandalf(veza));

            //Dalsia logika hry
            startRoom.ItemUsed += (room, player, item) =>
            {
                if (item is MahagonovyKluc)
                {
                    miestnostDraka.Locked = false;
                }
            };

            string name = ConsolePromt("Zadaj Meno");
            Player = new Player(startRoom, name);
            _startTime = DateTime.Now;
        }

        public override void EndGame()
        {
            base.EndGame();
            _database.InsertScore(Player.Name, (DateTime.Now - _startTime).Seconds,
                10000 - (DateTime.Now - _startTime).Seconds);
        }

        protected override void recoverAfrerLoad()
        {
            base.recoverAfrerLoad();
            Rooms.Find(r => r.Name == "Hlavná jaskyna").ItemUsed += (room, player, item) =>
            {
                if (item is MahagonovyKluc)
                {
                    ((DraciaJaskina) Rooms.Find(r => r is DraciaJaskina)).Locked = false;
                }
            };
        }

        public override string ShowScores()
        {
            _database.GetTop();
            return "";
        }
    }
}