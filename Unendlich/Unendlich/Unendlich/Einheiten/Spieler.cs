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
    public class Spieler:Einheit
    {
        #region Deklaration
        
        protected float _eingabeVerzoegerung=0.02f;
        protected float _letzteEingabe = 0.0f;
        #endregion



        #region Konstruktor

        public Spieler()
            : base(new GrosserJaeger(Vector2.Zero), Fraktion.spieler1)
        { }
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


        #region Update

        public override void Update(GameTime gameTime)
        {
            EingabeVerarbeiten();
            base.Update(gameTime);

            Kamera.geschwindigkeit = _aktuellesSchiff.geschwindigkeit;
        }
        #endregion
    }
}
