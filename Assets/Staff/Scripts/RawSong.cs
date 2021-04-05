using UnityEngine;

public class RawSong
{
    private Note[] notes;

    public RawSong()
    {
        notes = new Note[]{
            new Note(Player.Key.C, RhythmNote.WHOLE, 4),
            new Note(Player.Key.D, RhythmNote.WHOLE, 4),
            new Note(Player.Key.E, RhythmNote.WHOLE, 4),
            new Note(Player.Key.F, RhythmNote.WHOLE, 4),
            new Note(Player.Key.G, RhythmNote.WHOLE, 4),
            new Note(Player.Key.A, RhythmNote.WHOLE, 4),
            new Note(Player.Key.B, RhythmNote.WHOLE, 4),
        };
    }
}

public enum RhythmNote
{
    WHOLE,
    HALF,
    EIGTH,
    SIXTEENTH
}

public struct Note
{
    public Player.Key key;
    RhythmNote rhythm;
    public int octave;

    public Note(Player.Key key, RhythmNote rhythm, int octave)
    {
        this.key = key;
        this.rhythm = rhythm;
        this.octave = octave;
    }
}