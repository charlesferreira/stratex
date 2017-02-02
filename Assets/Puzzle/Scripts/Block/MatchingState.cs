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
        //Object.Destroy(block.gameObject);

        ParticleSystem particle = Object.Instantiate(block.blockParticle, block.transform.position + new Vector3(0, 0, -1), Quaternion.identity) as ParticleSystem;
        particle.startColor = block.Info.realColor;
        Object.Destroy(particle.gameObject, particle.duration + particle.startLifetime);
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
