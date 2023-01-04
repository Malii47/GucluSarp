using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuffPowerup : MonoBehaviour
{
    public enum BuffType
    {
        DoubleAttackDamage,
        DoubleSpeed,
        DoubleSex
    }

    public BuffType buffType;
    public float durationMin = 6f;
    public float durationMax = 9f;

    private float duration;
    private PlayerMovement playerController;
    private Combat playerAttack;

    private void Start()
    {
        duration = Random.Range(durationMin, durationMax);
        playerController = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<Combat>();
        ApplyBuff();
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            RemoveBuff();
            Destroy(this);
        }
    }

    private void ApplyBuff()
    {
        switch (buffType)
        {
            case BuffType.DoubleSpeed:
                playerController.a = false;
                playerController.moveSpeed = 10f;
                playerController.dashLength *= 2f;
                break;
            case BuffType.DoubleAttackDamage:
                playerAttack.hpdamage *= 2f;
                break;
            case BuffType.DoubleSex:
                Debug.Log("DoubleSex Apply");
                break;
            default:
                break;
        }
    }

    private void RemoveBuff()
    {
        switch (buffType)
        {
            case BuffType.DoubleSpeed:
                playerController.a = true;
                playerController.moveSpeed = 5f;
                playerController.dashLength /= 2f;
                break;
            case BuffType.DoubleAttackDamage:
                playerAttack.hpdamage /= 2f;
                break;
            case BuffType.DoubleSex:
                Debug.Log("DoubleSex Remove");
                break;
            default:
                break;
        }
    }
}