using UnityEngine;

public class VirtualPianoKey : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRend = null;
    [SerializeField] private Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
    private Color idleColor = Color.white;

    [SerializeField] private KeyCode keyCode;

    private bool m_pressed = false;
    private bool m_clicked = false;

    [SerializeField] private AudioSource noteAudio = null;
    [SerializeField] private float pitchIncrement = 1.0f;

    void Awake()
    {
        if(!spriteRend)
        {
            spriteRend = GetComponent<SpriteRenderer>();
        }
        idleColor = spriteRend.color;

        if(!noteAudio) noteAudio = GetComponent<AudioSource>();
        noteAudio.pitch = Mathf.Pow(2.0f, pitchIncrement / 12.0f);
    }

    void Update()
    {
        if(Input.GetKey(keyCode))
        {
            if(!m_pressed) Pressed();
        }
        else if(m_pressed)
        {
            Unpressed();
        }
    }

    private void Pressed()
    {
        noteAudio.Play();
        m_pressed = true;
        spriteRend.color = pressedColor;
    }

    private void Unpressed()
    {
        if(m_clicked) return;
        m_pressed = false;
        spriteRend.color = idleColor;
    }

    void OnMouseDown() { Pressed();   m_clicked = true;  }
    void OnMouseExit() { Unpressed(); m_clicked = false; }
    void OnMouseUp()   { Unpressed(); m_clicked = false; }

    public bool isPresssed
    {
        get { return m_pressed; }
    }
}
