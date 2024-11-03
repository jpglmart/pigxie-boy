using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockBack = new Vector2();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 directionKnocback = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            bool gotHit = damageable.Hit(attackDamage, directionKnocback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }

    }

}
