/*Geschafft:
 * **********
 * 
 * Allgemein:
 *      -Texturen sind immer static
 *          -daher wird weniger Speicher verbraucht und LoadContent muss nur einmal aufgerufen werden
 *      -Das Klassenmodell steht mehr oder weniger
 *          -SchusstTrifftGegner berichtigt
 *      -Schild fertig implementieren
 *      
 *          
 * Strukturelle �nderungen:
 *      -Sprite durch Spielobjekt ersetzten
 *      -Container bekommt absofort Texturen(die als Referenz zugewiesen werden)          
 * 
 * Effektmanager:
 *      -Animationen starten zuf�llig
 *
 * 
 * Behobene Bug:
 * *************      
 *      
 * Allgemein:
 *          -Alles wurde falsch gezeichnet
 *              -durch Angabe des Mittelpunkts als Rotationspunkt musste auch der Zeichenpunkt als Mitte und nicht mehr als Position(linke, obere Ecke) angegeben werden
 *          -da die Animationen statisch sind, hat jedes Raumschiff die selbe Zeit seit dem letzten Frame
 *              -in der Folge werden die Animationen immer schneller durchlaufen
 *      
 * Schiffswaffen:
 *          -Waffenrotation berichtigen!!!
 *              -dann sind auch mehrere Waffen m�glich
 *      
 * Interface:
 *          -Schild und Lebenpunkte korrekt anzeigen
 *          
 * Explosionen:
 *          -Explosion war falsch angezeigt worden, da die Framegr��e nicht ge�ndert worden war  
 * 
 * Zu tun:
 * *******
 *      -Interface Klassendesign �berarbeiten
 *      
 *      -weiter Schild/Schiffs und Waffentypen hinzuf�gen
 *      
 *      -Schiffstreffer erstellen
 *      
 *      -neue Zufallsfunktion einf�gen, alte ersetzten
 *          -testen ob es mit einem Random �ber die Helferklasse geht
 *
 *      -Anzeige/Interface
 *          
 *          -gegebenfalls die Anzeige der Position �berarbeiten
 * 
 *      -Effektmanager, Explosion und Effekt gegebenfalls �berarbeitet
 *          -vielleicht Schildtreffer �berarbeiten (wahrscheinlich nur Textur)
 *          -Sound einbauen
 *  
 *      -Asteroiden werden erstmal nachhinten verschoben
 *          -(Asteroiden eiern bei der Rotation)-->wahrscheinlich liegt das an skalierung
 *              -entweder die Seitenl�ngen belassen oder den Mittelpunkt anders berechnen
 *      BUGS:
 *          
 *     
 * */


using System;

namespace Unendlich
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

