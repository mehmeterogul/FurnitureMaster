using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Tree:Abstract_Resource
{
    public override IEnumerator GatherResourceCoroutine()
    {
        while (true)
        {
            if (_playerLeft)
            {
                // Player left, cancel the coroutine
                _player.LeaveGathering();
                gatherCoroutine = null;
                yield break;
            }
            TakeHit(_player.CutPower);

            yield return new WaitForSeconds(_player.CutPeriod);
        }
    }


}
