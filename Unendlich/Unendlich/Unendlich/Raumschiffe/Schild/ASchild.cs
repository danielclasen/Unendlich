using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class ASchild : BasisSchild
    {
        /*
         * Klasse ASchild
         * 
         * Energieverbrauch des Schildes: 0.1
         * Schild Effezienz pro 1/10 Flächeneinheiten: 0.1
         * Zeit bis Regeneration beginnt (in sek): 2s
         * 
         * */

        public ASchild(Raumschiff schiff)
            : base(schiff, 0.1f, 0.1f, 2)
        { }
    }
}
