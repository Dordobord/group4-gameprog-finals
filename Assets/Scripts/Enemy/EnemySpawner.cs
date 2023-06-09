using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    Vector2 spawnArea, roomSize,roomSize2, RoomMid;
    bool canSpawn;
    private void Start()
    {
        RoomMid = GetComponentInParent<Transform>().position;
        canSpawn = true;
        roomSize.x = RoomMid.x + this.GetComponent<BoxCollider2D>().size.x/2;
        roomSize.y = RoomMid.y + this.GetComponent<BoxCollider2D>().size.y/2;

        roomSize2.x = RoomMid.x - this.GetComponent<BoxCollider2D>().size.x/2;
        roomSize2.y = RoomMid.y - this.GetComponent<BoxCollider2D>().size.y/2;

    }

    private void Update()
    {
        spawnArea = new Vector2((Random.Range(roomSize.x, roomSize2.x)), (Random.Range(roomSize.y, roomSize2.y)));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && canSpawn == true)
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int n=0;
        while (n < 10)
        {
            canSpawn = false;
            Debug.Log("Enemy Spawned.");
            n++;
            Instantiate(Enemy, spawnArea, Quaternion.identity);
            yield return new WaitForSeconds(2);
            canSpawn = true;
        }
        Debug.Log("Enemy Spawned.");
;
    }
}
