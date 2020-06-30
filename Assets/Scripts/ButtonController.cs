using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Image thisSprite;
    
    public int thisButtonNumber;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite = this.gameObject.GetComponent<Image>();
        
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PointerDown()
    {
        AudioManager.instance.Play(thisButtonNumber + 1);
        thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 1f);

    }
    public void PointerUp()
    {
        thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 0.40f);
        manager.ColorPressed(thisButtonNumber);
        AudioManager.instance.Stop(thisButtonNumber + 1);
    }
}
