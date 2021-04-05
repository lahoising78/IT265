using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    [SerializeField] private Transform barsTransform = null;
    [SerializeField] private GameObject barPrefab = null;
    [SerializeField] private GameObject extraLinePrefab = null;
    private Queue<ProcessedNote> notes = new Queue<ProcessedNote>();
    private RawSong rawSong = null;
    private float spaceBetweenNotes = 0.0f;
    private float barWidth = 0.0f;

    void Awake()
    {
        Transform barDimensions = GameObject.Find("BarDimensions").transform;
        if(barDimensions)
        {
            spaceBetweenNotes = barDimensions.GetChild(1).position.y -
                                barDimensions.GetChild(0).position.y;
            spaceBetweenNotes /= 8.0f;

            barWidth =  barDimensions.GetChild(3).position.x -
                        barDimensions.GetChild(2).position.x;

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
    
    private struct ProcessedNote
    {
        public Note note;
        public float time;
    }
}
