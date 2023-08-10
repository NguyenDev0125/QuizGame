using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Image healthFill;
    [SerializeField] Image damageFill;


    public void ShowHealthBar(int _maxHealth)
    {
        
        healthBar.maxValue = _maxHealth;
        healthBar.value = _maxHealth;
        damageFill.fillAmount = 1f;
    }

    public void HideHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }
    public void UpdateHealthBar(int _health)
    {
        healthBar.value = _health;
        damageFill.DOFillAmount(_health / healthBar.maxValue, 1.5f);
    }
}
