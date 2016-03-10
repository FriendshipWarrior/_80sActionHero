using UnityEngine;
using System.Collections;

public class QuestGiver : MonoBehaviour
{

    private GameObject player; //The player object
                               //Quest
    private QuestManager _Manager; //A reference to the EasyQuestManager
    //private MouseLockCheck _MouseLock; //Checks for mouse locking
    private Quest Quest; //The referenece to the quest we give
    private TextMesh text; //The current text we have stored from the Quest
    private bool ComPQuest; //If you've completed previous quest
    private int OverallID; //Quest overall ID
    public int ProgressID; //The Progress ID of the quest
    public int QuestID; //The ID of the quest needed
    public float Distance = 2; //Distance you need to be to get quest
    public bool Progression = false; //If this quest needs the previous one done

    //GUI
    private Vector3 scale;
    public float originalWidth = 1280.0f;
    public float originalHeight = 720.0f;
    private bool ShowGUI;
    private bool CanGUI;
    public Rect Pos;
    public GUISkin skin;
    public float Spacing = 15;

    //GUI (Handles controller stuff)
    public bool UsingController = false;
    private int currentlyPressedButton = -1;
    private Rect[] myRects = new Rect[2];
    private string[] MenuLabels = new string[2];
    ///private JoystickButtonMenu mainMenu;

    void Start()
    {
        //Setting up legacy GUI to support controllers.
        CanGUI = true;
        myRects[0] = new Rect(Screen.width / 2 - 550, Screen.height / 2 + 275, 250, 50);
        myRects[1] = new Rect(Screen.width / 2 - 300, Screen.height / 2 + 275, 250, 50);
        MenuLabels[0] = "Accept";
        MenuLabels[1] = "Deny";
       // mainMenu = new JoystickButtonMenu(2, myRects, MenuLabels, "Submit", JoystickButtonMenu.JoyAxis.Horizontal);
        //mainMenu.enabled = false;

        //Grabbing our references
        player = GameObject.FindGameObjectWithTag("Hero");
        _Manager = GameObject.FindGameObjectWithTag("QManager").GetComponent<QuestManager>();
        //_MouseLock = GameObject.FindGameObjectWithTag("QManager").GetComponent<MouseLockCheck>();
        text = GetComponentInChildren<TextMesh>();
        ComPQuest = true;

        //Quest Giver getting the quest needed
        for (int i = 0; i < _Manager.QList.Count; i++)
        {
            if (_Manager.QList[i].QuestID == QuestID && _Manager.QList[i].ProgressionID == ProgressID)
            {
                Quest = _Manager.QList[i];
                OverallID = Quest.OverallID;
                return;
            }
        }

    }

    void Update()
    {
        //Setting the MouseLock 'QuestWindowOpen' depending if it is or not.
        //_MouseLock.QuestWindowOpen = ShowGUI;

        if (text)
        {
            if (Quest.FinishedQuest)
            {
                text.text = "...";
            }
            else if (Quest.CurrentQuest)
            {
                text.text = "?";
            }
            else {
                text.text = "!";
            }
        }

        //Checking for JoystickButtonMenu
        /*
        if (mainMenu != null)
        {
            if (mainMenu.enabled)
            {
                if (mainMenu.CheckJoystickAxis())
                {
                    Invoke("Delay", .25f);
                }
                currentlyPressedButton = mainMenu.CheckJoystickButton();

                switch (currentlyPressedButton)
                {
                    case 0:
                        //Case 0. In this case, the accept button.
                        CanGUI = false;
                        _Manager.AddQuest(OverallID);
                        ShowGUI = false;
                        StartCoroutine(DelayBeforeClose());
                        return;
                    case 1:
                        //Case 1. In this case, the deny button.
                        CanGUI = false;
                        mainMenu.enabled = false;
                        ShowGUI = false;
                        StartCoroutine(DelayBeforeClose());
                        return;
                }
            }
        }
        */
        //Checking controller input
        if (Input.GetButtonUp("Submit") && Vector3.Distance(player.transform.position, transform.position) <= Distance &&
            ShowGUI == false)
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        ComPQuest = true;
        //Checks if previous quest in line has been finished
        ComPQuest = _Manager.CheckQuestLine(QuestID, ProgressID);
        /*
        //Deciding if we should show the GUI or not
        if (Vector3.Distance(player.transform.position, transform.position) <= Distance)
        {
            if (_Manager.CurrentQuest == null)
            {
                if (ComPQuest == true || Progression == false)
                {
                    if (ShowGUI == false && CanGUI == true && Quest.CurrentQuest == false && Quest.FinishedQuest == false)
                    {
                        _MouseLock.QuestWindowOpen = true;
                        ShowGUI = true;
                    }
                }
            }
        }
        */
    }

    void DoMyWindow(int windowID)
    {
        GUILayout.BeginVertical();

        GUILayout.Space(Spacing);
        //Quest Description
        GUILayout.Box(Quest.Description);

        GUILayout.BeginHorizontal();

        //If we're not using a controller, just show the buttons for mouse clicking
        if (UsingController == false)
        {
            if (GUILayout.Button("Yes"))
            {
                CanGUI = false;
                ShowGUI = false;
               // _MouseLock.QuestWindowOpen = false;
                _Manager.AddQuest(OverallID);
                StartCoroutine(DelayBeforeClose());
            }
            if (GUILayout.Button("No"))
            {
                CanGUI = false;
                ShowGUI = false;
                //_MouseLock.QuestWindowOpen = false;
                StartCoroutine(DelayBeforeClose());
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    private void Delay()
    {
        //Delay for checking joystick. Used so that once you move the joystick it doesn't move twice or more.
        //mainMenu.isCheckingJoy = false;
    }

    IEnumerator DelayBeforeClose()
    {
        //Waits a few seconds, so that when the window it closed it's not picking up the same button press
        //that you used to close it.
        yield return new WaitForSeconds(0.5f);
        CanGUI = true;
    }

    void OnGUI()
    {
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1.0f;
        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);
        GUI.skin = skin;

        if (ShowGUI == true && Quest.CurrentQuest == false)
        {
            Pos = GUILayout.Window(0, Pos, DoMyWindow, "");
        }

        //Used if we're using a controller
        if (ShowGUI == true && Quest.CurrentQuest == false && UsingController)
        {
            //mainMenu.enabled = true;
            //_MouseLock.QuestWindowOpen = true;
            //mainMenu.DisplayButtons();
        }


        GUI.matrix = svMat; // restore matrix
    }
}

