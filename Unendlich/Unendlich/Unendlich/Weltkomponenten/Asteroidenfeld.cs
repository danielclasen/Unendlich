/*
 * Asteroiden rotieren nicht.
 * Es gibt noch keine Kollisionen unter den Asteroiden
 * 
 * Update ist noch nicht richtig
 *      -Asteroiden müssen sich solange bewegen, wie sie auf dem Bild sind 
 *          -eventuell die Setzten abfrage so ändern, das Asteroiden nur innerhalb des Asteroidenfeldes gesetzt werden können
 *      -Kollisionsabfrage
 *      -dies auch Abfragen, wenn Asteroiden gesetzt werden
 *      -setzte frei funktioniert nicht
 *      
 * 
 *      -sklaierung sorgt für falsche positionierung
 * 
 * 
 * */

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    /// <summary>
    /// Stellt ein Asteroidenfeld dar
    /// Allgemein:  Das Asteroidenfeld belegt einen gewissen Bereich im Raum. Wird dieser Raum vom Spieler berührt, tauchen dort Asteroiden auf, ansonsten passiert nichts
    /// 
    /// Asteroiden:
    /// Es gibt 3 Arten von Asteroiden
    /// 
    /// Klein
    /// Größe:  20*20 Pixel
    /// Masse:  400
    /// 
    /// Mittel
    /// Größe:  40*40 Pixel
    /// Masse:  1600
    /// 
    /// Groß
    /// Größe: 80*80 Pixel
    /// Masse: 6400
    /// 
    /// Sehr groß
    /// Größe: 160*160 Pixel
    /// Masse: 25600
    /// </summary>
    public class Asteroidenfeld
    {
        #region Deklaration

        protected static List<Asteroid> _asteroiden;
        protected int _anzahlAsteroiden;
        protected Rectangle _feldFlaeche;
        protected Random rand;
        protected int _minGeschwindigkeit = 5;
        protected int _maxGeschwindigkeit = 100;
        protected float _maxRotation = MathHelper.Pi;

        #endregion


        #region Eigenschaften

        public int anzahlAsteroiden
        {
            get { return _anzahlAsteroiden; }
        }

        public Vector2 position
        {
            get { return new Vector2(_feldFlaeche.X, _feldFlaeche.Y); }
        }

        public Rectangle flache
        {
            get { return _feldFlaeche; }
        }

        public Vector2 weltMitte
        {
            get { return new Vector2(_feldFlaeche.X + _feldFlaeche.Width / 2, _feldFlaeche.Y + _feldFlaeche.Height / 2); }
        }
        #endregion


        //TEMP!!!
        public static List<Asteroid> GETAlleAsteroiden()
        {
            return _asteroiden;
        }



        #region Konstruktor

        public Asteroidenfeld(Rectangle objektRechteck)
        {
            rand = new Random(Helferklasse.rand.Next(0,1000));
            _asteroiden = new List<Asteroid>();

            _feldFlaeche = objektRechteck;
            _anzahlAsteroiden = rand.Next(1, 10);    //wie viele Asteroiden zu sehen sein sollen

            for (int i = 0; i < anzahlAsteroiden; i++)
            {
                erstelleAsteroid();
            }
        }
        #endregion


        #region Helfermethoen


        //noch nicht funktionsbereit
        //**************************

        protected void checkInterneKollisionen()
        {
            for (int i = 0; i < anzahlAsteroiden; i++)
                for (int j = i; j < anzahlAsteroiden; j++)
                {
                    if (_asteroiden[i].IstKreisKollision(_asteroiden[j].weltMittelpunkt, _asteroiden[j].kollisionsRadius))
                        asteroidenKollision(_asteroiden[i], _asteroiden[j]);
                }
        }

        protected void asteroidenKollision(Asteroid asteroid1, Asteroid asteroid2)
        {
            Vector2 zentrumDerMasse = (asteroid1.geschwindigkeit + asteroid2.geschwindigkeit) / 2;

            Vector2 richtung1 = asteroid2.objektMitte - asteroid1.objektMitte;
            richtung1.Normalize();
            Vector2 richtung2 = asteroid1.objektMitte - asteroid2.objektMitte;
            richtung2.Normalize();

            asteroid1.geschwindigkeit -= zentrumDerMasse;
            asteroid1.geschwindigkeit = Vector2.Reflect(asteroid1.geschwindigkeit, richtung1);
            asteroid1.geschwindigkeit += zentrumDerMasse;

            asteroid2.geschwindigkeit -= zentrumDerMasse;
            asteroid2.geschwindigkeit = Vector2.Reflect(asteroid2.geschwindigkeit, richtung2);
            asteroid2.geschwindigkeit += zentrumDerMasse;
        }

        /// <summary>
        /// Funktion ist momentan unnötig.
        /// Muss angepasst werden, damit sie die neuerstellung von Asteroiden am Bildschirmrand übernimmt.
        /// </summary>
        /// <param name="objektRechteck"></param>
        /// <returns></returns>
        protected Vector2 findeFreieZufallspositionAmRand(Rectangle objektRechteck)
        {
            Vector2 zufallsPosition = Vector2.Zero;
            int zaehler = 0;
            bool positionIstFrei;

            //temp zur Überprüfung
            List<Vector2> positionen = new List<Vector2>();

            do
            {
                positionIstFrei = true;
                zufallsPosition = Helferklasse.gibNeuePosition(objektRechteck);
                
                //temp zur Überprüfung
                positionen.Add(zufallsPosition);

                for (int i = 0; i < _asteroiden.Count; i++)
                {
                    if (_asteroiden[i].IstKreisKollision(zufallsPosition, (int)MathHelper.Max(objektRechteck.X, objektRechteck.Y)))
                    {
                        positionIstFrei = false;
                        zufallsPosition = weltMitte;
                        break;
                    }
                }
                
                zaehler++;
            } while (positionIstFrei && zaehler < 10);

            return zufallsPosition;
        }


        protected void erstelleAsteroid()
        {
            int zufallsZahl = rand.Next(1, 11);//zufallszahl zwischen 1 und 11
            Asteroid neuerAsteroid;
            int seitenLaenge;
            float skalierung;

            if (zufallsZahl >= 1 && zufallsZahl <= 4)
            {
                seitenLaenge = 20;
                skalierung = 0.125f;

            }
            else if (zufallsZahl >= 5 && zufallsZahl <= 7)
            {
                seitenLaenge = 40;
                skalierung = 0.25f;
            }
            else if (zufallsZahl == 8 || zufallsZahl == 9)
            {
                seitenLaenge = 80;
                skalierung = 0.5f;
            }
            else
            {
                seitenLaenge = 160;
                skalierung = 1f;
            }


            neuerAsteroid = new Asteroid(
                weltMitte, Vector2.Zero, seitenLaenge, seitenLaenge,
                Helferklasse.zufallsRotation(),
                seitenLaenge/2,
                true,
                (float)Math.Pow(seitenLaenge, 2),
                0.31f,
                skalierung,
                "Asteroid" + rand.Next(1, 3 + 1).ToString() + "_fliegt");

            _asteroiden.Add(neuerAsteroid);
        }
        #endregion


        #region Update und Draw

        public virtual void Update(GameTime gameTime)
        {
            if (_feldFlaeche.Intersects(Kamera.sichtfeld))
            {
                foreach (Asteroid asteroid in _asteroiden)
                {
                    asteroid.Update(gameTime);

                    //kann erst verwendet werden, wenn die zufallszahl stimmt
                    //checkInterneKollisionen();//prüft alle Kollisionen


                    if (!Kamera.IstObjektSichtbar(asteroid.objektRechteck))
                    {
                        Vector2 neuePosition = findeFreieZufallspositionAmRand(asteroid.objektRechteck);

                        if (_feldFlaeche.Intersects(new Rectangle((int)neuePosition.X, (int)neuePosition.Y, 0, 0)))
                        {
                            asteroid.weltpositionAendern = neuePosition;
                            asteroid.geschwindigkeit = Helferklasse.zufallsGeschwindigkeit(_minGeschwindigkeit, _maxGeschwindigkeit);
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spirteBatch)
        {
            foreach (SpielObjekt asteroid in _asteroiden)
                asteroid.Draw(spirteBatch);
        }
        #endregion
    }
}
*/