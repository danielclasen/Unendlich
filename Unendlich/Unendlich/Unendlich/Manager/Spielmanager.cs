using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public static class Spielmanager
    {
        public static List<Quadrant> weltall;

        public static void Init()
        {
            weltall = new List<Quadrant>();
            weltall.Add(new Quadrant());

            weltall[0].HinzufuegenSpieler(new Spieler());

            Helferklasse.Init();

            Anzeige.Init((Spieler)weltall[0].alleSpieler[0]);
            KI.Init((Spieler)weltall[0].alleSpieler[0]);
            
            Sternenhimmel.Init();
            Effektmanager.Init();
        }


        #region Lade Inhalte

        /// <summary>
        /// Lädt alle Texturen und Animationen in den Zwischenspeicher
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            Containerklasse.LadeAlleTexturen(content);
            Containerklasse.LadeAlleSchriften(content);
        }
        #endregion


        #region Update

        public static void UpdateIngame(GameTime gameTime)
        {
            //temporärer Code
            weltall[0].Update(gameTime);

            Gegnermanager.Update(gameTime);

            Kollisionsmanager.Update(gameTime);
            Sternenhimmel.Update(gameTime);
            Kamera.Update(gameTime);
            Effektmanager.Update(gameTime);
        }
        #endregion


        #region Draw

        public static void DrawIngame(SpriteBatch spriteBatch)
        {
            Sternenhimmel.Draw(spriteBatch);

            weltall[0].Draw(spriteBatch);

            Effektmanager.Draw(spriteBatch);
            Anzeige.Draw(spriteBatch);
        }
        #endregion

    }
}
