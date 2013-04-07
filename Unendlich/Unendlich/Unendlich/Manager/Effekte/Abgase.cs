﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Abgase:Partikel
    {
        public Abgase(Vector2 position, Vector2 geschwindigkeit):
            base(position,
            geschwindigkeit,
            Vector2.Zero,
            1,
            1,
            0.21f,
            "WeiserPixel",
            geschwindigkeit.Length(),
            0.05f,
            Color.Yellow,
            new Color(255,0,0,0))
        {
            StarteAnimationVonAnfang(_aktuelleAnimation);
        }
    }
}
