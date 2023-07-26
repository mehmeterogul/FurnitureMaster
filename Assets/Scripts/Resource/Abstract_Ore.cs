using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Ore : Abstract_Resource
{
    public override IEnumerator GatherResourceCoroutine()
    {
        while (true)
        {
            TakeHit(_player.DigPower);
            if (_playerLeft)
            {
                // Player left, cancel the coroutine
                gatherCoroutine = null;
                yield break;
            }

            yield return new WaitForSeconds(_player.DigPeriod);
        }
    }
}
