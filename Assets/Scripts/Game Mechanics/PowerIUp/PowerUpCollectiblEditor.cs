using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PowerUpCollectible))]
public class PowerUpCollectiblEditor : Editor
{
    private string[] powerUpOptions = { "None", "Halsband", "GigaBeller", "Doppelsprung", "CoinMagnet" };
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        // Reference to the target script
        PowerUpCollectible powerUpCollectible = (PowerUpCollectible)target;

        // Draw default inspector for non-customizable fields
        DrawDefaultInspector();

        // Dropdown menu
        EditorGUILayout.LabelField("Power-Up Selector", EditorStyles.boldLabel);
        selectedIndex = EditorGUILayout.Popup("Selected Power-Up", selectedIndex, powerUpOptions);

        // Apply the selected power-up to the target object
        if (GUILayout.Button("Assign Power-Up"))
        {
            IPowerUp selectedPowerUp = null;

            switch (selectedIndex)
            {
                case 1:
                    selectedPowerUp = new Halsband();
                    break;
                case 2:
                    selectedPowerUp = new GigaBeller();
                    break;
                case 3:
                    selectedPowerUp = new Doppelsprung();
                    break;
                case 4:
                    selectedPowerUp = new CoinMagnet();
                    break;
            }

            powerUpCollectible.SetPowerUp(selectedPowerUp);
            EditorUtility.SetDirty(powerUpCollectible); // Mark the object as changed
        }
    }
}