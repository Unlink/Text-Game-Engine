using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Parser
{
    public class Parser
    {
        /// <summary>
        ///     Slovník príkazov
        /// </summary>
        private readonly Dictionary<GameState, Dictionary<string, ICommand>> _commands;

        public Parser()
        {
            _commands = new Dictionary<GameState, Dictionary<string, ICommand>>();
            foreach (GameState state in (GameState[]) Enum.GetValues(typeof (GameState)))
            {
                _commands.Add(state, new Dictionary<string, ICommand>());
            }
        }

        /// <summary>
        ///     Pridá príkaz do platných príkazov
        /// </summary>
        /// <param name="name">Názov</param>
        /// <param name="command">Príkaz</param>
        /// <param name="gameState">Stav/y hry v ktorých command funguje</param>
        public void AddCommand(ICommand command, GameState gameState, string name = null)
        {
            if (name == null)
            {
                name = command.Name;
            }
            foreach (GameState state in (GameState[]) Enum.GetValues(typeof (GameState)))
            {
                if ((gameState & state) == state)
                {
                    _commands[state].Add(name, command);
                }
            }
        }

        /// <summary>
        ///     Spracuje užívateľov vstup
        /// </summary>
        /// <param name="line">Užívateľský vstup</param>
        /// <param name="gameState">Stav hry</param>
        /// <returns>Vykonateľný príkaz alebo null</returns>
        public ExecutableCommand ParseLine(string line, GameState gameState)
        {
            string[] parsed = line.Split(new[] {' '}, 2);
            if (parsed.Length < 1)
            {
                return null;
            }
            if (!_commands[gameState].ContainsKey(parsed[0]))
            {
                return null;
            }
            return new ExecutableCommand(_commands[gameState][parsed[0]], (parsed.Length > 1) ? parsed[1] : "");
        }

        public string[] GetCommands(GameState state)
        {
            return _commands[state].Keys.ToArray();
        }
    }
}