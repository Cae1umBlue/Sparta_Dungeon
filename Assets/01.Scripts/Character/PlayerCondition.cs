using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    //Condition hunger { get { return uICondition.hunger; } }
    //Condition stamina { get { return uICondition.Stamina; } }

    //public float nohungerHealthDecay;
    public event Action onTakeDamage;
}
