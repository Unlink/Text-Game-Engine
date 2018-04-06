using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework
{
    [Serializable]
    public class Player : ICharacter
    {
        private readonly Dictionary<string, IItem> _backpack;
        private Room _room;

        public Player(Room room, string name)
        {
            Room = room;
            _backpack = new Dictionary<string, IItem>();
            Name = name;
        }

        /// <summary>
        ///     Aktuálna lokácia hráča
        /// </summary>
        public Room Room
        {
            get { return _room; }
            set
            {
                if (_room != null)
                {
                    _room.Leave(this);
                }
                _room = value;
                _room.Enter(this);
            }
        }

        /// <summary>
        ///     Hráč nerád odpovedá;
        /// </summary>
        /// <param name="player"></param>
        public void Ask(Player player)
        {
        }

        /// <summary>
        ///     Meno Postavy
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Popis postavy
        /// </summary>
        public string Description
        {
            get { return "Total crazzy person"; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Vloží item do batohu hráča
        /// </summary>
        /// <param name="item">Predmet</param>
        /// <returns></returns>
        public virtual bool AddItem(IItem item)
        {
            if (_backpack.ContainsKey(item.Name)) return false;
            _backpack.Add(item.Name, item);
            return true;
        }


        /// <summary>
        ///     Overí či ma hráč predmet v batohu
        /// </summary>
        /// <param name="item">Predmet</param>
        /// <returns></returns>
        public bool HasItem(string item)
        {
            return _backpack.ContainsKey(item);
        }


        /// <summary>
        ///     Vráti predmet z batohu
        /// </summary>
        /// <param name="item">Predmet</param>
        /// <param name="remove">Vyberie z batohu</param>
        /// <returns>Predmet</returns>
        public IItem GetItem(string item, bool remove = false)
        {
            IItem itm = _backpack[item];
            if (remove)
            {
                _backpack.Remove(item);
            }
            return itm;
        }

        /// <summary>
        ///     Použije predmet
        /// </summary>
        /// <param name="item">Názov predmetu</param>
        /// <returns></returns>
        public bool Use(string item)
        {
            if (HasItem(item) && GetItem(item) is IUsable)
            {
                ((IUsable) GetItem(item)).Use(this, Room);
                Room.OnItemUsed(this, GetItem(item));
                return true;
            }
            return Room.Use(item, this);
        }

        /// <summary>
        ///     Zodvidne predmet z miestnosti
        /// </summary>
        /// <param name="itm">Predmet</param>
        /// <returns></returns>
        public bool PickUp(string itm)
        {
            if (_backpack.ContainsKey(itm)) return false;
            IItem item = Room.PickUp(itm, this);
            if (item == null) return false;
            _backpack.Add(itm, item);
            return true;
        }

        /// <summary>
        ///     Položi predmet do miestnosti
        /// </summary>
        /// <param name="itm"></param>
        /// <returns></returns>
        public bool Drop(string itm)
        {
            if (!_backpack.ContainsKey(itm)) return false;
            IItem item = _backpack[itm];
            if (! Room.Drop(item, this)) return false;
            _backpack.Remove(itm);
            return true;
        }

        /// <summary>
        ///     Presunie hráča do inej miestnosti
        /// </summary>
        /// <param name="where">Smer</param>
        /// <returns></returns>
        public bool Move(string where)
        {
            Room room = Room.GetRoom(where, this);
            if (room == null) return false;
            Room = room;
            return true;
        }

        /// <summary>
        ///     Preskúma miestnosť
        /// </summary>
        /// <param name="what">Objekt na preskúmanie</param>
        public string Explore(string what)
        {
            if (string.IsNullOrEmpty(what))
            {
                return Room.GetInfo();
            }
            if (what == "backpack")
            {
                var sb = new StringBuilder();
                foreach (var item in _backpack)
                {
                    sb.Append(item.Key).Append("\n");
                }
                return sb.ToString();
            }
            if (_backpack.ContainsKey(what))
            {
                return _backpack[what].Description;
            }
            return Room.ExploreItem(what);
        }

        /// <summary>
        ///     Osloví postavu
        /// </summary>
        /// <param name="itm"></param>
        /// <returns></returns>
        public bool Ask(string itm)
        {
            if (!_room.GetCharacters().Contains(itm)) return false;
            ICharacter character = _room.GetCharacter(itm);
            character.Ask(this);
            return true;
        }
    }
}