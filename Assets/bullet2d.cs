using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    float m_speed = 1f;

    [ContextMenu(nameof(Fire))]
    void Fire()
    {
        m_rigid.velocity = transform.right * m_speed;

    }

    public void Fire(Vector3 v)
    {
        m_rigid.velocity = v;
        transform.right = v;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"OnCollisionEnter2D:{collision.gameObject.name}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError($"OnTriggerEnter2D:{collision.gameObject.name}");
        Debug.Log("Enemy¿¡ Ãæµ¹ÇßÀ½!");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {//
            MoveToTarget enemy = collision.GetComponent<MoveToTarget>();

            //
            if (enemy != null)
            {
                enemy.TakeDamage(5);

            }
            // Destroy(collision.gameObject);
            Destroy(gameObject); // ÃÑ¾Ë¸¸ ÆÄ±«
     
        }

    }

}
