using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    [SerializeField] private Transform barsTransform = null;
    [SerializeField] private GameObject barPrefab = null;
    [SerializeField] private GameObject extraLinePrefab = null;
    [SerializeField] private GameObject wholePrefab = null;
    private Queue<ProcessedNote> notes = new Queue<ProcessedNote>();
    private RawSong rawSong = null;
    
    private float spaceBetweenNotesY = 0.0f;
    private float spacePerQuarterNoteX = 0.0f;
    private float barWidth = 0.0f;
    private float bottomLineY = 0.0f;
    private float barStartOffset = 0.0f;

    void Awake()
    {
        Transform barDimensions = GameObject.Find("BarDimensions").transform;
        if(barDimensions)
        {
            Transform min = barDimensions.GetChild(0);
            Transform max = barDimensions.GetChild(1);
            barWidth =  max.position.x -
                        min.position.x;

            bottomLineY = min.position.y;

            spaceBetweenNotesY =    max.position.y -
                                    min.position.y;
            spaceBetweenNotesY /= 8.0f;

            Transform notesSpaceMin = barDimensions.GetChild(2);
            Transform notesSpaceMax = barDimensions.GetChild(3);
            spacePerQuarterNoteX =  notesSpaceMax.position.x -
                                    notesSpaceMin.position.x;
            spacePerQuarterNoteX /= 4.0f;

            barStartOffset = notesSpaceMin.localPosition.x;

            Destroy(barDimensions.gameObject);
        }

        SetupSong(new RawSong(), 60);
    }

    public void SetupSong(RawSong song, int bpm)
    {
        float secsPerWholeNote = bpm / 60.0f * 4.0f;
        this.rawSong = song;

        notes.Clear();
        float time = 0.0f;
        foreach(Note note in song.GetNotes())
        {
            ProcessedNote processed = new ProcessedNote();
            processed.note = note;
            
            float noteDuration = (float)(1 << (int)(note.rhythm));
            noteDuration = secsPerWholeNote / noteDuration;
            processed.time = time = time + noteDuration;

            Debug.Log("note time: " + processed.time);
            notes.Enqueue(processed);
        }
    }

    private GameObject InstantiateBarElement(GameObject prefab, Vector3 position)
    {
        return Instantiate(
            prefab,
            position,
            Quaternion.identity,
            barsTransform
        );
    }
    
    private struct ProcessedNote
    {
        public Note note;
        public float time;
    }
}
