using UnityEngine;

namespace Space.Arena.BlocksDistribution {
    public class SpaceBlock : MonoBehaviour {

        public float swingRadius;
        public float swingSpeed;

        BlockInfo info;
        Vector2 startingPosition;
        Vector2 phase;
        Vector2 speed;
        Vector2 swing;

        public void Init(BlockInfo info, Vector2 position) {
            this.info = info;
            GetComponent<Animator>().Play(info.name);

            startingPosition = position;
            phase = new Vector2(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f));
            speed = new Vector2(
                Random.Range(-swingSpeed, swingSpeed),
                Random.Range(-swingSpeed, swingSpeed));
            swing = new Vector2();
        }

        void Update() {
            swing.Set(
                Mathf.Cos(Time.time * speed.x + phase.x),
                Mathf.Sin(Time.time * speed.y + phase.y));
            transform.localPosition = startingPosition + swing * swingRadius;
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