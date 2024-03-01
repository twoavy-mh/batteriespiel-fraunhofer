using System;
using System.Linq;
using Helpers;
using UnityEngine;

public class FloorChunker : MonoBehaviour
{
    public GameObject chunk;
    public GameObject target;
    public const float ChunkSize = 3.37f;
    public int chunkCount = 10;
    
    public GameObject YellowLightning;
    public GameObject BlueLightning;

    private GameObject _col;
    private GameObject[] _chunks;

    private GameObject _lastTile;
    private Camera _main;
    private GameObject _obstacle;

    private int _blockCount = 0;

    void Awake()
    {
        _main = Camera.main;
        _chunks = new GameObject[chunkCount];
        _obstacle = Resources.Load<GameObject>("Prefabs/Jnr/Obstacle");
        SpawnChunk();
    }

    private void Update()
    {
        if (_lastTile == null)
        {
            return;
        }

        if (_main.WorldToScreenPoint(_lastTile.transform.position).x < Screen.width)
        {
            SpawnChunk();
        }

        foreach (Transform child in transform)
        {
            Vector3 earlierChildPos = new Vector3(child.position.x + 10f, child.position.y, child.position.z);
            if (_main.WorldToScreenPoint(earlierChildPos).x < 0)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void SpawnChunk()
    {
        Array.Clear(_chunks, 0, _chunks.Length);
        float xOffset = 0;

        int[] randomChunkToSpawnObstacle = new int[(int)GameState.Instance.GetCurrentMicrogame() + 2];
        for (int j = 0; j < randomChunkToSpawnObstacle.Length; j++)
        {
            int a = UnityEngine.Random.Range(0, chunkCount);
            if (randomChunkToSpawnObstacle.Contains(a)) continue;
            randomChunkToSpawnObstacle[j] = a;
        }
        
        for (int i = 0; i < chunkCount; i++)
        {
            
            GameObject currentChunk = Instantiate(chunk, new Vector3(i * ChunkSize + xOffset + _blockCount * (
                (chunkCount + 2) * ChunkSize), transform.parent.position.y, 0), Quaternion.identity);
            currentChunk.transform.SetParent(transform);
            SpriteRenderer s = currentChunk.transform.GetChild(0).GetComponent<SpriteRenderer>();
            s.sortingOrder = -i;
            _chunks[i] = currentChunk;
            if (randomChunkToSpawnObstacle.Contains(i))
            {
                GameObject obstacle = Instantiate(_obstacle, new Vector3(i * 3.42f + _blockCount * (
                    (chunkCount + 2) * ChunkSize), -3.5f, 0), Quaternion.identity);
                obstacle.transform.SetParent(currentChunk.transform);
            }
            
            if (i == chunkCount - 1)
            {
                _lastTile = currentChunk;
            }
        }

        GameObject col = new GameObject();
        col.transform.SetParent(transform.parent);
        col.transform.localScale = new Vector3(chunkCount * ChunkSize, 1f, 1f);
        col.AddComponent<BoxCollider2D>();
        col.transform.position = new Vector3(
            chunkCount * ChunkSize +
            _blockCount * ((chunkCount + 2) * ChunkSize)
            - col.transform.localScale.x / 2 - ChunkSize / 2,
            transform.parent.position.y, 0f);
        float randomX = UnityEngine.Random.Range(col.transform.position.x, col.transform.position.x + col.transform.localScale.x);
        float randomY = UnityEngine.Random.Range(0, 1);
        Instantiate(target, new Vector3(randomX, randomY, 0), Quaternion.identity);
        col.tag = "Floor";
        _blockCount++;
    }
}