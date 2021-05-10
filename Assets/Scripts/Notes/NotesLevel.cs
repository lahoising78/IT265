using UnityEngine;
using System.Collections;
using TMPro;

public class NotesLevel : MonoBehaviour
{
    [SerializeField]
    private NoteSceneStates startState = NoteSceneStates.Welcome;
    [SerializeField] private GameObject wholeNotePrefab = null;
    public TMP_Text textElement = null;

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
                TransitionText("Welcome! In this section you will learn how to recognize the notes in a staff. \nPress C to continue");
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
