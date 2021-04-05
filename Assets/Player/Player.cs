using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Dictionary<Key, KeyCode> keyToCode = new Dictionary<Key, KeyCode>{
        {Key.C, KeyCode.S},
        {Key.D, KeyCode.D},
        {Key.E, KeyCode.F},
        {Key.F, KeyCode.G},
        {Key.G, KeyCode.H},
        {Key.A, KeyCode.J},
        {Key.B, KeyCode.K},
    };

    private VirtualPianoKey[] virtualKeys = null;

    void Awake()
    {
        virtualKeys = GetComponentsInChildren<VirtualPianoKey>();
    }

    public bool IsKeyPressed(Key key)
    {
        return  virtualKeys[ (int)key - 1 ].isPresssed;
    }
    
    public enum Key : int
    {
        SILENCE = 0,
        C,
        D,
        E,
        F,
        G,
        A,
        B,
        COUNT
    }
}
