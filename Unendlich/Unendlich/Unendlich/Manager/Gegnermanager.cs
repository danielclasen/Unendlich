using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public static class Gegnermanager
    {
        #region Deklaration

        private static Spieler _spieler;
        private static List<Raumschiff> _gegner;

        //temporär
        private static float _spawnZeitMin = 3.0f;
        private static float _zeitSeitLetztemSpawn = 0.0f;
        #endregion


        #region Eigenschaften

        public static List<Raumschiff> alleGegner
        {
            get { return _gegner; }
        }

        #endregion


        #region Konstruktor

        public static void Init(Spieler spieler)
        {
            _spieler = spieler;
            _gegner = new List<Raumschiff>();
        }
        #endregion


        #region Helfermethoden

        public static void SpawnGegner(Vector2 position, Vector2 geschwindigkeit)
        {
            _gegner.Add(new KleinerJaeger(geschwindigkeit, position));
        }

        public static void SpawnGegner(Vector2 position)
        {
            SpawnGegner(position, Vector2.Zero);
        }

        private static void FliegeZuSpieler(Raumschiff gegner)
        {
            Vector2 neueRichtung = Vector2.Zero;

            neueRichtung.X = _spieler.weltmittelpunkt.X - gegner.weltMittelpunkt.X;
            neueRichtung.Y = _spieler.weltmittelpunkt.Y - gegner.weltMittelpunkt.Y;

            gegner.GeschwindigkeitAendern(neueRichtung);
        }

        private static float EntfernungZumSpieler(Raumschiff gegner)
        {
            return Vector2.Distance(_spieler.weltmittelpunkt, gegner.weltMittelpunkt);
        }

        private static bool IstSpielerImZiel(Raumschiff gegner)
        {
            Vector2 richtung = gegner.geschwindigkeit;
            richtung.Normalize();
            richtung*=EntfernungZumSpieler(gegner);//multipliziert die Richtung des Gegeners mit der Entfernung

            //wenn der Gegner in die Richtung des 2fachen Kollisionsradius des Spieler fliegt, soll er den Spieler im Ziel haben
            if (Vector2.Distance(gegner.weltMittelpunkt + richtung, _spieler.weltmittelpunkt) < _spieler.kollisionsRadius*2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Überprüft die Distanz zum Spieler und schießt, wenn der Spieler näher als Feuerreichweite ist und sich der Spieler im Ziel befindet
        /// </summary>
        /// <param name="gegner"></param>
        private static void SchiesseAufSpieler(Raumschiff gegner)
        {
            //Prüfungen werden geteilt, um Rechenleistung zu sparen
            if (IstSpielerImZiel(gegner))
                if ( EntfernungZumSpieler(gegner) < gegner.waffenreichweite)
                    gegner.BefehlZumFeuern();
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
                {//wenn der Gegner Aktiv ist, fliegt dieser zum Spieler und nimmt ihn unter Beschuss
                    FliegeZuSpieler(_gegner[i]);
                    SchiesseAufSpieler(_gegner[i]);
                }
            }
        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Raumschiff gegner in _gegner)
                gegner.Draw(spriteBatch);
        }

        #endregion
    }
}
