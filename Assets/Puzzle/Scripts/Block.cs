using System;
using UnityEngine;

public class Block : MonoBehaviour {




    

    //private void Entering() {
    //    if (!movement.IsMoving()) {
    //        state = BlockState.Active;
    //    }
    //}

    //private void Moving() {
    //    if (!movement.IsMoving()) {

    //        if (state == BlockState.Moving)
    //        {
    //            state = BlockState.Active;
                
    //            Grid.Instance.CheckMatch(Column, Row);
    //            return;
    //        }
    //        state = BlockState.Active;
    //    }
    //}

    //private void Falling()
    //{
    //    var finalCoord = Grid.Instance.GetGridCoord(new Vector3(Column, Row, 0));
        
    //    if (finalCoord.y >= transform.localPosition.y)
    //    {
    //        if (Grid.Instance.IsEmptySpace(Column, Row - 1))
    //        {
    //            Grid.Instance.DecreaseBlock(Column, Row);
    //        }
    //        else
    //        {
    //            freeFall.Stop();
    //            transform.localPosition = finalCoord;
    //            state = BlockState.Active;
    //            Grid.Instance.CheckMatch(Column, Row);
    //        }
    //    }
    //}

    //public void SetMatching() {
    //    state = BlockState.Matching;
    //}

    //public void ToFall()
    //{
    //    state = BlockState.Falling;
    //    freeFall.ToFall();
    //}
}
