using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    /// <summary>
    /// Beite: 40
    /// Höhe: 20
    /// Hüllenlebenspunkte: 80
    /// Energie: 400
    /// Höchstgeschwindigkeit: 800
    /// Beschleunigung: 20
    /// Standardwaffe: MG
    /// </summary>
    public class Spaeher : Raumschiff
    {

        #region Init

        protected override void InitKomponenten()
        {
            _waffen.Add(new MG(this, new Vector2(-11, -7)));
            _schild = new BSchild(this);
        }
        #endregion


        #region Konstruktor

        public Spaeher(Vector2 position, Fraktion fraktion)
            : this(position, Vector2.Zero, fraktion)
        { }

        public Spaeher(Vector2 position, Vector2 geschwindigkeit, Fraktion fraktion)
            : base(position, 40, 20, 80, 400, 800, 20f, fraktion, "Spaeher_fliegen")
        {
            AnimationHinzufuegen("Spaeher_fliegen", new AnimationsStreifen(Containerklasse.GebeTexture("Spaeher_fliegen"), 40, "Spaeher_fliegen", 0.3f, true));

            _aktuelleAnimation = "Spaeher_fliegen";
            SetzeGeschwindigkeit(geschwindigkeit);
        }
        #endregion
    }
}
