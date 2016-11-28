﻿using UnityEngine;
using UnityEditor;
using BlocksDistribution;
using System;

[CustomEditor(typeof(MapGenerator))]
public class BlocksDistributionMapGeneratorEditor : Editor {

    const float EditorWidthScale = 0.9f;

    MapGenerator generator;
    Texture2D previewImage;

    void OnEnable() {
        generator = (MapGenerator) target;

        UpdatePreviewImage();
    }

    private void UpdatePreviewImage() {
        previewImage = generator.GetOutputImage();
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
        if (generator.Histogram == null) return;

        // label
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Histogram Preview", EditorStyles.boldLabel);

        // calcula a largura das barras e reserva espaço para o desenho
        var maxWidth = EditorGUIUtility.currentViewWidth * EditorWidthScale;
        var maxHeight = 100f;
        var baseRect = GUILayoutUtility.GetRect(maxWidth, maxHeight);

        // desenha as barras
        var cols = Mathf.Min(256, generator.HistogramCount - 1) + 1;
        var colWidth = maxWidth / cols;
        var maxHeightRate = maxHeight / generator.HistogramMaxValue;
        for (int i = 0; i < cols; i++) {
            var index = i * (generator.HistogramCount / cols);
            var height = generator.Histogram[index] * maxHeightRate;
            var deltaPos = new Vector2(i * colWidth, maxHeight - height);
            var position = baseRect.position + deltaPos;
            var size = new Vector2(colWidth, height);
            var rect = new Rect(position, size);
            EditorGUI.DrawRect(rect, Color.cyan);
        }
    }
}
