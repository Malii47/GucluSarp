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
    public Collider2D cd;

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

    /*public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            pm.enabled = !pm.enabled;
            GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
            Invoke("SarpDie", 1f);
            
        }
    }*/
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
}
