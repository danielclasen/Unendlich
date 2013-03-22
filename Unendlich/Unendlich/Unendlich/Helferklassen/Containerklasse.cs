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
    /// Speichert alle Texturen
    /// </summary>
    public static class Containerklasse
    {
        private static Dictionary<string, Texture2D> texturen = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> schriften = new Dictionary<string, SpriteFont>();


        #region Lade Inhalte

        public static void LadeAlleSchriften(ContentManager content)
        {
            //Lade alle Schriften
            string pfad=@"Fonts\";

            LadeSchrift(pfad, "pericles14", content);
            LadeSchrift(pfad, "kootenay12", content);
        }

        public static void LadeAlleTexturen(ContentManager content)
        {
            //Lade Partikel
            string pfad = @"Texturen\";
            string texturName = "WeiserPixel";
            LadeTextur(pfad, texturName, content);

            //Lade Effekt
            pfad = @"Texturen\Effekte\";
            texturName = "Explosion_aktiv";
            LadeTextur(pfad, texturName, content);

            texturName = "SchildTreffer_aktiv";
            LadeTextur(pfad, texturName, content);

            //Lade Raumschiffe
            pfad = @"Texturen\Raumschiffe\KleinerJaeger\";
            texturName = "KleinerJaeger_fliegen";
            LadeTextur(pfad, texturName, content);

            pfad = @"Texturen\Raumschiffe\GrosserJaeger\";
            texturName = "GrosserJaeger_fliegen";
            LadeTextur(pfad, texturName, content);

            pfad = @"Texturen\Raumschiffe\Spaeher\";
            texturName = "Spaeher_fliegen";
            LadeTextur(pfad, texturName, content);

             //Lade Waffen
            pfad = @"Texturen\Raumschiffe\Waffen\Laser\";
            texturName = "Laser_aktiv";
            LadeTextur(pfad, texturName, content);

            texturName = "Laser_inaktiv";
            LadeTextur(pfad, texturName, content);

            texturName = "Laser_schuss";
            LadeTextur(pfad, texturName, content);

            pfad = @"Texturen\Raumschiffe\Waffen\MG\";
            texturName = "MG_aktiv";
            LadeTextur(pfad, texturName, content);

            texturName = "MG_inaktiv";
            LadeTextur(pfad, texturName, content);

            texturName = "MG_schuss";
            LadeTextur(pfad, texturName, content);

            //Lade Sternenhimmel
            pfad = @"Texturen/Sternenhimmel/";
            
            for (int i = 1; i <= 3; i++)
            {
                texturName = "Stern"+i.ToString();
                LadeTextur(pfad, texturName, content);
            }

            //Lade Asteroiden
            pfad = @"Texturen\Weltraumobjekte\";

            for (int i = 1; i <= 3; i++)
            {
                texturName = "Asteroid" + i.ToString() + "_fliegt";
                LadeTextur(pfad, texturName, content);
            }

            //Lade Interface
            pfad = @"Texturen\Interface\";

            texturName = "Interface_Hintergrund";
            LadeTextur(pfad, texturName, content);

            texturName = "Interface_Statusbalken";
            LadeTextur(pfad, texturName, content);
        }
        #endregion


        #region Helfermethoden

        private static void LadeTextur(string pfad, string texturName, ContentManager content)
        {
            Texture2D neueTextur = content.Load<Texture2D>(pfad + texturName);
            texturen.Add(texturName, neueTextur);
        }

        private static void LadeSchrift(string pfad, string schriftName, ContentManager content)
        {
            SpriteFont neueSchrift = content.Load<SpriteFont>(pfad + schriftName);
            schriften.Add(schriftName, neueSchrift);
        }

        #endregion


        #region Öffentliche Mtehoden

        /// <summary>
        /// Gibt die Textur mit entsprechendem Namen zurück
        /// </summary>
        /// <param name="texturName"></param>
        /// <returns></returns>
        public static Texture2D GebeTexture(string texturName)
        {
            if (texturen.ContainsKey(texturName))
                return texturen[texturName];
            else
                return null;
        }

        public static SpriteFont GebeSchrift(string schriftName)
        {
            if (schriften.ContainsKey(schriftName))
                return schriften[schriftName];
            else
                return null;
        }
        #endregion
    }
}
