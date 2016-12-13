using UnityEngine;

namespace Space.Arena.BlocksDistribution {
    public class SpaceBlock : MonoBehaviour {
        public void Init(Sprite sprite, Vector2 position) {
            var sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprite;

            transform.localScale *= sr.sprite.pixelsPerUnit / 100f;
            transform.localPosition = position;
        }
    }
}