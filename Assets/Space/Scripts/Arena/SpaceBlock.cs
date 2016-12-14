using UnityEngine;

namespace Space.Arena.BlocksDistribution {
    public class SpaceBlock : MonoBehaviour {

        BlockInfo info;

        public void Init(BlockInfo info, Vector2 position) {
            this.info = info;

            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = info.spaceSprite;

            transform.localScale *= sr.sprite.pixelsPerUnit / 100f;
            transform.localPosition = position;
        }

        void OnTriggerEnter2D(Collider2D other) {
            var shipToPuzzle = other.GetComponent<ShipToPuzzleInterface>();
            if (shipToPuzzle == null) return;

            if (shipToPuzzle.NotifyBlockCollected(info)) {
                Destroy(gameObject);
            }
        }
    }
}