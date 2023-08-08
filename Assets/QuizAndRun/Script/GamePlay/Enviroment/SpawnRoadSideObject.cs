using UnityEngine;

public class SpawnRoadSideObject: MonoBehaviour
{
    [SerializeField] GameObject[] listRoadside;
    [SerializeField] float minSpawnDistance;
    [SerializeField] float maxSpawnDistance;
    [SerializeField] Transform player;

    private Vector2 lastSpawnPosition;
    private void Update()
    {
        if(player.position.x > lastSpawnPosition.x)
        {
            lastSpawnPosition =  SpawnRoadsideObject();
        }
    }

    private Vector2 SpawnRoadsideObject()
    {

        int randObject = Random.Range(0, listRoadside.Length);


        float x = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 newSpawnPosition = new Vector2(x + lastSpawnPosition.x, listRoadside[randObject].transform.position.y);

        listRoadside[randObject].transform.position = newSpawnPosition;
        return newSpawnPosition;
        
    }
}
