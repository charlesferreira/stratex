using UnityEngine;

namespace Space.Arena.BlocksDistribution {
    public class SpaceBlock : MonoBehaviour {

        public float swingRadius;
        public float swingSpeed;

        BlockInfo info;
        Vector2 phase;
        Vector2 speed;
        Vector2 swing;

        public void Init(BlockInfo info, Vector2 position) {
            this.info = info;
            GetComponent<Animator>().Play(info.name);
            
            transform.localPosition = position;
            phase = new Vector2(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f));
            speed = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f));
            swing = new Vector2();
        }

        void FixedUpdate() {
            swing.Set(
                Mathf.Cos(Time.time + phase.x) * speed.x,
                Mathf.Sin(Time.time + phase.y) * speed.y);
            transform.Translate(swing * swingSpeed * swingRadius * Time.fixedDeltaTime);
        }

        void OnTriggerStay2D(Collider2D other) {
            var shipToPuzzle = other.GetComponent<ShipToPuzzleInterface>();
            if (shipToPuzzle == null) return;

            if (shipToPuzzle.NotifyBlockCollected(info)) {
                Destroy(gameObject);
            }
        }
    }
}