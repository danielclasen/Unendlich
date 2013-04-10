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
 * KI:
 *      -es wird geschossen
 * 
 * Anzeige/Interface
 *      -gegebenfalls die Anzeige der Position �berarbeiten
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
 *          -Struktur�nderung
 *              -Alle Objekte eines Sektors kommen in eine Liste, die dann durch gegangen wird
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
 *      -KI.SchiesseBeiZiel muss �berarbeitet werden
 *          -Nicht schie�en bei verb�ndetem
 *              -muss wahrscheinlich an die Waffe gehangen werden
 *              
 *      -Waffe muss wahrscheinlich �berarbeitet werden
 * 
 *      -Momentan wird das Spiel ab etwa 60 NPCs unspielbar
 * 
 *      -Interface Klassendesign �berarbeiten
 *      
 *      -weiter Schild/Schiffs und Waffentypen hinzuf�gen
 *      
 *      -Schiffstreffer erstellen
 *      
 *      -neue Zufallsfunktion einf�gen, alte ersetzten
 *          -testen ob es mit einem Random �ber die Helferklasse geht
 *
 * 
 *      -Effektmanager, Explosion und Effekt gegebenfalls �berarbeitet
 *          -vielleicht Schildtreffer �berarbeiten (wahrscheinlich nur Textur)
 *          -Sound einbauen
 *  
 *      -Asteroiden werden erstmal nachhinten verschoben
 *          -(Asteroiden eiern bei der Rotation)-->wahrscheinlich liegt das an skalierung
 *              -entweder die Seitenl�ngen belassen oder den Mittelpunkt anders berechnen
 *              
 *      -Wie sollen die Einheiten nachher mal verwaltet werden??
 *      
 *      -KI zuende enwickeln
 * 
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

