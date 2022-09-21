using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    PlayerMovement pm;
    Rigidbody2D rb;
    Collider2D cd;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SarpDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            cd.enabled= !cd.enabled;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            pm.enabled = !pm.enabled;
            GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
            Invoke("SarpDie", 1f);
        }
    }
    public void DeathbySwordEnemy()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        pm.enabled = !pm.enabled;
        GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
        Invoke("SarpDie", 1f);
    }
}
