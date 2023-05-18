using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    public Button button;
    public Image overlayImage;
    public float cooldownDuration = 5f;

    private bool isCooldown = false;
    private float cooldownStartTime = 0f;

    private void Start()
    {
        button.onClick.AddListener(StartCooldown);
        overlayImage.fillAmount = 0f;
        button.interactable = true; // Enable the button at the start
    }

    private void Update()
    {
        if (isCooldown)
        {
            float remainingCooldown = Mathf.Max(0f, cooldownDuration - (Time.time - cooldownStartTime));
            float fillAmount = 1f - (remainingCooldown / cooldownDuration);
            overlayImage.fillAmount = fillAmount;

            if (remainingCooldown <= 0f)
            {
                isCooldown = false;
                overlayImage.fillAmount = 0f;
                button.interactable = true; // Re-enable the button
            }
        }
    }

    private void StartCooldown()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            cooldownStartTime = Time.time; // Store the cooldown start time
            StartCoroutine(PerformCooldown());
        }
    }

    private IEnumerator PerformCooldown()
    {
        overlayImage.fillAmount = 1f; // Start the overlay image at full fill
        button.interactable = false; // Disable the button during cooldown

        yield return new WaitForSeconds(cooldownDuration);

        overlayImage.fillAmount = 0f;
        button.interactable = true; // Re-enable the button
    }
}





