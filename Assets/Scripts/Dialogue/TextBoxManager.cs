using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public Text theText;
    public TextAsset textFile;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;
    public HeroMovement hero;
    public bool isActive;
    public bool stopHeroMovement;
    public float typeSpeed;
    public static bool tbmExists;

    private bool isTyping = false;
    private bool cancelTyping = false;

    // Use this for initialization
    void Start()
    {
        if (!tbmExists)
        {
            tbmExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        hero = FindObjectOfType<HeroMovement>();
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping)
            {
                currentLine += 1;
                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(textScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }
    private IEnumerator textScroll(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }
    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        if (stopHeroMovement)
        {
            hero.canMove = false;
        }

        StartCoroutine(textScroll(textLines[currentLine]));
    }
    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
        hero.canMove = true;
    }
    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }
}
