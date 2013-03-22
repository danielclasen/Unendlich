using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    public class SpielObjekt
    {
        #region Deklaration

        protected Vector2 _weltposition;
        protected Vector2 _geschwindigkeit;
        protected int _objektBreite;
        protected int _objektHoehe;

        protected float _rotation;
        protected bool _istAktiv;

        protected bool _istRammbar;
        protected float _kollisionsRadius;
        protected float _masse = 0f;

        protected float _malTiefe;
        protected float _skalierung;

        protected Dictionary<string, AnimationsStreifen> _animationen =new Dictionary<string,AnimationsStreifen>();
        public string _aktuelleAnimation;
        protected Color _farbe;
        #endregion


        #region Eigenschaften

        public int objektBreite
        {
            get { return _objektBreite; }
        }

        public int objektHoehe
        {
            get { return _objektHoehe; }
        }

        public Vector2 weltposition
        {
            get { return _weltposition; }
        }

        public Vector2 weltpositionAendern
        {
            set
            {
                _weltposition.X = MathHelper.Clamp(value.X, float.MinValue, float.MaxValue);
                _weltposition.Y = MathHelper.Clamp(value.Y, float.MinValue, float.MaxValue);
            }
        }

        public virtual Vector2 geschwindigkeit
        {
            get { return _geschwindigkeit; }
            set { _geschwindigkeit = value; }
        }

        public bool istAktiv
        {
            get { return _istAktiv; }
        }

        public bool statusAendern
        {
            set { _istAktiv = value; }
        }

        public float masse
        {
            get { return _masse; }
        }

        /// <summary>
        /// Absolute / auf Karte
        /// </summary>
        public Rectangle objektRechteck
        {
            get
            {
                return new Rectangle(
                    (int)weltposition.X, (int)weltposition.Y,
                    objektBreite, objektHoehe);
            }
        }

        public Vector2 weltMittelpunkt
        {
            get { return weltposition + objektMitte; }
        }

        public Vector2 weltMittelpunktAendern
        {
            set { weltpositionAendern = value - objektMitte; }
        }

        public Vector2 objektMitte
        {
            get { return new Vector2(objektBreite / 2, objektHoehe / 2); }
        }


        public float kollisionsRadius
        {
            get { return _kollisionsRadius; }
        }

        public float rotation
        {
            get { return _rotation; }
            set { _rotation = value % MathHelper.TwoPi; }
        }


        public Color farbe
        {
            get { return _farbe; }
            set { _farbe = value; }
        }

        #endregion


        #region Konstruktor

        public SpielObjekt(
            Vector2 position, 
            Vector2 geschwindigkeit, 
            int breite, 
            int hoehe,
            float rotation,
            float kollisionsRadius, 
            bool istRammbar,
            float masse, 
            float malTiefe, 
            float skalierung, 
            string aktuelleAnimation,
            Color farbe)
        {
            _animationen = new Dictionary<string, AnimationsStreifen>();

            _weltposition = position;
            _geschwindigkeit = geschwindigkeit;
            _objektBreite = breite;
            _objektHoehe = hoehe;

            _rotation = rotation;
            _istAktiv = true;

            _istRammbar = istRammbar;
            _kollisionsRadius = kollisionsRadius;
            _masse = masse;

            _malTiefe = malTiefe;
            _skalierung = skalierung;
            _aktuelleAnimation = aktuelleAnimation;

            _farbe = new Color();
            _farbe = farbe;
        }

        public SpielObjekt(
            Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float rotation,
            float kollisionsRadius,
            bool istRammbar,
            float masse,
            float malTiefe,
            float skalierung,
            string aktuelleAnimation)
            : this(position, geschwindigkeit, breite, hoehe, rotation, kollisionsRadius, istRammbar, masse, malTiefe, skalierung, aktuelleAnimation, Color.White) 
        { }

        public SpielObjekt(
            Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float rotation,
            float kollisionsRadius,
            bool istRammbar,
            float masse,
            float malTiefe,
            string aktuelleAnimation)
            : this(position, geschwindigkeit, breite, hoehe, rotation, kollisionsRadius, istRammbar, masse, malTiefe, 1.0f, aktuelleAnimation)
        { }

        public SpielObjekt(
            Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float rotation,
            float kollisionsRadius,
            bool istRammbar,
            float malTiefe,
            string aktuelleAnimation)
            : this(position, geschwindigkeit, breite, hoehe, rotation, kollisionsRadius, istRammbar, 0.0f, malTiefe, aktuelleAnimation)
        { }

        public SpielObjekt(
            Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float rotation,
            float kollisionsRadius,
            float malTiefe,
            string aktuelleAnimation)
            : this(position, geschwindigkeit, breite, hoehe, rotation, kollisionsRadius, true, malTiefe, aktuelleAnimation)
        { }

        public SpielObjekt(
            Vector2 position,
            Vector2 geschwindigkeit,
            int breite,
            int hoehe,
            float malTiefe,
            string aktuelleAnimation)
            : this(position, geschwindigkeit, breite, hoehe, 0.0f, MathHelper.Max(breite,hoehe)/2, malTiefe, aktuelleAnimation)
        { }

        public SpielObjekt(
            Vector2 position,
            int breite,
            int hoehe,
            float malTiefe,
            string aktuelleAnimation)
            : this(position, Vector2.Zero, breite, hoehe, malTiefe, aktuelleAnimation)
        { }

        public SpielObjekt(
           Vector2 position,
           float malTiefe,
           string aktuelleAnimation)
            : this(position, 0, 0, malTiefe, aktuelleAnimation)
        { }
        
       
        #endregion


        #region Kollisionserkennung

        /// <summary>
        /// Prüft ob sich die beiden Rechtecken überlappen
        /// !!Muss noch an rotation angepasst werden!!
        /// </summary>
        /// <param name="anderesRechteck"></param>
        /// <returns></returns>
        public bool IstRechteckKollision(Rectangle anderesRechteck)
        {
            if (_istRammbar)
                return objektRechteck.Intersects(anderesRechteck);
            else
                return false;
        }

        /// <summary>
        /// Prüft ob die Entfernung der beiden Mittelpunkte kleiner als die Summe der Kollisionsradii ist
        /// </summary>
        /// <param name="andereMitte"></param>
        /// <param name="andererKollisionsradius"></param>
        /// <returns></returns>
        public bool IstKreisKollision(Vector2 andereMitte, float andererKollisionsradius)
        {
            if (_istRammbar)
                return Vector2.Distance(weltMittelpunkt, andereMitte) < _kollisionsRadius + andererKollisionsradius;
            else
                return false;
        }
        #endregion


        #region Helfermethoden

        public void rotiereZu(Vector2 richtung)
        {
            rotation = (float)Math.Atan2(richtung.Y, richtung.X);//berechnet die Drehung
        }

        protected Vector2 positionNachRotation(Vector2 position)
        {
            float cos = (float)Math.Cos(rotation);
            float sin = (float)Math.Sin(rotation);

            float x = position.X * cos - position.Y * sin;
            float y = position.X * sin + position.Y * cos;

            return new Vector2(x, y);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (_animationen.ContainsKey(_aktuelleAnimation))
            {
                if (_animationen[_aktuelleAnimation].istAnimationZuEnde)//wenn Animation abgelaufen ist, startet die nächste Animation
                    StarteAnimationVonAnfang(_animationen[_aktuelleAnimation].naechsteAnimation);//Gibt nächsten Animationsnamen zurück, der daraufhin gestartet wird
                else
                    _animationen[_aktuelleAnimation].Update(gameTime);
            }
        }

        public void AnimationHinzufuegen(string animationsName, AnimationsStreifen neuerAnimationsstreifen)
        {
            _animationen.Add(animationsName, neuerAnimationsstreifen);
        }
        #endregion


        #region Öffentliche Methoden

        public void StarteAnimationVonAnfang(string name)
        {
            _aktuelleAnimation = name;
            _animationen[_aktuelleAnimation].Start();
        }

        public void StarteAnimationVonZufall(string name)
        {
            _aktuelleAnimation=name;
            _animationen[_aktuelleAnimation].Start(Helferklasse.rand.Next(0,_animationen[_aktuelleAnimation].anzahlFrames));
        }
        #endregion

        
        #region Update und Draw
        /// <summary>
        /// Wird nur ausgeführt, wenn das Objekt aktiv ist.
        /// Änder die Geschwindigkeit des Objektes und aktuallisiert die Animation
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            if (!istAktiv)
                return;

            float vergangen = (float)gameTime.ElapsedGameTime.TotalSeconds;

            weltpositionAendern = weltposition + _geschwindigkeit*(float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateAnimation(gameTime);
        }


        /// <summary>
        /// Sollte/Darf nur ausgeführt werden wenn das Objekt zu sehen ist und wenn dieses Aktiv ist
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (istAktiv && Kamera.IstObjektSichtbar(objektRechteck))
            {
                if (_animationen.ContainsKey(_aktuelleAnimation))
                {
                    spriteBatch.Draw(
                        _animationen[_aktuelleAnimation].texture,
                        Kamera.WeltAufScreen(weltMittelpunkt),
                        _animationen[_aktuelleAnimation].frameRechteck,
                        _farbe,
                        rotation,
                        objektMitte,
                        _skalierung,
                        SpriteEffects.None,
                        _malTiefe);
                }
            }
        }
        #endregion
    }
}
