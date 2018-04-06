using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GameFramework.defaultCommands;
using GameFramework.DefaultCommands;
using GameFramework.Parser;

namespace GameFramework
{
    /// <summary>
    ///     Trieda reprezentujuca hru
    /// </summary>
    public abstract class Game
    {
        /// <summary>
        ///     Parser
        /// </summary>
        protected Parser.Parser _parser;

        /// <summary>
        ///     Zaciatok hry
        /// </summary>
        protected DateTime _startTime;

        public Game()
        {
            _parser = new Parser.Parser();
            Rooms = new List<Room>();
            State = GameState.New;
            InicializeCommands();
        }

        /// <summary>
        ///     Zoznam miestnosti
        /// </summary>
        public List<Room> Rooms { get; protected set; }

        /// <summary>
        ///     Stav hry
        /// </summary>
        public GameState State { get; protected set; }

        /// <summary>
        ///     Hráč
        /// </summary>
        public Player Player { get; protected set; }

        /// <summary>
        ///     Bežiaca hra
        /// </summary>
        public bool GameRunning { get; protected set; }

        protected void InicializeCommands()
        {
            _parser.AddCommand(new ExitCommand(), GameState.New | GameState.Running | GameState.End);
            _parser.AddCommand(new NewGameCommand(), GameState.New | GameState.End);
            _parser.AddCommand(new LoadCommand(), GameState.New | GameState.Running | GameState.End);
            _parser.AddCommand(new ScoresCommand(), GameState.New | GameState.End);
            _parser.AddCommand(new DropCommand(), GameState.Running);
            _parser.AddCommand(new ExploreCommand(), GameState.Running);
            _parser.AddCommand(new GoCommand(), GameState.Running);
            _parser.AddCommand(new PickUpCommand(), GameState.Running);
            _parser.AddCommand(new SaveCommand(), GameState.Running);
            _parser.AddCommand(new UseCommand(), GameState.Running);
            _parser.AddCommand(new AskCommand(), GameState.Running);
        }

        /// <summary>
        ///     Hlavná slučka hry
        /// </summary>
        public void Run()
        {
            GameRunning = true;
            while (GameRunning)
            {
                string consolePromt = ConsolePromt();
                ExecutableCommand cmd = _parser.ParseLine(consolePromt, State);
                if (cmd != null)
                {
                    try
                    {
                        string result = cmd.Execute(this);
                        if (!string.IsNullOrEmpty(result))
                        {
                            Console.WriteLine(result);
                        }
                    }
                    catch (EndGameException ex)
                    {
                        State = GameState.End;
                        Console.WriteLine(ex.Message);
                        EndGame();
                    }
                }
                else
                {
                    var suggestion = TextTools.GetSuggestion(_parser.GetCommands(State), consolePromt);
                    Console.WriteLine("Invallid command."+(suggestion != "" ? " Did you mean "+suggestion+"?" : ""));
                }
            }
        }

        internal string NewGame()
        {
            InicializeNewGame();
            State = GameState.Running;
            return "Game Started";
        }

        protected abstract void InicializeNewGame();

        public virtual string ConsolePromt(string message = null)
        {
            if (message != null)
            {
                Console.WriteLine(message);
            }
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void exit()
        {
            GameRunning = false;
        }

        public string Save(string opts)
        {
            Stream stream = File.Open(opts + ".save", FileMode.Create);
            var bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, new SaveFileData(Player, Rooms, _startTime));
            stream.Close();
            return "saved";
        }

        public string LoadGame(string opts)
        {
            Stream stream = File.Open(opts + ".save", FileMode.Open);
            var bFormatter = new BinaryFormatter();
            var data = (SaveFileData) bFormatter.Deserialize(stream);
            Player = data.Player;
            Rooms = data.Rooms;
            _startTime = data.DateTime;
            stream.Close();
            State = GameState.Running;
            recoverAfrerLoad();
            return "game loaded";
        }

        public abstract string ShowScores();

        public virtual void EndGame()
        {
            State = GameState.End;
        }

        protected virtual void recoverAfrerLoad()
        {
        }

        [Serializable]
        private struct SaveFileData
        {
            public readonly DateTime DateTime;
            public readonly Player Player;
            public readonly List<Room> Rooms;

            public SaveFileData(Player player, List<Room> rooms, DateTime begin)
            {
                Player = player;
                Rooms = rooms;
                DateTime = begin;
            }

            /// <summary>
            ///     Deserializuje objekt
            /// </summary>
            /// <param name="info"></param>
            /// <param name="ctxt"></param>
            public SaveFileData(SerializationInfo info, StreamingContext ctxt)
            {
                Rooms = (List<Room>) info.GetValue("rooms", typeof (List<Room>));
                Player = (Player) info.GetValue("player", typeof (Player));
                DateTime = (DateTime) info.GetValue("datetime", typeof (DateTime));
            }

            /// <summary>
            ///     Serializuje objekt
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("rooms", Rooms);
                info.AddValue("player", Player);
                info.AddValue("datetime", DateTime);
            }
        }
    }
}