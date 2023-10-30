using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Canvas _playerCanvas;
    public Image image;
    public Gradient gradient;
    private Slider _slider;

    private float _remapValue;
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
        _remapValue = (_slider.value - 0) / 100;
        image.color = gradient.Evaluate(_remapValue);
    }


}
