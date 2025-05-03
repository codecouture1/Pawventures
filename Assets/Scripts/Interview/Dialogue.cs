using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // Panel und Text für beide Sprecher
    public GameObject panelPlayer;
    public GameObject panelInterviewer;

    public TextMeshProUGUI textPlayer;
    public TextMeshProUGUI textInterviewer;

    // Dialogabschnitte und Verzögerung für das Tippen der Buchstaben
    public float textSpeed = 0.05f;

    // Manuelle Textaufteilung
    private string[][] dialogue = new string[][]
    {
        new string[] { "SPIELER/IN", "Hey, schön dich zu treffen! Du arbeitest mit Hunden aus dem Ausland, oder?" },
        new string[] { "TIERSCHÜTZERIN", "Ja, genau. Ich helfe vor allem in Ländern wie Rumänien oder Ungarn, wo es leider immer noch sehr viele Straßenhunde gibt." },
        new string[] { "TIERSCHÜTZERIN", "Es ist manchmal richtig hart, aber auch unglaublich schön zu sehen, wie aus einem ängstlichen kleinen Wesen ein fröhlicher Familienhund wird." },
        new string[] { "SPIELER/IN", "Das klingt total bewegend. Wie läuft das denn ab – so eine Rettung?" },
        new string[] { "TIERSCHÜTZERIN", "Ganz unterschiedlich. Manchmal rufen Leute an, die einen Hund gefunden haben." },
        new string[] { "TIERSCHÜTZERIN", "Manchmal streifen unsere Tierschützer*innen vor Ort selbst durch bestimmte Gegenden." },
        new string[] { "TIERSCHÜTZERIN", "Wenn sie einen Hund finden, bringen sie ihn erst mal in Sicherheit – meistens in ein Shelter." },
        new string[] { "TIERSCHÜTZERIN", "Dort wird er untersucht, versorgt, geimpft… und dann beginnt das Warten auf eine passende Pflegestelle oder ein Zuhause." },
        new string[] { "SPIELER/IN", "Und warum landen so viele Hunde überhaupt auf der Straße?" },
        new string[] { "TIERSCHÜTZERIN", "Puh, das hat viele Gründe. Manche Hunde werden einfach ausgesetzt – oft, weil die Leute sich nicht kümmern wollen oder können." },
        new string[] { "TIERSCHÜTZERIN", "Andere sind auf der Straße geboren." },
        new string[] { "TIERSCHÜTZERIN", "Vielen Menschen fehlt es an Aufklärung und in fast allen Gegenden mangelt es an Kastrationsmöglichkeiten." },
        new string[] { "SPIELER/IN", "Das ist traurig. Wie lange bleiben die Hunde denn in einem Shelter, bevor sie vermittelt werden?" },
        new string[] { "TIERSCHÜTZERIN", "Manche nur ein paar Wochen, andere viele Monate oder gar Jahre." },
        new string[] { "TIERSCHÜTZERIN", "Je nachdem, wie alt sie sind, wie zutraulich oder ängstlich, oder ob sie bestimmte Krankheiten oder Handicaps haben." },
        new string[] { "TIERSCHÜTZERIN", "Aber wir setzen die Hunde nicht unter Druck. Sie dürfen ankommen und sich erholen." },
        new string[] { "SPIELER/IN", "Und wenn jemand einen Hund adoptieren möchte – wie geht das?" },
        new string[] { "TIERSCHÜTZERIN", "Ganz entspannt. Erst mal lernen wir die Interessierten ein bisschen kennen." },
        new string[] { "TIERSCHÜTZERIN", "Dann schauen wir, welcher Hund passen könnte." },
        new string[] { "TIERSCHÜTZERIN", "Es gibt viele Gespräche und auch einen Besuch vor Ort bei den Interessierten." },
        new string[] { "TIERSCHÜTZERIN", "Wenn alles stimmig ist, organisieren wir die Ausreise." },
        new string[] { "TIERSCHÜTZERIN", "Natürlich ist der Hund dann geimpft, gechippt und hat einen Pass – alles, was dazugehört." },
        new string[] { "SPIELER/IN", "Und wenn man gerade keinen Hund adoptieren kann?" },
        new string[] { "TIERSCHÜTZERIN", "Dann gibt’s trotzdem viele Möglichkeiten zu helfen! Pflegestellen sind zum Beispiel Gold wert." },
        new string[] { "TIERSCHÜTZERIN", "Oder man übernimmt eine Patenschaft, spendet Futter oder Geld – oder hilft einfach beim Teilen von Vermittlungsposts." },
        new string[] { "TIERSCHÜTZERIN", "Alles hilft." },
        new string[] { "SPIELER/IN", "Voll schön. Gibt’s irgendwas, das du unseren Spieler*innen gern mitgeben würdest?" },
        new string[] { "TIERSCHÜTZERIN", "Ja. Wenn ihr Tiere liebt: Tut etwas. Auch kleine Gesten können Großes bewirken." },
        new string[] { "TIERSCHÜTZERIN", "Und wenn ihr euch mal für ein Tier entscheidet – dann bitte mit Herz und Verstand." },
        new string[] { "TIERSCHÜTZERIN", "Es ist eine Verantwortung fürs Leben, aber sie wird mit so viel Liebe belohnt." }
    };

    private int currentLine = 0;

    void Start()
    {
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        while (currentLine < dialogue.Length)
        {
            string speaker = dialogue[currentLine][0];
            string line = dialogue[currentLine][1];

            // Bestimme, welcher Sprecher spricht
            if (speaker == "SPIELER/IN")
            {
                panelPlayer.SetActive(true);
                panelInterviewer.SetActive(false);
                textPlayer.text = "";
                yield return StartCoroutine(TypeLine(textPlayer, line));
            }
            else if (speaker == "TIERSCHÜTZERIN")
            {
                panelPlayer.SetActive(false);
                panelInterviewer.SetActive(true);
                textInterviewer.text = "";
                yield return StartCoroutine(TypeLine(textInterviewer, line));
            }

            // Warten auf Klick für den nächsten Abschnitt
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            currentLine++;
        }

        // Dialog zu Ende, Panels ausblenden
        panelPlayer.SetActive(false);
        panelInterviewer.SetActive(false);
    }

    // Tippen der Zeile
    IEnumerator TypeLine(TextMeshProUGUI targetText, string line)
    {
        foreach (char c in line.ToCharArray())
        {
            targetText.text += c;
            yield return new WaitForSeconds(textSpeed); // Wartezeit zwischen Buchstaben
        }
    }
}

