using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private float knifeDamage = 10f;
    [SerializeField] private float shootDamage = 5f;
    [SerializeField] private string enemyKnifeName = "Player2Knife";

    private Animator anim;
    private float currentHealth;
    private int frame = 0;
    private Transform healthBar;
    

	void Start () {
        currentHealth = startingHealth;
        anim = GetComponentInChildren<Animator>();
        healthBar = FindObjectInChilds(gameObject, "Red health").GetComponent<Transform>();
    }

    public void restartGame()
    {
        anim.SetBool("Death", false);
        if (healthBar != null)
        {
            healthBar.localPosition = new Vector3(0f, 0f, 0f);
            healthBar.localScale = new Vector3(0.1f, 0.1f, 1f);
        }
        FindObjectInChilds(gameObject, "HealthBar").SetActive(true);
        currentHealth = startingHealth;
    }
	
	void Update () {
        
        
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("Death", true);
            currentHealth = -999;
        }
        if (currentHealth < -900)
            frame++;
        anim.SetBool("Hit", false);

    }

    IEnumerator FreezeAnimator()
    {

        yield return new WaitUntil(() => frame >= 55);
        anim.speed = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GetComponent<BoxCollider2D>().IsTouching(collider) && collider.tag == enemyKnifeName)
        {
            currentHealth -= knifeDamage;
            anim.SetBool("Hit", true);
            Destroy(collider.gameObject);
            substractLife();
        }
    }

    private void substractLife()
    {
        if (healthBar != null)
        {
            healthBar.localScale -= new Vector3(0.01f, 0, 0);
            healthBar.position -= new Vector3(0.04f, 0, 0);
        }
    }

    public bool announceEndGame()
    {
        return currentHealth <= 0;
    }

    public static GameObject FindObjectInChilds(GameObject gameObject, string gameObjectName)
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == gameObjectName)
            {
                return item.gameObject;
            }
        }

        return null;
    }
}
