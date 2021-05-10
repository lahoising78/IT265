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

    void Start()
    {
        SetState(startState);
    }

    public void SetState(NoteSceneStates state)
    {
        switch(state)
        {
            case NoteSceneStates.Welcome:
                TransitionText("Welcome! In this section you will learn how to recognize the notes in a staff");
                break;

            default:
                break;
        }
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
}
