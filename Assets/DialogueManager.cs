using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // text variables
    public List<KeyValuePair<string, string>> loadedText;
    int textIndex = 0;

    [Header("Timing")]
    [SerializeField]
    float textSpeed = 1;
    float textTimer = 0;
    [SerializeField]
    bool counting = false;

    [Header("Components")]
    [SerializeField]
    Transform NametagPos;
    [SerializeField]
    Text NametagText,TextBody;
    [SerializeField]
    Image LeftImg, RightImg;

    private void Awake()
    {
        loadedText = new List<KeyValuePair<string, string>>();
        NametagText.text = "";
        TextBody.text = "";
    }
    public void LoadText(string text, string subtext = default)
    {
        loadedText.Add(new KeyValuePair<string, string>(text, subtext));
        if (!counting)
        {
            textIndex = 0;
            textTimer = 0;
            counting = true;
        }
    }

    public void NextLine()
    {
        if (counting)
            return;

        loadedText.RemoveAt(0); // reset list to next
        textIndex = 0;

        NametagText.text = "";
        TextBody.text = "";

        if (loadedText.Count == 0)
        {
            // SET BACK TO NORMAL
        }
        else
            counting = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textTimer += textSpeed * Time.deltaTime; // increment time

        if (counting && textTimer > 1) // incrementing text
        {
            NametagText.text = loadedText[0].Value;

            TextBody.text += loadedText[0].Key[textIndex]; // add next letter
            textIndex++;

            textTimer = 0; // reset timer
            if (textIndex == loadedText[0].Key.Length) // end of phrase
                counting = false;
        }
    }
}
