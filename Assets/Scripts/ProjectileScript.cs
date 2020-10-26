using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public float speed;
    public float fireRate;

    public GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move the bullet
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        /*
        if(Vector3.Distance(transform.position, gameObject.transform.position) > 20)
        {
            Destroy(gameObject);
        }
        */

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

            ParticleEmission(collision);

            ZombieController zombie = collision.gameObject.GetComponent<ZombieController>();
            zombie.health -= 1;

            Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            Debug.Log("hit the wall");
            Destroy(gameObject);
        }
    }

    void ParticleEmission(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 splatterAngle = transform.position + collision.transform.position;
            GameObject splatter = Instantiate(bloodEffect, new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z), Quaternion.Euler(splatterAngle.x, splatterAngle.y, splatterAngle.z));
            ParticleSystem splatterParticle = splatter.GetComponent<ParticleSystem>();
            float totalDuration = splatterParticle.main.duration;
            Destroy(splatter, totalDuration);
        }

    }
}
