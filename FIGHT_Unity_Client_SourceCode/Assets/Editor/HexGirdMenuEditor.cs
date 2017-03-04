using UnityEngine;
using UnityEditor;

public class HexGirdMenuEditor : EditorWindow
{
    int width = 6;
    int height = 6;
    HexCell hex;

    [MenuItem("Window/HexGird")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        HexGirdMenuEditor window = (HexGirdMenuEditor)EditorWindow.GetWindow(typeof(HexGirdMenuEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("width", width);
        height = EditorGUILayout.IntField("height", height);
        hex = EditorGUILayout.ObjectField(new GUIContent("hex prefab"),hex, typeof(HexCell),false) as HexCell;
        if (GUILayout.Button("Init hex"))
        {
            HexGrid grid = new HexGrid(width,height,hex);
            grid.Init();
        }
    }
}
