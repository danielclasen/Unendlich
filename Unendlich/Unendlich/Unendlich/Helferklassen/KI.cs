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
        private static List<Raumschiff> _gegner;
        #endregion


        #region Init

        public static void Init(Spieler spieler)
        {
            _spieler = spieler;
            _gegner=(Gegnermanager.alleGegner);
        }
        #endregion


        #region Helfermethoden
        
        private static void FliegeZuSpieler(Raumschiff gegner)
        {
            Vector2 neueRichtung = Vector2.Zero;

            neueRichtung.X = _spieler.weltmittelpunkt.X - gegner.weltMittelpunkt.X;
            neueRichtung.Y = _spieler.weltmittelpunkt.Y - gegner.weltMittelpunkt.Y;

            gegner.GeschwindigkeitAendern(neueRichtung);
        }

        private static float EntfernungZumSpieler(Raumschiff gegner)
        {
            return Vector2.Distance(_spieler.weltmittelpunkt, gegner.weltMittelpunkt);
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
        #endregion


        #region Öffentliche Methoden

        public static void BerechneVerhalten(Raumschiff aktuellerGegner)
        {
            FliegeZuSpieler(aktuellerGegner);
            SchiesseAufSpieler(aktuellerGegner);
        }
        #endregion
    }
}
