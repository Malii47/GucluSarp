using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPosition : MonoBehaviour
{
    public float death_position = 3.97f;

    public float PositionStacker()
    {
        death_position = death_position - 0.03f;
        return death_position;
    }

}
