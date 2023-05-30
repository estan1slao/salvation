#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
#endregion

namespace Salvation_v2
{
    public class World
    {
        Vector2 offset;
        List<Projectile2D> projectiles = new List<Projectile2D>();
        List<AttackableObject> attackObjects = new List<AttackableObject>();
        UI UI;
        List<Basic2D> nonCollidingObjects = new List<Basic2D>();
        List<Spike> spikes = new List<Spike>();
        public List<Door> doors { get; private set; }
        PassObject ResetWorld;
        List<Basic2D> parralelObject = GameGlobals.parallelObject;
        public List<Button> buttons = GameGlobals.Buttons;
        List<Basic2D> images = new List<Basic2D>();

        public User user;
        public World(PassObject resetWorld, int levelNumber)
        {
            ResetWorld = resetWorld;
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            LoadData(levelNumber);
            offset = new Vector2(0, 0);
            UI = new UI();
        }

        public virtual void Update()
        {
            if (GameGlobals.isEnd) { return; }
            if (!user.hero.dead)
            {
                attackObjects.Clear();
                attackObjects.AddRange(user.GetAllObjects());
                if (GameGlobals.isParallel)
                    user.Update(user, offset, parralelObject, doors);
                else
                    user.Update(user, offset, nonCollidingObjects, doors);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (GameGlobals.isParallel)
                        projectiles[i].Update(offset, attackObjects, parralelObject);
                    else
                        projectiles[i].Update(offset, attackObjects, nonCollidingObjects);
                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
                attackObjects.Add(user.hero);
                for (int i = 0; i < spikes.Count; i++)
                    spikes[i].Update(offset, attackObjects);
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

        public virtual void Draw(Vector2 offset)
        {
            if (GameGlobals.isEnd) { return; }
            for (int i = 0; i < images.Count; i++)
                images[i].Draw(this.offset);

            for (int i = 0; i < buttons.Count; i++)
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

        #region Data
        private void LoadData(int level)
        {
            parralelObject.Clear();
            buttons.Clear();
            var path = $"XML\\Levels\\Level{level}.xml";
            if (!File.Exists(path))
            {
                GameGlobals.isEnd = true;
                return;
            }
            var xml = XDocument.Load(path);

            XElement element = null;
            if (xml.Element("Root").Element("User") != null)
                element = xml.Element("Root").Element("User");
            user = new User(0, element);

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
                var armoredList = (from t in element.Descendants("Armored")
                                   select t).ToList();
                for (int i = 0; i < armoredList.Count; i++)
                {
                    var armored = new Armored(new Vector2(Convert.ToInt32(armoredList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(armoredList[i].Element("Pos").Element("y").Value, Globals.cultureRU)), new Vector2(6, 4), i + 1, TypeOfDeath.Everywhere);
                    user.units.Add(armored);
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
                    var width = Convert.ToInt32(platformList[i].Element("Size").Element("Width").Value, Globals.cultureRU);
                    var height = Convert.ToInt32(platformList[i].Element("Size").Element("Height").Value, Globals.cultureRU);
                    var minSize = Math.Min(width, height);
                    var maxSize = Math.Max(width, height);
                    var position = new Vector2(Convert.ToInt32(platformList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(platformList[i].Element("Pos").Element("y").Value, Globals.cultureRU));
                    if (maxSize == width)
                    {
                        for (int j = 0; j < maxSize; j += minSize)
                        {
                            var platform = new Basic2D("2D\\Texture\\platform", new Vector2(position.X + j, position.Y),
                                new Vector2(minSize, minSize));
                            nonCollidingObjects.Add(platform);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < maxSize; j += minSize)
                        {
                            var platform = new Basic2D("2D\\Texture\\platform", new Vector2(position.X, position.Y + j),
                                new Vector2(minSize, minSize));
                            nonCollidingObjects.Add(platform);
                        }
                    }
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
                doors = new List<Door>();
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
                    var width = Convert.ToInt32(parObjList[i].Element("Size").Element("Width").Value, Globals.cultureRU);
                    var height = Convert.ToInt32(parObjList[i].Element("Size").Element("Height").Value, Globals.cultureRU);
                    var minSize = Math.Min(width, height);
                    var maxSize = Math.Max(width, height);
                    var position = new Vector2(Convert.ToInt32(parObjList[i].Element("Pos").Element("x").Value, Globals.cultureRU), Convert.ToInt32(parObjList[i].Element("Pos").Element("y").Value, Globals.cultureRU));
                    if (maxSize == width)
                    {
                        for (int j = 0; j < maxSize; j += minSize)
                        {
                            var platform = new Basic2D("2D\\Texture\\platform", new Vector2(position.X + j, position.Y),
                                new Vector2(minSize, minSize));
                            parralelObject.Add(platform);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < maxSize; j += minSize)
                        {
                            var platform = new Basic2D("2D\\Texture\\platform", new Vector2(position.X, position.Y + j),
                                new Vector2(minSize, minSize));
                            parralelObject.Add(platform);
                        }
                    }
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
        }
        #endregion
    }
}
