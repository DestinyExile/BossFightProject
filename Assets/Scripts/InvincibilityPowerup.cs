using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerup : PowerUpBase
{
    [SerializeField] Material playerBody;
    [SerializeField] Material playerTread;
    [SerializeField] Player player;

    Color playerBodyOrigColor;
    Color playerTreadOrigColor;

    bool isActive = false;
    int playerHealthOnPickUp;

    private void Awake()
    {
        playerBodyOrigColor = new Color(0.27f, 0.765f, 0.22f);
        playerTreadOrigColor = new Color(0, 0, 0);
    }

    protected override void PowerUp()
    {
        playerBody.color = Color.cyan;
        playerTread.color = Color.cyan;
        playerHealthOnPickUp = player.CurrentHealth;

        isActive = true;
    }

    protected override void PowerDown()
    {
        playerBody.color = playerBodyOrigColor;
        playerTread.color = playerTreadOrigColor;

        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            if (player.CurrentHealth != playerHealthOnPickUp)
            {
                player.CurrentHealth = playerHealthOnPickUp;
            }

            if (!player.gameObject.active)
            {
                player.gameObject.SetActive(true);
            }
        }
    }
}
