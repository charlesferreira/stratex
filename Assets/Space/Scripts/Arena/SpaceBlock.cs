using UnityEngine;

namespace Space.Arena.BlocksDistribution {
    public class SpaceBlock : MonoBehaviour {

        BlockInfo info;
        
        public void Init(BlockInfo info, Vector2 position) {
            this.info = info;
            GetComponent<Animator>().Play(info.name);

            //transform.localScale *= sr.sprite.pixelsPerUnit / 100f;
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