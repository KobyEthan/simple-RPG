using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionHealth : MonoBehaviour, IConsumable
{
    private Player player;
    public void Consume()
    {
        player.AddHealth(10);
        Destroy(gameObject);
    }
    public void Consume(CharacterStats stats)
    {

    }
}