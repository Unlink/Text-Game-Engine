using System.Text;
using GameFramework;
using GameFramework.Parser;

namespace Hra.commands
{
    internal class HelpCommand : ICommand
    {
        public string Execute(Game game, Options options)
        {
            var sb = new StringBuilder("Help: \n");
            sb.Append("newgame - spustí novú hru, pokiaľ iná nebeží\n");
            sb.Append("exit - ukonči program\n");
            sb.Append("explore - preskúma miestnosť alebo konkrétny predmet\n");
            sb.Append("ask - osloví osobu\n");
            sb.Append("explore backpack - preskúma batoh\n");
            sb.Append("drop - položí predmet z batohu do miestnosti\n");
            sb.Append("go - prejde do inej miestnosti\n");
            sb.Append("load - načíta uloženú hru, parameter je nazov uloženej hry\n");
            sb.Append("pickup - zodvihne predmet\n");
            sb.Append("save - uloží hru\n");
            sb.Append("showscore - zobrazí tabulku zo score\n");
            sb.Append("use - použije predmet\n");
            sb.Append("help - zobrazí tento help\n");
            return sb.ToString();
        }

        public string Name
        {
            get { return "help"; }
        }
    }
}