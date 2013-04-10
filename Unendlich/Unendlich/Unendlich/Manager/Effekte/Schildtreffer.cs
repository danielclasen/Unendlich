using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public class Schildtreffer : Partikel
    {
        public Schildtreffer(Raumschiff getroffenesSchiff, Schuss schuss)
            : base(
            FindePosition(getroffenesSchiff, schuss),
            getroffenesSchiff.geschwindigkeit,
            0,
            10,
            10,
            0.21f,
            "SchildTreffer_aktiv",
            getroffenesSchiff.geschwindigkeit.Length(),
            0.2f,
            StartFarbe(getroffenesSchiff),
            StartFarbe(getroffenesSchiff))
        {
            AnimationHinzufuegen(
                "SchildTreffer_aktiv",
                new AnimationsStreifen(
                    Containerklasse.GebeTexture("SchildTreffer_aktiv"), 
                    20, 
                    "SchildTreffer_aktiv",
                    0.02f));
            
            StarteAnimationVonZufall(_aktuelleAnimation);
        }


        #region Helfermethoden
        //Muss static sein, das Sie im Konstruktor verwendet wird
        protected static Vector2 FindePosition(Raumschiff getroffenesSchiff, Schuss schuss) 
        {
            Vector2 schussPosition = schuss.geschwindigkeit;
            schussPosition.Normalize();
            schussPosition *= -10;

            return schuss.weltMittelpunkt + schussPosition-new Vector2(10,10);
        }

        /// <summary>
        /// Gibt die Farbe an hand der Schildstärke wieder
        /// </summary>
        /// <param name="getroffenesSchiff"></param>
        protected static Color StartFarbe(Raumschiff getroffenesSchiff)
        {
            return Color.Lerp(Color.Red, Color.Green,getroffenesSchiff.schildRestProzentual);//Werte müssen umgedreht werden, schildRest immer kleiner wird
        }
        #endregion

    }

}
