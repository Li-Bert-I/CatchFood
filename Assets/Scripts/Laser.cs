using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Gun gun;
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            gun.setCatched(other.gameObject);
        }
        if (other.gameObject.tag == "Not Food")
        {
            Vector3 p = other.gameObject.transform.position;
            Instantiate(explosion, new Vector3(p.x, p.y, 0), explosion.transform.rotation);
            Destroy(other.gameObject);
            Stats stats = GameObject.FindObjectOfType<Stats>();
            stats.loseLife();
        }
    }
}
