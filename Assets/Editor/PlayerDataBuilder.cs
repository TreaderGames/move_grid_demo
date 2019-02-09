using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerDataBuilder : MonoBehaviour
{
    [MenuItem("Assets/Create/PlayerData")]
    public static void CreateMyAsset()
    {
        PlayerData asset = ScriptableObject.CreateInstance<PlayerData>();

        AssetDatabase.CreateAsset(asset, "Assets/GamePlay/PlayerData/Config/PlayerData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
