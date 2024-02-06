using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAbilityUI : MonoBehaviour
{
    public AbilityHolder holder;
    public Slider slidebar;

    private void Update()
    {
        if(holder.CurrentAbility != null)
        {
            slidebar.value = holder.CurrentAbility.CurrentCooldownRatio;
        }
    }
}
