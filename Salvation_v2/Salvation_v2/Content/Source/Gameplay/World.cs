#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Salvation_v2
{
    public class World
    {
        public Vector2 offset;
        public List<Projectile2D> projectiles = new List<Projectile2D>();
        public List<AttackableObject> allObjects = new List<AttackableObject>();
        public UI UI;
        PassObject ResetWorld;

        public User user;
        public World(PassObject resetWorld)
        {
            ResetWorld = resetWorld;
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;

            LoadData(1);

            offset = new Vector2(0, 0);

            //user.units.Add(new Simply(new Vector2(Globals.screenWidth / 2 - 44, Globals.screenHeight / 2 - 54), 1));
            //user.units.Add(new Simply(new Vector2(Globals.screenWidth - 88, Globals.screenHeight - 104), 2));

            UI = new UI();
        }

        public virtual void Update() 
        {
            if (!user.hero.dead)
            {
                allObjects.Clear();
                allObjects.AddRange(user.GetAllObjects());

                user.Update(user, offset);
                
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, allObjects);
                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                if (Globals.Keyboard.GetPress("R"))
                    ResetWorld(null);
            }
            UI.Update(this);
        }

        public virtual void AddMob(object info)
        {
            var unit = (Unit)info;
            if (user.ID == unit.ownerID)
                user.AddUnit(unit);
        }

        public virtual void AddProjectile(object info) => projectiles.Add((Projectile2D)info);

        public virtual void LoadData(int level)
        {
            var xml = XDocument.Load($"XML\\Levels\\Level{level}.xml");

            XElement element = null;
            if(xml.Element("Root").Element("User") != null)
                element = xml.Element("Root").Element("User");
            user = new User(0, element);

            element = null;
            if (xml.Element("Root").Element("Mob") != null)
            {
                element = xml.Element("Root").Element("Mob");
                var simplyList = (from t in element.Descendants("Simply")
                               select t).ToList();
                for (int i = 0; i < simplyList.Count; i++)
                    user.units.Add(new Simply(new Vector2(Convert.ToInt32(simplyList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(simplyList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), i+1));
            }
        }

        public virtual void Draw(Vector2 offset) 
        {
            user.Draw(this.offset);

            for (int i = 0; i < projectiles.Count; i++)
                projectiles[i].Draw(this.offset);

            UI.Draw(this);
        }
    }
}
