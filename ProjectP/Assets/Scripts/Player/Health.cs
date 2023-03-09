using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    [SerializeField] private UnityEvent Dead;
    [SerializeField] private float InvincibilityTime = 1f;
    [SerializeField] private bool IsBeingHit = false;
    [SerializeField] private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {

        if (health > numOfHearts)
            health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++) {

            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

        if(IsBeingHit)
        {
            DamagePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.transform.tag == "Enemy")
        {
            IsBeingHit = true;

        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.transform.tag == "Enemy")
        {
            IsBeingHit = false;
        }
    }


    public void DamagePlayer()
    {
        if(isInvincible)
            return;

        if(health > 0)
            health -= 1;

        if(health <= 0)
        {
            Dead.Invoke();
            return;
        }

        StartCoroutine(BecomeInvincible());
    }

    //Makes the player invincible for InvincibilityTime duration
    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(InvincibilityTime);

        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
}
