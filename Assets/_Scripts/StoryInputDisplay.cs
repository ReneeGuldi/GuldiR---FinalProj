using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryInputDisplay : MonoBehaviour
{
    public static StoryInputDisplay instanceDisplay;

    public Text displayStoryText;
    public ScrollRect scrollRect;
    public InputField playerInput;

    public GameObject lantern;
    public GameObject book;
    public GameObject bookPages;
    public GameObject note;
    public GameObject amulet;

    private int storyProgress = 0;

    public float timeBetweenSegments = 4.5f;
    public float timeSinceLastSegment = 0.0f;

    private bool continueStoryInteraction = false;

    private int continueStoryProgressThreshold =10;
    private int maxStoryProgress = 10;

    private void Awake()
    {
        if (instanceDisplay == null)
        {
            instanceDisplay = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayStorySegment();
    }

    void DisplayStorySegment()
    {
        if (displayStoryText == null)
        {
            return;
        }
        switch (storyProgress)
        {
            case 0:
                // display startup text
                displayStoryText.text += "Welcome to Trove of Mystics\n" +
                    "\nIt's a dreary evening in Saint Mary's, dark, cold, and rainy. The wind whistling through the sound of the heavy thunderstorm. " +
                    "\nJanice is a bright girl, who recently awakened to her magical calling. This evening she was awoke to the sound of whispers that sounded like screams." +
                    "\nJanice jolts awake in her bed, twisting the knob on her lantern to brighten the room. \n\nSo the story begins...";
                break;
            case 1:
                displayStoryText.text += "\nJanice approaches the window, drawn by the eerie wind. As she peers through the glass she notices strange, musical notes swirling in the wind gusts.";
                break;
            case 2:
                CollectItem("lantern");
                displayStoryText.text += "\nJanice slowly opens and begins to climb through the window";
                break;
            case 3:
                displayStoryText.text += "\nJanice notices a small figure in the corner of the dimly lit alley, as she approaches she notices it is a grey cat.";
                break;
            case 4:
                displayStoryText.text += "\n(Winston): Thank goodness someone heard my pleas, I feared they would fall upon deaf ears";
                break;
            case 5:
                displayStoryText.text += "\n(Winston): My owner has disappeared without a trace, I cannot catch his scent anywhere";
                break;
            case 6:
                displayStoryText.text += "\n(Winston): I sense something special in you, allow me?";
                break;
            case 7:
                displayStoryText.text += "\n Winston gently reaches his paw to Jance's forehead, a surge of energy coursing between the two";
                break;
            case 8:
                displayStoryText.text += "\n(Winston): Janice, the fate of Saint Mary's rests in your hands. Will you help me?";
                PlayerDecision();
                break;
            default:
                break;
        }
    }

    void PlayerChoice(string choice)
    {
        int choiceIndex;

        if (int.TryParse(choice, out choiceIndex))
        {
            switch (choiceIndex)
            {
                case 1:
                    ContinueJourney();
                    playerInput.onEndEdit.RemoveListener(PlayerChoice);
                    break;
                case 2:
                    RefuseWinston();
                    playerInput.onEndEdit.RemoveListener(PlayerChoice);
                    break;
            }
        }
    }


    void PlayerDecision()
    {
        displayStoryText.text += "\n\nWhat should Janice do next?" +
                                "\n 1. Continue along with Winston to uncover the truth" +
                                "\n 2. Retreat back to bed and ignore the talking cat";

        playerInput.onEndEdit.AddListener(PlayerChoice);
    }

    void ContinueJourney()
    {
            displayStoryText.text += "\n(Winston): My owner always returns home to me late in the evening, " +
                                     "\nhe has been missing for 24 hours now and I am worried he has been taken.";
    }

    void RefuseWinston()
    {
        displayStoryText.text += "\n(Janice): This must be a dream, cat's don't talk. I am going back to sleep.";
    }

    void CollectItem(string itemName)
    {
        if (InventoryManager.instance.HasItem(itemName))
        {
            Debug.Log(itemName + "is already in inventory");
            return;
        }


        InventoryManager.instance.AddItem(itemName, true);
        
        displayStoryText.text += $"\nJanice picks up the {itemName}.";

        switch (itemName)
        {
            case "lantern":
                lantern.SetActive(true);
                break;

            default:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        timeSinceLastSegment += Time.deltaTime;

        if (timeSinceLastSegment >= timeBetweenSegments)
        {
            ProgressStory();
        }

        if (!continueStoryInteraction && storyProgress >= continueStoryProgressThreshold)
        {
            maxStoryProgress = storyProgress;
            continueStoryInteraction = true;
        }
    }

    void ProgressStory()
    {
        storyProgress++;

        timeSinceLastSegment = 0.0f;
        if (scrollRect == null) 
        {
            Debug.LogWarning("ScrollRect component is null or has been destroyed");
            return;
        }

        DisplayStorySegment();

        Canvas.ForceUpdateCanvases();
        scrollRect.normalizedPosition = new Vector2(0, 0);
        Debug.Log("Progressed story and scrolled to bottom");

        if (storyProgress >= maxStoryProgress)
        {
            LoadNextScene();
        } 
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("LevelTwo");
    }
}
