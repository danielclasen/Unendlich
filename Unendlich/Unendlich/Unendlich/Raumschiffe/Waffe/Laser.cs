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
    /// Energieverbrauch: 1
    /// Schussgeschwindigkeit: 800
    /// Mündung: (6,7) und (6,-7)
    /// Schaden: 10
    /// Minimale Schusszeit: 0.1
    /// </summary>
    public class Laser : BasisWaffe
    {

        #region Konstruktor

        public Laser(Raumschiff schiff, Vector2 positionAufSchiff)
           : base(schiff, 32, 34, 1f, 800f, positionAufSchiff, new Vector2(6, -7), 10, 0.1f, "Laser_inaktiv", "Laser_schuss")
        {
            AnimationsStreifen neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("Laser_aktiv"), 32, "Laser_aktiv", 0.02f, false);
            neuerAnimationsstreifen.naechsteAnimation = "Laser_inaktiv";
            AnimationHinzufuegen("Laser_aktiv", neuerAnimationsstreifen);

            neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("Laser_inaktiv"), 32, "Laser_inaktiv", 0.2f, true);
            AnimationHinzufuegen("Laser_inaktiv", neuerAnimationsstreifen);

            neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("Laser_schuss"), 7, "Laser_schuss", 2.0f, true);
            AnimationHinzufuegen("Laser_schuss", neuerAnimationsstreifen);
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
