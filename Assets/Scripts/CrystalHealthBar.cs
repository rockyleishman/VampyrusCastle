using UnityEngine;
using UnityEngine.UI;

public class CrystalHealthBar : MonoBehaviour
{
    private Canvas _healthBarCanvas;
    private Slider _slider;
    [SerializeField] public Image Fill;
    [SerializeField] public Color FullHealthColour;
    [SerializeField] public Color TwoThirdsHealthColour;
    [SerializeField] public Color OneThirdHealthColour;

    private void Start()
    {
        _healthBarCanvas = GetComponent<Canvas>();
        _healthBarCanvas.worldCamera = Camera.main;
        _slider = _healthBarCanvas.transform.GetChild(0).GetComponent<Slider>();
        _slider.maxValue = DataManager.Instance.LevelDataObject.MaxCrystalHP;
    }

    public void UpdateHP()
    {
        //change slider value
        _slider.value = DataManager.Instance.LevelDataObject.CrystalHP;

        //change colour
        float hpRatio = DataManager.Instance.LevelDataObject.CrystalHP / DataManager.Instance.LevelDataObject.MaxCrystalHP;
        if (hpRatio >= 2.0f / 3.0f)
        {
            Fill.color = Color.Lerp(TwoThirdsHealthColour, FullHealthColour, (hpRatio - (2.0f / 3.0f)) * 3.0f);
        }
        else if (hpRatio >= 1.0f / 3.0f)
        {
            Fill.color = Color.Lerp(OneThirdHealthColour, TwoThirdsHealthColour, (hpRatio - (1.0f / 3.0f)) * 3.0f);
        }
        else
        {
            Fill.color = OneThirdHealthColour;
        }
    }
}
