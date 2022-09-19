using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    TankController _tankController;
    Health _tankHealth;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _tankHealth = GetComponent<Health>();
    }
}
