using UnityEngine;

public class NotesLevel : MonoBehaviour
{
    [SerializeField]
    private NoteSceneStates startState = NoteSceneStates.ShowAllNotes;
    [SerializeField] private GameObject wholeNotePrefab = null;

    void Start()
    {
        SetState(startState);
    }

    public void SetState(NoteSceneStates state)
    {
        switch(state)
        {
            default:
                break;
        }
    }
}
