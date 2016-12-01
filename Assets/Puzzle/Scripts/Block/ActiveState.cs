using UnityEngine;
using System.Collections;

public class ActivegState : IBlockState
{
    private readonly StatePatternBlock block;

    public ActivegState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {

    }

    public void ToFallingState()
    {
        block.currentState = block.fallingState;
        block.freeFall.ToFall();
    }

    public void ToSwappingState()
    {
        block.currentState = block.swappingState;
    }

    public void ToActiveState()
    {

    }

    public void ToMatchingState()
    {
        block.currentState = block.matchingState;
    }

    public void Decrease()
    {
        block.Row--;
        ToFallingState();
    }
}
