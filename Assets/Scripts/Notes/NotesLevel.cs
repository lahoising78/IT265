using UnityEngine;
using System.Collections;
using TMPro;

public class NotesLevel : MonoBehaviour
{
    [SerializeField]
    private NoteSceneStates startState = NoteSceneStates.Welcome;
    [SerializeField] private GameObject wholeNotePrefab = null;
    public TMP_Text textElement = null;
    public GameObject allNotes = null;

    private IEnumerator coroutine = null;
    private NoteSceneStates currentState = (NoteSceneStates)0;

    void Start()
    {
        SetState(startState);
    }

    public void SetState(NoteSceneStates state)
    {
        switch(state)
        {
            case NoteSceneStates.Welcome:
                TransitionText("Welcome! In this section you will learn how to recognize the notes in a staff.\nPress C to continue");
                allNotes.SetActive(false);
                break;
            
            case NoteSceneStates.ShowAllNotes:
                TransitionText("Here are the notes that we will talk about. There are more, but after this tutorial you will be able to read all notes.\nPress C to continue");
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ExplainCleff:
                TransitionText("Notice how the notes are ordered. When you go up the staff, the note also increases in alphabetical order. For example, notice how C is at the bottom, and right above it is D, just like in the alphabet.\nPress C to continue");
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ShowOutsideOfStaffExplanation:
                TransitionText("The same holds true for the rest of the Staff, even outside the lines and spaces. Notice how D is below the first line of the Staff from bottom to top.\nPress C to continue");
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ShowExtraLinesExplanation:
                TransitionText("When the note goes beyond the Staff, and it is supposed to be in a line, extra lines are added to the Staff. See how C is placed on a line, but that line is not connected to the Staff like all the others\nPress C to continue");
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ExplainActivity:
                TransitionText("Now that you know how to identify the notes, let's try to play them. In the next activity, try to play the corresponding note with the virtual piano.\nPress C to continue");
                allNotes.SetActive(false);
                break;

            default:
                Debug.Log("idk");
                break;
        }
        currentState = state;
    }

    private void TransitionText(string text, float moveTime = 1.0f)
    {
        Color original = textElement.color;
        Color transparent = original;
        transparent.a = 0.0f;

        coroutine = TextColorTransition(original, transparent, moveTime);
        StartCoroutine(coroutine);
        textElement.text = text;
        coroutine = TextColorTransition(transparent, original, moveTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator TextColorTransition(Color startColor, Color endColor, float moveTime = 1.0f)
    {
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            textElement.color = Color.Lerp(startColor, endColor, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textElement.color = endColor;
        yield return null;
    }

    public void OnPianoKeyClick(string key)
    {
        if(key == "C")
        {
            switch(currentState)
            {
                case NoteSceneStates.Welcome:
                case NoteSceneStates.ShowAllNotes:
                case NoteSceneStates.ExplainCleff:
                case NoteSceneStates.ExplainActivity:
                // case NoteSceneStates.ExplainStaff:
                case NoteSceneStates.GoBlind:
                case NoteSceneStates.ShowExtraLinesExplanation:
                case NoteSceneStates.ShowOutsideOfStaffExplanation:
                    NextState();
                    break;

                default:
                    break;
            }
        }
    }

    public void NextState()
    {
        if(currentState >= NoteSceneStates.Congrats) return;
        SetState(currentState+1);
    }
}
