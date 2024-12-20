using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stamina : Singleton<Stamina>
{
    [SerializeField] private int refreshingStaminaTime = 3;
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    public int CurrentStamina
    { get; private set; }
    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";




    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());
    }
    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }



    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = startingStamina;
        UpdateStaminaImages();
    }
    private IEnumerator RefreshStaminaRoutine()
    {
        while (CurrentStamina < maxStamina)
        {
            yield return new WaitForSeconds(refreshingStaminaTime);
            RefreshStamina();
        }
    }
    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            Transform child = staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();
            if (i <= CurrentStamina - 1)
            {
                image.sprite = fullStaminaImage;
            }
            else
            {
                image.sprite = emptyStaminaImage;
            }
        }

    }


}
