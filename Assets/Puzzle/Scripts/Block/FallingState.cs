using UnityEngine;
using System.Collections;

public class FallingState : IBlockState
{

    private readonly StatePatternBlock block;

    public FallingState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {
        var finalCoord = block.Grid.GetGridCoord(new Vector3(block.Col, block.Row, 0));
        if (finalCoord.y >= block.transform.localPosition.y)
        {
            if (block.Grid.IsEmptySpace(block.Col, block.Row - 1))
            {
                block.Grid.DecreaseBlock(block.Col, block.Row);
            }
            else
            {
                block.freeFall.Stop();
                block.transform.localPosition = finalCoord;
                ToActiveState();
            }
        }
    }

    public void ToFallingState()
    {

    }

    public void ToSwappingState()
    {
        block.freeFall.Stop();
        block.currentState = block.swappingState;
        block.comboSequence = 0;
    }

    public void ToActiveState()
    {
        block.currentState = block.activegState;
        block.Grid.CheckMatch(block.Col, block.Row);
        block.comboSequence = 0;
    }

    public void ToMatchingState()
    {
        block.currentState = block.matchingState;
    }

    public void Decrease()
    {
        block.Row--;
    }
}
