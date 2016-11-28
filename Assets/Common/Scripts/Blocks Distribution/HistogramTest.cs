using UnityEngine;

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
        public int NumberOfColumns {
            get {
                return Mathf.Min(numberOfColumns, generator.HistogramCount);
            }
        }


        float maxHeight;
        float heightRate;
        float colWidth;
        Vector2 histogramPosition;
        uint[] histogram;


        Column[] columns;

        void Start () {
            histogram = generator.GetReducedHistogram(NumberOfColumns);

            maxHeight = columnBase.localScale.y;
            colWidth =  columnBase.localScale.x / NumberOfColumns;
            histogramPosition = transform.position + Vector3.left * columnBase.localScale.x / 2f;

            columns = new Column[NumberOfColumns];
            heightRate = maxHeight / generator.HistogramMaxValue;

            for (int j = 0; j < 2; j++) {
                for (int i = 0; i < histogram.Length; i++) {
                    // desenha a base do histograma
                    var colPosition =  i * colWidth * Vector2.right;
                    var colHeight = histogram[i] * heightRate;
                    CreateColumn(columnBase.gameObject, colPosition, colHeight);

                    // na segunda passada, cria uma cópia do bloco na mesma posição
                    if (j == 1) {
                        var bar = CreateColumn(columnOverlay.gameObject, colPosition, 0f);
                        columns[i] = new Column(bar);
                    }
                }
            }

            for (int i = 0; i < 200; i++) {
                RaffleColumn();
            }
        }

        void Update () {
            //SortColumn();
        }

        private void RaffleColumn() {
            var value = (uint)Random.Range(1, generator.HistogramMaxValue);
            var i = FindHistogramIndex(value);

            columns[i].Increment(heightRate, (uint)generator.HistogramCount * 2);
        }

        private int FindHistogramIndex(uint value) {
            var min = 0;
            var max = histogram.Length;
            var interval = max - min;
            var index = min + interval / 2;
            print(index);

            var count = 0;
            while (interval > 2) {
                var currentValue = histogram[index];

                if (currentValue >= value) {
                    max = index;
                } else if (value > currentValue) {
                    min = index;
                }

                interval = max - min;
                index = min + interval / 2;

                if (++count > 300) {
                    print("Index: " + index +
                          ", value: " + value + 
                          ", current: " + currentValue + 
                          ", min: " + min + 
                          ", max: " + max +
                          ", interval: " + interval +
                          ", left: " + histogram[index - 1] +
                          ", right: " + histogram[index + 1]);
                    break;
                }
            }

            //print("Value: " + value + ", Index: " + index);

            return index;
        }

        private Transform CreateColumn(GameObject prefab, Vector2 position, float height) {
            var col = ((GameObject)Instantiate(prefab, transform.position, transform.rotation, transform)).transform;
            col.localScale = new Vector2(colWidth, height);
            col.position = histogramPosition + position;
            return col;
        }
    }

}