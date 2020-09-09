using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// Управление здоровьем игрока и UI
public class HealthBar : GlobalFunctions
{
    public Color st = new Color(0f, 0.7843137f, 1f, 1f);
    public Color im = new Color(1f, 0.8823529f, 0f, 1f);
    public GameObject player;
    public GameObject background;
    public GameObject health;
    public Management m;
    public Animator animator;
    public Image healthBarImage;
    public float speed;
    public float healthEnd = 1f;
    private float timerCount;
    private bool active = false;
    private bool animActive = false;
    private string tagDead = "Saw";

    private bool immortality = false;
    private bool newImmortality = false;
    private float newImmortalityTimer = 0f;
    
    private Rigidbody2D playerRB;
    private SpriteRenderer playerSR;
    private TrailRenderer playerTR;
    private bool radVF = false;

    private void Start()
    {
        animActive = PlayerPrefs.GetString("graphicsquality") == "high";
        playerRB = player.GetComponent<Rigidbody2D>();
        playerSR = player.GetComponent<SpriteRenderer>();
        playerTR = player.GetComponent<TrailRenderer>();
        VisFalse();
        UpdateColor(false);
    }

    /// Триггер к началу получения постоянного урона от VelocityField
    public void StartVFRad(float damage)
    {
        countersActiveVF++;
        if (radVF == false)
        {
            radVF = true;
            animator.SetBool("isVisible", true);
            if (coroutineVFRad != null) { StopCoroutine(coroutineVFRad); }
            coroutineVFRad = StartCoroutine(VFRad(damage));
        }
    }

    /// Получение постоянного урона от VelocityField
    private int counterVF = 0;
    private Coroutine coroutineVFRad;
    private int countersActiveVF = 0;
    public IEnumerator VFRad(float damage)
    {
        yield return new WaitForSeconds(0.01f);
        if (radVF)
        {
            counterVF += 1;
            StraightDamage(damage, "VelocityField");
            coroutineVFRad = StartCoroutine(VFRad(damage));
        }
    }

    /// Восстановление здоровья до максимума
    public IEnumerator VFRadHeal(float heal)
    {
        yield return new WaitForSeconds(0.01f);
        if (counterVF > 0)
        {
            counterVF -= 1;
            StraightHeal(heal, "VelocityField");
            coroutineVFRad = StartCoroutine(VFRadHeal(heal));
        }
        else
        {
            animator.SetBool("isVisible", false);
        }
    }

    /// Триггер к окончанию нанесения урона от VelocityField и восстановлению здоровья до максимума
    public void EndVFRad(float heal)
    {
        countersActiveVF--;
        if (radVF && countersActiveVF == 0f)
        {
            radVF = false;
            if (coroutineVFRad != null) { StopCoroutine(coroutineVFRad); }
            if (heal > 0f) { coroutineVFRad = StartCoroutine(VFRadHeal(heal)); }
            else { animator.SetBool("isVisible", false); }
        }
    }

    /// Таймер того, сколько после получения урона должен быть видимым HealthBar
    private IEnumerator VisibleFalse()
    {
        yield return new WaitForSeconds(0.1f);
        if (timerCount > 0f)
        {
            timerCount -= 0.1f;
            StartCoroutine(VisibleFalse());
        }
        else
        {
            VisFalse();
        }
    }

    /// Затухание HealthBar
    public void VisFalse()
    {
        animator.SetBool("isVisible", false);
    }

    /// Активация временного бессмертия
    public void Immortality(float setSecImmortality, bool isLaser = false)
    {
        if (immortality == false)
        {
            active = false;
            speed = healthEnd = 0f;
            immortality = true;
            UpdateColor();
            StartCoroutine(ImmortalityFalse(setSecImmortality));
        }
        else
        {
            if (isLaser == false)
            {
                newImmortalityTimer += setSecImmortality;
                newImmortality = true;
            }
        }
    }

    /// Упрощённая активация временного бессмертия
    public void Immortality()/////////////////////////
    {
        active = false;
        speed = healthEnd = 0f;
        if (immortality == false)
        {
            immortality = true;
            animator.SetBool("isVisible", true);
            timerCount = 3f;
        }
        StartCoroutine(VisibleFalse());
        UpdateColor(false);
    }

    /// Конец бессмертия
    private Coroutine coroutine;
    public void End(float setSecImmortality)
    {
        if (setSecImmortality == 0) { IF(); return; }
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = StartCoroutine(ImmortalityFalse(setSecImmortality));
    }

    /// Тайме бессмертия, связанный с HealthBar
    private IEnumerator ImmortalityFalse(float setSecImmortality)
    {
        yield return new WaitForSeconds(setSecImmortality);
        IF();
    }

    /// Изменение HealthBar после окончания бессмертия
    public void IF()
    {
        immortality = false;
        if (newImmortality == true)
        {
            newImmortality = false;
            Immortality(newImmortalityTimer);
            newImmortalityTimer = 0f;
        }
        else
        {
            UpdateColor();
        }
        coroutine = null;
        return;
    }

