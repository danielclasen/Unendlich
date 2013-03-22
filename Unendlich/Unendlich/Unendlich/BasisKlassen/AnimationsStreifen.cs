using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class AnimationsStreifen
    {
        #region Deklaration

        private Texture2D _textur;
        private int _frameWeite;
        private int _frameHoehe;

        private float _frameZeit;
        private float _frameVerzoegerung;

        private int _aktuellerFrame;

        private bool _istAnimationEndlos; //bei true startet Animation stets von neuem
        private bool _istAnimationZuEnde; //bei Ende wird die naechsteAnimation ausgeführt

        private string _name;
        private string _naechsteAnimation;   //Animation die nach Ende folgt, wird nur bei "_istAnimationEndlos=false" gesetzt
        #endregion


        #region Eigenschaften

        public int frameWeite
        {
            get { return _frameWeite; }
        }

        public int frameHoehe
        {
            get { return _frameHoehe; }
        }
        
        public Texture2D texture
        {
            get { return _textur; }
        }

        public string name
        {
            get { return _name; }
        }

        public string naechsteAnimation
        {
            get { return _naechsteAnimation; }
            set { _naechsteAnimation = value; }
        }

        public bool istAnimationEndlos
        {
            get { return _istAnimationEndlos; }
            set { _istAnimationEndlos = value; }
        }

        public bool istAnimationZuEnde
        {
            get { return _istAnimationZuEnde; }
        }

        public int anzahlFrames
        {
            get { return texture.Width / frameWeite; }
        }

        public float frameLaenge
        {
            get { return _frameVerzoegerung; }
        }

        /// <summary>
        /// Ist Position auf der Textur
        /// </summary>
        public Rectangle frameRechteck
        {
            get { return new Rectangle(_aktuellerFrame * frameWeite, 0, frameWeite, frameHoehe); }
        }
        #endregion


        #region Konstruktoren

        public AnimationsStreifen(Texture2D textur, int frameWeite, string name)
            : this(textur, frameWeite, name, 0.5f)
        { }

        public AnimationsStreifen(Texture2D textur, int frameWeite, string name, float frameVerzoegerung)
            : this(textur, frameWeite, name, frameVerzoegerung, true)
        { }

        public AnimationsStreifen(Texture2D textur, int frameWeite, string name, float frameVerzoegerung, bool istAnimationEndlos)
        {
            _textur = textur;   //direkter Zugriff da "textur" nur Getter hat
            _frameWeite = frameWeite;
            _frameHoehe = textur.Height;
            _name = name;
            _istAnimationEndlos = istAnimationEndlos;
            _istAnimationZuEnde = false;
            _frameVerzoegerung = frameVerzoegerung;
        }
        #endregion


        #region Helfermethoden

        /// <summary>
        /// Startet Frame von anfang
        /// </summary>
        public void Start()
        {
            _aktuellerFrame = 0;
        }

        /// <summary>
        /// Startet Frame vom Parameter aus
        /// </summary>
        /// <param name="startFrame"></param>
        public void Start(int startFrame)
        {
            _aktuellerFrame = startFrame;
        }
        #endregion


        #region Update

        public void Update(GameTime gameTime)
        {
            float vergangen = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameZeit += vergangen;

            if (_frameZeit > _frameVerzoegerung)
            {
                _aktuellerFrame++;
                if (_aktuellerFrame >= anzahlFrames)
                {
                    if (istAnimationEndlos)
                        _aktuellerFrame = 0;
                    else
                    {
                        _aktuellerFrame = anzahlFrames - 1;
                        _istAnimationZuEnde = true;
                    }
                }
                _frameZeit = 0;
            }
        }
        #endregion
    }
}
