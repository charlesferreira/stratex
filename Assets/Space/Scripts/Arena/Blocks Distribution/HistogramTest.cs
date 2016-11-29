using UnityEngine;
using Common.Math;

namespace Space {
    namespace Arena {
        namespace BlocksDistribution {
            public class HistogramTest : MonoBehaviour {

                struct Column {
                    public Transform trans;
                    public uint value;

                    public Column(Transform trans) {
                        this.trans = trans;
                        this.value = 0;

                        trans.position += Vector3.back;
                    }

                    public void Increment(float heightRate, uint increment) {
                        value += increment;
                        var scale = trans.localScale;
                        scale.y = value * heightRate;
                        trans.localScale = scale;
                    }
                }

                [Header("References")]
                public MapGenerator generator;
                public Transform columnBase;
                public Transform columnOverlay;

                [Header("Histogram")]
                [Range(10, 500)]
                public int numberOfColumns = 200;
                [Range(100, 5000)]
                public int numberOfSamples = 2000;

                float maxHeight;
                float heightRate;
                float colWidth;
                Vector2 histogramPosition;
                Histogram histogram;


                Column[] columns;

                void Start() {
                    histogram = Histogram.Reduce(generator.Histogram, numberOfColumns);

                    maxHeight = columnBase.localScale.y;
                    colWidth = columnBase.localScale.x / histogram.Count;
                    histogramPosition = transform.position + Vector3.left * columnBase.localScale.x / 2f;

                    columns = new Column[histogram.Count];
                    heightRate = maxHeight / histogram.MaxValue;

                    for (int j = 0; j < 2; j++) {
                        for (int i = 0; i < histogram.Count; i++) {
                            // desenha a base do histograma
                            var colPosition = i * colWidth * Vector2.right;
                            var colHeight = histogram[i] * heightRate;
                            CreateColumn(columnBase.gameObject, colPosition, colHeight);

                            // na segunda passada, cria uma cópia do bloco na mesma posição
                            if (j == 1) {
                                var bar = CreateColumn(columnOverlay.gameObject, colPosition, 0f);
                                columns[i] = new Column(bar);
                            }
                        }
                    }

                    for (int i = 0; i < numberOfSamples; i++) {
                        RaffleColumn();
                    }
                }

                void Update() {
                }

                private void RaffleColumn() {
                    var value = (uint)Random.Range(1, histogram.MaxValue);
                    var i = histogram.Search(value);

                    columns[i].Increment(heightRate, (uint)generator.Histogram.Count * 2);
                }

                private Transform CreateColumn(GameObject prefab, Vector2 position, float height) {
                    var col = ((GameObject)Instantiate(prefab, transform.position, transform.rotation, transform)).transform;
                    col.localScale = new Vector2(colWidth, height);
                    col.position = histogramPosition + position;
                    return col;
                }
            }

        }
    }
}