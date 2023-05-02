#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Salvation_v2
{
    public class Player
    {
        public int ID;
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public Player(int id, XElement data) 
        {
            id = ID;
            LoadData(data);
        }

        public virtual void Update(Player enemy, Vector2 offset)
        {
            if (hero != null) 
                hero.Update(offset);

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Update(offset, enemy);
                if (units[i].dead)
                {
                    ChangeScore(1);
                    units.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddUnit(object info)
        {
            var unit = (Unit)info;
            unit.ownerID = ID;
            units.Add(unit);
        }

        public virtual void ChangeScore(int score)
        {
            GameGlobals.score += score;
        }

        public virtual List<AttackableObject> GetAllObjects()
        {
            var objects = new List<AttackableObject>();
            objects.AddRange(units.ToList<AttackableObject>());
            return objects;
        }

        public virtual void LoadData(XElement data)
        {
            if (data.Element("Hero") != null)
                hero = new Hero("2D\\hero", new Vector2(Convert.ToInt32(data.Element("Hero").Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(data.Element("Hero").Element("Pos").Element("y").Value, Globals.cultureRU)), new Vector2(108, 140), ID);
        }

        public virtual void Draw(Vector2 offset)
        {
            if (hero != null)
                hero.Draw(offset);
            for (int i = 0; i < units.Count; i++)
                units[i].Draw(offset);
        }
    }
}
