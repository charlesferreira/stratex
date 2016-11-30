using UnityEngine;
using System.Collections;

public interface IBlockState{

    void Update();

    void ToFallingState();

    void ToSwappingState();

    void ToActiveState();

    void ToMatchingState();

    void Decrease();
}
