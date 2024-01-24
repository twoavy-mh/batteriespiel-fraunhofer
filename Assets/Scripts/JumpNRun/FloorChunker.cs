using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpNRun
{
    
}

[ExecuteInEditMode]
public class FloorChunker : MonoBehaviour
{
    public GameObject chunk;
    public const float ChunkSize = 3.37f;
    public int chunkCount = 10;
    
    private GameObject _col;
    private GameObject[] _chunks;
    
    void Awake()
    {
        _chunks = new GameObject[chunkCount];
        float xOffset = 0;
        
        for (int i = 0; i < chunkCount; i++)
        {
            GameObject currentChunk = Instantiate(chunk, new Vector3(i * ChunkSize + xOffset, transform.parent.position.y, 0), Quaternion.identity);
            currentChunk.transform.SetParent(transform);
            SpriteRenderer s = currentChunk.transform.GetChild(0).GetComponent<SpriteRenderer>();
            s.sortingOrder = -i;
            _chunks[i] = currentChunk;
        }

        GameObject col = new GameObject();
        col.transform.SetParent(transform.parent);
        col.transform.localScale = new Vector3(chunkCount * ChunkSize, 1f, 1f);
        col.AddComponent<BoxCollider2D>();
        col.transform.position = new Vector3((chunkCount * ChunkSize) / 2f - ChunkSize / 2f, transform.parent.position.y, 0f);
        col.tag = "Floor";
        _col = col;
    }

    private void OnApplicationQuit()
    {
        DestroyImmediate(transform.parent.gameObject);
    }
}
