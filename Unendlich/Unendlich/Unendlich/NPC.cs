using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public class NPC:Einheit
    {
        #region Deklaration

        
        #endregion

        #region Konstruktor

        public NPC(Raumschiff aktuellesRaumschiff, Fraktion fraktion)
            : base(aktuellesRaumschiff, fraktion)
        { }
        #endregion


        #region Update
        public override void Update(GameTime gameTime)
        {
            KI.BerechneLogik(this);
            base.Update(gameTime);
        }

        #endregion
    }
}
