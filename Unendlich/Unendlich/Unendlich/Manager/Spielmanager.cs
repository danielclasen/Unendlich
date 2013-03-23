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
        private static Spieler _spieler;
        private static List<Gegnermanager> _alleFraktionen;

        public static void Init(Spieler spieler)
        {
            Helferklasse.Init();

            _spieler = spieler;
            
            _alleFraktionen = new List<Gegnermanager>();
            _alleFraktionen.Add(new Gegnermanager());

            Anzeige.Init(spieler);
            KI.Init(_spieler,_alleFraktionen);
            Kollisionsmanager.Init(spieler, _alleFraktionen);           
            
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
            _spieler.Update(gameTime);
            
            foreach(Gegnermanager fraktion in _alleFraktionen)
                fraktion.Update(gameTime);

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
            _spieler.Draw(spriteBatch);

            foreach (Gegnermanager fraktion in _alleFraktionen)
            {
                fraktion.Draw(spriteBatch);
            }

            Effektmanager.Draw(spriteBatch);
            Anzeige.Draw(spriteBatch);
        }
        #endregion

    }
}
