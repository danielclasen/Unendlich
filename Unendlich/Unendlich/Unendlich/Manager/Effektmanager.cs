/*Doku:
 * 
 * Explosionen sind nich optimal, aber ok.
 * 
 * Fehlende Effekte:
 *      Treffer beim Schuss
 *      Antrieb
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public static class Effektmanager
    {
        #region Deklaration

        static private List<Partikel> _effekte;
        static private List<Explosion> _explosionen;
        static private List<Abgase> _abgase;
        #endregion


        #region Initialisierung

        public static void Init()
        {
            _effekte = new List<Partikel>();
            _explosionen = new List<Explosion>();
            _abgase = new List<Abgase>();
        }
        #endregion


        #region Öffentliche Methoden

        public static void HinzufuegenSchildeffekt(Raumschiff getroffenesSchiff, Schuss schuss)
        {
            _effekte.Add(new Schildtreffer(getroffenesSchiff,schuss));
        }

        public static void HinzufuegenAbgaseffekt(Raumschiff raumschiff)
        {
            int anzahlEffekte = Helferklasse.rand.Next(10, 15);

            for (int i = 0; i < anzahlEffekte; i++)
            {
                _effekte.Add(new Abgase(raumschiff.weltMittelpunkt, Helferklasse.ZufallsGeschwindigkeit(15, 20)));
            }
        }

        public static void HinzufuegenExplosion(
            Vector2 position,
            Vector2 objektGeschwindigkeit,
            int minEffekZahl,
            int maxEffekZahl,
            int minExplosionenZahl,
            int maxExplosionenZahl,
            float beschleunigung,
            float minDauer,
            float maxDauer,
            Color anfangsFarbe,
            Color endFarbe)
        {
            float explosionHoechstgeschwindigkeit = 200f;
            int minEffektGeschwindigkeit = (int)beschleunigung * 2;
            int maxEffektGeschwindigkeit = (int)beschleunigung * 5;

            int anzahlEffekte =  Helferklasse.rand.Next(minEffekZahl, maxEffekZahl + 1);

            Vector2 effektRichtung = Vector2.Zero;
            Vector2 effektBeschleunigung = Vector2.Zero;

            for (int x = 0; x < anzahlEffekte; x++)
            {
                effektRichtung = Helferklasse.ZufallsGeschwindigkeit(minEffektGeschwindigkeit, maxEffektGeschwindigkeit) + objektGeschwindigkeit / 3;
                effektBeschleunigung = effektRichtung;
                effektBeschleunigung.Normalize();
                effektBeschleunigung *= beschleunigung;

                _effekte.Add(new Partikel(
                    ZufallsPosition(position, 10),
                    effektRichtung,
                    effektBeschleunigung,
                    1,
                    1,
                    0.20f,
                    "WeiserPixel",
                    explosionHoechstgeschwindigkeit,
                    ZufallsZahlUnter1(minDauer,maxDauer),
                    anfangsFarbe,
                    endFarbe));
            }

            int anzahlExplosionen = Helferklasse.rand.Next(minExplosionenZahl, maxExplosionenZahl);

            for (int i = 0; i < anzahlExplosionen; i++)
            {
                Vector2 explosionsGeschwindigkeit =  Helferklasse.GibZufallsrichtung();
                 _explosionen.Add(new Explosion(ZufallsPosition(position,10), explosionsGeschwindigkeit+objektGeschwindigkeit/2, 20, 20, ZufallsZahlUnter1(minDauer,maxDauer)));
                _explosionen[_explosionen.Count - 1]._aktuelleAnimation = "Explosion_aktiv";
            }

        }

        public static void HinzufuegenExplosion(Vector2 position, Vector2 objektGeschwindigkeit)
        {
            HinzufuegenExplosion(
                position,
                objektGeschwindigkeit,
                50,
                80,
                20,
                30,
                5.0f,
                0.5f,
                1f,
                new Color(1.0f, 0.3f, 0f, 0.5f),
                new Color(1.0f, 0.3f, 0f, 0f));
        }
        
        private static float ZufallsZahlUnter1(float min, float max)
        {
            return (float)(Helferklasse.rand.Next((int)min * 100, (int)max * 100 + 1)) / 100;
        }

        private static Vector2 ZufallsPosition(Vector2 ursprungsPosition, int maxAbweichung)
        {
            return new Vector2(Helferklasse.rand.Next((int)ursprungsPosition.X - maxAbweichung, (int)ursprungsPosition.X + maxAbweichung), Helferklasse.rand.Next((int)ursprungsPosition.Y - maxAbweichung, (int)ursprungsPosition.Y + maxAbweichung));
        }

        #endregion


        #region Update und Draw

        static public void Update(GameTime gameTime)
        {
            //Update alle Effekte
            for (int i = _effekte.Count - 1; i >= 0; i--)
            {
                _effekte[i].Update(gameTime);

                if (!_effekte[i].istAktiv)
                    _effekte.RemoveAt(i);
            }

            //Update alle Explosionen
            for (int i = _explosionen.Count - 1; i >= 0; i--)
            {
                _explosionen[i].Update(gameTime);
                if (!_explosionen[i].istAktiv)
                    _explosionen.RemoveAt(i);
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Partikel effekt in _effekte)
                effekt.Draw(spriteBatch);

            foreach (SpielObjekt explosion in _explosionen)
                explosion.Draw(spriteBatch);
        }
        #endregion
    }
}
