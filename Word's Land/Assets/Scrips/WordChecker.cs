using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class WordChecker : MonoBehaviour
{
    public int currentScore;
    [SerializeField] private TMP_Text AlowMovesCountText;
    public int AlowMovesCount = 8;
    [SerializeField] private TMP_Text currentScoreText;

    private RoundManager _roundManager;
    public TMP_Text ScoreText;
    public TMP_Text wordText;
    public int score;
    public int sumOfPointOfLetters = 0;
    public int numOfTiles = 0;
    [SerializeField] private TextAsset database;
    private BlocksMaker _blocksMaker;
    private List<Alphabet> selectedWords = new List<Alphabet>();

    public string _word;


    void Start()
    {
        _roundManager = FindObjectOfType<RoundManager>();
        _blocksMaker = FindObjectOfType<BlocksMaker>();
    }

    private void OnEnable()
    {
        GameEvents.OnCheckSqaure += SqaureSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSqaure -= SqaureSelected;
    }

    private void SqaureSelected(Alphabet obj, string letter, Vector3 position, int index)
    {
        if (AlowMovesCount > 0)
        {
            numOfTiles++;
            sumOfPointOfLetters += obj.pointOfLetter;
            GameEvents.SelectSqaureMethod(position);
            _word += letter;
            selectedWords.Add(obj);
            Debug.Log("corent number of selected Tiles is " + numOfTiles);
            Debug.Log("corent Point is " + sumOfPointOfLetters);
        }
    }

    public void CheckWord()
    {
        if (AlowMovesCount > 0)
        {
            var words = database.text.Split();
            var wordToFind = _word.ToLower();

            if (wordToFind.Length >= 3)
            {
                var foundWord = words.FirstOrDefault(x => x == wordToFind);

                if (foundWord != null)
                {
                    DestroyCurrentWords();
                    Debug.Log(_word + "*************");

                    CalculateScore();
                    AlowMovesCount -= 1;
                    AlowMovesCountText.text = AlowMovesCount.ToString();
                }
            }

            ResetSelectedWord();
        }
    }

    public void ResetSelectedWord()
    {
        numOfTiles = 0;
        sumOfPointOfLetters = 0;
        selectedWords.Clear();
        _word = string.Empty;
    }

    private void DestroyCurrentWords()
    {
        foreach (var alphabet in selectedWords)
        {
            Instantiate(alphabet.BurstSFX, alphabet.transform.position, alphabet.transform.rotation);
            //    alphabet.isPartOfWord = true;
            _blocksMaker.DestroyAlphabetOfWordAt(alphabet.pos);
        }

        _blocksMaker.DecreaseRowCoRutin();
    }

    private void CalculateScore()
    {
        currentScore = numOfTiles * sumOfPointOfLetters * 10;

        foreach (var alphabet in selectedWords)
        {
            if (alphabet.inGoldenBlock)
            {
                currentScore *= 2;
            }
        }

        currentScoreText.text = currentScore.ToString();
        score += currentScore;
        ScoreText.text = $"{score}";
        Debug.Log("score is " + score);
        _roundManager.currentScore = score;
    }
}