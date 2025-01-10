using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public float Stamina { get; private set; }

    public float maxStamina;
    public float staminaRate;
    public float staminaRegen;

    public bool tryUseStamina;
    public bool CanUseStamina { get; private set; }

    private void Awake()
    {
        Stamina = maxStamina;
        tryUseStamina = false;
        CanUseStamina = true;
    }

    private void Update()
    {
        if (CanUseStamina)
        {
            if (tryUseStamina)
            {
                Stamina -= staminaRate * Time.deltaTime;
                if (Stamina <= 0)
                {
                    CanUseStamina = false;
                }
            }
            else
            {
                if (Stamina < maxStamina) Stamina += staminaRate * Time.deltaTime;
                if (Stamina > maxStamina) Stamina = maxStamina;
            }
        }
        else
        {
            Stamina += staminaRegen * Time.deltaTime;
            if (Stamina > maxStamina)
            {
                Stamina = maxStamina;
                CanUseStamina = true;
            }
        }
    }
}
