using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksMaker : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private Alphabet[] alphabetsPrefabs;

    private Dictionary<string, DicItem> currentAlphabets = new Dictionary<string, DicItem>();

    public class DicItem
    {
        public Alphabet alphabet;
        public Vector2 pos;

        public DicItem(Alphabet alphabet, Vector2 pos)
        {
            this.alphabet = alphabet;
            this.pos = pos;
        }
    }

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
            for (int y = 0; y < height;)
            {
                var chance = Random.Range(0, 100);

                Vector2 pos = new Vector2(x + offset * x, y + offset * y);
                int alphabetToUse = Random.Range(0, alphabetsPrefabs.Length);

                if (chance < alphabetsPrefabs[alphabetToUse].Chance)
                {
                    Debug.Log(chance);

                    spawnAlphabet(new Vector2(pos.x, pos.y), alphabetsPrefabs[alphabetToUse]);
                    y++;
                }
            }
        }
    }

    void spawnAlphabet(Vector2 posIndext, Alphabet alphabetToSpawn)
    {
        Alphabet alphabet = Instantiate(alphabetToSpawn, new Vector2(posIndext.x, posIndext.y),
            quaternion.identity, transform);

        alphabet.name = "Alphabet " + " ( " + posIndext.x + " , " + posIndext.y + " ) ";
        alphabet.name = $"Alphabet ({posIndext.x},{posIndext.y})";
        alphabet.SetUpAlphabet(posIndext, this);

        currentAlphabets.Add(alphabet.name, new DicItem(alphabet, posIndext));
    }

    public void AddNewAlphabet(Vector2 pos)
    {
        var curDic = currentAlphabets.SingleOrDefault(x => x.Value.pos == pos);
        var toReplace =
            currentAlphabets.SingleOrDefault(x =>
                x.Value.pos.x == curDic.Value.pos.x && x.Value.pos.y + offset == curDic.Value.pos.y);

        if (toReplace.Key != null)
        {
            toReplace.Value.alphabet.transform.DOMove(curDic.Value.pos, moveDuration).OnComplete(
                () => { curDic.Value.alphabet = toReplace.Value.alphabet; }
            );
        }
    }
}