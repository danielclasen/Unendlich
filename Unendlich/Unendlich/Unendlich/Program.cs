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
 * Strukturelle Änderungen:
 *      -Sprite durch Spielobjekt ersetzten
 *      -Container bekommt absofort Texturen(die als Referenz zugewiesen werden)          
 * 
 * Effektmanager:
 *      -Animationen starten zufällig
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
 *              -dann sind auch mehrere Waffen möglich
 *      
 * Interface:
 *          -Schild und Lebenpunkte korrekt anzeigen
 *          
 * Explosionen:
 *          -Explosion war falsch angezeigt worden, da die Framegröße nicht geändert worden war  
 * 
 * Zu tun:
 * *******
 *      -Interface Klassendesign überarbeiten
 *      
 *      -weiter Schild/Schiffs und Waffentypen hinzufügen
 *      
 *      -Schiffstreffer erstellen
 *      
 *      -neue Zufallsfunktion einfügen, alte ersetzten
 *          -testen ob es mit einem Random über die Helferklasse geht
 *
 *      -Anzeige/Interface
 *          
 *          -gegebenfalls die Anzeige der Position überarbeiten
 * 
 *      -Effektmanager, Explosion und Effekt gegebenfalls überarbeitet
 *          -vielleicht Schildtreffer überarbeiten (wahrscheinlich nur Textur)
 *          -Sound einbauen
 *  
 *      -Asteroiden werden erstmal nachhinten verschoben
 *          -(Asteroiden eiern bei der Rotation)-->wahrscheinlich liegt das an skalierung
 *              -entweder die Seitenlängen belassen oder den Mittelpunkt anders berechnen
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

