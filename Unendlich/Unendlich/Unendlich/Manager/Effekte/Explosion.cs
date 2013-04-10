using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public class Explosion : Partikel
    {

        #region Konstruktor

        public Explosion(Vector2 position, Vector2 geschwindigkeit, int breite, int hoehe, float lebensDauer)
            : base(position, geschwindigkeit, 0, breite, hoehe, 0.21f, "Explosion_aktiv", geschwindigkeit.Length(), lebensDauer, Color.White, Color.White)
        {
            AnimationHinzufuegen("Explosion_aktiv", new AnimationsStreifen(Containerklasse.GebeTexture("Explosion_aktiv"), 20, "Explosion_aktiv", 0.2f));
            StarteAnimationVonZufall(_aktuelleAnimation);
        }
        #endregion
    }
}
