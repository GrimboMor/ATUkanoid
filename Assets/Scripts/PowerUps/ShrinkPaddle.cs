using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPaddle : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        PaddleScript.Instance.ChangePaddleSizeAnimated(1.2f);
    }
}
