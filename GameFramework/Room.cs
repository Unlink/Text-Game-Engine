using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GameFramework
{
    /// <summary>
    ///     Trieda reprezentujúca miestnosť
    /// </summary>
    [Serializable]
    public class Room : IMovingDirection, ISerializable
    {
        /// <summary>
        ///     Delegát pre room udalosti
        /// </summary>
        /// <param name="room"></param>
        /// <param name="player"></param>
        public delegate void RoomHandler(Room room, Player player);

        /// <summary>
        ///     Delegát pre item room udalosti
        /// </summary>
        /// <param name="room"></param>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public delegate void RoomItemHandler(Room room, Player player, IItem item);

        /// <summary>
        ///     Zoznam pomenovaných objektov v miestnosti
        /// </summary>
        protected readonly Dictionary<string, INamed> _namedObjects;

        /// <summary>
        ///     Konštruktor basic miestnosti
        /// </summary>
        /// <param name="name"></param>
        public Room(string name)
        {
            Name = name;
            _namedObjects = new Dictionary<string, INamed>();
        }

        /// <summary>
        ///     Deserializuje dáta
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public Room(SerializationInfo info, StreamingContext ctxt)
        {
            Name = (string) info.GetValue("name", typeof (string));
            _namedObjects = (Dictionary<string, INamed>) info.GetValue("objects", typeof (Dictionary<string, INamed>));
            /*PlayerEntered = (RoomHandler) info.GetValue("event_1", typeof(RoomHandler));
            PlayerLeaved = (RoomHandler) info.GetValue("event_2", typeof(RoomHandler));
            ItemDroped = (RoomItemHandler) info.GetValue("event_3", typeof(RoomItemHandler));
            ItemPicked = (RoomItemHandler) info.GetValue("event_4", typeof(RoomItemHandler));
            ItemUsed = (RoomItemHandler) info.GetValue("event_5", typeof(RoomItemHandler));*/
        }

        /// <summary>
        ///     Názov miestnosti
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Výpis miestnosti
        /// </summary>
        public string Description
        {
            get { return "The door to another room or a gateway to the hell"; }
            private set { }
        }

        /// <summary>
        ///     Vráti this
        /// </summary>
        /// <returns>this</returns>
        public Room GetDestination()
        {
            return this;
        }

        /// <summary>
        ///     Serialize object
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("name", Name);
            info.AddValue("objects", _namedObjects);
            /*info.AddValue("event_1", PlayerEntered);
            info.AddValue("event_2", PlayerLeaved);
            info.AddValue("event_3", ItemDroped);
            info.AddValue("event_4", ItemPicked);
            info.AddValue("event_5", ItemUsed);*/
        }

        /// <summary>
        ///     Event ktorý sa vykoná keď hráč vojde do miestnosti
        /// </summary>
        [field: NonSerialized]
        public event RoomHandler PlayerEntered;

        /// <summary>
        ///     PlayerEntered handler
        /// </summary>
        /// <param name="room"></param>
        /// <param name="player"></param>
        protected virtual void OnPlayerEntered(Player player)
        {
            RoomHandler handler = PlayerEntered;
            if (handler != null) handler(this, player);
        }

        /// <summary>
        ///     Event ktorý sa vykoná keď hráč odíde z miestnosti
        /// </summary>
        [field: NonSerialized]
        public event RoomHandler PlayerLeaved;

        /// <summary>
        ///     PlayerLeaved Handler
        /// </summary>
        /// <param name="room"></param>
        /// <param name="player"></param>
        protected virtual void OnPlayerLeaved(Player player)
        {
            RoomHandler handler = PlayerLeaved;
            if (handler != null) handler(this, player);
        }

        /// <summary>
        ///     Event vykonavaný keď player pužije predmet
        /// </summary>
        [field: NonSerialized]
        public event RoomItemHandler ItemUsed;

        /// <summary>
        ///     ItemUsed handler
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        public virtual void OnItemUsed(Player player, IItem item)
        {
            RoomItemHandler handler = ItemUsed;
            if (handler != null) handler(this, player, item);
        }

        /// <summary>
        ///     Event vykonavaný keď player zodvihne predmet
        /// </summary>
        [field: NonSerialized]
        public event RoomItemHandler ItemPicked;

        /// <summary>
        ///     ItemPicked handler
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        protected virtual void OnItemPicked(Player player, IItem item)
        {
            RoomItemHandler handler = ItemPicked;
            if (item is INoticableItem)
            {
                ((INoticableItem) item).LocationChange(null, player);
            }
            if (handler != null) handler(this, player, item);
        }

        /// <summary>
        ///     Event vykonavaný keď player položi predmet
        /// </summary>
        [field: NonSerialized]
        public event RoomItemHandler ItemDroped;

        /// <summary>
        ///     ItemDroped handler
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        protected virtual void OnItemDroped(Player player, IItem item)
        {
            RoomItemHandler handler = ItemDroped;
            if (item is INoticableItem)
            {
                ((INoticableItem) item).LocationChange(this, player);
            }
            if (handler != null) handler(this, player, item);
        }


        /// <summary>
        ///     Pripojí inú miestnosť k aktualnej miestnosti
        /// </summary>
        /// <param name="direction">Smer</param>
        /// <param name="room">Druhá miestnosť</param>
        public void ConnectTo(string direction, Room room)
        {
            if (room == this)
            {
                throw new ArgumentException("You cannot connect room to yourself");
            }
            _namedObjects.Add(direction, room);
        }

        /// <summary>
        ///     Hráč vojde do miestnosti
        /// </summary>
        /// <param name="player">Hráč</param>
        internal virtual void Enter(Player player)
        {
            OnPlayerEntered(player);
            //Len v prípade, že bude zarušené, že hráč nemôže mať meno ako iný objekt z hry
            //AddCharacter(player);
        }

        /// <summary>
        ///     Hráč opustí miestnosť
        /// </summary>
        /// <param name="player">Hráč</param>
        internal virtual void Leave(Player player)
        {
            OnPlayerLeaved(player);
            //RemoveCharacter(player);
        }

        /// <summary>
        ///     Vráti možné smery pohybu
        /// </summary>
        /// <returns></returns>
        public List<string> GetNeighbors()
        {
            return new List<string>(_namedObjects.Where(o => o.Value is Room).Select(o => o.Key));
        }

        //public bool Move(string direction, Player player)
        //{
        //    IMovingDirection room;
        //    if (_rooms.ContainsKey(direction))
        //    {
        //        room = _rooms[direction];
        //    }
        //    //Aj do predmetu v miestnosti sa dá vojsť alebo ho použiť ako delegáta miestnosti
        //    else if (_namedObjects.ContainsKey(direction) && _namedObjects[direction] is IMovingDirection)
        //    {
        //        room = (IMovingDirection)_namedObjects[direction];
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    //Overíme prístup do miestnosti, a pokiaľ cieľ nieje miestnosť ale sprostredkovateľ tak ho pridáme

        //    if (!room.GetDestination().CheckAccess(this, player, !(room is Room) ? room : null))
        //    {
        //        throw new InvalidRoomAccessException();
        //    }
        //    player.Room = room.GetDestination();
        //    return true;
        //}

        /// <summary>
        ///     Vráti miestnosť na ktorú odkazuje smer, alebo Null pokiaľ taká miestnosť nieje
        /// </summary>
        /// <param name="direction">Smer alebo názov</param>
        /// <param name="player">Hráč</param>
        /// <returns>Room alebo null</returns>
        /// <throws>InvalidRoomAccessException</throws>
        public Room GetRoom(string direction, Player player)
        {
            if (!_namedObjects.ContainsKey(direction) || !(_namedObjects[direction] is IMovingDirection)) return null;
            var ptr = (IMovingDirection) _namedObjects[direction];
            if (!ptr.GetDestination().CheckAccess(this, ptr, player)) return null;
            return ptr.GetDestination();
        }

        /// <summary>
        ///     Overí prístupnosť miestnosti
        /// </summary>
        /// <param name="room">Miestnosť z ktorej prichadzame</param>
        /// <param name="mediator">Buď this, alebo objekt, ktorý sprostredkúva presun do miestnosti</param>
        /// <param name="player">Hráč</param>
        /// <returns></returns>
        /// <throws>InvalidRoomAccessException</throws>
        public virtual bool CheckAccess(Room room, IMovingDirection mediator, Player player)
        {
            return true;
        }

        /// <summary>
        ///     Vráti zoznam predmetov v miestnosti
        /// </summary>
        /// <returns></returns>
        public List<string> GetItems()
        {
            return new List<string>(_namedObjects.Where(o => o.Value is IItem).Select(o => o.Key));
        }

        /// <summary>
        ///     Vráti zoznam osôb v miestnosti
        /// </summary>
        /// <returns></returns>
        public List<string> GetCharacters()
        {
            return new List<string>(_namedObjects.Where(o => o.Value is ICharacter).Select(o => o.Key));
        }

        /// <summary>
        ///     Pridá predmet to miestnosti
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IItem item)
        {
            _namedObjects.Add(item.Name, item);
        }

        /// <summary>
        ///     Odstrani predmet z miestnosti
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(string item)
        {
            _namedObjects.Remove(item);
        }

        /// <summary>
        ///     Zodvihne predmet v miestnosti
        ///     Predmet bude z miestnosti vyňatý
        /// </summary>
        /// <param name="item">Nazov predmetu</param>
        /// <param name="player">Hráč</param>
        /// <returns></returns>
        public IItem PickUp(string item, Player player)
        {
            if (!_namedObjects.ContainsKey(item) || !(_namedObjects[item] is IItem)) return null;
            var itm = (IItem) _namedObjects[item];
            if (!itm.CanMove(player, this)) return null;
            if (!_namedObjects.Remove(item)) return null;
            OnItemPicked(player, itm);
            return itm;
        }

        /// <summary>
        ///     Položí predmet do miestnosti
        /// </summary>
        /// <param name="item">Predmet</param>
        /// <param name="player">Hráč</param>
        /// <returns></returns>
        public bool Drop(IItem item, Player player)
        {
            if (_namedObjects.ContainsKey(item.Name)) return false;
            _namedObjects.Add(item.Name, item);
            OnItemDroped(player, item);
            return true;
        }

        /// <summary>
        ///     Použije predmet v miestnosti
        /// </summary>
        /// <param name="usable">Nazov predmetu</param>
        /// <param name="player">Hráč</param>
        /// <returns></returns>
        public virtual bool Use(string usable, Player player)
        {
            if (_namedObjects.ContainsKey(usable) && _namedObjects[usable] is IUsable)
            {
                ((IUsable) _namedObjects[usable]).Use(player, this);
                OnItemUsed(player, (IItem) _namedObjects[usable]);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Pridá postavu do miestnosti
        /// </summary>
        /// <param name="character">Postava</param>
        /// <returns></returns>
        public bool AddCharacter(ICharacter character)
        {
            if (_namedObjects.ContainsKey(character.Name)) return false;
            _namedObjects.Add(character.Name, character);
            if (character.Room != this) character.Room = this;
            return true;
        }

        /// <summary>
        ///     Odoberie postavu z miestnosti
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool RemoveCharacter(ICharacter character)
        {
            if (!_namedObjects.ContainsKey(character.Name)) return false;
            return _namedObjects.Remove(character.Name);
        }

        /// <summary>
        ///     Vráti postavu v mistnosti
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public ICharacter GetCharacter(string character)
        {
            if (_namedObjects.ContainsKey(character) && _namedObjects[character] is ICharacter)
            {
                return (ICharacter) _namedObjects[character];
            }
            return null;
        }


        /// <summary>
        ///     Vypíše informácie o miestnosti
        /// </summary>
        /// <returns></returns>
        public virtual string GetInfo()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append("Room: ").Append(Name).Append("\n");
            }
            if (GetNeighbors().Count > 0)
            {
                sb.Append("Doors:").Append("\n");
                foreach (string neighbor in GetNeighbors())
                {
                    sb.Append(" ").Append(neighbor).Append("\n");
                }
            }

            if (GetItems().Count > 0)
            {
                sb.Append("Items:").Append("\n");
                foreach (string imt in GetItems())
                {
                    sb.Append(" ").Append(imt).Append("\n");
                }
            }

            if (GetCharacters().Count > 0)
            {
                sb.Append("Characters:").Append("\n");
                foreach (string c in GetCharacters())
                {
                    sb.Append(" ").Append(c).Append("\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Vyhľadá popis predmetu v miestnosti
        /// </summary>
        /// <param name="what"></param>
        /// <returns></returns>
        public string ExploreItem(string what)
        {
            if (_namedObjects.ContainsKey(what))
                return _namedObjects[what].Description;
            return null;
        }
    }
}