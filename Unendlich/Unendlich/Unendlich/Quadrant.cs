using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unendlich
{
    public class Quadrant
    {
        #region Deklaration

        protected List<List<Einheit>> _einheitenListe;
        protected List<SpielObjekt> _objektListe;
        #endregion


        #region Eigenschaften

        public List<Einheit> alleSpieler
        {
            get { return _einheitenListe[0]; }
        }

        public List<Einheit> alleNPCs
        {
            get { return _einheitenListe[1]; }
        }

        public List<Einheit> alleEinheiten
        {
            get
            {
                List<Einheit> tempEinheiten = new List<Einheit>();

                foreach (List<Einheit> einheitenListe in _einheitenListe)
                {
                    tempEinheiten.AddRange(einheitenListe);
                }

                return tempEinheiten;
            }
        }
        #endregion


        #region Konstruktor

        public Quadrant()
        {
            _einheitenListe = new List<List<Einheit>>();
            _einheitenListe.Add(new List<Einheit>());//Spielerliste
            _einheitenListe.Add(new List<Einheit>());//NPCliste
        }
        #endregion


        #region Methoden
        public void HinzufuegenSpieler(Spieler neuerSpieler)
        {
            _einheitenListe[0].Add(neuerSpieler);
        }

        public void HinzufuegenNPC(NPC neuerNpc)
        {
            _einheitenListe[1].Add(neuerNpc);
        }
        #endregion


        #region Update und Draw
        
        public virtual void Update(GameTime gameTime)
        {
            foreach (List<Einheit> einheitenListe in _einheitenListe)
            {
                for(int i=einheitenListe.Count-1;i>=0;i--)
                    
                {
                    if (einheitenListe[i].istAktiv)
                    {
                        einheitenListe[i].Update(gameTime);
                    }
                    else
                    {
                        if (!(einheitenListe[i] is Spieler))
                            einheitenListe.RemoveAt(i);
                        //temporär
                        else
                            einheitenListe[i].Update(gameTime);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<Einheit> einheitenListe in _einheitenListe)
            {
                foreach (Einheit einheit in einheitenListe)
                {
                    einheit.Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}
