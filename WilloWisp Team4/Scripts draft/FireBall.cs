using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public GameObject explosion;
    public int damage;
    // Use this for initialization
    private void Start ()
    {
        Invoke("DestroyFireBall", lifeTime);
	}
	
	// Update is called once per frame
	 private void Update ()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

	}
    void DestroyFireBall()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyFireBall();
        }
        if (collision.tag == "boss")
        {
            collision.GetComponent<BossJester>().TakeDamage(damage);
            DestroyFireBall();
        }
    }
}
