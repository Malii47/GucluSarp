using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffPowerup;

public class CollectibleItem : MonoBehaviour
{
    private static BuffType previousBuffType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GiveBuff(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void GiveBuff(GameObject player)
    {
        BuffPowerup buffPowerup = player.AddComponent<BuffPowerup>();
        BuffPowerup.BuffType[] buffTypes = (BuffPowerup.BuffType[])System.Enum.GetValues(typeof(BuffPowerup.BuffType));
        BuffType buffType;
        do
        {
            buffType = buffTypes[Random.Range(0, buffTypes.Length)];
        } while (buffType == previousBuffType);
        previousBuffType = buffType;
        buffPowerup.buffType = buffType;
    }
}