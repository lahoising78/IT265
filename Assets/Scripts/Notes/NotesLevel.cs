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
    public GameObject singleNote = null;

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
                singleNote.SetActive(false);
                break;
            
            case NoteSceneStates.ShowAllNotes:
                TransitionText("Here are the notes that we will talk about. There are more, but after this tutorial you will be able to read all notes.\nPress C to continue");
                allNotes.SetActive(true);
                singleNote.SetActive(false);
                break;
            
            case NoteSceneStates.ExplainCleff:
                TransitionText("Notice how the notes are ordered. When you go up the staff, the note also increases in alphabetical order. For example, notice how C is at the bottom, and right above it is D, just like in the alphabet.\nPress C to continue");
                singleNote.SetActive(false);
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ShowOutsideOfStaffExplanation:
                TransitionText("The same holds true for the rest of the Staff, even outside the lines and spaces. Notice how D is below the first line of the Staff from bottom to top.\nPress C to continue");
                singleNote.SetActive(false);
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ShowExtraLinesExplanation:
                TransitionText("When the note goes beyond the Staff, and it is supposed to be in a line, extra lines are added to the Staff. See how C is placed on a line, but that line is not connected to the Staff like all the others\nPress C to continue");
                singleNote.SetActive(false);
                allNotes.SetActive(true);
                break;
            
            case NoteSceneStates.ExplainActivity:
                TransitionText("Now that you know how to identify the notes, let's try to play them. In the next activity, try to play the corresponding note with the virtual piano.\nPress C to continue");
                allNotes.SetActive(false);
                singleNote.SetActive(false);
                break;
            
            case NoteSceneStates.PlayC3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.C);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayD3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.D);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayE3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.E);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayF3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.F);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayG3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.G);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayA3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.A);
                singleNote.SetActive(true);
                break;
            
            case NoteSceneStates.PlayB3:
                TransitionText("");
                allNotes.SetActive(false);
                PlaceNote(Player.Key.B);
                singleNote.SetActive(true);
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
        original.a = 1.0f;
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
                case NoteSceneStates.PlayC3:
                case NoteSceneStates.BlindC3:
                    NextState();
                    break;

                default:
                    break;
            }
        }

        if(key == "D" && (currentState == NoteSceneStates.PlayD3))
            NextState();

        if(key == "E" && (currentState == NoteSceneStates.PlayE3))
            NextState();

        if(key == "F" && (currentState == NoteSceneStates.PlayF3))
            NextState();

        if(key == "G" && (currentState == NoteSceneStates.PlayG3))
            NextState();

        if(key == "A" && (currentState == NoteSceneStates.PlayA3))
            NextState();

        if(key == "B" && (currentState == NoteSceneStates.PlayB3))
            NextState();
    }

    public void NextState()
    {
        if(currentState >= NoteSceneStates.Congrats) return;
        SetState(currentState+1);
    }

    private void PlaceNote(Player.Key key)
    {
        Transform bar = transform.GetChild(0);
        Vector3 notePosition = singleNote.transform.position;
        notePosition.y = bar.GetChild((int)key-1).position.y;
        singleNote.transform.position = notePosition;
        singleNote.transform.GetChild(0).gameObject.SetActive(key == Player.Key.C);
    }
}
