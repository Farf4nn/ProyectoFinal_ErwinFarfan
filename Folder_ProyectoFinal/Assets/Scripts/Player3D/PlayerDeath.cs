using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public string deathSceneName = "Lose";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(deathSceneName);
    }
}
