#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
#endregion

namespace Salvation_v2
{
    public class World
    {
        public Vector2 offset;
        public List<Projectile2D> projectiles = new List<Projectile2D>();
        public List<AttackableObject> allObjects = new List<AttackableObject>();
        public UI UI;
        public List<Basic2D> nonCollidingObjects = new List<Basic2D>();
        public List<Spike> spikes = new List<Spike>();
        public List<Door> doors = new List<Door>();
        PassObject ResetWorld;
        public List<Basic2D> parralelObject = GameGlobals.parallelObject;
        public List<Button> buttons = GameGlobals.Buttons;
        public List<Basic2D> images = new List<Basic2D>();

        public User user;
        public World(PassObject resetWorld, int levelNumber)
        {
            ResetWorld = resetWorld;
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;

            parralelObject.Clear();
            buttons.Clear();
            LoadData(levelNumber);

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
                if (GameGlobals.isParallel)
                    user.Update(user, offset, parralelObject, doors);
                else
                    user.Update(user, offset, nonCollidingObjects, doors);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (GameGlobals.isParallel)
                        projectiles[i].Update(offset, allObjects, parralelObject);
                    else
                        projectiles[i].Update(offset, allObjects, nonCollidingObjects);
                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
                allObjects.Add(user.hero);
                for (int i = 0; i < spikes.Count; i++)
                    spikes[i].Update(offset, allObjects);
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
            if (xml.Element("Root").Element("User") != null)
                element = xml.Element("Root").Element("User");
            user = new User(0, element);
            //nonCollidingObjects.Add(user.hero);
            //parralelObject.Add(user.hero);

            element = null;
            if (xml.Element("Root").Element("Mob") != null)
            {
                element = xml.Element("Root").Element("Mob");
                var simplyList = (from t in element.Descendants("Simply")
                                  select t).ToList();
                for (int i = 0; i < simplyList.Count; i++)
                {
                    var simply = new Simply(new Vector2(Convert.ToInt32(simplyList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(simplyList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), new Vector2(8, 8), i + 1, TypeOfDeath.OnlyReal);
                    user.units.Add(simply);
                }
                var speedyList = (from t in element.Descendants("Speedy")
                                  select t).ToList();
                for (int i = 0; i < speedyList.Count; i++)
                {
                    var speedy = new Speedy(new Vector2(Convert.ToInt32(speedyList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(speedyList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), new Vector2(8, 6), i + 1, TypeOfDeath.OnlyParallel);
                    user.units.Add(speedy);
                }
            }

            if (xml.Element("Root").Element("Spikes") != null)
            {
                element = xml.Element("Root").Element("Spikes");
                var spikeList = (from t in element.Descendants("Spike")
                                 select t).ToList();
                for (int i = 0; i < spikeList.Count; i++)
                {
                    var spike = new Spike(new Vector2(Convert.ToInt32(spikeList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(spikeList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), new Vector2(25, 25), 0, new Vector2(1, 1), 10, TypeOfDeath.Everywhere);
                    spikes.Add(spike);
                }

                spikeList = (from t in element.Descendants("SpikesList")
                             select t).ToList();
                for (int i = 0; i < spikeList.Count; i++)
                {
                    var startPos = new Vector2(Convert.ToInt32(spikeList[i].Element("StartPos").Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(spikeList[i].Element("StartPos").Element("Pos").Element("y").Value, Globals.cultureRU));
                    var endPos = new Vector2(Convert.ToInt32(spikeList[i].Element("EndPos").Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(spikeList[i].Element("EndPos").Element("Pos").Element("y").Value, Globals.cultureRU));
                    for (int j = 0; j < endPos.X - startPos.X; j += 25)
                    {
                        var spike = new Spike(new Vector2(j, (int)startPos.Y), new Vector2(25, 25), 0, new Vector2(1, 1), 10, TypeOfDeath.Everywhere);
                        spikes.Add(spike);
                    }
                }
            }

            if (xml.Element("Root").Element("Design") != null)
            {
                element = xml.Element("Root").Element("Design");
                var platformList = (from t in element.Descendants("Platform")
                                    select t).ToList();
                for (int i = 0; i < platformList.Count; i++)
                {
                    var platform = new Basic2D("2D\\Texture\\platform", new Vector2(Convert.ToInt32(platformList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(platformList[i].Element("Pos").Element("y").Value, Globals.cultureRU)),
                        new Vector2(Convert.ToInt32(platformList[i].Element("Size").Element("Width").Value, Globals.cultureRU), Convert.ToInt32(platformList[i].Element("Size").Element("Height").Value, Globals.cultureRU)));
                    nonCollidingObjects.Add(platform);
                }

                var imageList = (from t in element.Descendants("Image")
                                 select t).ToList();
                for (int i = 0; i < imageList.Count; i++)
                {
                    var image = new Basic2D(Convert.ToString(imageList[i].Element("Path").Value),
                        new Vector2(Convert.ToInt32(imageList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(imageList[i].Element("Pos").Element("y").Value, Globals.cultureRU)),
                        new Vector2(Convert.ToInt32(imageList[i].Element("Size").Element("Width").Value, Globals.cultureRU), Convert.ToInt32(imageList[i].Element("Size").Element("Height").Value, Globals.cultureRU)));
                    images.Add(image);
                }
            }

            if (xml.Element("Root").Element("NextLevel") != null)
            {
                element = xml.Element("Root").Element("NextLevel");
                var doorList = (from t in element.Descendants("Door")
                                select t).ToList();
                for (int i = 0; i < doorList.Count; i++)
                {
                    Door door;
                    if (doorList[i].Element("IsActive") == null)
                        door = new Door(new Vector2(Convert.ToInt32(doorList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(doorList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), Convert.ToInt32(doorList[i].Element("Level").Value, Globals.cultureRU));
                    else
                        door = new Door(new Vector2(Convert.ToInt32(doorList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(doorList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), Convert.ToInt32(doorList[i].Element("Level").Value, Globals.cultureRU), Convert.ToBoolean(doorList[i].Element("IsActive").Value));
                    doors.Add(door);
                }

            }

            if (xml.Element("Root").Element("Parallel") != null)
            {
                element = xml.Element("Root").Element("Parallel");
                var parObjList = (from t in element.Descendants("Platform")
                                  select t).ToList();
                for (int i = 0; i < parObjList.Count; i++)
                {
                    var parObj = new Basic2D("2D\\Texture\\platform", new Vector2(Convert.ToInt32(parObjList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(parObjList[i].Element("Pos").Element("y").Value, Globals.cultureRU)),
                        new Vector2(Convert.ToInt32(parObjList[i].Element("Size").Element("Width").Value, Globals.cultureRU), Convert.ToInt32(parObjList[i].Element("Size").Element("Height").Value, Globals.cultureRU)));
                    parralelObject.Add(parObj);
                }
            }

            if (xml.Element("Root").Element("Buttons") != null)
            {
                element = xml.Element("Root").Element("Buttons");
                var buttonsList = (from t in element.Descendants("Button")
                                   select t).ToList();
                for (int i = 0; i < buttonsList.Count; i++)
                {
                    var button = new Button(new Vector2(Convert.ToInt32(buttonsList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(buttonsList[i].Element("Pos").Element("y").Value, Globals.cultureRU)),
                        Convert.ToInt32(buttonsList[i].Element("Time").Value));
                    buttons.Add(button);
                }
            }

            #region Test
            //nonCollidingObjects.Add(new Basic2D("2D\\simplyNoAnim", new Vector2(200, 600), new Vector2(100, 100)));
            //var spikeObj = new Spike(new Vector2(700, Globals.screenHeight - 25), new Vector2(25, 25), 0, new Vector2(1, 1), 0);
            //spikes.Add(spikeObj);
            #endregion
        }

        public virtual void Draw(Vector2 offset)
        {
            for (int i = 0; i < images.Count; i++)
                images[i].Draw(this.offset);

            for (int i=0; i < buttons.Count; i++)
                buttons[i].Draw(this.offset);

            for (int i = 0; i < doors.Count; i++)
                doors[i].Draw(this.offset);

            user.Draw(this.offset);

            for (int i = 0; i < projectiles.Count; i++)
                projectiles[i].Draw(this.offset);

            if (GameGlobals.isParallel)
                for (int i = 0; i < parralelObject.Count; i++)
                    parralelObject[i].Draw(this.offset);
            else
                for (int i = 0; i < nonCollidingObjects.Count; i++)
                    nonCollidingObjects[i].Draw(this.offset);

            for (int i = 0; i < spikes.Count; i++)
                spikes[i].Draw(this.offset);

            UI.Draw(this);
        }
    }
}
