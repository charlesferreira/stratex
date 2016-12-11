using UnityEngine;
using System.Collections;

public class SwappingState : IBlockState
{
    private readonly StatePatternBlock block;

    public SwappingState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {
        if (!block.movement.IsMoving())
        {
            ToActiveState();
        }
    }

    public void ToFallingState()
    {
        block.currentState = block.fallingState;
        block.freeFall.Gravity = 10;
        block.freeFall.ToFall();
    }

    public void ToSwappingState()
    {

    }

    public void ToActiveState()
    {
        block.currentState = block.activegState;
        if (block.Grid.IsEmptySpace(block.Col, block.Row - 1))
            block.Grid.DecreaseBlock(block.Col, block.Row);
        else
            block.Grid.CheckMatch(block.Col, block.Row);
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
