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

        private static void FliegeZumZiel(NPC aktuellerGegner)
        {
            Vector2 richtungZuSpieler = Vector2.Zero;

            richtungZuSpieler.X = _spieler.weltMittelpunkt.X - aktuellerGegner.weltMittelpunkt.X;
            richtungZuSpieler.Y = _spieler.weltMittelpunkt.Y - aktuellerGegner.weltMittelpunkt.Y;
            richtungZuSpieler.Normalize();

            aktuellerGegner.aktuellesSchiff.GeschwindigkeitAendern(richtungZuSpieler);
        }

        private static float EntfernungZumZiel(NPC aktuellerGegner)
        {
            return Vector2.Distance(_spieler.weltMittelpunkt, aktuellerGegner.weltMittelpunkt);
        }

        private static bool IstSpielerImZiel(NPC gegner)
        {
            Vector2 richtung = gegner.geschwindigkeit;
            richtung.Normalize();
            richtung *= EntfernungZumZiel(gegner);//multipliziert die Richtung des Gegeners mit der Entfernung

            //wenn der Gegner in die Richtung des 2fachen Kollisionsradius des Spieler fliegt, soll er den Spieler im Ziel haben
            if (Vector2.Distance(gegner.weltMittelpunkt + richtung, _spieler.weltMittelpunkt) < _spieler.kollisionsRadius * 2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Überprüft die Distanz zum Spieler und schießt, wenn der Spieler näher als Feuerreichweite ist und sich der Spieler im Ziel befindet
        /// </summary>
        /// <param name="gegner"></param>
        private static void SchiesseAufZiel(NPC gegner)
        {
            //Prüfungen werden geteilt, um Rechenleistung zu sparen
            if (IstSpielerImZiel(gegner))
                if (EntfernungZumZiel(gegner) < gegner.aktuellesSchiff.waffenreichweite)
                    gegner.aktuellesSchiff.BefehlZumFeuern();
        }

        private static float BerechneSchadesVerhaeltis(Einheit aktuellerGegner, Einheit potenziellesZiel)
        {
            return potenziellesZiel.verbuendetenSchaden / aktuellerGegner.verbuendetenSchaden;
        }

        private static Einheit BerechneNaechstesZiel(Einheit aktuelleEinheit)
        {
            Einheit naechstesZiel = null;
            float naechsteEntfernung = 0.0f;

            foreach (NPC gegner in Gegnermanager.alleGegner)
            {
                if (gegner.fraktion != aktuelleEinheit.fraktion)
                {
                    if (naechstesZiel == null ||
                        (aktuelleEinheit.fraktion != gegner.fraktion && naechsteEntfernung > Vector2.Distance(gegner.weltMittelpunkt, aktuelleEinheit.weltMittelpunkt)))
                    {
                        naechstesZiel = gegner;
                        naechsteEntfernung = Vector2.Distance(aktuelleEinheit.weltMittelpunkt, naechstesZiel.weltMittelpunkt);
                    }
                }
            }

            if (aktuelleEinheit.fraktion != _spieler.fraktion && (naechstesZiel==null || Vector2.Distance(aktuelleEinheit.weltMittelpunkt,_spieler.weltMittelpunkt)<naechsteEntfernung))
            {
                naechstesZiel=_spieler;
            }

            return naechstesZiel;
        }

        private static void BerechneNaechstenSchritt(NPC aktuellerGegner)
        {
            
        }

        private static void FuehreAngriffAus(NPC aktuellerGegner)
        {
            FliegeZumZiel(aktuellerGegner);
            BerechneSchadesVerhaeltis(aktuellerGegner, BerechneNaechstesZiel(aktuellerGegner));
            SchiesseAufZiel(aktuellerGegner);
        }

        private static void FuehereFluchtAus(NPC aktuellerGegner)
        {

        }

        private static void FuehreWartenAus(NPC aktuellerGegener)
        {

        }

        private static void BerechneFlugbahn(Raumschiff aktuellerGegner)
        {
            
        }
        #endregion


        #region Öffentliche Methoden

        public static void BerechneLogik(NPC gegner)
        {
            //temporär
            FuehreAngriffAus(gegner);
        }
        #endregion
    }
}
