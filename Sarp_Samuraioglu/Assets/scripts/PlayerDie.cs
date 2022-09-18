using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public Animator animator;

    public void SarpDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            GameObject.FindGameObjectWithTag("Fade").GetComponent<LevelChanger>().FadeToNextLevel();
            Invoke("SarpDie", 1f);
            
        }
    }
}
