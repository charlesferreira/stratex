using UnityEngine;
using System.Collections;

public interface IBlockState{

    void Update();

    void ToEnteringState();

    void ToFallingState();

    void ToMovingState();

    void ToActiveState();

    void ToMatchingState();
}
