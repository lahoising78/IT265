using UnityEngine;

public class RawSong
{
    private Note[] notes;

    public RawSong()
    {
        notes = new Note[]{
            new Note(Player.Key.C, RhythmNote.WHOLE,    4),
            new Note(Player.Key.D, RhythmNote.HALF,     4),
            new Note(Player.Key.E, RhythmNote.HALF,     4),
            new Note(Player.Key.F, RhythmNote.QUARTER,  4),
            new Note(Player.Key.G, RhythmNote.QUARTER,  4),
            new Note(Player.Key.A, RhythmNote.QUARTER,  4),
            new Note(Player.Key.B, RhythmNote.QUARTER,  4),
        };
    }

    public Note[] GetNotes() { return notes; }
}

public enum RhythmNote
{
    WHOLE = 0,
    HALF,
    QUARTER,
    EIGTH,
    SIXTEENTH
}

public struct Note
{
    public Player.Key key;
    public RhythmNote rhythm;
    public int octave;

    public Note(Player.Key key, RhythmNote rhythm, int octave)
    {
        this.key = key;
        this.rhythm = rhythm;
        this.octave = octave;
    }
}