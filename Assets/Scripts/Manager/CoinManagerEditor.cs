using UnityEngine;
using UnityEditor;
 
[CustomEditor(typeof(CoinManager))]
public class CoinManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CoinManager coinManager = (CoinManager)target;

        if (GUILayout.Button("Reset Coins"))
        {
            coinManager.ResetCoins();
        }
    }
}
