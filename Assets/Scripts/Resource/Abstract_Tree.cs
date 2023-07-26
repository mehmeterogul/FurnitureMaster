using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Tree:Abstract_Resource
{
    public Item item;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void GatherResource()
    {
        if (gatherCoroutine == null)
        {
            gatherCoroutine = StartCoroutine(GatherResourceCoroutine());
            TakeHit(_player.CutPower);
        }
    }
    public IEnumerator GatherResourceCoroutine()
    {
        while (true)
        {
            GatherResource();
            yield return new WaitForSeconds(_player.CutPeriod);
        }
    }

}
