using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    public float spawnOffset = 85;
    public Vector3 nextSpawn = new Vector3(0, 0, 0);
    public List<GameObject> activeLevels = new List<GameObject>();
    public GameObject player;
    public List<GameObject> level0 = new List<GameObject>();
    public List<GameObject> level1 = new List<GameObject>();
    public List<GameObject> level2 = new List<GameObject>();
    public List<GameObject> level3 = new List<GameObject>();
    private List<List<GameObject>> levelList = new List<List<GameObject>>() ;
    private GameObject levelToBeSpawned;
    private int preLevel;
    private List<int> wantedLevel; 

    // Start is called before the first frame update
    void Start()
    {
        wantedLevel = new List<int>() { 0,1,3};
        levelList.Add(level0);
        levelList.Add(level1);
        levelList.Add(level2);
        levelList.Add(level3);
        for (int i=0;i<4;i++)
        {
            spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {



        if ( player.transform.position.z - activeLevels[0].transform.position.z > 85)
        {
            delete();
            spawn();
        }


    }

    private void spawn()
    {
        levelToBeSpawned = GameObjectGetter(RandomLevelGenerator());

       // activeLevels.Add(Instantiate(levels[RandomLevelGenerator()], nextSpawn,Quaternion.identity));

        activeLevels.Add(levelToBeSpawned);
        levelToBeSpawned.transform.position = nextSpawn;
        levelToBeSpawned.SetActive(true);
        nextSpawn += new Vector3(0, 0, spawnOffset);
    }

    private void delete()
    {
        activeLevels[0].GetComponent<PlatFormDisabler>().Disabler();
        activeLevels.RemoveAt(0);
    }

    int RandomLevelGenerator()
    {
        int level = Random.Range(0,levelList.Count);
        if (level == 2 )
        {
            level = Random.Range(0, levelList.Count);
            preLevel = level;
            return level;
        }
        else if(level == preLevel)
        {
            level = wantedLevel[Random.Range(0, wantedLevel.Count)];
            preLevel = level;
            return level;
        }
        else
        {
            preLevel = level;
            return level;
        }
    }

    private GameObject GameObjectGetter(int platform)
    {
        for(int i = 0;i < levelList[platform].Count; i++)
        {
            if (!levelList[platform][i].activeInHierarchy)
            {
                return levelList[platform][i];
            }
        }
        return null;
    }
}
