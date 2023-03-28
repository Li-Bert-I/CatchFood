using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int score
    {
        get; private set;
    }

    public int lifes
    {
        get; private set;
    }

    public bool isOver = false;

    public void addScore(int addScore)
    {
        score += addScore;
    }

    public void loseLife()
    {
        lifes -= 1;
        if (lifes == 0) {
            isOver = true;
            GameObject.FindObjectOfType<Gun>().releaseCatched();
            Destroy(GameObject.FindObjectOfType<Gun>().gameObject);
            Destroy(GameObject.FindObjectOfType<Laser>().gameObject);
            Destroy(GameObject.Find("Objects Spawner"));
            GameObject.FindObjectOfType<MeshDestroy>().DestroyMesh();
            GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
            foreach (GameObject explosion in explosions)
            {
                Destroy(explosion);
            }
        }
    }

    Text txt;

    void Start()
    {
        txt = GameObject.FindObjectOfType<Text>();
        lifes = 5;
    }
    void Update()
    {
        txt.text = score.ToString();
    }
}
