using System;
using System.Collections.Generic;

namespace Common {
    namespace Math {
        [Serializable]
        public class Histogram {

            // private
            public List<uint> values;


            // properties
            public int Count { get { return values.Count; } }
            public int MaxIndex { get { return Count - 1; } }
            public float MaxValue { get { return values[MaxIndex]; } } 


            // indexer
            public uint this[int i] {
                get { return values[i]; }
            }


            // ctor
            public Histogram(uint[] values) {
                this.values = new List<uint>(values);
            }


            // static
            public static Histogram Reduce(Histogram histogram, int columns) {
                columns = System.Math.Min(columns, histogram.Count);
                var reduced = new uint[columns];
                for (int i = 0; i < columns; i++) {
                    var index = i * (histogram.Count / columns);
                    reduced[i] = histogram[index];
                }

                return new Histogram(reduced);
            }

            // binary search
            public int Search(uint value) {
                var min = 0;
                var max = MaxIndex;
                while (true) {
                    var interval = max - min;
                    if (interval == 1)
                        return this[min] >= value ? min : max;
                    
                    var mid = min + interval / 2;
                    if (this[mid] >= value) max = mid;
                    else min = mid;
                }
            }
        }
    }
}