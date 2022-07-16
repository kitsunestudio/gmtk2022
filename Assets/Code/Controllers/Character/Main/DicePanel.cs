using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePanel : MonoBehaviour
{
    public Image d4Image;
    public Image d6Image;
    public Image d8Image;
    public Image d10Image;
    public Image d12Image;
    public Image d20Image;
    public Color selected;
    public Color normal;
    public Color empty;

    public void setActive() {
        gameObject.SetActive(true);
    }

    public bool getActive() {
        return gameObject.activeSelf;
    }

    public void highlightActive(int die) {
        if(die == 4) {
            d4Image.color = selected;
            d6Image.color = normal;
            d8Image.color = normal;
            d12Image.color = normal;
            d20Image.color = normal;
        } else if(die == 6) {
            d4Image.color = normal;
            d6Image.color = selected;
            d8Image.color = normal;
            d12Image.color = normal;
            d20Image.color = normal;
        } else if(die == 8) {
            d4Image.color = normal;
            d6Image.color = normal;
            d8Image.color = selected;
            d12Image.color = normal;
            d20Image.color = normal;
        } else if(die == 12) {
            d4Image.color = normal;
            d6Image.color = normal;
            d8Image.color = normal;
            d12Image.color = selected;
            d20Image.color = normal;
        } else if(die == 20) {
            d4Image.color = normal;
            d6Image.color = normal;
            d8Image.color = normal;
            d12Image.color = normal;
            d20Image.color = selected;
        }
    }
}
