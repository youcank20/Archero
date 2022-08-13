using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomDesigner))]
public class RoomDesignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RoomDesigner roomDesigner = target as RoomDesigner;

        DrawDefaultInspector();

        if (GUILayout.Button("Make room"))
        {
            roomDesigner.MakeRoom();
        }
    }
}
