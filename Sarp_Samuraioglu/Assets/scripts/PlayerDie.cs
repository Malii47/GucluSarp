using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;


    public Animator animator;
    public PlayerMovement pm;
    public Rigidbody2D rb;
    public CapsuleCollider2D cd;


    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        cd = GetComponent<CapsuleCollider2D>();
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
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            cd.enabled = !cd.enabled;
            pm.enabled = !pm.enabled;
            GetComponent<Combat>().enabled = false;
            GetComponentInChildren<KatanaFunction>().enabled = false;
            GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
            Invoke("SarpDie", 1f);
            
        }
    }
    public void DeathbySwordEnemy()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        cd.enabled = !cd.enabled;
        pm.enabled = !pm.enabled;
        GetComponent<Combat>().enabled = false;
        GetComponentInChildren<KatanaFunction>().enabled = false;
        GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
        Invoke("SarpDie", 1f);
    }
}
