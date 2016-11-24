using UnityEngine;
using System.IO;

public class BlocksDistributionMapGenerator : MonoBehaviour {

    [Header("Input")]
    public Texture2D inputTexture;

    [Header("Output")]
    public string outputPath = "/Common/teste.png";
    [Range(1, 10)]
    public int compression = 2;

    public void GenerateMap () {
        // cria uma nova textura redimensionada
        var outPixelSize = (int) Mathf.Pow(2, compression);
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
        File.WriteAllBytes(Application.dataPath + outputPath, outTexture.EncodeToPNG());
        print("Arquivo gerado em: " + outputPath);
    }
}
