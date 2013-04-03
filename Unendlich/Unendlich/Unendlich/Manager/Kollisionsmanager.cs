using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unendlich
{
    public class Kollisionsmanager
    {
        
        #region Helfer Methoden

        protected static void SchussTrifftNPC()
        {
            for (int i = 0; i < Spielmanager.weltall[0].alleNPCs.Count; i++)
            {
                for (int k = 0; k < Spielmanager.weltall[0].alleNPCs.Count; k++)
                {
                    if (i == k)//wenn i==j wahr ist, handelt es sich um den selben Gegener (Gegner soll sich nicht selbst abschießen können)
                        continue;
                    else
                    {
                        foreach (Schuss schuss in Spielmanager.weltall[0].alleNPCs[i].aktuellesSchiff.AlleSchuesse())
                        {
                            if (Spielmanager.weltall[0].alleNPCs[k].aktuellesSchiff.IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                            {
                                Spielmanager.weltall[0].alleNPCs[k].aktuellesSchiff.WurdeGetroffen(schuss);
                                schuss.HatGetroffen();
                            }
                        }
                    }
                }

                foreach (Schuss schuss in Spielmanager.weltall[0].alleSpieler[0].aktuellesSchiff.AlleSchuesse())
                {
                    if (Spielmanager.weltall[0].alleNPCs[i].aktuellesSchiff.IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                    {
                        Spielmanager.weltall[0].alleNPCs[i].aktuellesSchiff.WurdeGetroffen(schuss);
                        schuss.HatGetroffen();

                        //Hier können nachher Punkte vergeben werden
                    }
                }
            }
        }

        protected static List<Schuss> AlleSchuesse()
        {
            List<Schuss> alleSchuesse = new List<Schuss>();
            alleSchuesse.AddRange(Spielmanager.weltall[0].alleSpieler[0].aktuellesSchiff.AlleSchuesse());


            foreach (NPC gegner in Spielmanager.weltall[0].alleNPCs)
                alleSchuesse.AddRange(gegner.aktuellesSchiff.AlleSchuesse());

            return alleSchuesse;
        }

        protected static void SchussTrifftSpieler()
        {

            foreach (NPC gegner in Spielmanager.weltall[0].alleNPCs)
                foreach (Schuss schuss in gegner.aktuellesSchiff.AlleSchuesse())
                {
                    if (Spielmanager.weltall[0].alleSpieler[0].aktuellesSchiff.IstKreisKollision(schuss.weltMittelpunkt, schuss.kollisionsRadius))
                    {
                        //Zu Testzwecken kann der Spieler nicht zerstört werden
                        Spielmanager.weltall[0].alleSpieler[0].aktuellesSchiff.WurdeGetroffen(schuss);
                        schuss.HatGetroffen();
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
            SchussTrifftNPC();
            SchussTrifftSpieler();
            SchussTrifftSchuss();

            //temp
            //schussTrifftAsteroid();
        }
        #endregion
    }
}
