using UnityEngine;
using System.Collections;

public class EnteringState : IBlockState {

    private readonly StatePatternBlock block;

    public EnteringState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {

    }

    public void ToEnteringState()
    {

    }

    public void ToFallingState()
    {

    }

    public void ToMovingState()
    {

    }

    public void ToActiveState()
    {

    }

    public void ToMatchingState()
    {

    }
}
