using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    /// <summary>
    /// Breite: 32
    /// Höhe: 34
    /// Energieverbrauch: 0.1
    /// Schussgeschwindigkeit: 1600
    /// Mündung: (6,7) und (6,-7)
    /// Schaden: 2
    /// Minimale Schusszeit: 0.05
    /// </summary>
    public class MG : BasisWaffe
    {

        #region Konstruktor

        public MG(Raumschiff schiff, Vector2 positionAufSchiff)
            : base(schiff, 32, 34, 1.0f, 1600f, positionAufSchiff, new Vector2(6, -7), 2, 0.05f, "MG_inaktiv", "MG_schuss")
        {
            AnimationsStreifen neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("MG_aktiv"), 32, "MG_aktiv", 0.02f, false);
            neuerAnimationsstreifen.naechsteAnimation = "MG_inaktiv";
            AnimationHinzufuegen("MG_aktiv", neuerAnimationsstreifen);

            neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("MG_inaktiv"), 32, "MG_inaktiv", 0.2f, true);
            AnimationHinzufuegen("MG_inaktiv", neuerAnimationsstreifen);

            neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("MG_schuss"), 3, "MG_schuss", 2.0f, true);
            AnimationHinzufuegen("MG_schuss", neuerAnimationsstreifen);
        }
        #endregion


        #region Öffentliche Methoden

        /// <summary>
        /// Errechnet die beiden Mündungen des Laiser und lässt je Mündung einen Schuss im Schussmanager erzeugen
        /// </summary>
        /// <param name="schiffsMitte"></param>
        /// <param name="energie"></param>
        public override void Schiessen()
        {
            base.Schiessen(positionMuendung);
            base.Schiessen(new Vector2(positionMuendung.X, -positionMuendung.Y));
        }

        #endregion
    }
}
