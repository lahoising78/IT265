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
        {Key.B, KeyCode.J},
    };

    public bool IsKeyPressed(Key key)
    {
        return Input.GetKey( keyToCode[key] );
    }
    
    public enum Key : int
    {
        SILENCE = 0,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        COUNT
    }
}
