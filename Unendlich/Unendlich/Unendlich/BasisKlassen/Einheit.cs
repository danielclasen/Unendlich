using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Einheit
    {
        #region Deklartion
        public enum Aufgabe { angreifen, fluechten, warten };
        protected Stack<Aufgabe> aufgabenListe;

        public enum Fraktion { spieler1, gegner1, gegner2 };
        protected Fraktion _fraktion;

        protected Raumschiff _aktuellesSchiff;
        #endregion


        #region Eigenschaft

        public Fraktion fraktion
        {
            get { return _fraktion; }
        }

        public bool istAktiv
        {
            get { return aktuellesSchiff.istAktiv; }
        }

        /// <summary>
        /// Liefert den gesamten Schaden aller Verbündeten innerhalb eines bestimmten Bereiches.
        /// Dieser Schaden wird abhängig von der Entfernung zum Spieler berechnet.
        /// </summary>
        public float verbuendetenSchaden
        {
            get
            {
                float schadenGesamt = 0.0f;

                foreach (NPC potenziellerVerbuendeter in Gegnermanager.alleGegner)
                {
                    //Wenn der Spieler in einem Gewissenbreich (innerhalb von 20 Sek anwesend) ist 
                    //UND nicht man selber UND in der selben Fraktion
                    if (Vector2.Distance(this.weltMittelpunkt, potenziellerVerbuendeter.weltMittelpunkt) < potenziellerVerbuendeter.aktuellesSchiff.geschwindigkeitMax * 20f &&
                        potenziellerVerbuendeter.fraktion == fraktion &&
                        !potenziellerVerbuendeter.Equals(this))
                    {
                        //Schaden wird abhängig von der Entfernung addiert
                        schadenGesamt += potenziellerVerbuendeter.aktuellesSchiff.schadenProSek * (1 - potenziellerVerbuendeter.aktuellesSchiff.geschwindigkeitMax / Vector2.Distance(weltMittelpunkt, potenziellerVerbuendeter.weltMittelpunkt));
                    }
                }
                //Man selbst ist ja auch Verbündeter, sein eigener Schaden wird jedoch doppelt gewichtet
                return schadenGesamt + aktuellesSchiff.schadenProSek * 2;
            }
        }
        #endregion


        #region Eigenschaften

        public Raumschiff aktuellesSchiff
        {
            get { return _aktuellesSchiff; }
        }

        public Vector2 geschwindigkeit
        {
            get { return _aktuellesSchiff.geschwindigkeit; }
        }

        public Vector2 weltposition
        {
            get { return _aktuellesSchiff.weltposition; }
        }

        public Vector2 weltMittelpunkt
        {
            get { return _aktuellesSchiff.weltMittelpunkt; }
        }

        public float rotation
        {
            get { return _aktuellesSchiff.rotation; }
        }

        public float kollisionsRadius
        {
            get { return _aktuellesSchiff.kollisionsRadius; }
        }
        #endregion


        #region Konstruktor
        
        public Einheit(Raumschiff aktuellesRaumschiff, Fraktion fraktion)
        {
            //Auswahl des Anfangsraumschiffes
            _aktuellesSchiff = aktuellesRaumschiff;
            _fraktion = fraktion;
        }
        #endregion


        #region Update und Draw

        public virtual void Update(GameTime gameTime)
        {
            _aktuellesSchiff.Update(gameTime);  //aktuallisiert unter anderem auch die Position in GrafikObjekt
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _aktuellesSchiff.Draw(spriteBatch);
        }
        #endregion
    }
}
