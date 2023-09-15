using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksMaker : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] int width;
    [SerializeField] int height;

    private int alphabetToUse;
    [SerializeField] private Alphabet[] alphabetsPrefabs;

    [SerializeField] private Alphabet[,] alphabetsGrid;

    public enum BoardState
    {
        wait,
        move
    }

    public BoardState currentState = BoardState.move;

    // Start is called before the first frame update
    void Start()
    {
        DecreaseRowCoRutin();
        alphabetsGrid = new Alphabet[width, height];
        SetupBlocks();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shuffle();
        }
    }

    // Update is called once per frame
    void SetupBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; /*y++*/)
            {
                var chance = Random.Range(0, 100);
                Vector2 pos = new Vector2(x /*+ offset * x*/, y /*+ offset * y*/);
                alphabetToUse = Random.Range(0, alphabetsPrefabs.Length);
                if (chance < alphabetsPrefabs[alphabetToUse].Chance)
                {
                    Debug.Log(chance);
                    spawnAlphabet(new Vector2Int(x, y), alphabetsPrefabs[alphabetToUse]);

                    y++;
                }
            }
        }
    }

    private void spawnAlphabet(Vector2Int posIndex, Alphabet alphabetToSpawn)
    {
        Alphabet alphabet;
        alphabet = Instantiate(alphabetToSpawn, new Vector3(posIndex.x, posIndex.y + height, 0f),
            Quaternion.identity);
        alphabet.transform.parent = this.transform;
        alphabet.name = "Alphabet " + " ( " + posIndex.x + " , " + posIndex.y + " ) ";
        alphabet.SetUpAlphabet(posIndex, this);
        alphabetsGrid[posIndex.x, posIndex.y] = alphabet;
    }

    public void DecreaseRowCoRutin()
    {
        StartCoroutine(DecreaseRowCo());
    }

    public void DestroyAlphabetOfWordAt(Vector2Int pos)
    {
        if (alphabetsGrid[pos.x, pos.y] != null)
        {
            Destroy(alphabetsGrid[pos.x, pos.y].gameObject);
            alphabetsGrid[pos.x, pos.y] = null;
            /*if (alphabetsGrid[pos.x, pos.y].isPartOfWord)
            {
            }*/
        }
    }


    private IEnumerator DecreaseRowCo()
    {
        currentState = BoardState.wait;
        yield return new WaitForSeconds(.2f);

        int nullCounter = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (alphabetsGrid[x, y] == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    alphabetsGrid[x, y].pos.y -= nullCounter;

                    alphabetsGrid[x, y - nullCounter] = alphabetsGrid[x, y];
                    alphabetsGrid[x, y] = null;
                }
            }

            nullCounter = 0;
        }

        StartCoroutine(RefillingCo());
    }

    private IEnumerator RefillingCo()
    {
        currentState = BoardState.wait;
        yield return new WaitForSeconds(.5f);
        RefillingBoared();
        currentState = BoardState.move;
    }


    private void RefillingBoared()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var chance = Random.Range(0, 100);
                if (alphabetsGrid[x, y] == null)
                {
                    int alphabetToUsse = Random.Range(0, alphabetsPrefabs.Length);

                    spawnAlphabet(new Vector2Int(x, y), alphabetsPrefabs[alphabetToUsse]);
                    if (chance < alphabetsPrefabs[alphabetToUse].Chance)
                    {
                        /*y++;*/
                    }
                }
            }
        }
    }


    

    public void Shuffle()
    {
        if (currentState != BoardState.wait)
        {
            currentState = BoardState.wait;

            List<Alphabet> alphabetFormBoard = new List<Alphabet>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    alphabetFormBoard.Add(alphabetsGrid[x, y]);
                    alphabetsGrid[x, y] = null;
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int alphabetToUse = Random.Range(0, alphabetFormBoard.Count);

                    alphabetFormBoard[alphabetToUse].SetUpAlphabet(new Vector2Int(x, y), this);
                    alphabetsGrid[x, y] = alphabetFormBoard[alphabetToUse];
                    alphabetFormBoard.RemoveAt(alphabetToUse);
                }
            }


            currentState = BoardState.move;
        }
    }
}