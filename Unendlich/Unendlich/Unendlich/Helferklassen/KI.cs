using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public static class KI
    {
        #region Deklaration

        private static Spieler _spieler;
        private enum MoeglicheAktionen { Flucht, Warten, Angriff };
        private static MoeglicheAktionen _naechsterSchritt;
        #endregion


        #region Init

        public static void Init(Spieler spieler)
        {
            _spieler = spieler;
            _naechsterSchritt = new MoeglicheAktionen();
        }
        #endregion


        #region Helfermethoden

        private static void FliegeZuSpieler(Raumschiff aktuellerGegner)
        {
            Vector2 richtungZuSpieler = Vector2.Zero;

            richtungZuSpieler.X = _spieler.weltmittelpunkt.X - aktuellerGegner.weltMittelpunkt.X;
            richtungZuSpieler.Y = _spieler.weltmittelpunkt.Y - aktuellerGegner.weltMittelpunkt.Y;
            richtungZuSpieler.Normalize();

            bool gegnerIstGewesen = false;

            Vector2 ausweichRichtung=Vector2.Zero;

            foreach (Raumschiff gegner in Gegnermanager.alleGegner)
            {
                if (!gegnerIstGewesen)
                {
                    if (aktuellerGegner.Equals(gegner))
                    {
                        gegnerIstGewesen = true;
                        continue;
                    }
                }
                else
                {
                    if (Vector2.Distance(aktuellerGegner.weltMittelpunkt,gegner.weltMittelpunkt)<2000)//es werden nur gegner berücksichtig, die näher als 2000 Pixel sind
                    {
                        //Berechnung für ausweichen
                    }
                }
            }

            aktuellerGegner.GeschwindigkeitAendern(richtungZuSpieler);
        }

        private static float EntfernungZumSpieler(Raumschiff aktuellerGegner)
        {
            return Vector2.Distance(_spieler.weltmittelpunkt, aktuellerGegner.weltMittelpunkt);
        }

        private static bool IstSpielerImZiel(Raumschiff gegner)
        {
            Vector2 richtung = gegner.geschwindigkeit;
            richtung.Normalize();
            richtung *= EntfernungZumSpieler(gegner);//multipliziert die Richtung des Gegeners mit der Entfernung

            //wenn der Gegner in die Richtung des 2fachen Kollisionsradius des Spieler fliegt, soll er den Spieler im Ziel haben
            if (Vector2.Distance(gegner.weltMittelpunkt + richtung, _spieler.weltmittelpunkt) < _spieler.kollisionsRadius * 2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Überprüft die Distanz zum Spieler und schießt, wenn der Spieler näher als Feuerreichweite ist und sich der Spieler im Ziel befindet
        /// </summary>
        /// <param name="gegner"></param>
        private static void SchiesseAufSpieler(Raumschiff gegner)
        {
            //Prüfungen werden geteilt, um Rechenleistung zu sparen
            if (IstSpielerImZiel(gegner))
                if (EntfernungZumSpieler(gegner) < gegner.waffenreichweite)
                    gegner.BefehlZumFeuern();
        }

        private static void BerechneNaechstenSchritt(Raumschiff aktuellerGegner)
        {
            
        }

        private static void FuehreAngriffAus(Raumschiff aktuellerGegner)
        {
            FliegeZuSpieler(aktuellerGegner);
            SchiesseAufSpieler(aktuellerGegner);
        }

        private static void FuehereFluchtAus(Raumschiff aktuellerGegner)
        {

        }

        private static void FuehreWartenAus(Raumschiff aktuellerGegener)
        {

        }

        private static void BerechneFlugbahn(Raumschiff aktuellerGegner)
        {
            
        }
        #endregion


        #region Öffentliche Methoden

        public static void BerechneLogik(Raumschiff aktuellerGegner)
        {
            //temporär
            FuehreAngriffAus(aktuellerGegner);
        }
        #endregion
    }
}
