using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit;

namespace Salvation_v2.Tests
{
    public class Tests
    {
        public class GamePlaysTest
        {
            private List<GamePlay> GamePlays = new List<GamePlay>();
            public GamePlaysTest()
            {
                var levelCount = new DirectoryInfo("XML\\Levels").GetFiles().Length;
                for (int i = 1; i <= levelCount; i++)
                {
                    var game = new Main();
                    game.RunOneFrame();
                    GamePlays.Add(new GamePlay(i));
                }  
            }

            [Fact]
            private void GamePlayIsNotNull()
            {
                foreach(var gamePlay in GamePlays)
                {
                    Assert.NotNull(gamePlay.world.user);
                    Assert.NotNull(gamePlay.world.user.hero);
                    Assert.NotNull(gamePlay.world.doors);
                }
            }

            [Fact]
            private void AddProjectileInGamePlay()
            {
                foreach(var gamePlay in GamePlays)
                {
                    try
                    {
                        gamePlay.world.AddProjectile(null);
                        Assert.True(true);
                    }
                    catch
                    {
                        Assert.Fail("The projectile was not created");
                    }
                }
            }

            [Fact]
            private void DoUnitsGetDamage()
            {
                foreach (var gamePlay in GamePlays)
                {
                    float oldHealth;
                    foreach (var unit in gamePlay.world.user.units)
                    {
                        oldHealth = unit.health;
                        unit.GetHit(1);
                        if (oldHealth == unit.health)
                            Assert.Fail("The unit did not receive a damage");
                    }

                    oldHealth = gamePlay.world.user.hero.health;
                    gamePlay.world.user.hero.GetHit(1);
                    if (oldHealth == gamePlay.world.user.hero.health)
                        Assert.Fail("The unit did not receive a damage");
                }
                Assert.True(true);
            }
        }
    }
}
