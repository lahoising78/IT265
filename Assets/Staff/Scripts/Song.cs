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
    private Transform currentBar = null;
    
    private float spaceBetweenNotesY = 0.0f;
    private float spacePerQuarterNoteX = 0.0f;
    private float barWidth = 0.0f;
    private float bottomLineY = 0.0f;
    private float barNoteStartOffset = 0.0f;

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

            barNoteStartOffset = notesSpaceMin.localPosition.x;

            Destroy(barDimensions.gameObject);
        }

        SetupSong(new RawSong(), 60);
    }

    public void SetupSong(RawSong song, int bpm)
    {
        this.rawSong = song;

        notes.Clear();
        float time = 0.0f;
        int barCount = 0;
        float currentBarCompletion = 1.0f;
        float currentOffset = 0.0f;
        foreach(Note note in song.GetNotes())
        {
            AddNote(note, ref time, bpm);
            PlaceNote(note, ref barCount, ref currentBarCompletion, ref currentOffset);
        }
    }

    private void AddNote(Note note, ref float time, int bpm)
    {
        float secsPerWholeNote = bpm / 60.0f * 4.0f;
        ProcessedNote processed = new ProcessedNote();
        processed.note = note;
        
        float noteDuration = (float)(1 << (int)(note.rhythm));
        noteDuration = secsPerWholeNote / noteDuration;
        processed.time = time = time + noteDuration;

        notes.Enqueue(processed);
    }

    private void PlaceNote(Note note, ref int barCount, ref float currentBarCompletion, ref float currentOffset)
    {
        if(currentBarCompletion == 1.0f)
        {
            InstantiateBar(ref barCount, ref currentOffset);
        }
    }

    private void InstantiateBar(ref int barCount, ref float currentOffset)
    {
        Vector3 newBarPosition = new Vector3(
            barsTransform.position.x + barCount * barWidth,
            barsTransform.position.y,
            barsTransform.position.z
        );

        currentBar = InstantiateBarElement(
            barPrefab,
            newBarPosition
        ).transform;

        barCount++;
        currentOffset += barWidth;
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
