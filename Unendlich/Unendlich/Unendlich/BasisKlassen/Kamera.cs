/*
 * Größtenteils komplett:
 *      -wenn gezoomt werden soll muss die Darstellung und Berechnung für den Screen angepasst werden 
 * 
 * 
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{

    public static class Kamera
    {
        #region Deklaration
        private static Vector2 _position = Vector2.Zero;
        private static Vector2 _sichtfeldGroesse = Vector2.Zero;
        private static Rectangle _weltGroesse = new Rectangle(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);
        private static Vector2 _geschwindigkeit=Vector2.Zero;

        public static float vergroesserung = 1;//wird nachher zum Skalieren verwendet
        #endregion


        #region Eigenschaften
        public static Vector2 position
        {
            get { return _position; }
            set
            {
                _position = new Vector2(
                    MathHelper.Clamp(value.X,
                        _weltGroesse.X, _weltGroesse.Width -
                        sichtfeldBreite),
                    MathHelper.Clamp(value.Y,
                        _weltGroesse.Y, _weltGroesse.Height -
                        sichtfeldHoehe));
            }
        }

        public static Rectangle weltGroesse
        {
            get { return _weltGroesse; }
            set { _weltGroesse = value; }
        }

        public static int sichtfeldBreite
        {
            get { return (int)_sichtfeldGroesse.X; }
            set { _sichtfeldGroesse.X = value; }
        }

        public static int sichtfeldHoehe
        {
            get { return (int)_sichtfeldGroesse.Y; }
            set { _sichtfeldGroesse.Y = value; }
        }

        public static Rectangle sichtfeld
        {
            get
            {
                return new Rectangle(
                    (int)_position.X, (int)_position.Y,
                    sichtfeldBreite, sichtfeldHoehe);
            }
        }

        public static Vector2 sichtfeldMitte
        {
            get { return new Vector2(position.X - sichtfeldBreite / 2, position.Y - sichtfeldHoehe / 2); }
        }
        #endregion


        #region Öffentliche Methoden
        public static void Bewegen(Vector2 abstand)
        {
            _position += abstand;
        }

        public static Vector2 geschwindigkeit
        {
            get { return _geschwindigkeit; }
            set { _geschwindigkeit = value; }
        }


        public static void ZentrumSetzen(int zentrumX, int zentrumY)
        {
            _position.X = zentrumX - _sichtfeldGroesse.X / 2;
            _position.Y = zentrumY - _sichtfeldGroesse.Y / 2;
        }

        public static void ZentrumSetzen(Vector2 neuesZentrum)
        {
            ZentrumSetzen((int)neuesZentrum.X, (int)neuesZentrum.Y);
        }

        public static bool IstObjektSichtbar(Rectangle objekt)
        {
            return (sichtfeld.Intersects(objekt));
        }

        public static Vector2 WeltAufScreen(Vector2 weltPosition)//des Objekts
        {
            return weltPosition - _position;
        }


        public static Rectangle WeltAufScreen(Rectangle weltRechteck)//des Objekts
        {
            return new Rectangle(
                weltRechteck.Left - (int)position.X,
                weltRechteck.Top - (int)position.Y,
                weltRechteck.Width,
                weltRechteck.Height);
        }

        public static Vector2 ScreenAufWelt(Vector2 screenPosition)//des Objekts
        {
            return screenPosition + _position;
        }

        public static Rectangle ScreenAufWelt(Rectangle sichtfeld)//des Objekts
        {
            return new Rectangle(
                sichtfeld.Left + (int)_position.X,
                sichtfeld.Top + (int)_position.Y,
                sichtfeld.Width,
                sichtfeld.Height);
        }
        #endregion


        #region Init

        public static void Init(Vector2 mittelpunkt)
        {
            ZentrumSetzen(mittelpunkt);
        }
        #endregion


        #region Update

        public static void Update(GameTime gameTime)
        {
            position += geschwindigkeit*(float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        #endregion
    }
}
