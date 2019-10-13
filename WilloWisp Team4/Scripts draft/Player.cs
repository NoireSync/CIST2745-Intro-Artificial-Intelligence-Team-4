using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    public Animator hurtAnim;

    private Vector2 moveAmount;

    public int health;// Hearts

    public Image[] _hearts;
    public Sprite _fullHearts;
    public Sprite _emptyHearts;

    private SceneTransition sceneTransitions;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransition>();

    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * playerSpeed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI(health);
        hurtAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            Destroy(this.gameObject);
            sceneTransitions.LoadScene("Lost");
        }
    }
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }
    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < _hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                _hearts[i].sprite = _fullHearts;
            }
            else
            {
                _hearts[i].sprite = _emptyHearts;
            }
        }
    }
    public void HealMe(int healAmount)
    {
        if(health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
