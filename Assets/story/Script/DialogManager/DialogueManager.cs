using UnityEngine.UI;
using UnityEngine;
using LitJson;

public class DialogueManager : MonoBehaviour
{
   
    public Animator AnimP;
    public Animator AnimK;
    public Text textDisplay;

    private Color dialogueColor;
    private JsonData dialogue;
    private int index;
    private string speaker;

    private bool inDialogue;
    public bool LoadDialogue(string path)
    {
        if (!inDialogue)
        {
            index = 0;
            var jsonTextFile = Resources.Load<TextAsset>("Dialogue/" + path);
            dialogue = JsonMapper.ToObject(jsonTextFile.text);
            inDialogue = true;
            return true;
        }
        return false;
    }

    public bool  PrintLine()
    {
        if (inDialogue)
        {
            JsonData line = dialogue[index];
            if (line[0].ToString() == "EOD")
            {
                inDialogue = false;
                textDisplay.text = "";
                return false;
            }
            foreach (JsonData key in line.Keys)
                speaker = key.ToString();
            DialogueTextColor();
            textDisplay.text = speaker + ": " + line[0].ToString();
            index++;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadDialogue("Scene/Dialogue0");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintLine();
        }
    }
    private void DialogueTextColor()
    {
        if (speaker == "Shani")
        {
            textDisplay.color = Color.cyan;
            AnimP.SetBool("IsSpeakingP", true);
            AnimK.SetBool("IsSpeakingK", false);
       
        }
        else if (speaker == "Surya The King")
        {
          
            textDisplay.color = Color.yellow;
            AnimP.SetBool("IsSpeakingP", false);
            AnimK.SetBool("IsSpeakingK", true);
        }else
        {
            AnimP.SetBool("IsSpeakingP",false);
            textDisplay.color = Color.white;
            AnimK.SetBool("IsSpeakingK", false);
        }
        //textDisplay.color = GameObject.FindGameObjectWithTag(speaker).GetComponent<CharacterColor>().GetDialogueColor();
    }
}
