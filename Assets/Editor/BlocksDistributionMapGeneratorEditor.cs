using Game.Math;
using UnityEngine;
using UnityEditor;
using BlocksDistribution;

[CustomEditor(typeof(MapGenerator))]
public class BlocksDistributionMapGeneratorEditor : Editor {

    const float EditorWidthScale = 0.9f;
    const int HistogramPreviewColumns = 256;

    MapGenerator generator;
    Texture2D previewImage;
    Histogram histogram;

    void OnEnable() {
        generator = (MapGenerator) target;

        UpdatePreviewImage();
        UpdatePreviewHistogram();
    }

    void UpdatePreviewImage() {
        previewImage = generator.GetOutputImage();
    }

    void UpdatePreviewHistogram() {
        histogram = Histogram.Reduce(generator.Histogram, HistogramPreviewColumns);
    }

    override public void OnInspectorGUI() {
        serializedObject.Update();
        DrawDefaultInspector();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Generate", GUILayout.Width(80), GUILayout.Height(35))) {
            generator.GenerateMap();
            UpdatePreviewImage();
            generator.GenerateHistogram(previewImage);
            UpdatePreviewHistogram();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        DrawImagePreview();
        DrawHistogramPreview();
    }

    private void DrawImagePreview() {
        if (previewImage == null) return;

        // label
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Image Preview", EditorStyles.boldLabel);

        // redimensiona a imagem para ocupar a largura total do inspector
        var width = EditorGUIUtility.currentViewWidth * EditorWidthScale;
        var height = previewImage.height * width / previewImage.width;
        var rect = GUILayoutUtility.GetRect(width, height);
        
        // preview
        EditorGUI.DrawTextureTransparent(rect, previewImage, ScaleMode.ScaleToFit);
    }

    private void DrawHistogramPreview() {
        if (histogram == null) return;

        // label
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Histogram Preview", EditorStyles.boldLabel);

        // calcula a largura das barras e reserva espaço para o desenho
        var maxWidth = EditorGUIUtility.currentViewWidth * EditorWidthScale;
        var maxHeight = 100f;
        var baseRect = GUILayoutUtility.GetRect(maxWidth, maxHeight);

        // desenha as barras
        var colWidth = maxWidth / histogram.Count;
        var maxHeightRate = maxHeight / histogram.MaxValue;
        for (int i = 0; i < histogram.Count; i++) {
            var height = histogram[i] * maxHeightRate;
            var deltaPos = new Vector2(i * colWidth, maxHeight - height);
            var position = baseRect.position + deltaPos;
            var size = new Vector2(colWidth, height);
            var rect = new Rect(position, size);
            EditorGUI.DrawRect(rect, Color.cyan);
        }
    }
}
