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
        private static MoeglicheAktionen _verhalten;
        #endregion


        #region Init

        public static void Init(Spieler spieler)
        {
            _spieler = spieler;
            _verhalten = new MoeglicheAktionen();
        }
        #endregion


        #region Helfermethoden

        private static Vector2 FlugbahnZumNaechstenZiel(Vector2 npcPosition, Vector2 zielPosition)
        {
            Vector2 richtungZuZiel = Vector2.Zero;

            richtungZuZiel.X = zielPosition.X - npcPosition.X;
            richtungZuZiel.Y = zielPosition.Y - npcPosition.Y;
            richtungZuZiel.Normalize();

            return richtungZuZiel;
        }


        private static float EntfernungZumZiel(Einheit aktuellerNpc, Einheit gegner)
        {
            return Vector2.Distance(gegner.weltMittelpunkt, aktuellerNpc.weltMittelpunkt);
        }

        private static bool IstGegnerImZiel(Einheit aktuelleEinheit, Einheit gegner)
        {
            Vector2 richtung = aktuelleEinheit.geschwindigkeit;
            richtung.Normalize();
            richtung *= EntfernungZumZiel(aktuelleEinheit,gegner);//multipliziert die Richtung des Gegeners mit der Entfernung

            //wenn der Gegner in die Richtung des 2fachen Kollisionsradius des Spieler fliegt, soll er den Spieler im Ziel haben
            if (Vector2.Distance(aktuelleEinheit.weltMittelpunkt + richtung, gegner.weltMittelpunkt) < gegner.kollisionsRadius * 2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Überprüft die Distanz zum Spieler und schießt, wenn der Spieler näher als Feuerreichweite ist und sich der Spieler im Ziel befindet
        /// </summary>
        /// <param name="npc"></param>
        private static void SchiesseBeiZiel(NPC npc)
        {
            foreach (Einheit einheit in Spielmanager.weltall[0].alleEinheiten)
            {
                //Er soll nicht auf Verbündete schießen
                if (npc.fraktion == einheit.fraktion)
                    continue;

                if (IstGegnerImZiel(npc,einheit))
                    if (EntfernungZumZiel(npc,einheit) < npc.aktuellesSchiff.waffenreichweite)
                    {
                        npc.aktuellesSchiff.BefehlZumFeuern();
                        break;
                    }
            }
        }

        private static float BerechneSchadesVerhaeltis(Einheit aktuellerNpc, Einheit potenziellesZiel)
        {
            return potenziellesZiel.verbuendetenSchaden / aktuellerNpc.verbuendetenSchaden;
        }

        private static float BerechneSchildVerhaeltnis(Einheit aktuellerNpc, Einheit potenziellesZiel)
        {
            return potenziellesZiel.aktuellesSchiff.schildRest / aktuellerNpc.aktuellesSchiff.schildRest;
        }

        private static float BerechneGeschwindigkeitVerhaeltnis(Einheit aktuellerNpc, Einheit potenziellesZiel)
        {
            return potenziellesZiel.aktuellesSchiff.geschwindigkeitMax / aktuellerNpc.aktuellesSchiff.geschwindigkeitMax;
        }

        private static float BerechneHpVerhaeltnis(Einheit aktuellerNpc, Einheit potenziellesZiel)
        {
            return potenziellesZiel.verbuendetenHP / aktuellerNpc.verbuendetenHP;
        }

        private static void BerechneNaechstenGegner(Einheit aktuelleEinheit)
        {
            Einheit naechstesZiel = null;
            float naechsteEntfernung = 0.0f;

            foreach (Einheit einheit in Spielmanager.weltall[0].alleEinheiten)
            {
                if (einheit.fraktion != aktuelleEinheit.fraktion)
                {
                    if (naechstesZiel == null ||
                        (naechsteEntfernung > Vector2.Distance(einheit.weltMittelpunkt, aktuelleEinheit.weltMittelpunkt)))
                    {
                        naechstesZiel = einheit;
                        naechsteEntfernung = Vector2.Distance(aktuelleEinheit.weltMittelpunkt, naechstesZiel.weltMittelpunkt);
                    }
                }
            }

            aktuelleEinheit.naechstesZiel = naechstesZiel;
        }

        private static void BerechneVerhalten(NPC aktuellerNpc)
        {
            float verhaeltnisSchaden = BerechneSchadesVerhaeltis(aktuellerNpc, aktuellerNpc.naechstesZiel);
            float verhaeltnisSchild = BerechneSchildVerhaeltnis(aktuellerNpc, aktuellerNpc.naechstesZiel);
            float verhaeltnisHP = BerechneHpVerhaeltnis(aktuellerNpc, aktuellerNpc.naechstesZiel);
            float verhaeltnisGeschwindigkeit = BerechneGeschwindigkeitVerhaeltnis(aktuellerNpc, aktuellerNpc.naechstesZiel);

            //Gewichtung
            verhaeltnisSchaden = MathHelper.Clamp(verhaeltnisSchaden, 0, 2);
            verhaeltnisSchaden *= 0.3f;
            verhaeltnisSchild = MathHelper.Clamp(verhaeltnisSchild, 0, 2);
            verhaeltnisSchild *= 0.4f;
            verhaeltnisHP = MathHelper.Clamp(verhaeltnisHP, 0, 2);
            verhaeltnisHP *= 0.15f;
            verhaeltnisGeschwindigkeit = MathHelper.Clamp(verhaeltnisGeschwindigkeit, 0, 2);
            verhaeltnisGeschwindigkeit *= 0.15f;

            float ergebnis = verhaeltnisSchaden + verhaeltnisSchild + verhaeltnisHP + verhaeltnisGeschwindigkeit;

            if (ergebnis < 0.8f)
                _verhalten = MoeglicheAktionen.Flucht;
            else
                _verhalten = MoeglicheAktionen.Angriff;
        }

        private static Vector2 BerechneAngriff(NPC aktuellerNpc)
        {
            return aktuellerNpc.naechstesZiel.weltMittelpunkt;
        }

        private static Vector2 BerechneFlucht(NPC aktuellerNpc)
        {
            Vector2 fluchtpunkt = aktuellerNpc.naechstesZiel.weltMittelpunkt - aktuellerNpc.weltposition;
            fluchtpunkt.Normalize();//Vektor von Gegner weg
            
            fluchtpunkt*=1000;//Entfernung wird berechnet

            return fluchtpunkt + aktuellerNpc.weltMittelpunkt;
        }

        private static void FuehreWartenAus(NPC aktuellerNpc)
        {

        }
/*
        private static Vector2 Ausweichen(NPC aktuellerNPC)
        {
            SpielObjekt naechstesObjekt = null;
            float naechsteEntfernung = 0.0f;
            Vector2 ausweichVektor = Vector2.Zero;

            foreach (Einheit einheit in Spielmanager.weltall[0].alleEinheiten)
            {
                //Nur Objekte in gewissem Radius
                if (Vector2.Distance(einheit.weltMittelpunkt, aktuellerNPC.weltMittelpunkt) < aktuellerNPC.kollisionsRadius * 10 &&
                    !einheit.Equals(aktuellerNPC) && (naechstesObjekt == null ||
                   (naechsteEntfernung > Vector2.Distance(einheit.weltMittelpunkt, aktuellerNPC.weltMittelpunkt))))
                {
                    naechstesObjekt = (SpielObjekt)einheit.aktuellesSchiff;
                }
            }
        }
        */
        private static void BerechneFlugbahn(NPC aktuellerNpc)
        {
            Vector2 zielKoordinate = Vector2.Zero;

            //Berechnet je nach Verhalten das Ziel
            if (_verhalten == MoeglicheAktionen.Angriff)
                zielKoordinate = BerechneAngriff(aktuellerNpc);
            else if (_verhalten == MoeglicheAktionen.Flucht)
                zielKoordinate = BerechneFlucht(aktuellerNpc);

            Vector2 flugrichtung = FlugbahnZumNaechstenZiel(aktuellerNpc.weltMittelpunkt, zielKoordinate);

            aktuellerNpc.aktuellesSchiff.GeschwindigkeitAendern(flugrichtung);
        }
        #endregion


        #region Öffentliche Methoden

        public static void BerechneLogik(NPC npc)
        {
            if (npc.naechstesZiel == null)
                BerechneNaechstenGegner(npc);

            BerechneVerhalten(npc);
            BerechneFlugbahn(npc);
            SchiesseBeiZiel(npc);
        }
        #endregion
    }
}
