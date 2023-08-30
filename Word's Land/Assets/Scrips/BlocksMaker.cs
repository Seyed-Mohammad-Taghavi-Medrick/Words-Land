using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksMaker : MonoBehaviour
{
    [SerializeField] int width;

    [SerializeField] int height;

    [SerializeField] private Alphabet[] alphabetsPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SetupBlocks();
    }

    // Update is called once per frame
    void SetupBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                int alphabetToUse = Random.Range(0, alphabetsPrefabs.Length);

                spawnAlphabet(new Vector2Int(pos.x, pos.y), alphabetsPrefabs[alphabetToUse]);
                
            }
        }
    }

    void spawnAlphabet(Vector2Int posIndext, Alphabet alphabetToSpawn)
    {
        Alphabet alphabet = Instantiate(alphabetToSpawn, new Vector3Int(posIndext.x, posIndext.y, 0),
            quaternion.identity, transform);

        alphabet.name = "Alphabet " + " ( " + posIndext.x + " , " + posIndext.y + " ) ";
        alphabet.SetUpAlphabet(posIndext , this);
    }
}