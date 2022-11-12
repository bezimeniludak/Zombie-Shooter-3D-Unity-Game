using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;

    public void SetHealth(int health)
    {
        _slider.value = health;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
    public void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;

        _fill.color=_gradient.Evaluate(1f);
    }
}
