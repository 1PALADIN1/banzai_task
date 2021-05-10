using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class HpUiView : MonoBehaviour
    {
        [SerializeField] private Image _hpImage;

        public void SetHp(int currentValue, int maxValue)
        {
            var hpValue = (float) currentValue / maxValue;
            _hpImage.fillAmount = Mathf.Clamp01(hpValue);;
        }
    }
}