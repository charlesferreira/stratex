using System;
using UnityEngine;

public class PuzzleToShipInterfaceTest : MonoBehaviour {

    [Serializable]
    public struct MatchTest {
        public string key;
        public Match match;
    }

    public MatchTest[] tests;

    PuzzleToShipInterface ship;

    void Start() {
        ship = GetComponent<PuzzleToShipInterface>();
    }

    void Update() {
        for (int i = 0; i < tests.Length; i++) {
            if (tests[i].key != "" && Input.GetKeyDown(tests[i].key))
                ship.NotifyMatch(tests[i].match);
        }
    }
}
