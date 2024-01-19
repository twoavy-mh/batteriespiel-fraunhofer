using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpNRun
{
    
}

public class FloorChunker : MonoBehaviour
{

    public GameObject chunk;
    public const float ChunkSize = 3.37f;
    public int chunkCount = 10;
    
    void Start()
    {
        for (int i = 0; i < chunkCount; i++)
        {
            GameObject currentChunk = Instantiate(chunk, new Vector3(i * ChunkSize, transform.parent.position.y, 0), Quaternion.identity);
            currentChunk.transform.SetParent(transform);
            SpriteRenderer s = currentChunk.transform.GetChild(0).GetComponent<SpriteRenderer>();
            s.sortingOrder = -i;
        }

        GameObject col = new GameObject();
        col.transform.SetParent(transform.parent);
        col.transform.localScale = new Vector3(chunkCount * ChunkSize, 1f, 1f);
        col.AddComponent<BoxCollider2D>();
        col.transform.position = new Vector3((chunkCount * ChunkSize) / 2f - ChunkSize / 2f, transform.parent.position.y, 0f);
        col.tag = "Floor";
    }
}
