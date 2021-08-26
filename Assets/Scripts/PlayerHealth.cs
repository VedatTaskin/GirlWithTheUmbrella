using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float startingHealth = 1.0f;
    float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    internal void GetDamage(float damage)
    {
        currentHealth -= damage;
        GetComponent<Animator>().SetTrigger("Hit");
        UIControl.Instance.SetHealthValue(currentHealth);
        if (currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Ragdoll>().enabled = true;
            UIControl.Instance.LooseMenu();
        }
    }
}
