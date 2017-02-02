using UnityEngine;

public class MatchingState : IBlockState
{
    private readonly StatePatternBlock block;

    public MatchingState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {
        Object.Destroy(block.gameObject);
    }

    public void ToFallingState()
    {

    }

    public void ToSwappingState()
    {

    }

    public void ToActiveState()
    {

    }

    public void ToMatchingState()
    {

    }

    public void Decrease()
    {

    }
}
