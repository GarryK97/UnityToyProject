using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerState : MonoBehaviour
{   
    public Rigidbody rb;
    public Slider hpSlider;
    private TextMeshProUGUI hpText;
    public Slider staminaSlider;
    private TextMeshProUGUI staminaText;
    public static float PLAYERMAXHP = 100;
    public static float playerHp = PLAYERMAXHP;
    public static float PLAYERMAXSTAMINA = 100;
    public static float playerStamina = PLAYERMAXSTAMINA;
    public float StaminaRegainValue;
    public float StaminaRegenCooldown;
    private static float StaminaRegenCheckTime;
    public static bool isRunning = false;
    public static bool isMidAir = false;
    public static bool isFalling = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        hpSlider.maxValue = (int)PLAYERMAXHP;
        hpSlider.value = (int)playerHp;
        hpText = hpSlider.transform.Find("HpText").gameObject.GetComponent<TextMeshProUGUI>();

        staminaSlider.maxValue = (int)PLAYERMAXSTAMINA;
        staminaSlider.value = (int)playerStamina;
        staminaText = staminaSlider.transform.Find("StaminaText").gameObject.GetComponent<TextMeshProUGUI>();

        StaminaRegenCheckTime = StaminaRegenCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
        LimitHpStamina();

        float zMove = Input.GetAxis("Vertical");
        isRunning = (Input.GetKey(KeyCode.LeftShift) && zMove > 0f && s_IsStaminaAvailable());
    }

    void FixedUpdate()
    {   
        StaminaRegenCheckTime += Time.deltaTime;
        RegainStamina();
    }

    void UpdateSliders()
    {
        hpSlider.value = playerHp;
        hpText.text = $"HP {(int)playerHp} / {PLAYERMAXHP}";

        staminaSlider.value = playerStamina;
        staminaText.text = $"Stamina {(int)playerStamina} / {PLAYERMAXSTAMINA}";
    }

    void LimitHpStamina()
    {   
        // Upper Limit
        playerHp = Mathf.Min(PLAYERMAXHP, playerHp);
        playerStamina = Mathf.Min(PLAYERMAXSTAMINA, playerStamina);

        // Lower Limit
        playerHp = Mathf.Max(0, playerHp);
        playerStamina = Mathf.Max(0, playerStamina);

        StaminaRegenCheckTime = Mathf.Min(100, StaminaRegenCheckTime);
    }

    public static void takeDamage(float damageValue)
    {
        playerHp -= damageValue;
    }

    public static void HealPlayer(float healValue)
    {
        playerHp += healValue;
    }

    private void RegainStamina()
    {   
        if (StaminaRegenCheckTime > StaminaRegenCooldown)
        {
            playerStamina += StaminaRegainValue;
        }
        
    }

    public static void s_UseStamina(float staminaBurnValue)
    {   
        playerStamina -= staminaBurnValue;
        StaminaRegenCheckTime = 0f;
    }

    public static bool s_IsStaminaAvailable()
    {
        return playerStamina > 0f;
    }

    float getPlayerMaxHp(){return PLAYERMAXHP;}
    float getPlayerMaxStamina(){return PLAYERMAXSTAMINA;}
}
