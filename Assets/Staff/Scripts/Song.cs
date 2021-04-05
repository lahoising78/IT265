using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    private Queue<ProcessedNote> notes = null;
    private RawSong rawSong = null;

    public void SetupSong(RawSong song, int bpm)
    {
        float secsPerWholeNote = bpm / 60.0f * 4.0f;
        this.rawSong = song;

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
