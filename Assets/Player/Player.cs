using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // [SerializeField] private Transform virtualPianoButtons = null;
    // private Button[] buttons = null;

    private Dictionary<Key, KeyCode> keyToCode = new Dictionary<Key, KeyCode>{
        {Key.C, KeyCode.S},
        {Key.D, KeyCode.D},
        {Key.E, KeyCode.F},
        {Key.F, KeyCode.G},
        {Key.G, KeyCode.H},
        {Key.A, KeyCode.J},
        {Key.B, KeyCode.K},
    };

    private Dictionary<Key, bool> virtualPiano = new Dictionary<Key, bool>();

    void Awake()
    {
        // if(virtualPianoButtons)
        // {
        //     buttons = virtualPianoButtons.GetComponentsInChildren<Button>();
        //     foreach(Button btn in buttons)
        //     {
        //         Key key = (Key)(btn.transform.GetSiblingIndex() + 1);
        //         virtualPiano[key] = false;
                
        //         btn.onClick.AddListener(delegate() {
        //             virtualPiano[ key ] = true;
        //         });
        //     }
        // }
    }

    void Update()
    {
        // for(int i = 1; i < (int)Key.COUNT; i++)
        // {
        //     Key key = (Key)i;
        //     if(key == Key.SILENCE) continue;
        //     if(IsKeyPressed(key))
        //         buttons[i-1].onClick.Invoke();
        // }
    }

    public bool IsKeyPressed(Key key)
    {
        return Input.GetKey( keyToCode[key] );
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
