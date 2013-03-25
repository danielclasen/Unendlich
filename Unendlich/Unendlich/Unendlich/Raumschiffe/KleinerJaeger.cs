using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    /// <summary>
    /// Beite: 32
    /// Höhe: 34
    /// Hüllenlebenspunkte: 100
    /// Energie: 500
    /// Höchstgeschwindigkeit: 400
    /// Beschleunigung: 10
    /// Standardwaffe: Laser
    /// </summary>
    public class KleinerJaeger : Raumschiff
    {
        #region Init

        protected override void InitKomponenten()
        {
            _waffen.Add(new Laser(this, Vector2.Zero));
            _schild = new ASchild(this);
        }
        #endregion


        #region Konstruktor

        public KleinerJaeger(Vector2 position)
            : this(position, Vector2.Zero)
        { }

        public KleinerJaeger(Vector2 position, Vector2 geschwindigkeit)
            : base(position, 32, 34, 100, 500, 400, 10f,"KleinerJaeger_fliegen")
        {
            AnimationHinzufuegen("KleinerJaeger_fliegen", new AnimationsStreifen(Containerklasse.GebeTexture("KleinerJaeger_fliegen"), 32, "KleinerJaeger_fliegen", 0.5f, true));

            _aktuelleAnimation = "KleinerJaeger_fliegen";
            SetzeGeschwindigkeit(geschwindigkeit);
        }
        #endregion
    }
}
