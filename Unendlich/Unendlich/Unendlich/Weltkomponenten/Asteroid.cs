/*
 * Konstrukor stimmt noch nicht:
 *      Asteroiden drehen sich immer in die selbe Richtung
 * 
 * */
/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Asteroid:SpielObjekt
    {
        protected float _standardRotation;

        public Asteroid(Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float rotation,
            float kollisionsRadius,
            bool istRammbar,
            float masse,
            float malTiefe,
            float skalierung,
            string aktuelleAnimation):base(position,geschwindigkeit,breite,hoehe,rotation,kollisionsRadius,istRammbar,masse,malTiefe,skalierung,aktuelleAnimation)
        {
            string animationsName;

            for (int i = 1; i <= 3; i++)
            {
                animationsName = "Asteroid" + i.ToString() + "_fliegt";
                AnimationHinzufuegen(animationsName, new AnimationsStreifen(Containerklasse.GebeTexture(animationsName), 160, animationsName, 0.2f));
            }

            _standardRotation = Helferklasse.zufallsRotation(1, 5);

            //keine dauerlösung
            rand = new Random(Helferklasse.rand.Next(0, 1000));//da ansonsten immer der selbe zufallswert zurück gegeben wird

            if (rand.Next(0, 2) == 0)// 50:50 Chance, dass in welche Richtung sich ein Asteroid dreht
                _standardRotation *= -1;
        }

        public override void Update(GameTime gameTime)
        {
            rotation += _standardRotation;
            base.Update(gameTime);
        }
    }
}
*/