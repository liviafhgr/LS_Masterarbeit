using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRaetsel1Skript : MonoBehaviour
{
    [SerializeField] private ChatInputHandler chatInputHandler; // Referenz auf das ChatInputHandler-Skript
    [SerializeField] private GameObject prefab2122; // Prefab 2.12.2
    [SerializeField] private GameObject prefab2131; // Prefab 2.13.1
    [SerializeField] private GameObject prefab2133; // Prefab 2.13.3
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

    private HashSet<string> erlaubteAntworten2131 = new HashSet<string>
    {
        "Festival della Castagna Special - Fuoco d'Autunno",
        "Festival della Castagna Special Fuoco d'Autunno",
        "Festival della Castagna Special Fuoco dAutunno",
        "Festival della Castagna Special Fuoco d Autunno",
        "Festival della Castagna Special  Fuoco dAutunno",
        "Festival della Castagna Special   Fuoco d Autunno",
        "Festival della Castagna Special- Fuoco d'Autunno",
        "Festival della Castagna Special -Fuoco d'Autunno",
        "Festival della Castagna Special – Fuoco d'Autunno",
        "Festival della Castagna Special - Fuoco d´Autunno",
        "Festival della Castagna Special - Fuoco d`Autunno",
        "festival della castagna special - fuoco d'autunno",
        "Festival Della Castagna Special - Fuoco D'Autunno",
        "FESTIVAL DELLA CASTAGNA SPECIAL - FUOCO D'AUTUNNO",
        "festival Della Castagna Special - Fuoco d'Autunno",
        "Fetival della Castagna Special - Fuoco d'Autunno",
        "Fetsival della Castagna Special - Fuoco d'Autunno",
        "Festiwal della Castagna Special - Fuoco d'Autunno",
        "Festival della Castaqna Special - Fuoco d'Autunno", 
        "Festival della Castangna Special - Fuoco d'Autunno",
        "Festival della Castangha Special - Fuoco d'Autunno",
        "Festival della Casstagna Special - Fuoco d'Autunno",
        "Festival della Castagna Specail - Fuoco d'Autunno",
        "Festival della Castagna Speical - Fuoco d'Autunno",
        "Festival della Castagna Speciale - Fuoco d'Autunno",
        "Festival dela Castagna Special - Fuoco d'Autunno",
        "Festival dele Castagna Special - Fuoco d'Autunno",
        "Festival dell Castagna Special - Fuoco d'Autunno",
        "Festival delle Castagna Special - Fuoco d'Autunno",
        "Festival della Castagna Special - Fuoco d'Autuno",
        "Festival della Castagna Special - Fuoco d'Atunno",
        "Festival della Castagna Special - Fuoco d'Autumno",
        "Festival della Castagna Special - Fuoco d'Autunna",
        "Festival della Castagna Special - Fuoco d'Autunnoo",
        "Festival della Castagna Special - Fuoco d'Auutunno",
        "Festival della Castagna Special - Fuoco di Autunno",
        "Festival della Castagna Special - Fuoco d Autunno",
        "Festival della Castagna Special - Fuoco dé Autunno",
        "Festival della Castagna Special - Fuco d'Autunno",
        "Festival della Castagna Special - Fuoco di'Autunno",
        "Festival della Castagna Special - Fuoco dell'Autunno",
        "Festival della Castagna Special - Fuocco d'Autunno",
        "Festival della Castagna Special - Fuocco d Autunno",
        "Festivaldella Castagna Special - Fuoco d'Autunno",
        "Festival dellaCastagna Special - Fuoco d'Autunno",
        "Festival della CastagnaSpecial - Fuoco d'Autunno",
        "Festival della Castagna Special-Fuoco d'Autunno",
        "Festival della Castagna Special -Fuoco d'Autunno",
        "Festival della Castagna Special - Fuocod'Autunno",
        "FestivaldellaCastagnaSpecial - Fuoco d'Autunno",
        "Festival della Castagna Special -FuocodAutunno",
        "FestivaldellaCastagnaSpecial-FuocodAutunno",
        "FestivaldellaCastagnaSpecialFuocodAutunno",
        "Festival della Castagna Special",
        "Festival della Castagna",
        "Festival della",
        "Festival",
        "della Castagna Special",
        "della Castagna",
        "della",
        "Castagna Special",
        "Castagna",
        "Special",
        "Fuoco d'Autunno",
        "Fuoco dAutunno",
        "Fuoco d Autunno",
        "Fuoco Autunno",
        "d'Autunno",
        "dAutunno",
        "d Autunno",
        "Autunno",
        "Fuoco",
        "Festival Fuoco",
        "Festival Autunno",
        "Castagna Fuoco",
        "Castagna d'Autunno",
        "Special Fuoco",
        "Special d'Autunno",
        "Festival della Fuoco",
        "della Castagna Fuoco",
        "della Special Fuoco",
        "Festival Special d'Autunno",
        "Festivale della Castagna Special - Fuoco d'Autunno",
        "Festival della Chastagna Special - Fuoco d'Autunno",
        "Festival della Kastagna Special - Fuoco d'Autunno",
        "Festival della Castagna Spezial - Fuoco d'Autunno",
        "Festival della Castagna Especial - Fuoco d'Autunno",
        "Festival della Castagna Special - Fuego d'Autunno",
        "Festival della Castagna Special - Fuoco de Autunno",
        "Festival della Castagna Special - Fuoco d'Outunno",
        "Festival della Castagna Special - Fuoco d'Ottunno",
        "Festival della Castagna Special - Fuoco d'Autuno",
        "Fastival della Castagna Special - Fuoco d'Autunno",
        "Festivel della Castagna Special - Fuoco d'Autunno",
        "Festival dela Castagna Special - Fuoco d'Autunno",
        "Festival dell Castagna Special - Fuoco d'Autunno",
        "Festival della Kastagna Special - Fuoco d'Autunno",
        "Festival della Castania Special - Fuoco d'Autunno",
        "Festival della Castagnia Special - Fuoco d'Autunno",
        "Festival della Castagna Espezial - Fuoco d'Autunno",
        "Festival della Castagna Spezial - Fuoco d'Autunno",
        "Festival della Castagna Spec1al - Fuoco d'Autunno",
        "Festival della Ca$tagna Special - Fuoco d'Autunno",
        "Festival della Castagna Special - Fu0co d'Autunno",
        "Festival della Castagna Special - Fuoco d'Autun0",
        "Festivall della Castagna Special - Fuoco d'Autunno",
        "Festival della Castanja Special - Fuoco d'Autunno",
        "Festival della Castagna Spesial - Fuoco d'Autunno",
        "Festival della Castagna Special - Fuoko d'Autunno",
        "Festival della Castagna Special - Fuoco d'Ottunno",
        "Festival  della  Castagna  Special  -  Fuoco  d'Autunno",
        " Festival della Castagna Special - Fuoco d'Autunno ",
        "  Festival  della  Castagna  Special  -  Fuoco  d'Autunno  ",
        "della Festival Castagna Special - Fuoco d'Autunno",
        "Festival Castagna della Special - Fuoco d'Autunno",
        "Festival della Special Castagna - Fuoco d'Autunno",
        "Festival della Castagna Special - d'Autunno Fuoco",
        "Festival delle Castagne Special - Fuoco d'Autunno",
        "Festival della Castagna Speciale - Fuoco d'Autunno",
        "Festival della Castagna Special - Fuoco di Autunno",
        "Festival della Castagna Special - Fuoco dell'Autunno",
        "Festa della Castagna Special - Fuoco d'Autunno"
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

                bool found = false;

                // Prüfung für prefab2122
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
                        found = true;
                        break;
                    }
                }

                // Prüfung für prefab2131
                if (!found)
                {
                    foreach (var antwort in erlaubteAntworten2131)
                    {
                        string cleanedAntwort = antwort.Trim()
                            .Replace("’", "'")
                            .Replace("`", "'")
                            .Replace("´", "'")
                            .Trim('\u00A0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000');

                        if (string.Equals(cleanedInput, cleanedAntwort, System.StringComparison.OrdinalIgnoreCase))
                        {
                            Debug.Log("Gültige Antwort erkannt, Prefab 2.13.1 wird instanziiert!");
                            if (prefab2131 != null && receiveContentEinführungsszene != null)
                                Instantiate(prefab2131, receiveContentEinführungsszene);
                            found = true;
                            break;
                        }
                    }
                }

                // Prüfung für prefab2133 (alle anderen Eingaben, außer leer)
                if (!found && !string.IsNullOrEmpty(cleanedInput))
                {
                    Debug.Log("Ungültige Antwort, Prefab 2.13.3 wird instanziiert!");
                    if (prefab2133 != null && receiveContentEinführungsszene != null)
                        Instantiate(prefab2133, receiveContentEinführungsszene);
                }
            }
        }
    }

    public void PruefePlayerInputNachSenden()
    {
        if (chatInputHandler != null)
        {
            string aktuellerInput = chatInputHandler.playerInput;

            // Vereinheitliche und trimme den Input
            string cleanedInput = aktuellerInput.Trim()
                .Replace("’", "'")
                .Replace("`", "'")
                .Replace("´", "'")
                .Replace("-", "–") // falls Bindestrich-Varianten
                .Replace("–", "-")
                .Replace("—", "-")
                .Replace("  ", " ")
                .Replace("   ", " ")
                .Trim('\u00A0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000');

            if (!string.IsNullOrEmpty(cleanedInput))
            {
                // Prüfung für 2.13.1
                foreach (var antwort in erlaubteAntworten2131)
                {
                    string cleanedAntwort = antwort.Trim()
                        .Replace("’", "'")
                        .Replace("`", "'")
                        .Replace("´", "'")
                        .Replace("-", "–")
                        .Replace("–", "-")
                        .Replace("—", "-")
                        .Replace("  ", " ")
                        .Replace("   ", " ")
                        .Trim('\u00A0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000');

                    if (string.Equals(cleanedInput, cleanedAntwort, System.StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log("Gültige Antwort für 2.13.1 erkannt, Prefab 2.13.1 wird instanziiert!");
                        if (prefab2131 != null && receiveContentEinführungsszene != null)
                            Instantiate(prefab2131, receiveContentEinführungsszene);
                        break;
                    }
                }
            }
        }
    }
}
