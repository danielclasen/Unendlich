using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public class Kollisionsmanager
    {
        #region Deklaration

        private static Spieler _spieler;
        #endregion


        #region Init

        public static void Init(Spieler spieler)
        {
            _spieler = spieler;
        }
        #endregion


        #region Helfer Methoden

        protected static void SchussTrifftGegner()
        {
            for (int i = 0; i < Gegnermanager.alleGegner.Count; i++)
            {
                for (int j = 0; j < Gegnermanager.alleGegner.Count; j++)
                {
                    if (i == j)//wenn i==j wahr ist, handelt es sich um den selben Gegener (Gegner soll sich nicht selbst abschießen können)
                        continue;
                    else
                    {
                        foreach (Schuss schuss in Gegnermanager.alleGegner[i].AlleSchuesse())
                        {
                            if (Gegnermanager.alleGegner[j].IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                            {
                                Gegnermanager.alleGegner[j].WurdeGetroffen(schuss);
                                schuss.HatGetroffen();
                            }
                        }
                    }
                }

                foreach (Schuss schuss in _spieler.aktuellesSchiff.AlleSchuesse())
                {
                    if (Gegnermanager.alleGegner[i].IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                    {
                        Gegnermanager.alleGegner[i].WurdeGetroffen(schuss);
                        schuss.HatGetroffen();

                        //Hier können nachher Punkte vergeben werden
                    }
                }
            }
        }

        protected static List<Schuss> AlleSchuesse()
        {
            List<Schuss> alleSchuesse = new List<Schuss>();
            alleSchuesse.AddRange(_spieler.aktuellesSchiff.AlleSchuesse());

            foreach (Raumschiff gegner in Gegnermanager.alleGegner)
                alleSchuesse.AddRange(gegner.AlleSchuesse());

            return alleSchuesse;
        }

        protected static void SchussTrifftSpieler()
        {
            foreach (Raumschiff gegner in Gegnermanager.alleGegner)
            {
                foreach (Schuss schuss in gegner.AlleSchuesse())
                {
                    if (_spieler.aktuellesSchiff.IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                    {
                        //Zu Testzwecken kann der Spieler nicht zerstört werden
                        _spieler.aktuellesSchiff.WurdeGetroffen(schuss);
                        schuss.HatGetroffen();
                    }
                }
            }
        }

        protected static void SchussTrifftSchuss()
        {
            List<Schuss> alleSchuesse = AlleSchuesse();

            for (int i = 0; i < alleSchuesse.Count; i++)
                for (int j = 0; j < alleSchuesse.Count; j++)
                {
                    if (i == j)//ein Schuss kann sich nicht selbst treffen
                        continue;

                    if (alleSchuesse[i].IstRechteckKollision(alleSchuesse[j].objektRechteck))
                    {
                        alleSchuesse[i].HatGetroffen();
                        alleSchuesse[j].HatGetroffen();
                    }
                }
        }

        /*
        protected static void SchussTrifftAsteroid()
        {
            List<Schuss> alleSchuesse = AlleSchuesse();
            //temp
            List<Asteroid> astFeld = Asteroidenfeld.GETAlleAsteroiden();

            for (int i = 0; i < alleSchuesse.Count; i++)
                for (int j=0; j<astFeld.Count;j++)
            {
                if (alleSchuesse[i].IstKreisKollision(astFeld[j].weltMittelpunkt,astFeld[j].kollisionsRadius))
                {
                    alleSchuesse[i].HatGetroffen();
                }
            }
        }*/

        #endregion


        #region Update

        public static void Update(GameTime gameTime)
        {
            SchussTrifftGegner();
            SchussTrifftSpieler();
            SchussTrifftSchuss();

            //temp
            //schussTrifftAsteroid();
        }
        #endregion
    }
}
