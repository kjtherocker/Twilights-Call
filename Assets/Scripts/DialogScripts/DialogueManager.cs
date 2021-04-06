using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : Singleton<DialogueManager>
{

    public enum ChatBoxType
    {
        White,
        Black
    }

    public enum DialogueType
    {
        DialogueBox,
        DialogueVertical
    }

    public enum FontTypes
    {
        Arial,

        NumberOfFonts
    }

    [SerializeField] Texture[] m_TypesPortraits;
    [SerializeField] Font[] m_TypesOfFonts;

    public List<Dialogue> m_Dialogue;
    public List<GameObject> m_DialogueObjects;

    public DialogueTrigger m_DialogueTrigger;

    public DialogueType m_CurrentDialogueType;
    
    public TextMeshProUGUI m_DialogueBoxText;
    public TextMeshProUGUI m_VerticalText;
    public TextMeshProUGUI m_DisplayText;
    public TextMeshProUGUI m_DisplayName;
    public Canvas m_DialogueCanvas;

    public RawImage m_ChatBox;
    public RawImage m_Portrait;



    public GameObject m_DialogueBox;
    public GameObject m_DialogueVertical;

    public int AnimatedTextiterator;
    public string CurrentText;

    public PlayableBehaviour m_Timeline;
    public PlayableDirector m_ActivePlayableDirector;
    private int m_LastDialoguesMaxCharacterCount;
    private int m_MaxVerticalDialogueCharacters;
    public TextAsset m_DialogueJson;
    
    int ObjectToDestroy;
    public bool RemoveGameObject;
    public bool TextScroll;
    
    public PlayerInput m_DialogueControls;

    public AudioClip DialogueClick;
    // Use this for initialization
    void Start()
    {
        //m_DialogueTrigger.m_Dialogue = new List<Dialogue>();
        //m_DialogueCanvas.gameObject.SetActive(false);
        TextScroll = false;

        m_DialogueControls = new PlayerInput();

        m_MaxVerticalDialogueCharacters = 200;
        m_DialogueControls.Player.XButton.performed += XButton => ContinueDialogue();
    }

    public void StartDialogue(TextAsset aTextAsset, DialogueType aDialogueType)
    {

        if (Constants.Helpers.TurnDialogueOff == false)
        {
            gameObject.SetActive(true);

            m_DialogueJson = aTextAsset;
            DeSerializeJsonDialogue(m_DialogueJson);

            m_DialogueControls.Enable();
            

            m_CurrentDialogueType = aDialogueType;
            
            
            if (m_CurrentDialogueType == DialogueType.DialogueBox)
            {
                m_DialogueBox.SetActive(true);
                m_DialogueBoxText.text = "";
                m_DisplayText = m_DialogueBoxText;
                
            }
            else if (m_CurrentDialogueType == DialogueType.DialogueVertical)
            {
                m_DialogueVertical.SetActive(true);
                m_VerticalText.text = "";
                m_DisplayText = m_VerticalText;
            }
            
            m_DialogueCanvas.gameObject.SetActive(true);

            InputManager.Instance.m_BaseMovementControls.Disable();
           // DisplayNextSentence();
        }
    }

    public void DisplayNextSentence(bool aClearScreen)
    {
        

        if (m_Dialogue.Count == 0)
        {

            EndDialogue();                
            
            return;
        }

        if (RemoveGameObject == true)
        {

            Destroy(m_DialogueObjects[0]);
            m_DialogueObjects.RemoveAt(0);

        }




        if (m_Dialogue[0].m_ChatBoxType == ChatBoxType.White)
        {
          //  m_DisplayText.color = Color.white;
        }


       // SetPortrait(m_DialogueTrigger.m_Dialogue[0].m_PortraitType);
        SetFont(m_Dialogue[0].m_FontTypes);


        if (m_Dialogue[0].m_GameObjectToAppearInCutscene != null)
        {
            GameObject temp;
            temp = Instantiate<GameObject>(m_Dialogue[0].m_GameObjectToAppearInCutscene, m_Dialogue[0].m_GameobjectSpawnPoint.gameObject.transform);


            m_DialogueObjects.Add(temp);
        }
     
        if (aClearScreen == true)
        {
            m_DisplayText.text = "";
        }
        CurrentText = m_Dialogue[0].m_Sentances;
     
        SetText(CurrentText);
        

        m_DisplayName.text = m_Dialogue[0].m_Name;

        RemoveGameObject = m_Dialogue[0].DestroyGameObjectOnEndOfDialogue;
        ObjectToDestroy = m_Dialogue.Count;

        m_Dialogue.RemoveAt(0);
    }


    public void ContinueDialogue()
    {

       if (m_Dialogue.Count > 0)
       {
           ResumeTimeline();
       }
       else
       {
           EndDialogue();
       }
        
    }

    public void SetText(string strComplete)
    {

        AnimatedTextiterator = 0;
        
        TextScroll = true;


        if (m_CurrentDialogueType == DialogueType.DialogueBox)
        {
           // m_DisplayText.text = "";
            m_DialogueBox.SetActive(true);

            StartCoroutine(AnimateText(strComplete));

        }
        else if (m_CurrentDialogueType == DialogueType.DialogueVertical)
        {
            
            StartCoroutine(AnimateText("\n" + strComplete));
        }


    }


    public IEnumerator AnimateText(string aText)
    {
        
        if (m_CurrentDialogueType == DialogueType.DialogueBox)
        {
            m_DisplayText.text = aText;
            m_LastDialoguesMaxCharacterCount = 0;
        }
        else if (m_CurrentDialogueType == DialogueType.DialogueVertical)
        {
            m_DisplayText.text += aText;
        }

        
        
        int TotalVisibleCharacters = m_DisplayText.textInfo.characterCount;
        int counter = m_LastDialoguesMaxCharacterCount;


        while (true)
        {
            int visibleCount = counter % (TotalVisibleCharacters + 1);
            m_DisplayText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= TotalVisibleCharacters)
            {
                if (m_CurrentDialogueType == DialogueType.DialogueVertical)
                {
                    m_LastDialoguesMaxCharacterCount = TotalVisibleCharacters;
                }

                break;
            }
            AudioManager.Instance.PlaySoundOneShot(DialogueClick,AudioManager.Soundtypes.Dialogue);
            counter += 1;
            yield return  new WaitForSeconds(0.03f);
        }

    }

    public void DeSerializeJsonDialogue(TextAsset a_JsonFile)
    {
        m_Dialogue.Clear();

        Dialogue[] m_DialogueFromJson = JsonHelper.FromJson<Dialogue>(a_JsonFile.text);

        for (int i = 0; i < m_DialogueFromJson.Length; i++)
        {
            m_Dialogue.Add(m_DialogueFromJson[i]);
            m_Dialogue[i].Initalize();
        }

    }

    public void PauseTimeline(PlayableDirector whichOne)
    {
        m_ActivePlayableDirector = whichOne;
        m_ActivePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
      // gameMode = GameMode.DialogueMoment; //InputManager will be waiting for a spacebar to resume
      // UIManager.Instance.TogglePressSpacebarMessage(true);
    }

    public void ResumeTimeline()
    {
        m_ActivePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);

    }


    public void ShowTextWithParse(string strComplete)
    {
        TextScroll = false;
    }

    public void SetPortrait(Constants.Portrait aPortrait, string sourceName = "Global")
    {
        m_Portrait.texture = m_TypesPortraits[(int)aPortrait];
    }

    public void SetFont(FontTypes aFont, string sourceName = "Global")
    {
       // m_DisplayText.fontStyle = m_TypesOfFonts[(int)aFont];

    }

    public void StunPlayers()
    {
        
    }

    public void EndDialogue()
    {

        m_DialogueControls.Disable();
        if (m_DialogueObjects != null)
        {
            for (int i = 0; i < m_DialogueObjects.Count; i++)
            {
                Destroy(m_DialogueObjects[i]);
                m_DialogueObjects.RemoveAt(i);
            }
        }
        m_DialogueCanvas.gameObject.SetActive(false);
        if (m_DialogueTrigger != null)
        {
            m_DialogueTrigger.DialogueIsDone = true;
        }
        m_DisplayText.text = "";
        
        m_DialogueBox.SetActive(false);
        m_DialogueVertical.SetActive(false);
        Destroy(m_DialogueTrigger.gameObject);
        //AudioManager.Instance.StopSound();
        InputManager.Instance.m_BaseMovementControls.Enable();
 
    }


}
