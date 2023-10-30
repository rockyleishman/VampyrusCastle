using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Canvas _playerCanvas;

    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _playerCanvas = this.GetComponent<Canvas>();
        _playerCanvas.worldCamera=FindObjectOfType<Camera>();
        _playerCanvas.sortingLayerName = "Player";
        _playerCanvas.sortingOrder = 3;
        _slider=_playerCanvas.transform.GetChild(0).GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealthValue(int value)
    {
        _slider.value = value;
    }
}
