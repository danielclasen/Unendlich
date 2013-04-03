using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public static class Gegnermanager
    {
        #region Deklaration

        //temporär
        private static float _spawnZeitMin = 3.0f;
        private static float _zeitSeitLetztemSpawn = 0.0f;
        #endregion


        #region Helfermethoden

        public static void SpawnGegner(Raumschiff neuesRaumschiff, Einheit.Fraktion fraktion, Quadrant aktuellerQuadtrant)
        {
            aktuellerQuadtrant.HinzufuegenNPC(new NPC(neuesRaumschiff, fraktion));
        }
        #endregion

        
        #region Update und Draw

        public static void Update(GameTime gameTime)
        {
            //temporär
            float vergangenSeitLetztenFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _zeitSeitLetztemSpawn += vergangenSeitLetztenFrame;

            if (_zeitSeitLetztemSpawn > _spawnZeitMin)
            {
                SpawnGegner(new KleinerJaeger(new Vector2(2000, 2000)), Einheit.Fraktion.gegner1, Spielmanager.weltall[0]);
                SpawnGegner(new Spaeher(new Vector2(-2000, -2000)), Einheit.Fraktion.gegner2, Spielmanager.weltall[0]);
                SpawnGegner(new GrosserJaeger(new Vector2(0, 0)), Einheit.Fraktion.spieler1, Spielmanager.weltall[0]);
                _zeitSeitLetztemSpawn = 0f;
            }
        }
        #endregion
    }
}
