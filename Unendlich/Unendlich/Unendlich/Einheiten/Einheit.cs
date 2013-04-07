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
        
        protected Stack<Einheit> _naechstesZiel;
        protected float _entfernungZuZiel;

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

        public Einheit naechstesZiel
        {
            get
            {
                if (_naechstesZiel.Count == 0)
                    return null;
                else
                    return _naechstesZiel.Peek();
            }

            set
            {
                _naechstesZiel.Push(value);
            }
        }

        public float entfernungZuZiel
        {
            get { return _entfernungZuZiel; }
            set { _entfernungZuZiel = MathHelper.Max(0, value); }
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

                foreach (Einheit potenziellerVerbuendeter in  Spielmanager.weltall[0].alleEinheiten)
                {
                    //Wenn der Spieler in einem Gewissenbreich (innerhalb von 20 Sek anwesend) ist 
                    //UND nicht man selber UND in der selben Fraktion
                    if (Vector2.Distance(this.weltMittelpunkt, potenziellerVerbuendeter.weltMittelpunkt) < potenziellerVerbuendeter.aktuellesSchiff.geschwindigkeitMax * 20f &&
                        potenziellerVerbuendeter.fraktion == fraktion &&
                        !potenziellerVerbuendeter.Equals(this))
                    {
                        //Schaden wird abhängig von der Entfernung addiert
                        schadenGesamt += potenziellerVerbuendeter.aktuellesSchiff.schadenProSek * (1 - VerhaeltnisEntfernungGeschwindigkeitMax(potenziellerVerbuendeter));
                    }
                }
                //Man selbst ist ja auch Verbündeter, sein eigener Schaden wird jedoch doppelt gewichtet
                return schadenGesamt + aktuellesSchiff.schadenProSek * 2;
            }
        }

        /// <summary>
        /// Liefert den gesamten Lebenspunkte aller Verbündeten innerhalb eines bestimmten Bereiches.
        /// Diese Lebenspunkte werden abhängig von der Entfernung zum Spieler berechnet.
        /// </summary>
        public float verbuendetenHP
        {
            get
            {
                float hpGesamt = 0.0f;

                foreach (Einheit potenziellerVerbuendeter in Spielmanager.weltall[0].alleEinheiten)
                {
                    //Wenn der Spieler in einem Gewissenbreich (innerhalb von 20 Sek anwesend) ist 
                    //UND nicht man selber UND in der selben Fraktion
                    if (Vector2.Distance(this.weltMittelpunkt, potenziellerVerbuendeter.weltMittelpunkt) < potenziellerVerbuendeter.aktuellesSchiff.geschwindigkeitMax * 20f &&
                        potenziellerVerbuendeter.fraktion == fraktion &&
                        !potenziellerVerbuendeter.Equals(this))
                    {
                        //HP werden abhängig von der Entfernung addiert
                        hpGesamt += potenziellerVerbuendeter.aktuellesSchiff.hp * (1 - VerhaeltnisEntfernungGeschwindigkeitMax(potenziellerVerbuendeter));
                    }
                }
                //Man selbst ist ja auch Verbündeter, sein eingene Hp werden einfach hinzuaddiert
                return hpGesamt + aktuellesSchiff.hp;
            }
        }
        #endregion


        #region Konstruktor
        
        public Einheit(Raumschiff aktuellesRaumschiff, Fraktion fraktion)
        {
            //Auswahl des Anfangsraumschiffes
            _aktuellesSchiff = aktuellesRaumschiff;
            _fraktion = fraktion;
            _naechstesZiel = new Stack<Einheit>();
        }
        #endregion


        #region Helfermtehoden

        public void ZielErfuellt()
        {
            _naechstesZiel.Pop();
        }

        private float VerhaeltnisEntfernungGeschwindigkeitMax(Einheit andereEinheit)
        {
            return andereEinheit.aktuellesSchiff.geschwindigkeitMax / Vector2.Distance(weltMittelpunkt, andereEinheit.weltMittelpunkt);
        }

        public void ErhaltenNeuesZiel(Einheit objekt)
        {
            _naechstesZiel.Push(objekt);
        }
        #endregion


        #region Update und Draw

        public virtual void Update(GameTime gameTime)
        {
            if (naechstesZiel != null)
                entfernungZuZiel = Vector2.Distance(weltMittelpunkt, naechstesZiel.weltMittelpunkt);

            _aktuellesSchiff.Update(gameTime);  //aktuallisiert unter anderem auch die Position in GrafikObjekt
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _aktuellesSchiff.Draw(spriteBatch);
        }
        #endregion
    }
}
