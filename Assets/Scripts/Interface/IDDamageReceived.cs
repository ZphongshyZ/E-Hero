using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDDamageReceived
{
    public IEnumerator DamageReceived(bool isImortal, float time)
    {
        isImortal = true;
        yield return new WaitForSeconds(time);
        isImortal = false;
    }
}
