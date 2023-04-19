using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : PowerUpBase
{
    protected override void ApplyPowerUp()
    {
        GameManagerScript.Instance.Lives = GameManagerScript.Instance.Lives + 1;
    }
}
