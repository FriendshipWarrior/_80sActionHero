using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestGiver : MonoBehaviour {

	private GameObject hero; //The hero object
	//Quest
	private QuestManager manager; //A reference to the QuestManager
	private MouseLockCheck _MouseLock; //Checks for mouse locking
	private Quest Quest; //The referenece to the quest we give
	//private TextMesh text; //The current text we have stored from the Quest
	private bool ComPQuest; //If you've completed previous quest
	private int OverallID; //Quest overall ID
    //public Text description;
    public Text iconText;
    //public Texture tex;
    //public GameObject questGiverBox;
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

	void Start(){

		CanGUI = true;
       
		//Grabbing our references
		hero = GameObject.FindGameObjectWithTag ("Hero");
		manager = GameObject.FindGameObjectWithTag("QManager").GetComponent<QuestManager>();
		_MouseLock = GameObject.FindGameObjectWithTag("QManager").GetComponent<MouseLockCheck>();
		ComPQuest = true;

		//Quest Giver getting the quest needed
		for(int i = 0; i < manager.QList.Count; i++)
        {
			if(manager.QList[i].QuestID == QuestID && manager.QList[i].ProgressionID == ProgressID)
            {
				Quest = manager.QList[i];
				OverallID = Quest.OverallID;
				return;
			}
		}
	}

	void Update()
    {
		//Setting the MouseLock 'QuestWindowOpen' depending if it is or not.
        
		_MouseLock.QuestWindowOpen = ShowGUI;

        if (Quest.FinishedQuest)
        {
				iconText.text = "...";
		}
        else if(Quest.CurrentQuest)
        {
				iconText.text = "?";
		}
        else
        {
				iconText.text = "!";
		}

        if (Input.GetButtonUp("Submit") && Vector3.Distance(hero.transform.position, transform.position) <= Distance && ShowGUI == false)
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        ComPQuest = true;
        //Checks if previous quest in line has been finished
        ComPQuest = manager.CheckQuestLine(QuestID, ProgressID);

        //Deciding if we should show the GUI or not
        if (Vector3.Distance(hero.transform.position, transform.position) <= Distance)
        {
            if (manager.CurrentQuest == null)
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
    }

    void DoMyWindow(int windowID)
    {
        GUILayout.BeginVertical();

        GUILayout.Space(Spacing);
        
        //Quest Description
        GUILayout.Box(Quest.Description);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Yes"))
        {
            CanGUI = false;
            ShowGUI = false;
            _MouseLock.QuestWindowOpen = false;
            manager.AddQuest(OverallID);
            StartCoroutine(DelayBeforeClose());
        }
        if (GUILayout.Button("No"))
        {
            CanGUI = false;
            ShowGUI = false;
            _MouseLock.QuestWindowOpen = false;
            StartCoroutine(DelayBeforeClose());
        }
      
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
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
        GUI.matrix = Matrix4x4.TRS(new Vector3(500, 75, 0), Quaternion.identity, scale);
        GUI.skin = skin;

        if (ShowGUI == true && Quest.CurrentQuest == false)
        {
            Pos = GUILayout.Window(0, Pos, DoMyWindow, "");
        }

        GUI.matrix = svMat; // restore matrix
    }
}
