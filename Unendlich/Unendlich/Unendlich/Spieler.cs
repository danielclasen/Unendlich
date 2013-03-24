/*Steuerung muss überarbeitet werden
 * 
 * 
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Unendlich
{
    public class Spieler
    {
        #region Deklaration
        
        protected Raumschiff _aktuellesSchiff;

        protected float _eingabeVerzoegerung=0.02f;
        protected float _letzteEingabe = 0.0f;
        #endregion


        #region Eigenschaften

        public Raumschiff aktuellesSchiff
        {
            get { return _aktuellesSchiff; }
        }

        public Vector2 geschwindigkeit
        {
            get { return _aktuellesSchiff.geschwindigkeit; }
        }

        public Vector2 weltposition
        {
            get { return _aktuellesSchiff.weltposition; }
        }

        public Vector2 weltmittelpunkt
        {
            get { return _aktuellesSchiff.weltMittelpunkt; }
        }

        public float rotation
        {
            get { return _aktuellesSchiff.rotation; }
        }

        public float kollisionsRadius
        {
            get { return _aktuellesSchiff.kollisionsRadius; }
        }
        #endregion


        #region Konstruktor

        public Spieler()
        {
            //Auswahl des Anfangsraumschiffes
            _aktuellesSchiff = new KleinerJaeger(Vector2.Zero, Vector2.Zero, Raumschiff.Fraktion.spieler1);
        }
        #endregion


        #region Spielereingabe

        protected Vector2 TastaturEingabe(KeyboardState tastatur)
        {
            Vector2 richtungsAnderung= Vector2.Zero;

            if (tastatur.IsKeyDown(Keys.W))
                richtungsAnderung.Y = -1;
            else if (tastatur.IsKeyDown(Keys.S))
                richtungsAnderung.Y = 1;
            
            if (tastatur.IsKeyDown(Keys.A))
                richtungsAnderung.X = -1;
            else if (tastatur.IsKeyDown(Keys.D))
                richtungsAnderung.X = 1;
            
            if(tastatur.IsKeyDown(Keys.Space))
                _aktuellesSchiff.BefehlZumFeuern();

            return richtungsAnderung;
        }

        protected Vector2 GamePadEingabe(GamePadState gamepad)
        {
            if (gamepad.IsButtonDown(Buttons.A))
                _aktuellesSchiff.BefehlZumFeuern();

            return new Vector2(gamepad.ThumbSticks.Left.X, -gamepad.ThumbSticks.Left.Y);
        }

        protected void EingabeVerarbeiten()
        {
            Vector2 neueRichtung=Vector2.Zero;

            neueRichtung = TastaturEingabe(Keyboard.GetState());
            neueRichtung+=GamePadEingabe(GamePad.GetState(PlayerIndex.One));

            _aktuellesSchiff.GeschwindigkeitAendern(neueRichtung);
        }
        #endregion


        #region Update und Draw

        public void Update(GameTime gameTime)
        {
            float vergangen = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            EingabeVerarbeiten();
            
            _aktuellesSchiff.Update(gameTime);  //aktuallisiert unter anderem auch die Position in GrafikObjekt

            Kamera.geschwindigkeit = _aktuellesSchiff.geschwindigkeit;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _aktuellesSchiff.Draw(spriteBatch);
        }
        #endregion
    }
}
