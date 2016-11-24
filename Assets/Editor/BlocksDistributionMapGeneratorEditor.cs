using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BlocksDistributionMapGenerator))]
public class BlocksDistributionMapGeneratorEditor : Editor {

    BlocksDistributionMapGenerator generator;

    void OnEnable() {
        generator = (BlocksDistributionMapGenerator) target;
    }

    override public void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Generate", GUILayout.Width(80), GUILayout.Height(25))) {
            generator.GenerateMap();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
