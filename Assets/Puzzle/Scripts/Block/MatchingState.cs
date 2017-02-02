using UnityEngine;

public class MatchingState : IBlockState
{
    private readonly StatePatternBlock block;
    bool start = false;

    public MatchingState(StatePatternBlock statePatternBlock)
    {
        block = statePatternBlock;
    }

    public void Update()
    {
        if (!start)
        {
            Object.Destroy(block.gameObject);

            //Object.Instantiate(block.blockParticle, block.transform.position + new Vector3(0, 0, -1), Quaternion.identity, block.transform);
            //block.GetComponent<SpriteRenderer>().enabled = false;
            //Object.Destroy(block.gameObject, 2f);
            //start = true;
        }
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
