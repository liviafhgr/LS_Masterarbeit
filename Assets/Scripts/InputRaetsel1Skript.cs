using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRaetsel1Skript : MonoBehaviour
{
    [SerializeField] private ChatInputHandler chatInputHandler; // Referenz auf das ChatInputHandler-Skript
    [SerializeField] private GameObject prefab2122; // Prefab 2.12.2
    [SerializeField] private Transform receiveContentEinführungsszene; // Ziel-Container

    private string gespeicherterPlayerInput = "";

    // Die Liste deiner erlaubten Strings
    private HashSet<string> erlaubteAntworten = new HashSet<string>
    {
        "Fiamme dell'Oscurità","Fiamme dell Oscurità","Fiamme dellOscurità","Fiamme dell'Oscurita","Fiamme dell'Oscuritá","Fiamme dell'Oscuritä","Fiamme dell'Oscurita","Fiamme dell´Oscurità","Fiamme dell`Oscurità","fiamme dell'oscurità","FIAMME DELL'OSCURITÀ","Fiamme Dell'Oscurità","Fiamme Oscurità","dell'Oscurità","Fiamme dell","FiammedellOscurità","Fiamme dellOscurita","Fiamme del Oscurità","Fiamme dele Oscurità","Fiamme delle Oscurità","Fiamme della Oscurità","Fiamme di Oscurità","Fiame dell'Oscurità","Fiamme dell'Oscuritaa","Fiamme dell'Oscuritah","Fiamme dell'Osccurità","Fiamme dell'Osurità","Fiamme dell'Oscuurità","Fiamme dell'Oskurità","Fiamme dell'Oscorità","Fiamme dell'Oscrità","Fiamme","Oscurità","Fiamme del Oscurita",
        "Notte delle Streghe","Notte delle streghe","NOTTE DELLE STREGHE","Notte Delle Streghe","notte delle streghe","Notte delle Streghe","Note delle Streghe","Notte dele Streghe","Notte della Streghe","Notte delle Stregge","Notte delle Strege","Notte delle Streghi","Notte delle Streche","Notte delle Stretze","Notte delle Stregh","NottedelleStreghe","Notte delleStreghe","Nottedelle Streghe","Notte  delle  Streghe"," Notte delle Streghe ","Notte del Streghe","Notte di Streghe","Notte delle Streghe ","Notte","delle Streghe","Streghe","Notte delle","Notto delle Streghe","Notte delle Stregher","Notte delle Strege","Notte delle Streche","Notte delle Strege","Notte delli Streghe",
        "Estate Infuocata: La Fiamma Dance Party","Estate Infuocata La Fiamma Dance Party","Estate Infuocata:La Fiamma Dance Party","Estate Infuocata: LaFiamma Dance Party","Estate Infuocata: La Fiamma DanceParty","Estate Infuocata: La Fiamma Dance-Party","Estate Infuocata - La Fiamma Dance Party","Estate Infuocata. La Fiamma Dance Party","Estate Infuocata; La Fiamma Dance Party","estate infuocata: la fiamma dance party","ESTATE INFUOCATA: LA FIAMMA DANCE PARTY","Estate Infuocata: La Fiamma Dance","Estate Infuocata: La Fiamma","Estate Infuocata: Dance Party","Infuocata: La Fiamma Dance Party","La Fiamma Dance Party","Estate Infuocata","EstateInfuocata: La Fiamma Dance Party","Estate Infuocata:LaFiamma Dance Party","Estate Infuocata: La FiammaDance Party","Estate Infuocata: LaFiammaDanceParty","EstateInfuocata:LaFiammaDanceParty","Estate Infuocata : La Fiamma Dance Party","Estate Infuocata : La Fiamma Dance Party","Estate Infuoccata: La Fiamma Dance Party","Estate Infuocatta: La Fiamma Dance Party","Estate Infiocata: La Fiamma Dance Party","Estate Infuokata: La Fiamma Dance Party","Estate Infuocata: Le Fiamma Dance Party","Estate Infuocata: La Fiame Dance Party","Estate Infuocata: La Fiamma Danze Party","Estate Infuocata: La Fiamma Danc Party","Estate Infuocata: La Fiama Dance Party","Estat Infuocata: La Fiamma Dance Party","Estate Infuoccata: La Fiamma Dance Party","Estate Infiocata: La Fiamma Dance Party","Estate Infuocata: La Fiamma Dance Perty","Estate Infuocata: La Fiamma Dance Parti","Estate Infuocata: La Fiamma Dance Parte",
        "La Fiamma Dance Party","la fiamma dance party","LA FIAMMA DANCE PARTY","La Fiamma dance party","La fiamma Dance Party","La Fiamma Dance party","LaFiamma Dance Party","La FiammaDance Party","La Fiamma DanceParty","LaFiammaDanceParty","La Fiamma Dance-Party","La Fiamma Dance Party","La Fiamma Dance-Party","La Fiamma Danse Party","La Fiamma Dance Parti","La Fiama Dance Party","La Fiamma Danc Party","La Fiamma Dance Pary","La Fiamma Dance Partty","La Fiamma Dance Parry","La Fiamme Dance Party","Le Fiamma Dance Party","La Fiama Dance Party","La Fiammaa Dance Party","La Fiamma","Dance Party","Party","Fiamma Dance","La Dance Party",
        "Estate Infuocata","estate infuocata","ESTATE INFUOCATA","Estate infuocata","Estate Infuocata "," Estate Infuocata","EstateInfuocata","Estate-Infuocata","Estate_Infuocata","Estate.Infuocata","Estate,Infuocata","Estate Infuoccata","Estate Infuocatta","Estate Infuokata","Estate Infiocata","Estate Infuocada","Estat Infuocata","Estate Infokata","Estat Infokata","Estata Infuocata","Estade Infuocata","Estate","Infuocata","Estado Infuocata","Estate Inffuocata"
    };

    void Update()
    {
        if (chatInputHandler != null)
        {
            string aktuellerInput = chatInputHandler.playerInput;

            // Entferne ALLE Arten von Leerzeichen am Anfang und Ende (auch Unicode)
            string cleanedInput = aktuellerInput.Trim()
                .Replace("’", "'")
                .Replace("`", "'")
                .Replace("´", "'")
                .Trim('\u00A0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000');

            // Nur speichern, wenn nicht leer und anders als der gespeicherte Wert
            if (!string.IsNullOrEmpty(cleanedInput) && cleanedInput != gespeicherterPlayerInput)
            {
                gespeicherterPlayerInput = cleanedInput;
                Debug.Log("Player Input gespeichert: '" + gespeicherterPlayerInput + "'");

                // Prüfe, ob der Input in der erlaubten Liste ist (case-insensitive)
                foreach (var antwort in erlaubteAntworten)
                {
                    string cleanedAntwort = antwort.Trim()
                        .Replace("’", "'")
                        .Replace("`", "'")
                        .Replace("´", "'")
                        .Trim('\u00A0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000');

                    if (string.Equals(cleanedInput, cleanedAntwort, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log("Gültige Antwort erkannt, Prefab 2.12.2 wird instanziiert!");
                        if (prefab2122 != null && receiveContentEinführungsszene != null)
                            Instantiate(prefab2122, receiveContentEinführungsszene);
                        break;
                    }
                }
            }
        }
    }
}
