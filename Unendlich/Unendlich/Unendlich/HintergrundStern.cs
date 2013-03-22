using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public class HintergrundStern : SpielObjekt
    {
        #region Konstruktor

        public HintergrundStern(Vector2 position, float malTiefe, string aktuelleAnimation)
            : base(position, malTiefe, aktuelleAnimation)
        {
            for (int i = 1; i <= 3; i++)
            {
                AnimationHinzufuegen("Stern" + i.ToString(), new AnimationsStreifen(Containerklasse.GebeTexture("Stern" + i.ToString()), i, "Stern" + i.ToString()));
            }
        }
        #endregion
    }
}
