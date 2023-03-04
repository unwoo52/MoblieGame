using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContoller : AnimMovement
{
    public Action<Vector3> OnMove => Move;
    public Action OnOnlyMove => OnlyMove;
    public Action OnStopMove => StopMove;
    public Action<Vector3, float> OnLookat => OnceLookat;
    public Action OnStopLookat => StopLookat;
}
