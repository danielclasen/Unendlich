using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class BSchild : BasisSchild
    {
        /*
         * Klasse BSchild
         * 
         * Energieverbrauch des Schildes: 0.2
         * Schild Effezienz pro 1/15 Flächeneinheiten: 0.15
         * Zeit bis Regeneration beginnt (in sek): 2.5s
         * 
         * */

        public BSchild(Raumschiff schiff)
            : base(schiff, 0.2f, 0.15f, 2.5f)
        { }
    }
}