using Game.Math;
using System;
using System.IO;
using UnityEngine;

namespace BlocksDistribution {

    [ExecuteInEditMode]
    public class MapGenerator : MonoBehaviour {

        // Inspector

        [Header("Input")]
        public Texture2D inputTexture;

        [Header("Output")]
        [SerializeField]
        string outputPath;
        [Range(0, 8)]
        public byte compression = 2;


        // Properties

        public string OutputPath { get { return Application.dataPath + outputPath; } }
        [SerializeField]
        [HideInInspector]
        Histogram histogram;
        public Histogram Histogram { get { return histogram; } }


        // Methods

        void Awake() {
            GenerateHistogram(GetOutputImage());
        }

        public Texture2D GetOutputImage() {
            Texture2D tex = new Texture2D(1, 1);
            try {
                tex.LoadImage(File.ReadAllBytes(OutputPath));
                tex.filterMode = FilterMode.Point;
            }
            catch (Exception) {
                return null;
            }

            tex.Apply();
            return tex;
        }

        public void GenerateMap() {
            // cria uma nova textura redimensionada
            var outPixelSize = (int)Mathf.Pow(2, compression);
            var outWidth = inputTexture.width / outPixelSize;
            var outHeight = inputTexture.height / outPixelSize;
            Texture2D outTexture = new Texture2D(outWidth, outHeight);

            // preenche a imagem de saída
            for (int x = 0; x < outWidth; x++) {
                for (int y = 0; y < outHeight; y++) {
                    Color sum = new Color();
                    for (int i = 0; i < outPixelSize; i++) {
                        for (int j = 0; j < outPixelSize; j++) {
                            var col = x * outPixelSize + i;
                            var row = y * outPixelSize + j;
                            sum += inputTexture.GetPixel(col, row);
                        }
                    }
                    var average = sum / (outPixelSize * outPixelSize);
                    outTexture.SetPixel(x, y, average);
                }
            }

            // grava o arquivo
            File.WriteAllBytes(OutputPath, outTexture.EncodeToPNG());
            //print("Arquivo gerado em: " + outputPath);
        }

        public void GenerateHistogram(Texture2D previewImage) {
            var size = previewImage.width * previewImage.height;
            var values = new uint[size];

            uint sum = 0;
            for (int y = 0; y < previewImage.height; y++) {
                for (int x = 0; x < previewImage.width; x++) {
                    // utiliza somente o canal "r", pois pressupõe imagem em tons de cinza
                    var current = (uint)(previewImage.GetPixel(x, y).r * 256f);
                    sum += current;

                    var index = x + y * previewImage.width;
                    values[index] = sum;
                }
            }
            
            histogram = new Histogram(values);
        }
    }
}