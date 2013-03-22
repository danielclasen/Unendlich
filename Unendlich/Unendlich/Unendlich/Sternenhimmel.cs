using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unendlich
{
    static public class Sternenhimmel
    {
        #region Deklaration

        private static List<List<HintergrundStern>> _sterne;
        private static int _anzahlEbenen = 5;
        private static List<int> _anzahlSterne;

        private static List<SpielObjekt> _hintergrund;

        private static Random rand = new Random();
        private static Color[] _farben = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray }; // alle möglichen Sternfarben
        #endregion


        #region Initialisierung

        public static void Init()
        {
            rand = new Random();

            _sterne = new List<List<HintergrundStern>>();
            _anzahlSterne = new List<int>();
            int sternenKante = 0;   //da sterne quatratisch sind steht dieser Wert für Höhe und Breite
                                    //ebenfalls gibt er das dazu gehörige Bild an


            for (int i = 0; i < _anzahlEbenen; i++)
            {
                switch (i)//gibt Anzahl der Sterne pro Ebene an
                {
                    case 0:
                        _anzahlSterne.Add(Kamera.sichtfeldBreite / 10);
                        break;

                    case 1:
                        _anzahlSterne.Add(Kamera.sichtfeldBreite / 16);
                        break;

                    case 2:
                        _anzahlSterne.Add(Kamera.sichtfeldBreite / 20);
                        break;

                    case 3:
                        _anzahlSterne.Add(Kamera.sichtfeldBreite / 32);
                        break;

                    case 4:
                        _anzahlSterne.Add(Kamera.sichtfeldBreite / 40);
                        break;
                }

                _sterne.Add(new List<HintergrundStern>());

                if (i == 0 || i == 1)
                    sternenKante = 1;
                else if (i == 2 || i == 3)
                    sternenKante = 2;
                else
                    sternenKante = 3;

                for (int j = 0; j < _anzahlSterne[i]; j++)
                {
                    Vector2 neuePosition = Vector2.Zero;

                    neuePosition.X = rand.Next(0, Kamera.sichtfeldBreite + 1);
                    neuePosition.Y = rand.Next(0, Kamera.sichtfeldHoehe + 1);

                    _sterne[i].Add(new HintergrundStern(Kamera.ScreenAufWelt(neuePosition), 0.9f - i * 0.01f, "Stern" + sternenKante.ToString()));
                    _sterne[i][j].farbe = ZufallsFarbe();
                }
            }
        }
        #endregion


        #region Helfermethoden

        private static Color ZufallsFarbe()
        {
            return _farben[rand.Next(0, _farben.Length)];
        }
        #endregion


        #region Update und Draw

        public static void Update(GameTime gameTime)
        {
            if (Kamera.geschwindigkeit != Vector2.Zero)
            {
                //jede Ebene wird mit jedem Stern durchgegangen
                for (int i = 0; i < _anzahlEbenen; i++)
                    for (int j = 0; j < _anzahlSterne[i]; j++)
                    {
                        //wenn Stern auserhalb von Sichtfeld, wird neu gezeichnet und Farbe neu zugewiesen
                        if (!Kamera.IstObjektSichtbar(_sterne[i][j].objektRechteck))
                        {
                            _sterne[i][j].weltpositionAendern = Helferklasse.GibNeuePosition();
                            _sterne[i][j].farbe = ZufallsFarbe();
                        }

                        switch (i)
                        {
                            case 0:
                                _sterne[i][j].geschwindigkeit = Kamera.geschwindigkeit / 2;
                                break;
                            case 1:
                                _sterne[i][j].geschwindigkeit = Kamera.geschwindigkeit / 3;
                                break;
                            case 2:
                                _sterne[i][j].geschwindigkeit = Kamera.geschwindigkeit / 4;
                                break;
                            case 3:
                                _sterne[i][j].geschwindigkeit = Kamera.geschwindigkeit / 5;
                                break;
                            case 4:
                                _sterne[i][j].geschwindigkeit = Vector2.Zero;
                                break;
                        }

                        _sterne[i][j].Update(gameTime);
                    }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)//malt jeden Stern
        {
            foreach (List<HintergrundStern> ebene in _sterne)
                foreach (HintergrundStern stern in ebene)
                    stern.Draw(spriteBatch);
        }
        #endregion
    }
}
