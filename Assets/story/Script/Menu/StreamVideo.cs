using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using LitJson;
public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer video;
    public GameObject GameOverImage;
    public GameObject GameOverUI;

    //dialogue
    public Animator AnimP;
    public Animator AnimK;
    public Text textDisplay;
    public GameObject DialogueMenu;
    public GameObject Character;

    private Color dialogueColor;
    private JsonData dialogue;
    private int index;
    private string speaker;

    private bool inDialogue;
    public void Awake()
    {
        LoadDialogue("Scene/DialogueGameOver");
        GameOverImage.SetActive(false);
        GameOverUI.SetActive(false);
        DialogueMenu.SetActive(false);
        Character.SetActive(false);
        StartCoroutine(PlayVideo());

    }
    void Start()
    {
      
        InvokeRepeating("DialogueFlow", 10f, 3f);
        Invoke("GameOverMenu", 50f);
    }
    public void DialogueFlow()
    {
        PrintLine();
    }
    IEnumerator PlayVideo()
    {
        video.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!video.isPrepared)
        {
            yield return video.texture;
        }
        rawImage.texture = video.texture;
        video.Play();

        yield return new WaitForSeconds(10f);
        DialogueMenu.SetActive(true);
        Character.SetActive(true);
    }
   
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

    public bool PrintLine()
    {
        if (inDialogue)
        {
            JsonData line = dialogue[index];
            if (line[0].ToString() == "EOD")
            {

                inDialogue = false;
                //SceneManager.LoadScene("Main");
                DialogueMenu.SetActive(false);
                Character.SetActive(false);
                GameOverImage.SetActive(true);
                GameOverUI.SetActive(true);
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

    private void DialogueTextColor()
    {
        if (speaker == "Minister")
        {
            textDisplay.color = Color.green;
            AnimP.SetBool("IsSpeakingP", true);
            AnimK.SetBool("IsSpeakingK", false);

        }
        else if (speaker == "Surya The King")
        {

            textDisplay.color = Color.yellow;
            AnimP.SetBool("IsSpeakingP", false);
            AnimK.SetBool("IsSpeakingK", true);
        }
        else
        {
            AnimP.SetBool("IsSpeakingP", false);
            textDisplay.color = Color.white;
            AnimK.SetBool("IsSpeakingK", false);
        }
    }
    public void GameOverMenu()
    {
        rawImage.gameObject.SetActive(false);
        GameOverImage.SetActive(true);
        GameOverUI.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Restart()
    {
        Debug.Log("Re-Play");
        SceneManager.LoadScene("Main");
    }
    public void Menu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }


}
