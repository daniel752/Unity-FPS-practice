using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] Image frontXpBar;
    [SerializeField] Image backXpBar;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject levelUpVfx;
    AudioSource levelUpSfx;
    private int exp = 0;
    private int expLevelCap = 10;
    private int level = 1;

    private void Awake() 
    {
        levelUpSfx = GetComponent<AudioSource>();
        levelText.text = level.ToString();
    }
    public void GainExp(int xp)
    {
        // Debug.Log($"adding to exp {xp} and is now {exp + xp}");
        exp += xp;
        if (exp >= expLevelCap)
            LevelUp();
        UpdateExpUI();
    }
    private void LevelUp()
    {
        exp = 0;
        expLevelCap += 10;
        level++;
        levelText.text = level.ToString();
        levelUpSfx.Play();
        Instantiate(levelUpVfx,transform);
        // Debug.Log($"level: {level}");
    }
    private void UpdateExpUI()
    {
        float exp = this.exp;
        float expLevelCap = this.expLevelCap;
        // Debug.Log($"fill amount is {exp / expLevelCap}");
        frontXpBar.fillAmount = exp / expLevelCap;
    }
}
