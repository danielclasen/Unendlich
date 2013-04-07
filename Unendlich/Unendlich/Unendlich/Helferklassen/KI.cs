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

        private static bool IstObjektImZiel(Einheit aktuelleEinheit, Einheit gegner)
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
            bool sollSchiessen=false;

            foreach (Einheit einheit in Spielmanager.weltall[0].alleEinheiten)
            {
                //Er soll nicht auf Verbündete schießen
                if (npc.fraktion == einheit.fraktion)
                    continue;

                if (IstObjektImZiel(npc, einheit) && EntfernungZumZiel(npc, einheit) < npc.aktuellesSchiff.waffenreichweite)// es befindet sich wer im Ziel
                {
                    if (npc.fraktion == einheit.fraktion)// ein Verbündeter
                    {
                        sollSchiessen = false;
                        break;// er soll nicht schießen
                    }
                    else if (sollSchiessen == false)
                        sollSchiessen = true; //wenn kein Verbündeter mehr im Ziel ist, soll geschossen werden
                }
            }

            if (sollSchiessen == true)
                npc.aktuellesSchiff.BefehlZumFeuern();
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
            aktuelleEinheit.entfernungZuZiel = naechsteEntfernung;
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

            if (ergebnis < 0.5f)
                _verhalten = MoeglicheAktionen.Flucht;
            else
                _verhalten = MoeglicheAktionen.Angriff;
        }

        private static Vector2 BerechneAngriff(Einheit aktuellerNpc)
        {
            return aktuellerNpc.naechstesZiel.weltMittelpunkt;
        }

        private static Vector2 BerechneFlucht(Einheit aktuellerNpc)
        {
            Vector2 fluchtpunkt = Vector2.Zero;//aktuellerNpc.naechstesZiel.weltMittelpunkt - aktuellerNpc.weltposition;

            if (aktuellerNpc.weltMittelpunkt.X > aktuellerNpc.naechstesZiel.weltMittelpunkt.X)
            {
                fluchtpunkt.X = 1000;
            }
            else if (aktuellerNpc.weltMittelpunkt.X < aktuellerNpc.naechstesZiel.weltMittelpunkt.X)
            {
                fluchtpunkt.X = -1000;
            }

            if (aktuellerNpc.weltMittelpunkt.Y > aktuellerNpc.naechstesZiel.weltMittelpunkt.Y)
            {
                fluchtpunkt.Y = 1000;
            }
            else if (aktuellerNpc.weltMittelpunkt.Y < aktuellerNpc.naechstesZiel.weltMittelpunkt.Y)
            {
                fluchtpunkt.Y = -1000;
            }

            return aktuellerNpc.weltMittelpunkt + fluchtpunkt;
        }

        private static void FuehreWartenAus(Einheit aktuellerNpc)
        {

        }

        private static void PruefeZielerfuellung(Einheit aktuelleEinheit)
        {
            if (_verhalten == MoeglicheAktionen.Flucht)
            {
                if (aktuelleEinheit.entfernungZuZiel > 2000)
                    aktuelleEinheit.ZielErfuellt();
            }
            else if (_verhalten == MoeglicheAktionen.Angriff)
            {
                if (!aktuelleEinheit.naechstesZiel.istAktiv || aktuelleEinheit.entfernungZuZiel>1000)
                    aktuelleEinheit.ZielErfuellt();
            }
        }

        /// <summary>
        /// Prüft alle Einheite (nachher alle Objekte) innerhalb des Sektor. Befinden sich die innerhalb eines gewissen Radius (x-fache Kollisionsradius) wird überprüft, welches Objekt das nächste ist.
        /// Anschließend wir versucht, dem Objekt bestmöglich auszuweichen.
        /// </summary>
        /// <param name="aktuellerNPC"></param>
        /// <returns>Es wird der Ausweichvektor zurück gegeben</returns>
        private static Vector2 Ausweichen(NPC aktuellerNPC)
        {
            SpielObjekt naechstesObjekt = null;
            float naechsteEntfernungBisKollision = 0.0f;
            float berücksichtigungsRadius = aktuellerNPC.kollisionsRadius * 10;//Radius, in dem Objekte brücksichtigt werdem

            foreach (Einheit einheit in Spielmanager.weltall[0].alleEinheiten)
            {
                //Nur Objekte in gewissem Radius
                if ((naechstesObjekt == null ||
                    naechsteEntfernungBisKollision > Vector2.Distance(einheit.weltMittelpunkt, aktuellerNPC.weltMittelpunkt) - einheit.kollisionsRadius + aktuellerNPC.kollisionsRadius) &&
                    !einheit.Equals(aktuellerNPC) &&
                    Vector2.Distance(einheit.weltMittelpunkt, aktuellerNPC.weltMittelpunkt) - einheit.kollisionsRadius < berücksichtigungsRadius)
                {
                    naechstesObjekt = (SpielObjekt)einheit.aktuellesSchiff;
                    naechsteEntfernungBisKollision = Vector2.Distance(aktuellerNPC.weltMittelpunkt, einheit.weltMittelpunkt) - aktuellerNPC.kollisionsRadius - einheit.kollisionsRadius; //Von der Entfernung wird noch die Größe der beiden Objekte abgezogen
                }
            }

            if (naechstesObjekt != null)
            {
                Vector2 ausweichVektor = Vector2.Zero;
                ausweichVektor = aktuellerNPC.weltMittelpunkt - naechstesObjekt.weltMittelpunkt;
                ausweichVektor.Normalize();
                ausweichVektor *= (1 - naechsteEntfernungBisKollision / berücksichtigungsRadius);// Vielleicht zu stark gewichtet
                return ausweichVektor;
            }
            else
                return Vector2.Zero; //Falls kein Objekt in der Nähe ist
        }

        /// <summary>
        /// Führt "FlugbahnZumNaechstenZiel" und "Ausweichen" aus
        /// Abschließend wird die neue Richtung an "aktuellerNpc.GeschwindigkeitAendern" übergeben
        /// </summary>
        /// <param name="aktuellerNpc"></param>
        private static void BerechneFlugbahn(NPC aktuellerNpc)
        {
            Vector2 zielKoordinate = Vector2.Zero;

            //Berechnet je nach Verhalten das Ziel
            if (_verhalten == MoeglicheAktionen.Angriff)
                zielKoordinate = BerechneAngriff(aktuellerNpc);
            else if (_verhalten == MoeglicheAktionen.Flucht)
                zielKoordinate = BerechneFlucht(aktuellerNpc);

            Vector2 flugRichtung = FlugbahnZumNaechstenZiel(aktuellerNpc.weltMittelpunkt, zielKoordinate);
            Vector2 ausweichRichtung = Ausweichen(aktuellerNpc); //nur das nahste Objekte wird berücksichtig

            flugRichtung *= (1 - ausweichRichtung.Length());

            aktuellerNpc.aktuellesSchiff.GeschwindigkeitAendern(flugRichtung + ausweichRichtung);
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

            PruefeZielerfuellung(npc);
        }
        #endregion
    }
}
