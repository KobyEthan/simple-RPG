using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI health, level;
    [SerializeField] private Image healthFill, levelFill;
    [SerializeField] private Player player;

    // Stats
    private List<TMPro.TextMeshProUGUI> playerStatTexts = new List<TMPro.TextMeshProUGUI>();
    [SerializeField] private TMPro.TextMeshProUGUI playerStatPrefab;
    [SerializeField] private Transform playerStatPanel;

    //Equipped Weapon
    [SerializeField] private Sprite defaultWeaponSprite;
    private PlayerWeaponController playerWeaponController;
    [SerializeField]
    private TMPro.TextMeshProUGUI weaponStatPrefab;
    [SerializeField]
    private Transform weaponStatPanel;
    [SerializeField]
    private TMPro.TextMeshProUGUI weaponNameText;
    [SerializeField]
    private Image weaponIcon;
    private List<TMPro.TextMeshProUGUI> weaponStatTexts = new List<TMPro.TextMeshProUGUI>();
    void Start()
    {
        playerWeaponController = player.GetComponent<PlayerWeaponController>();
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;
        InitializeStats();
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

   void UpdateLevel()
    {
       this.level.text = player.PlayerLevel.Level.ToString();
       this.levelFill.fillAmount = (float)player.PlayerLevel.CurrentExperience / (float)player.PlayerLevel.RequiredExperience;
       
    }

    void InitializeStats()
    {
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab));
            playerStatTexts[i].transform.SetParent(playerStatPanel);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatTexts[i].text = player.characterStats.stats[i].StatName + ": " + player.characterStats.stats[i].GetCalculatedStatValue().ToString();
        }
    }

    void UpdateEquippedWeapon(Item item)
    {
        weaponIcon.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
        weaponNameText.text = item.ItemName;

        for (int i = 0; i < item.Stats.Count; i++)
        {
            weaponStatTexts.Add(Instantiate(weaponStatPrefab));
            weaponStatTexts[i].transform.SetParent(weaponStatPanel);
            weaponStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
    }

    public void UnequipWeapon()
    {
        weaponNameText.text = "-";
        weaponIcon.sprite = defaultWeaponSprite;
        for (int i = 0; i < weaponStatTexts.Count; i++)
        {
            Destroy(weaponStatTexts[i].gameObject);
        }
        playerWeaponController.UnequipWeapon();
    }


}
