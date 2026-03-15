  using TMPro;
  using UnityEngine;
  using UnityEngine.UI;

  public class PlayerAmmoHUD : MonoBehaviour
  {
      [SerializeField] Text ammoText;
      [SerializeField] TextMeshProUGUI reloadText;      
      [SerializeField] Image ammoBar;          // optional
      [SerializeField] float lerpSpeed = 6f;   // smooth fill

      WeaponController weapon;

      void Awake() => weapon = FindFirstObjectByType<WeaponController>();

      void OnEnable()  { if (weapon) weapon.OnAmmoChanged += UpdateUI; }
      void OnDisable() { if (weapon) weapon.OnAmmoChanged -= UpdateUI; }

      void UpdateUI(int current, int max, bool reloading)
      {
          float pct = max > 0 ? (float)current / max : 0f;
          if (ammoBar) ammoBar.fillAmount = Mathf.Lerp(ammoBar.fillAmount, pct, lerpSpeed * Time.deltaTime);
          ammoText.text = reloading ? "Reloading..." : $"Ammo: {current}/{max}";
          reloadText.text = reloading ? "Reloading..." : "Reload";
      }
  }
