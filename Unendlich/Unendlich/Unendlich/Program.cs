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
 * KI:
 *      -es wird geschossen
 * 
 * Anzeige/Interface
 *      -gegebenfalls die Anzeige der Position überarbeiten
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
 *          -Strukturänderung
 *              -Alle Objekte eines Sektors kommen in eine Liste, die dann durch gegangen wird
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
 *      -KI.SchiesseBeiZiel muss überarbeitet werden
 *          -Nicht schießen bei verbündetem
 *              -muss wahrscheinlich an die Waffe gehangen werden
 *              
 *      -Waffe muss wahrscheinlich überarbeitet werden
 * 
 *      -Momentan wird das Spiel ab etwa 60 NPCs unspielbar
 * 
 *      -Interface Klassendesign überarbeiten
 *      
 *      -weiter Schild/Schiffs und Waffentypen hinzufügen
 *      
 *      -Schiffstreffer erstellen
 *      
 *      -neue Zufallsfunktion einfügen, alte ersetzten
 *          -testen ob es mit einem Random über die Helferklasse geht
 *
 * 
 *      -Effektmanager, Explosion und Effekt gegebenfalls überarbeitet
 *          -vielleicht Schildtreffer überarbeiten (wahrscheinlich nur Textur)
 *          -Sound einbauen
 *  
 *      -Asteroiden werden erstmal nachhinten verschoben
 *          -(Asteroiden eiern bei der Rotation)-->wahrscheinlich liegt das an skalierung
 *              -entweder die Seitenlängen belassen oder den Mittelpunkt anders berechnen
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

