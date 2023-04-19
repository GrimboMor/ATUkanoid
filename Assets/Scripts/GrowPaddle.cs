using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrowPaddle : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        PaddleScript.Instance.ChangePaddleSizeAnimated(3.6f);
    }
}
