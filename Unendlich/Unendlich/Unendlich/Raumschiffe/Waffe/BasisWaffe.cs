using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public abstract class BasisWaffe : SpielObjekt
    {
        #region Deklaration

        protected float _engerieVerbrauch;
        protected int _schaden;
        protected float _schussZeitMin;
        protected float _schussZeitTimer;
        protected bool _schussAktiv;    //ob geschossen werden soll

        protected List<Schuss> _schuesse;
        protected string _schussAnimation;

        protected Vector2 _positionMuendung;
        protected Vector2 _positionAufSchiff;   //die Stelle an der die Waffenplattform auf dem Schiff liegt
        protected Vector2 _schussRichtung;
        protected float _schussGeschwindigkeit;
        protected Raumschiff _schiff;   //ist das Schiff auf der sich die Waffe befindet, z.B nötig zur Positionsbestimmung
        #endregion


        #region Eigenschaften

        public float energieVerbrauch
        {
            get { return _engerieVerbrauch; }
        }

        /// <summary>
        /// normalisiert die Geschwindigkeit, sodass man eine Richtung erhält
        /// </summary>
        /// <param name="geschwindigkeit"></param>
        /// <returns></returns>
        public Vector2 SchussRichtung(Vector2 geschwindigkeit)
        {
            geschwindigkeit.Normalize();
            return geschwindigkeit;
        }

        public Vector2 positionMuendung
        {
            get { return _positionMuendung; }
        }

        public bool istSchussBereit
        {
            get { return _schussZeitMin < _schussZeitTimer; }
        }

        public int schaden
        {
            get { return _schaden; }
        }

        public float schuesseProSek
        {
            get { return 60.0f/_schussZeitMin; }
        }

        public float schussGeschwindigkeit
        {
            get { return _schussGeschwindigkeit; }
        }

        public float schadenProSek
        {
            get { return schaden * schuesseProSek; }
        }

        public int waffenreichweite
        {
            get
            {
                return (int)(_schiff.energie / energieVerbrauch);
            }
        }
        #endregion


        #region Konstruktor

        public BasisWaffe(Raumschiff schiff, int breite, int hoehe, float energieVerbrauch, float schussGeschwindigkeit, Vector2 positionAufRumpf, Vector2 positionMuendung, int schaden, float schussZeitMin, string aktuelleAnimation, string schussAnimation)
            : base(schiff.weltposition + positionAufRumpf, breite, hoehe, 0.38f, aktuelleAnimation)
        {
            _engerieVerbrauch = energieVerbrauch;
            _positionMuendung = positionMuendung;
            _schiff = schiff;
            _schaden = schaden;
            _schussAktiv = false;
            _schussZeitMin = schussZeitMin;
            _schussZeitTimer = 0.0f;
            _schussGeschwindigkeit = schussGeschwindigkeit;
            _positionAufSchiff = positionAufRumpf;
            _schussAnimation = schussAnimation;

            _schuesse = new List<Schuss>();
        }
        #endregion


        #region HelferMethoden

        public void Deaktivieren()
        {
            _istAktiv = false;
        }

        public int AnzahlSchuesse()
        {
            return _schuesse.Count;
        }

        public List<Schuss> AlleSchuesse()
        {
            return _schuesse;
        }

        public abstract bool IstObjektImZiel(Einheit andereEinheit);

        public bool IstInReichweite(Einheit andereEinheit)
        {
            return Vector2.Distance(weltMittelpunkt, andereEinheit.weltMittelpunkt) < waffenreichweite;
        }

        public void BefehlZumFeuern()//setzte den Befehl zum Schiessen auf wahr
        {
            _schussAktiv = true;
        }

        public abstract void Schiessen();

        protected virtual void Schiessen(Vector2 muendung)
        {
            Vector2 neuePosition =- positionNachRotation(muendung);
            neuePosition += weltMittelpunkt;

            AddSchuss( new Schuss(neuePosition, SchussRichtung(_schiff.geschwindigkeit) * _schussGeschwindigkeit, Vector2.Zero, 7, 3, _schussAnimation, _schussGeschwindigkeit, (_schiff.energie / _engerieVerbrauch), schaden));
        }

        protected void AddSchuss(Schuss schuss)
        {
            _schuesse.Add(schuss);
        }

        #endregion


        #region Update

        public override void Update(GameTime gameTime)
        {
            if (istAktiv)
            {
                rotation = _schiff.rotation;//rotiert die Waffe
                weltMittelpunktAendern = _schiff.weltMittelpunkt + positionNachRotation(_positionAufSchiff - _schiff.objektMitte + objektMitte);//setzt die Position der Waffe einfach an die Position des Raumschiffes
                base.Update(gameTime);

                _schussZeitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_schussZeitTimer > _schussZeitMin && _schussAktiv)  //prüft ob geschossen werden darf
                {
                    Schiessen();
                    _schussZeitTimer = 0.0f;
                    _schussAktiv = false;

                    StarteAnimationVonAnfang(this.GetType().Name + "_aktiv");   //schreibt Waffennamen vor Aktiv   z.B. Laser + _aktiv
                }
            }

            UpdateSchuesse(gameTime);
        }


        protected void UpdateSchuesse(GameTime gameTime)
        {
            //Aktuallisiert die Schüsse
            for (int i = _schuesse.Count - 1; i >= 0; i--)
            {
                if (_schuesse[i].istAktiv)
                    _schuesse[i].Update(gameTime);
                else
                    _schuesse.RemoveAt(i);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Schuss schuss in _schuesse)
                schuss.Draw(spriteBatch);// ob der Schuss aktiv ist wird in schuss.Draw geprüft

            base.Draw(spriteBatch);
        }
        #endregion

    }
}