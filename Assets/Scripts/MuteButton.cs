using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Image imageRender;
    private bool muted = false;


    public void ToggleGraphic()
    {
        if (!muted)
        {
            muted = !muted;
            imageRender.sprite = soundOff;
        }
        else
        {
            muted = !muted;
            imageRender.sprite = soundOn;
        }
    }
}
