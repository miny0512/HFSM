using System.Collections;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    Player player;
    AbilityBase[] abilities;
    public AbilityBase CurrentAbility;

    public bool IsActiveAbility { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();
        abilities = GetComponentsInChildren<AbilityBase>();
    }

    private void Update()
    {
        if (IsActiveAbility == true) return;
        CheckAbilityButtonDown();
    }

    public IEnumerator ActivateAbility()
    {
        IsActiveAbility = true;
        CurrentAbility.Enter();
        while (CurrentAbility.IsPerformed == false)
        {
            CurrentAbility.Activate();
            yield return null;
        }
        CurrentAbility.Exit();
        IsActiveAbility = false;
    }

    public void CheckAbilityButtonDown()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown(abilities[i].Data.Key) && abilities[i].CanUseAbility())
            {
                CurrentAbility = abilities[i];
                StartCoroutine(ActivateAbility());
                return;
            }
        }
    }

}
