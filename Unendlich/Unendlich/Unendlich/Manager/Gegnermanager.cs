using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Gegnermanager
    {
        #region Deklaration

        private List<Raumschiff> _gegner;

        //temporär
        private float _spawnZeitMin = 3.0f;
        private float _zeitSeitLetztemSpawn = 0.0f;
        #endregion


        #region Eigenschaften

        public List<Raumschiff> alleGegner
        {
            get { return _gegner; }
        }
        #endregion


        #region Konstruktor

        public Gegnermanager()
        {
            _gegner = new List<Raumschiff>();
        }
        #endregion


        #region Helfermethoden

        public void SpawnGegner(Vector2 position, Vector2 geschwindigkeit)
        {
            _gegner.Add(new KleinerJaeger(geschwindigkeit, position));
        }

        public void SpawnGegner(Vector2 position)
        {
            SpawnGegner(position, Vector2.Zero);
        }
        #endregion

        
        #region Update und Draw

        public void Update(GameTime gameTime)
        {
            //temporär
            float vergangenSeitLetztenFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _zeitSeitLetztemSpawn += vergangenSeitLetztenFrame;
            if (_zeitSeitLetztemSpawn > _spawnZeitMin)
            {
                SpawnGegner(Vector2.Zero);
                _zeitSeitLetztemSpawn = 0f;
            }

            for (int i = _gegner.Count - 1; i >= 0; i--)
            {
                _gegner[i].Update(gameTime);//muss ausgeführt werden, damit die Schüsse zuende berechnet werden

                if (_gegner[i].istAktiv == false)
                {
                    if (_gegner[i].AnzahlSchuesse() == 0)//Gegner werden erst gelöscht, wenn alle Schüsse von Ihnen verschwunden sind
                        _gegner.RemoveAt(i);
                }
                else
                    KI.BerechneVerhalten(_gegner[i]); // Berechnet Verhalten des Gegners
                
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Raumschiff gegner in _gegner)
                gegner.Draw(spriteBatch);
        }
        #endregion
    }
}
