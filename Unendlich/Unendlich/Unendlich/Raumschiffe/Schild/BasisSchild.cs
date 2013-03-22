/*
 * Wie stell ich die Animation richtig da, damit sie auch für verschiedene Größen gilt
 * 
 * 
 * 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class BasisSchild:SpielObjekt
    {
        #region Deklaration

        protected Raumschiff _schiff;
        protected float _schildMax;
        protected float _schildAktuell;
        protected float _schildRegeneration;
        protected float _zeitBisRegeneration;
        #endregion


        #region Eigenschaften

        public float schildMax
        {
            get{return _schildMax;}
        }

        public float schildRest
        {
            get{return _schildAktuell;}
        }

        public float schildRegeneration
        {
            get { return _schildRegeneration; }
        }

        public float schildRestProzentual
        {
            get{return schildRest/schildMax;}
        }

      
        #endregion


        #region Konstruktor
        /// <summary>
        /// Das Maximum und die Regeneration des Schildes errechnet sich aus der Energie des Schiffes und dem Energieverbrauch, sowie der Fläche
        /// </summary>
        /// <param name="schiff"></param>
        /// <param name="aktuelleAnimation"></param>
        /// <param name="energieVerbrauch">pro 1/10 Flächeneinheit</param>
        /// <param name="effizienz">(pro Sekunde) : schildMax*effizient</param>
        /// <param name="zeitBisRegeneration"></param>
        public BasisSchild(Raumschiff schiff, float energieVerbrauch, float effizienz,float zeitBisRegeneration)
            : base(schiff.weltposition, schiff.objektBreite, schiff.objektHoehe, 0.20f, "")
        {
            _schiff = schiff;
            _schildMax = ErrechneMaxSchild(energieVerbrauch);
            _schildAktuell = _schildMax;
            _schildRegeneration = _schildMax * effizienz;
            _zeitBisRegeneration = zeitBisRegeneration;
        }
        #endregion


        #region Helfer Methoden

        /// <summary>
        /// Setzt die Schildenergie
        /// </summary>
        /// <param name="energieVerbrauch">pro 1/10 Flächeneinheit</param> 
        /// <returns></returns>
        /// 
        private float ErrechneMaxSchild(float energieVerbrauch)
        {
            return _schiff.energie / (_schiff.objektBreite * _schiff.objektHoehe / 10 * energieVerbrauch);
        }

        protected void RestSchildErhoehen(float zeitSeitLetzemFrame)
        {
            _schildAktuell = MathHelper.Min(_schildMax, _schildAktuell + schildRegeneration * zeitSeitLetzemFrame);
        }

        public void Deaktivieren()
        {
            statusAendern = false;
        }

        public void WurdeGetroffen(float schaden)
        {
            _schildAktuell = MathHelper.Max(0, _schildAktuell - schaden);
        }
        #endregion


        #region Update

        public override void Update(GameTime gameTime)
        {
            if (!istAktiv)
                return;

            weltMittelpunktAendern = _schiff.weltMittelpunkt;

            float vergangen = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_schiff.zeitSeitLetztemTreffer > _zeitBisRegeneration && _schildMax > _schildAktuell)
                RestSchildErhoehen(vergangen);

            base.Update(gameTime);
        }
        #endregion
    }
}
