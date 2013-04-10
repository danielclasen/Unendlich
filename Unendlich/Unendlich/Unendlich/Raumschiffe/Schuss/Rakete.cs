using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public class Rakete : Schuss
    {
        public Rakete(
            Vector2 startposition,
            Vector2 richtung,
            float beschleunigung,
            int breite,
            int hoehe,
            string aktuelleAnimation,
            float hoechstGeschwindigkeit,
            float lebensZeit,
            int schaden)
            : base(startposition, richtung, beschleunigung, breite, hoehe, aktuelleAnimation, hoechstGeschwindigkeit, lebensZeit, schaden)
        {
        }
    }
}
