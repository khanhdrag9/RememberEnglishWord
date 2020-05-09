using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Gameplay : MonoBehaviour
{
    public Image image;
    public Text resultText;
    public Text countText;
    public Text question;
    public Button[] answerBtns;
    public Button nextBtn;
    public GameObject resultScreen;
    public Text totalText;
    public Text rightText;
    public Text wrongText;

    private ResourceLoader resource;
    private Unit currentUnit;
    private int currentWord = -1;
    private string rightAnswer;

    private int rightCount = 0;
    private int wrongCount = 0;
    private int total = 0;

    private bool isNew;
    private bool isRight;


    void Start()
    {
        resultScreen.SetActive(false);

        GameManager manager = FindObjectOfType<GameManager>();
        resource = manager.resourceLoader;
        currentUnit = resource.GetUnit(manager.unit);
        currentUnit.Shuffer();

        Load();
        nextBtn.onClick.AddListener(() => Load());
    }

    public void Load()
    {
        countText.text = $"{total}/{currentUnit.words.Count}";
        currentWord++;
        if(currentWord >= currentUnit.words.Count)
        {
            Result();
            return;
        }

        isNew = true;
        isRight = false;
        resultText.text = string.Empty;

        int rand = 0;

        var pair = currentUnit.words.ElementAt(currentWord);
        rightAnswer = pair.Key;
        question.text = $"What does \"{pair.Value}\" mean?";

        int btnRight = Random.Range(0, answerBtns.Length);
        int div = currentUnit.words.Count / answerBtns.Length;

        List<int> used = new List<int>();
        used.Add(currentWord);
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].onClick.RemoveAllListeners();
            string use = rightAnswer;

            if (i != btnRight)
            {
                while (true)
                {
                    rand = Random.Range(0, currentUnit.words.Count);
                    if (used.FindIndex(e => e == rand) < 0) break;
                }

                used.Add(rand);
                var pairWrong = currentUnit.words.ElementAt(rand);
                use = pairWrong.Key;
            }

            answerBtns[i].GetComponentInChildren<Text>().text = use;
            answerBtns[i].onClick.AddListener(()=>Answer(use));
        }
    }

    private void Answer(string value)
    {
        if (isRight) return;

        if(value.Equals(rightAnswer))
        {
            if(isNew)
            {
                isNew = false;
                rightCount++;
                total++;
            }

            isRight = true;
            resultText.text = "Congrualation!";
            Invoke("Load", 1f);
            return;
        }

        resultText.text = "Wrong!";
        if (isNew)
        {
            isNew = false;
            wrongCount++;
            total++;
        }
    }
    
    public void Result()
    {
        resultScreen.SetActive(true);
        totalText.text = "Total: " + total.ToString();
        rightText.text = "Right: " + rightCount.ToString();
        wrongText.text = "Wrong: " + wrongCount.ToString();
    }

    public void BackToMM()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
