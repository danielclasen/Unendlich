/*
 * Random in der statischen Klasse funktioniert nicht richtig
 *      -Problem noch nicht gelöst
 *          -Herr Schardt, haben Sie eine Idee???
 * 
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
    public static class Helferklasse
    {
        #region Deklaration

        public static Random rand;
        #endregion


        #region Konstruktor

        public static void Init()
        {
            rand = new Random();
        }
        #endregion


        #region Helfermethoden

        /// <summary>
        /// Gibt eine neue Position am Bildschirmrand, je nach Flugrichtung
        /// </summary>
        /// <returns></returns>
        public static Vector2 GibNeuePosition()
        {
            return GibNeuePosition(new Rectangle());
        }

        /// <summary>
        /// Gibt eine neue Position am Bildschirmrand, je nach Flugrichtung
        /// </summary>
        /// <returns></returns>
        public static Vector2 GibNeuePosition(Rectangle objektRechteck)
        {
            Vector2 bildschirmPosition = Vector2.Zero;

            //setzt die Position ensprechend der Geschwindigkeit
            if (Kamera.geschwindigkeit.X < 0)
            {
                if (Kamera.geschwindigkeit.Y < 0)
                {
                    if (rand.Next(0, 2) == 0)
                        bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), 0);
                    else
                        bildschirmPosition = new Vector2(0, rand.Next(0, Kamera.sichtfeldHoehe + 1));
                }
                else if (Kamera.geschwindigkeit.Y > 0)
                {
                    if (rand.Next(0, 2) == 0)
                        bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), Kamera.sichtfeldHoehe);
                    else
                        bildschirmPosition = new Vector2(0, rand.Next(0, Kamera.sichtfeldHoehe + 1));
                }
                else
                    bildschirmPosition = new Vector2(0, rand.Next(0, Kamera.sichtfeldHoehe));
            }
            else if (Kamera.geschwindigkeit.X > 0)
            {
                if (Kamera.geschwindigkeit.Y < 0)
                {
                    if (rand.Next(0, 2) == 0)
                        bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), 0);
                    else
                        bildschirmPosition = new Vector2(Kamera.sichtfeldBreite, rand.Next(0, Kamera.sichtfeldHoehe + 1));
                }
                else if (Kamera.geschwindigkeit.Y > 0)
                {
                    if (rand.Next(0, 2) == 0)
                        bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), Kamera.sichtfeldHoehe);
                    else
                        bildschirmPosition = new Vector2(Kamera.sichtfeldBreite, rand.Next(0, Kamera.sichtfeldHoehe + 1));
                }
                else
                    bildschirmPosition = new Vector2(Kamera.sichtfeldBreite, rand.Next(0, Kamera.sichtfeldHoehe + 1));
            }
            else if (Kamera.geschwindigkeit.Y < 0)
                bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), 0);
            else if (Kamera.geschwindigkeit.Y > 0)
                bildschirmPosition = new Vector2(rand.Next(0, Kamera.sichtfeldBreite + 1), Kamera.sichtfeldHoehe);

            //setzt das Objekt entsprechend der Objektgröße aus dem Bildschirm
            if (bildschirmPosition.X == 0)
                bildschirmPosition.X -= objektRechteck.Width;
            else if (bildschirmPosition.X == Kamera.sichtfeldBreite)
                bildschirmPosition.X += objektRechteck.Width;

            if (bildschirmPosition.Y == 0)
                bildschirmPosition.Y -= objektRechteck.Height;
            else if (bildschirmPosition.Y == Kamera.sichtfeldBreite)
                bildschirmPosition.Y += objektRechteck.Height;

            return Kamera.ScreenAufWelt(bildschirmPosition);
        }

        /// <summary>
        /// Gibt eine zufällige Richtung im Bereich der minGeschwindigkeit und maxGeschwindigkeit zurück
        /// </summary>
        /// <param name="minGeschwindigkeit"></param>
        /// <param name="maxGeschwindigkeit"></param>
        /// <returns></returns>
        public static Vector2 ZufallsGeschwindigkeit(int minGeschwindigkeit, int maxGeschwindigkeit)
        {
            Vector2 zufallsRichtung = GibZufallsrichtung();
            zufallsRichtung.Normalize();
            zufallsRichtung *= rand.Next(minGeschwindigkeit, maxGeschwindigkeit);

            return zufallsRichtung;
        }

        public static Vector2 GibZufallsrichtung()
        {
            Vector2 zufallsRichtung;

            do
            {
                zufallsRichtung = new Vector2(rand.Next(0, 100) - 50, rand.Next(0, 100) - 50);
            } while (zufallsRichtung.Length() == 0);

            return zufallsRichtung;
        }

        /// <summary>
        /// Erstellt eine zufällige Gradzahl (zwischen 0 und 360°)
        /// </summary>
        /// <returns>in Bogenmaß</returns>
        public static float ZufallsRotation()
        {
            return ZufallsRotation(0, 360);
        }

        /// <summary>
        /// Erstellt eine zufällige Gradzahl innerhalb des Bereiches
        /// </summary>
        /// <param name="minRotation">Parameter in Grad </param>
        /// <param name="maxRotation">Parameter in Grad </param>
        /// <returns>in Bogenmaß</returns>
        public static float ZufallsRotation(int minRotation, int maxRotation)
        {
            return MathHelper.ToRadians(rand.Next(minRotation, maxRotation));
        }
        #endregion
    }
}
