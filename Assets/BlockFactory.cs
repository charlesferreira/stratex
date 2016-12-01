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
                int mapHeight;


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
                    mapHeight = mg.GetOutputImage().height;

                    //for (int i = 0; i < 5000; i++) {
                    //    CreateBlock();
                    //}
                }

                void Update() {
                    CreateBlock();
                }


                // Public methods
                void CreateBlock() {
                    var go = (Transform)Instantiate(blockPrefab, Vector2.zero, Quaternion.identity, transform);
                    var position = RandomPosition();
                    go.localPosition = position;
                }


                // Private methods
                private Vector2 RandomPosition() {
                    var value = (uint)Random.Range(1, histogram.MaxValue);
                    var index = histogram.Search(value);

                    var coords = new Vector2(
                        (index % mapWidth) + Random.Range(0f, 1f),
                        (index / mapWidth) + Random.Range(0f, 1f));

                    var halfMapSize = new Vector2(mapWidth, mapHeight) / 2f;
                    return coords - halfMapSize;
                }
            }

        }
    }
}