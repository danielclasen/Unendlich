using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public static class Anzeige
    {
        #region Deklaration

        private static Spieler _spieler;
        private static Texture2D _hintergrund;
        private static Texture2D _statusBalken;
        private static Vector2 _position;
        private static SpriteFont _schrift;
        #endregion


        #region Eigenschaften

        public static Rectangle objektRechteck
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _hintergrund.Width, _hintergrund.Height); }
        }

        #endregion


        #region Init

        public static void Init(Spieler spieler)
        {
            _hintergrund = Containerklasse.GebeTexture("Interface_Hintergrund");
            _statusBalken = Containerklasse.GebeTexture("Interface_Statusbalken");
            _spieler = spieler;
            _position = new Vector2(Kamera.sichtfeldBreite - _hintergrund.Width, Kamera.sichtfeldHoehe - _hintergrund.Height);
            _schrift = Containerklasse.GebeSchrift("kootenay12");
        }
        #endregion


        #region Draw

        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawHintergrund(spriteBatch);
            DrawHpBalken(spriteBatch);
            DrawSchildBalken(spriteBatch);
            DrawPosition(spriteBatch);
            
        }

        private static void DrawHintergrund(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _hintergrund,
                objektRechteck,
                new Rectangle(0, 0, _hintergrund.Width, _hintergrund.Height),
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.12f);
        }

        private static void DrawHpBalken(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _statusBalken,
                new Rectangle(
                    (int)_position.X + 30,
                    (int)_position.Y + 67 + _statusBalken.Height - (int)(_statusBalken.Height * _spieler.aktuellesSchiff.hpRestProzentual),
                    _statusBalken.Width,
                    (int)(_statusBalken.Height * _spieler.aktuellesSchiff.hpRestProzentual)),
                new Rectangle(
                    0,
                    (int)(_statusBalken.Height - _statusBalken.Height * _spieler.aktuellesSchiff.hpRestProzentual),
                    (int)_statusBalken.Width,
                    (int)(_statusBalken.Height * _spieler.aktuellesSchiff.hpRestProzentual)),
                Color.Red,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.11f);
        }

        private static void DrawSchildBalken(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _statusBalken,
                new Rectangle(
                    (int)_position.X + 52,
                    (int)_position.Y + 67 + _statusBalken.Height - (int)(_statusBalken.Height * _spieler.aktuellesSchiff.schildRestProzentual),
                    _statusBalken.Width,
                    (int)(_statusBalken.Height * _spieler.aktuellesSchiff.schildRestProzentual)),
                new Rectangle(
                    0,
                    (int)(_statusBalken.Height - _statusBalken.Height * _spieler.aktuellesSchiff.schildRestProzentual),
                    (int)_statusBalken.Width,
                    (int)(_statusBalken.Height * _spieler.aktuellesSchiff.hpRestProzentual)),
                Color.Blue,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.11f);
        }

        private static void DrawPosition(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_schrift, "X", _position+ new Vector2(128, 53), Color.Black,0f,Vector2.Zero,1f,SpriteEffects.None,0.1f);
            spriteBatch.DrawString(
                _schrift,
                ((int)_spieler.weltmittelpunkt.X).ToString(),
                _position + new Vector2(149, 53),
                Color.Black, 0f,
                Vector2.Zero, 
                1f, 
                SpriteEffects.None,
                0.1f);

            spriteBatch.DrawString(_schrift, "Y", _position + new Vector2(128, 74), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            spriteBatch.DrawString(
                _schrift,
                ((int)_spieler.weltmittelpunkt.Y).ToString(),
                _position + new Vector2(149, 74),
                Color.Black, 0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0.1f);
        }
        #endregion
    }
}
