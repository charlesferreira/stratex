using UnityEngine;

namespace DominationAreaStates {

    public class MovingState : AbstractState {
        
        public MovingState(DominationArea dominationArea) : base(dominationArea) { }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }
    }
}
