using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public class Partikel : SpielObjekt
    {
        #region Deklaration

        protected float _lebensZeit;
        protected float _restZeit;

        protected Vector2 _beschleunigung;
        protected float _hoechstGeschwindigkeit;
        protected Color _anfangsFarbe;
        protected Color _endFarbe;
        #endregion


        #region Eigenschaften

        public float vergangeneZeit
        {
            get { return _lebensZeit - _restZeit; }
        }

        public float prozentualeRestzeit
        {
            get { return (float)vergangeneZeit / (float)_lebensZeit; }
        }


        /// <summary>
        /// Prüft ob die Geschwindigkeit größer als die Höchstgeschwindigkeit ist und passt diese gegebenfalls an
        /// </summary>
        public override Vector2 geschwindigkeit
        {
            get { return base.geschwindigkeit; }
            set
            {
                if (value.Length() > _hoechstGeschwindigkeit)
                {
                    value.Normalize();
                    value *= _hoechstGeschwindigkeit;
                }

                base.geschwindigkeit = value;
            }
        }
        #endregion


        #region Konstruktor

        public Partikel(
            Vector2 startposition,
            Vector2 geschwindigkeit,
            Vector2 beschleunigung,
            int breite,
            int hoehe,
            float malTiefe,
            string aktuelleAnimation,
            float hoechstGeschwindigkeit,
            float lebensZeit,
            Color anfangsFarbe,
            Color endFarbe)
            : base(startposition,geschwindigkeit,breite,hoehe,malTiefe,aktuelleAnimation)
        {
            _lebensZeit = lebensZeit;
            _restZeit = lebensZeit;
            _beschleunigung = beschleunigung;
            _hoechstGeschwindigkeit = hoechstGeschwindigkeit;
            _anfangsFarbe = anfangsFarbe;
            _endFarbe = endFarbe;

            AnimationHinzufuegen("WeiserPixel", new AnimationsStreifen(Containerklasse.GebeTexture("WeiserPixel"), 1, "WeiserPixel"));
        }

        #endregion


        #region Update and Draw

        public override void Update(GameTime gameTime)
        {

            if (_restZeit <= 0)
                _istAktiv = false;

            if (istAktiv)
            {
                geschwindigkeit += _beschleunigung;

                _farbe = Color.Lerp(_anfangsFarbe, _endFarbe, prozentualeRestzeit);
            }

            _restZeit -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
        #endregion
    }
}
