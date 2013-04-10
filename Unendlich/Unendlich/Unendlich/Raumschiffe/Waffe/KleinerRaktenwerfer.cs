using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Unendlich
{
    /// <summary>
    /// Breite: 32
    /// Höhe: 34
    /// Energieverbrauch: 800
    /// Schussgeschwindigkeit: 1000
    /// Mündung: (3/3) und (3/-3) und (3/7) und (3/-7)
    /// Schaden: 50
    /// Minimale Schusszeit: 1
    /// 
    /// Besonderheit: Energieverbrauch bezieht sich auf Schussrate
    /// </summary>
    public class KleinerRaktenwerfer : BasisWaffe
    {
        #region Konstruktor

        public KleinerRaktenwerfer(Raumschiff schiff, Vector2 positionAufSchiff)
            : base(schiff, 32, 34, 1f, 1000f, positionAufSchiff, new Vector2(3, 3), 50, 1f, "KleinerRaktenwerfer_inaktiv", "KleinerRaktenwerfer_inaktiv")
        {
            AnimationsStreifen neuerAnimationsstreifen = new AnimationsStreifen(Containerklasse.GebeTexture("KleinerRaktenwerfer_inaktiv"), 32, "KleinerRaktenwerfer_inaktiv", 1f, true);
            AnimationHinzufuegen("KleinerRaktenwerfer_inaktiv", neuerAnimationsstreifen);
        }
        #endregion


        #region Helfermethoden

        public override void Schiessen()
        {
            
        }

        public override bool IstObjektImZiel(Einheit andereEinheit)
        {
            if (IstInReichweite(andereEinheit))
                return true;
            else
                return false;
        }
        #endregion
    }
}
