using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffPowerup;

public class CollectibleItem : MonoBehaviour
{
    public static Dictionary<BuffType, BuffPowerup> activeBuffs = new Dictionary<BuffType, BuffPowerup>();

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
        if (activeBuffs.Count == buffTypes.Length)
        {
            activeBuffs.Clear();
        }
        do
        {
            buffType = buffTypes[Random.Range(0, buffTypes.Length)];
        } while (activeBuffs.ContainsKey(buffType));
        activeBuffs[buffType] = buffPowerup;
        buffPowerup.buffType = buffType;
    }
}