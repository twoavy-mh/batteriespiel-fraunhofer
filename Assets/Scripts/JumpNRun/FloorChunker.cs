using System;
using System.Collections.Generic;
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

        bool[] randomChunkToSpawnObstacle = RandomPlacer((int)GameState.Instance.GetCurrentMicrogame() + 2, chunkCount);

        for (int i = 0; i < chunkCount; i++)
        {
            GameObject currentChunk = Instantiate(chunk, new Vector3(i * ChunkSize + xOffset + _blockCount * (
                (chunkCount + 2) * ChunkSize), transform.parent.position.y, 0), Quaternion.identity);
            currentChunk.transform.SetParent(transform);
            SpriteRenderer s = currentChunk.transform.GetChild(0).GetComponent<SpriteRenderer>();
            s.sortingOrder = -i;
            _chunks[i] = currentChunk;
            if (randomChunkToSpawnObstacle[i])
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
        col.transform.localScale = new Vector3(chunkCount * ChunkSize, 0.1f, 1f);
        col.AddComponent<BoxCollider2D>();
        col.transform.position = new Vector3(
            chunkCount * ChunkSize +
            _blockCount * ((chunkCount + 2) * ChunkSize)
            - col.transform.localScale.x / 2 - ChunkSize / 2,
            transform.parent.position.y, 0f);

        GameObject vert = new GameObject();
        vert.transform.SetParent(transform.parent);
        vert.transform.localScale = new Vector3(0.1f, 2f, 2f);
        vert.AddComponent<BoxCollider2D>();
        vert.transform.position = new Vector3(
            col.transform.position.x - col.transform.localScale.x / 2 - vert.transform.localScale.x / 2,
            transform.parent.position.y - vert.transform.localScale.y / 2 - col.transform.localScale.y / 2, 0f);
        vert.name = "Vertical";
        vert.transform.tag = "Obstacle";

        float randomX = UnityEngine.Random.Range(col.transform.position.x,
            col.transform.position.x + col.transform.localScale.x);
        float randomY = UnityEngine.Random.Range(0, 1);
        Instantiate(target, new Vector3(randomX, randomY, 0), Quaternion.identity);
        col.tag = "Floor";
        _blockCount++;
    }

    private bool[] RandomPlacer(int n, int arrLength)
    {
        int[] array = new int[arrLength];
        if (n >= array.Length - 2)
        {
            throw new ArgumentException("n must be less than the length of the array minus 2.");
        }

        for (int i = 0; i < array.Length; i++)
            array[i] = 0;

        List<int> indices = new List<int>();
        for (int i = 1; i < array.Length - 1; i++)
        {
            indices.Add(i);
        }

        System.Random rand = new System.Random();
        for (int i = 0; i < n; i++)
        {
            int randomIndex = rand.Next(indices.Count);
            array[indices[randomIndex]] = 1;
            indices.RemoveAt(randomIndex);
        }

        return ToBoolArray(array);
    }

    private bool[] ToBoolArray(int[] intArray)
    {
        bool[] boolArray = new bool[intArray.Length];
        for (int i = 0; i < intArray.Length; i++)
        {
            boolArray[i] = intArray[i] == 1;
        }

        return boolArray;
    }
}