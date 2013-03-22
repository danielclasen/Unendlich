using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Schuss : Partikel
    {
        #region Deklaration

        protected int _schaden;
        #endregion


        #region Eigenschaften

        public int schaden
        {
            get { return _schaden; }
        }
        #endregion


        #region Konstruktor

        public Schuss(
            Vector2 startposition,
            Vector2 richtung,
            Vector2 beschleunigung,
            int breite,
            int hoehe,
            string aktuelleAnimation,
            float hoechstGeschwindigkeit,
            float lebensZeit,
            int schaden)
            :base(startposition,richtung,beschleunigung,breite,hoehe,0.39f,aktuelleAnimation,hoechstGeschwindigkeit,lebensZeit,Color.White,Color.White)
        {
            AnimationHinzufuegen(aktuelleAnimation, new AnimationsStreifen(Containerklasse.GebeTexture(aktuelleAnimation), 7, aktuelleAnimation));

            _schaden = schaden;
        }
        #endregion


        #region Helfermethoden

        public void HatGetroffen()
        {
            _istAktiv = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (_restZeit <= 0)
                _istAktiv = false;

            if (istAktiv)
            {
                base.Update(gameTime);
                rotiereZu(_geschwindigkeit);
                _restZeit -= (float)gameTime.ElapsedGameTime.TotalSeconds*_geschwindigkeit.Length();
            }
        }

        #endregion
    }
}
