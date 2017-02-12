using UnityEngine;
using Common.Math;

namespace Space.Arena.BlocksDistribution {
    public class BlockFactory : MonoBehaviour {

        // Inspector
        public SpaceBlock blockPrefab;
        public Transform blocksContainer;

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
        }

        // Public methods
        public void CreateBlock(BlockInfo info) {
            var block = (SpaceBlock)Instantiate(blockPrefab, transform);
            block.Init(info, RandomPosition());
            block.transform.SetParent(blocksContainer);
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