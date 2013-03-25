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

        private static List<NPC> _gegner;

        //temporär
        private static float _spawnZeitMin = 3.0f;
        private static float _zeitSeitLetztemSpawn = 0.0f;
        #endregion


        #region Eigenschaften

        public static List<NPC> alleGegner
        {
            get { return _gegner; }
        }
        #endregion


        #region Konstruktor

        public static void Init()
        {
            _gegner = new List<NPC>();
        }
        #endregion


        #region Helfermethoden

        public static void SpawnGegner(Vector2 position, Vector2 geschwindigkeit,Einheit.Fraktion fraktion)
        {
            _gegner.Add(new NPC(new KleinerJaeger(geschwindigkeit, position), fraktion));
        }

        public static void SpawnGegner(Vector2 position, Einheit.Fraktion fraktion)
        {
            SpawnGegner(position, Vector2.Zero, fraktion);
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
                SpawnGegner(Vector2.Zero, Einheit.Fraktion.gegner1);
                _zeitSeitLetztemSpawn = 0f;
            }

            for (int i = _gegner.Count - 1; i >= 0; i--)
            {
                _gegner[i].Update(gameTime);//muss ausgeführt werden, damit die Schüsse zuende berechnet werden

                if (_gegner[i].istAktiv == false)
                {
                    if (_gegner[i].aktuellesSchiff.AnzahlSchuesse() == 0)//Gegner werden erst gelöscht, wenn alle Schüsse von Ihnen verschwunden sind
                        _gegner.RemoveAt(i);
                }
                else
                    KI.BerechneLogik(_gegner[i]); // Berechnet Verhalten des Gegners
                
            }
        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (NPC gegner in _gegner)
                gegner.Draw(spriteBatch);
        }
        #endregion
    }
}
