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
        var finalCoord = Grid.Instance.GetGridCoord(new Vector3(block.Col, block.Row, 0));
        if (finalCoord.y >= block.transform.localPosition.y)
        {
            if (Grid.Instance.IsEmptySpace(block.Col, block.Row - 1))
            {
                Grid.Instance.DecreaseBlock(block.Col, block.Row);
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
    }

    public void ToActiveState()
    {
        block.currentState = block.activegState;
        Grid.Instance.CheckMatch(block.Col, block.Row);
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
