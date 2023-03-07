using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthSystem
{
    [Header("HealthBar")]
    public Image frontHealthBar;
    public Image backHealthBar;
    protected float lerpTimer;
    public float chipSpeed = 2f;
    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        lerpTimer = 0;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,0);

    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Player health: " + health);
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health > 30)
            {
                durationTimer += Time.deltaTime;
                if (durationTimer > duration)
                {
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,tempAlpha);
                }
            }
        }
    }
    private void UpdateHealthUI()
    {
        // Debug.Log(gameObject.name + " updating healthbar");

        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = Mathf.Pow((lerpTimer / chipSpeed),2);
            backHealthBar.fillAmount = Mathf.Lerp(fillBack,healthFraction,percentComplete);
        }
        else if (fillFront < healthFraction)
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = Mathf.Pow((lerpTimer / chipSpeed),2);
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront,healthFraction,percentComplete);
        }
    }
    public override void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b,1);

    }
    public override void RestoreDamage(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
    // private void OnCollisionEnter(Collision other) 
    // {
    //     Projectile projectile = other.gameObject.GetComponent<Projectile>();
    //     if(projectile != null)
    //     {
    //         Debug.Log("Player taking damage of " + projectile.GetDamage());
    //         TakeDamage(projectile.GetDamage());
    //     }
    // }
}
