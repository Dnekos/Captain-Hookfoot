using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class DialogueManager : Databaser
{
    Controls inputs;

    // text variables
    List<DialogueLine> table;
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
    Text NametagText,PlayerBody,CrewBody;
    [SerializeField]
    Image LeftImg, RightImg;

    private void Awake()
    {
        NametagText.text = "";
        PlayerBody.text = "";
        CrewBody.text = "";

        inputs = new Controls();
        inputs.Game.AdvanceText.performed += ctx => NextLine(); // bind the escape key to the OnExit Function
    }

    public void StartDialogue(int TreeID)
    {
        table = FetchDialog(TreeID);

        if (table[0].Left != "")
            LeftImg.sprite = Resources.Load<Sprite>("Portraits/" + table[0].Left);
        if (table[0].Right != "")
            RightImg.sprite = Resources.Load<Sprite>("Portraits/" + table[0].Right);

        textIndex = 0;
        textTimer = 0;
        counting = true;
    }

    public void NextLine()
    {
        if (counting)
        {
            counting = false;
            if (table[0].Name == "Marg")
                PlayerBody.text = table[0].Body;
            else
                CrewBody.text = table[0].Body;
            return;
        }
        table.RemoveAt(0);
        textIndex = 0;

        if (table.Count == 0)
        {
            PlayerBody.text = "";
            CrewBody.text = "";
            gameObject.SetActive(false);
            return;
        }
        else
            counting = true;

        NametagText.text = "";
        if (table[0].Name == "Marg")
            PlayerBody.text = "";
        else
            CrewBody.text = "";

        if (table[0].Left != "")
            LeftImg.sprite = Resources.Load<Sprite>("Portraits/" +table[0].Left);
        if (table[0].Right != "")
            RightImg.sprite = Resources.Load<Sprite>("Portraits/" + table[0].Right);

    }

    // Update is called once per frame
    void Update()
    {
        textTimer += textSpeed * Time.deltaTime; // increment time

        if (counting && textTimer > 1) // incrementing text
        {
            NametagText.text = table[0].Name.ToString();//loadedText[0].Value;

            if (table[0].Name == "Marg")
                PlayerBody.text += table[0].Body.ToString()[textIndex]; // add next letter
            else
                CrewBody.text += table[0].Body.ToString()[textIndex]; // add next letter

            textIndex++;

            textTimer = 0; // reset timer
            if (textIndex == table[0].Body.ToString().Length) // end of phrase
                counting = false;
        }
    }

    private void OnEnable()
    {
        inputs.Game.Enable();
    }
    private void OnDisable()
    {
        inputs.Game.Disable();
    }
}
