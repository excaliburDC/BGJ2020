using UnityEngine.UI;
using UnityEngine;
using LitJson;

public class DialogueManager : MonoBehaviour
{
    public Text textDisplay;
    private JsonData dialogue;
    private int index;
    private string speaker;

    private bool inDialogue;
    private void LoadDialogue(string path)
    {
        if (!inDialogue)
        {
            index = 0;
            var jsonTextFile = Resources.Load<TextAsset>("Dialogue/" + path);
            dialogue = JsonMapper.ToObject(jsonTextFile.text);
            inDialogue = true;
        }
    }

    private bool  PrintLine()
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PrintLine();
        }
    }
}
