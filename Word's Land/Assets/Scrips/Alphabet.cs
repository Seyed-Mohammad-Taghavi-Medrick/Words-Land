using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Alphabet : MonoBehaviour
{
    public Vector2Int pos;

    private BlocksMaker blocksMaker;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetUpAlphabet(Vector2Int posIndex, BlocksMaker theBlocksMaker)
    {
        this.pos = posIndex;
        blocksMaker = theBlocksMaker;
    }
}