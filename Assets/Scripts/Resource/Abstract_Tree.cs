using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Tree:Abstract_Resource
{
    public Item item;

    public override void GatherResource()
    {
        if (gatherCoroutine == null)
        {
            gatherCoroutine = StartCoroutine(GatherResourceCoroutine());       
        }
    }
    public IEnumerator GatherResourceCoroutine()
    {
        while (true)
        {     
            TakeHit(_player.CutPower);
            yield return new WaitForSeconds(_player.CutPeriod);
        }
    }


}
