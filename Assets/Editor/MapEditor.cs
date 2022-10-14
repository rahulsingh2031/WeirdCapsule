using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapGenerator map = target as MapGenerator;
        if (GUI.changed)
        {

            map.GenerateMap();

        }
        if (GUILayout.Button("GenerateMap"))
        {
            map.GenerateMap();
        }

    }
}
