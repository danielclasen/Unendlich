using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    /// <summary>
    /// Beite: 50
    /// Höhe: 50
    /// Hüllenlebenspunkte: 250
    /// Energie: 800
    /// Höchstgeschwindigkeit: 420
    /// Beschleunigung: 11
    /// Standardwaffe: Laser
    /// </summary>
    public class GrosserJaeger : Raumschiff
    {

        #region Init

        protected override void InitKomponenten()
        {
            _waffen.Add(new Laser(this, new Vector2(-8, 8)));
            _schild = new ASchild(this);
        }
        #endregion


        #region Konstruktor

        public GrosserJaeger(Vector2 position, Fraktion fraktion)
            : this(position, Vector2.Zero, fraktion)
        { }

        public GrosserJaeger(Vector2 position, Vector2 geschwindigkeit, Fraktion fraktion)
            : base(position, 50, 50, 250, 800, 420, 11f, fraktion, "GrosserJaeger_fliegen")
        {
            AnimationHinzufuegen("GrosserJaeger_fliegen", new AnimationsStreifen(Containerklasse.GebeTexture("GrosserJaeger_fliegen"), 50, "GrosserJaeger_fliegen", 0.3f, true));

            _aktuelleAnimation = "GrosserJaeger_fliegen";
            SetzeGeschwindigkeit(geschwindigkeit);
        }
        #endregion
    }
}