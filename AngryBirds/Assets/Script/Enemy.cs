using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _cloudParticlePrefab;

    [SerializeField]
    private float min_collision_power = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.collider.GetComponent<Bird>() != null)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            return;
        }

        //if something collide top of the object - destroy this object
        if (collision.contacts[0].normal.y < -0.5 || collision.relativeVelocity.magnitude > min_collision_power)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
