using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public abstract class Raumschiff:SpielObjekt
    {
        #region Deklaration

        protected string _name;
        protected int _energie;
        protected float _schiffsHuelle;
        protected float _schiffsHuelleMax;

        protected int _geschwindigkeitMax;
        protected float _schub;

        protected float _zeitSeitLetzemTreffer;

        protected List<BasisWaffe> _waffen;
        protected BasisSchild _schild;
        #endregion


        #region Eigenschaften

        public float zeitSeitLetztemTreffer
        {
            get { return _zeitSeitLetzemTreffer; }
        }

        public float schadenProSek
        {
            get
            {
                float schadenProSekGesamt = 0.0f;

                foreach (BasisWaffe waffe in _waffen)
                    schadenProSekGesamt += waffe.schadenProSek;

                return schadenProSekGesamt;
            }
        }

        public int energie
        {
            get { return _energie; }
        }

        public int waffenreichweite
        {
            get
            {
                if (_waffen.Count == 0)
                    return 0;
                else
                {
                    float maxEngergieverbrauch = _waffen[0].energieVerbrauch;

                    for (int i = 1; i < _waffen.Count; i++)
                        maxEngergieverbrauch = MathHelper.Max(maxEngergieverbrauch, _waffen[i].energieVerbrauch);

                    return (int)(energie / maxEngergieverbrauch);
                }
            }

        }

        public int geschwindigkeitMax
        {
            get { return _geschwindigkeitMax; }
        }

        public float schub
        {
            get { return _schub; }
        }

        public float hp
        {
            get { return _schiffsHuelle; }
        }

        public float hpMax
        {
            get { return _schiffsHuelleMax; }
        }

        public float hpRestProzentual
        {
            get { return hp / hpMax; }
        }

        public float schildRest
        {
            get { return _schild.schildRest; }
        }

        public float schildRestProzentual
        {
            get { return _schild.schildRestProzentual; }
        }

        /// <summary>
        /// prüft ob der Vektor größer als die Höchstgeschwindigkeit ist, ist dies der Fall wird die Geschwindigkeit durch die Höchstgeschwinigkeit ersetzt
        /// </summary>
        protected void SetzeGeschwindigkeit(Vector2 neueGeschwindigkeit)
        {
            if (geschwindigkeitMax > neueGeschwindigkeit.Length())
                this.geschwindigkeit = neueGeschwindigkeit;
            else
            {
                neueGeschwindigkeit.Normalize();
                this.geschwindigkeit = neueGeschwindigkeit * geschwindigkeitMax;
            }
           
        }
        #endregion


        #region Init Methoden

        protected abstract void InitKomponenten();
        #endregion


        #region Konstruktor

        public Raumschiff(Vector2 position, int breite, int hoehe, int huelleMax, int energie, int geschwindigkeitMax, float schub, string aktuelleAnimation)
            : base(position, breite, hoehe, 0.4f, aktuelleAnimation)
        {
            _schiffsHuelleMax = huelleMax;
            _schiffsHuelle = _schiffsHuelleMax;
            _geschwindigkeitMax = geschwindigkeitMax;
            _schub = schub;
            _masse = _schiffsHuelle;
            _energie = energie;

            _waffen = new List<BasisWaffe>();
            InitKomponenten();
        }
        #endregion


        #region Helfermethoden

        public void InstalliereNeuesSchild(BasisSchild neuesSchild)
        {
            _schild = neuesSchild;
        }

        /// <summary>
        /// Ändert die Geschwindigkeit. Schubkraft ist stets 100%
        /// </summary>
        /// <param name="neueRichtung"></param>
        public void GeschwindigkeitAendern(Vector2 neueRichtung)
        {
            GeschwindigkeitAendern(neueRichtung, 1);
        }

        /// <summary>
        /// Es kann angegeben werden, wie stark beschleunigt werden soll
        /// </summary>
        /// <param name="neueRichtung"></param>
        /// <param name="schubKraft"></param>
        public void GeschwindigkeitAendern(Vector2 neueRichtung, float schubKraft)
        {
            if (neueRichtung != Vector2.Zero && schubKraft != 0)
            {
                neueRichtung.Normalize();
                neueRichtung *= schub * schubKraft;
                SetzeGeschwindigkeit(geschwindigkeit + neueRichtung);
            }
        }

        public int AnzahlSchuesse()
        {
            int anzahl = 0;

            foreach (BasisWaffe waffe in _waffen)
                anzahl += waffe.AnzahlSchuesse();

            return anzahl;
        }

        public List<Schuss> AlleSchuesse()
        {
            List<Schuss> alleSchuesse = new List<Schuss>();

            foreach (BasisWaffe waffe in _waffen)
                alleSchuesse.AddRange(waffe.AlleSchuesse());

            return alleSchuesse;
        }

        public void BefehlZumFeuern()
        {
            foreach (BasisWaffe waffe in _waffen)
            {
                if (waffe.istSchussBereit)
                    waffe.BefehlZumFeuern();
            }
        }

        protected void CheckIntegritaet()
        {
            if (hp == 0 && istAktiv)
            {
                _istAktiv = false;

                foreach (BasisWaffe waffe in _waffen)
                    waffe.Deaktivieren();

                Effektmanager.HinzufuegenExplosion(weltposition, geschwindigkeit);
            }
        }

        public void WurdeGetroffen(Schuss schuss)
        {
            _zeitSeitLetzemTreffer = 0;

            if (_schild.schildRest - schuss.schaden > 0) // hat der Schuss mehr Schaden als Schildenergie übrig ist, wird dieses einfach durchschlagen
            {
                _schild.WurdeGetroffen(schuss.schaden);
                Effektmanager.HinzufuegenSchildeffekt(this, schuss);
            }
            else
            {
                _schiffsHuelle = MathHelper.Max(0, _schiffsHuelle - schuss.schaden);
                CheckIntegritaet();
            }
        }
        #endregion


        #region Update und Draw

        public override void Update(GameTime gameTime)
        {
            float vergangen = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _zeitSeitLetzemTreffer += vergangen;

            rotiereZu(_geschwindigkeit);//aktuallisiert die Rotation anhand der Geschwindigkeit
            base.Update(gameTime);

            if (geschwindigkeit != Vector2.Zero)
                Effektmanager.HinzufuegenAbgaseffekt(this);

            foreach (BasisWaffe waffe in _waffen)
                waffe.Update(gameTime);

            _schild.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (istAktiv)
                base.Draw(spriteBatch);

            foreach (BasisWaffe waffe in _waffen)
                waffe.Draw(spriteBatch);
        }
        #endregion
    }
}
