using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballMove : MonoBehaviour
{
    public float bulletSpeed = 10f; // 총알의 이동 속도
    CapsuleCollider2D collision;
    private void Start()
    {
        collision = GetComponent<CapsuleCollider2D>();


    }

    private void Update()
    {
        if(transform.rotation.y == 0)
        {
            transform.Translate(transform.right * bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * (-1) *  bulletSpeed * Time.deltaTime);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GoblinController enemyScript = collision.gameObject.GetComponent<GoblinController>();
            //if (enemyScript != null)
            //{
                enemyScript.OnDamaged();
           // }
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}







