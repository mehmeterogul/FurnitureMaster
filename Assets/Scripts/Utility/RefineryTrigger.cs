using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefineryTrigger : Trigger
{
    [SerializeField] private Refinery _refinery;

    public override void OnTriggerEnter(Collider other)
    {
        if (!IsResourceEnough())
            return;

        base.OnTriggerEnter(other);
    }

    private bool IsResourceEnough()
    {
        return _refinery.CanRefine();
    }

    public override void OnComplete()
    {
        _canTrigger = _refinery.CanRefine();
    }

    public override void OnTriggerExit(Collider other)
    {
        _canDecrease = true;
        _canTrigger = false;

    }
}
