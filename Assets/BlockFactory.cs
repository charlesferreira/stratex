using UnityEngine;
using Common.Math;

namespace Space {
    namespace Arena {
        namespace BlocksDistribution {
            public class BlockFactory : MonoBehaviour {

                // Inspector
                public Transform blockPrefab;


                // Private
                Histogram histogram;
                int mapWidth;


                // Static
                static BlockFactory instance;
                public static BlockFactory Instance {
                    get {
                        if (instance == null)
                            instance = FindObjectOfType<BlockFactory>();
                        return instance;
                    }
                }


                // Lyfe cycle methods
                void Start() {
                    var mg = GetComponent<MapGenerator>();
                    histogram = mg.Histogram;
                    mapWidth = mg.GetOutputImage().width;

                    for (int i = 0; i < 100; i++) {
                        CreateBlock();
                    }
                }


                // Public methods
                void CreateBlock() {
                    var position = RandomPosition();
                    Instantiate(blockPrefab, position, Quaternion.identity);
                }


                // Private methods
                private Vector2 RandomPosition() {
                    var value = (uint)Random.Range(1, histogram.MaxValue);
                    var index = histogram.Search(value);
                    var randomAngle = Random.Range(0f, 360f);
                    var randomSize = Random.Range(0f, 1f);
                    var offset = Quaternion.AngleAxis(randomAngle, Vector3.back) * Vector2.right * randomSize;

                    return new Vector3(
                        index % mapWidth,
                        index / mapWidth) + offset;
                }
            }

        }
    }
}