    /// Бонусное восстановление здоровья
    public void Heal(bool isSet, float inputHealth)
    {
        if (animActive == true)
        {
            if (isSet == true)
            {
                if (!(immortality == true && inputHealth < healthBarImage.fillAmount))
                {
                    healthEnd = inputHealth;
                    speed += 0.05f;
                }
            }
            else
            {
                float heal = Stable2(healthEnd + inputHealth, 0f, 1f);
                if (immortality == false && heal < healthBarImage.fillAmount)
                {
                    healthEnd = heal;
                    speed += 0.05f;
                }
            }
            active = true;
        }
        else
        {
            if (isSet == true)
            {
                healthEnd = inputHealth;
                healthBarImage.fillAmount = inputHealth;
            }
            else
            {
                float heal = Stable2(healthEnd + inputHealth, 0f, 1f);
                if (immortality == false && heal < healthBarImage.fillAmount)
                {
                    healthEnd = heal;
                    healthBarImage.fillAmount = heal;
                }
            }
            
            animator.SetBool("isVisible", true);
            if (healthBarImage.fillAmount == 0f)
            {
                animator.SetBool("isDead", true);
                animator.SetBool("isVisible", false);
                playerTR.startColor = new Color(0f, 0f, 0f, 0f);
                radVF = false;
                m.Dead(tagDead);
            }
            else
            {
                timerCount = 2f;
                StartCoroutine(VisibleFalse());
            }
            UpdateColor(false);
        }
    }

    /// Упрощённое восстановление здоровья
    public void Heal(float inp)
    {
        healthBarImage.fillAmount += inp;
        UpdateColor(false);
        animator.SetBool("isVisible", healthBarImage.fillAmount != 1f);
    }

    /// Перманентная смерть
    public void StraightDamage(float damage, string tag)
    {
        healthEnd -= damage;
        if (healthEnd < 0f) { healthEnd = 0f; }
        healthBarImage.fillAmount = healthEnd;
        if (healthBarImage.fillAmount == 0f)
        {
            tagDead = tag;
            animator.SetBool("isDead", true);
            playerTR.startColor = new Color(0f, 0f, 0f, 0f);
            radVF = false;
            m.Dead(tagDead);
        }
        else
        {
            UpdateColor(false);
        }
    }

    /// Перманентный heal
    public void StraightHeal(float heal, string tag)
    {
        healthEnd += heal;
        if (healthEnd > 1f) { healthEnd = 1f; }
        healthBarImage.fillAmount = healthEnd;
        UpdateColor(false);
    }

    /// Нанесение урона (в частности бомбами)
    public void Damage(float damage, string tag, Vector2 velocity)
    {
        if (velocity != Vector2.zero)
        {
            playerRB.velocity += velocity;
        }
        if (damage != 0f)
        {
            tagDead = tag;
            if (animator.GetBool("isDead") == false && immortality == false)
            {
                damage /= 2f;
                if (animActive == true)
                {
                    healthEnd = Stable2(healthEnd - damage, 0f, 1f);
                    speed = speed + damage;
                    active = true;
                }
                else
                {
                    healthEnd = Stable2(healthEnd - damage, 0f, 1f);
                    healthBarImage.fillAmount = Stable2(healthBarImage.fillAmount - damage, 0f, 1f);
                    
                    animator.SetBool("isVisible", true);
                    if (healthBarImage.fillAmount == 0f)
                    {
                        animator.SetBool("isDead", true);
                        animator.SetBool("isVisible", false);
                        playerTR.startColor = new Color(0f, 0f, 0f, 0f);
                        radVF = false;
                        m.Dead(tagDead);
                    }
                    else
                    {
                        timerCount = 2f;
                        StartCoroutine(VisibleFalse());
                    }
                    UpdateColor(false);
                }
            }
        }
    }

    /// Обновление цвета HealthBar
    private void UpdateColor(bool see = true)
    {
        if (immortality == false)
        {
            float f = healthBarImage.fillAmount;
            Color st1 = new Color(st.r, st.g, st.b, healthBarImage.color.a);
            Color pl1 = new Color(st.r * f, st.g * f, st.b * f, 1f);
            if (healthBarImage.fillAmount != 0f)
            {
                healthBarImage.color = st1;
                playerSR.color = pl1;
                playerTR.startColor = pl1;
            }
        }
        else
        {
            Color im2 = new Color(im.r, im.g, im.b, healthBarImage.color.a);
            if (healthBarImage.fillAmount != 0f)
            {
                healthBarImage.color = im2;
                playerSR.color = im;
                playerTR.startColor = im;
            }
        }

        if (see == true)
        {
            animator.SetBool("isVisible", true);
            timerCount = 3f;
            StartCoroutine(VisibleFalse());
        }
    }

    /// Анимация полоски здоровья (не очень)
    private void Update()
    {
        if (active == true)
        {
            if (healthBarImage.fillAmount != healthEnd)
            {
                float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
                if (animator.GetBool("isVisible") == false)
                {
                    animator.SetBool("isVisible", true);
                }
                healthBarImage.fillAmount = MoveToward(healthBarImage.fillAmount, healthEnd, speed * s, new Vector2(0f, 1f));
                UpdateColor();
            }
            else
            {
                if (healthBarImage.fillAmount == 0f)
                {
                    animator.SetBool("isDead", true);
                    animator.SetBool("isVisible", false);
                    playerTR.startColor = new Color(0f, 0f, 0f, 0f);
                    radVF = false;
                    m.Dead(tagDead);
                }
                else
                {
                    UpdateColor(false);
                }
                speed = healthEnd = 0f;
                active = false;
            }
        }
    }
}
