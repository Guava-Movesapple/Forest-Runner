using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    public List<GameObject> levels = new List<GameObject>();
    public float spawnOffset = 85;
    public Vector3 nextSpawn = new Vector3(0,0,0);
    public List<GameObject> spawnedLevels = new List<GameObject>();
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {



        if ( player.transform.position.z - spawnedLevels[0].transform.position.z > 85)
        {
            spawn();
            delete();
        }


    }

    private void spawn()
    {

        spawnedLevels.Add(Instantiate(levels[Random.Range(0,levels.Count)], nextSpawn,Quaternion.identity));
        nextSpawn += new Vector3(0, 0, spawnOffset);
    }

    private void delete()
    {
        Destroy(spawnedLevels[0]);
        spawnedLevels.RemoveAt(0);
    }
}
