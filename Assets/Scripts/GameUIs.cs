using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIs : MonoBehaviour
{
    public Image PlayerHealthBar;
    public Image BossHealthBar;
    public PlayerData player;
    public BossData boss;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerData>();
        PlayerHealthBar = GetComponent<Image>();     
        PlayerHealthBar.fillAmount = 1f;   
    }

    public void updatePlayerHealthBar(int curPlayerHealth, int maxPlayerHealth)
    {
        PlayerHealthBar.fillAmount = ((float)curPlayerHealth / (float)maxPlayerHealth);
    }

    public void updateBossHealthBar(int curBossHealth, int maxBossHealth)
    {
        BossHealthBar.fillAmount = ((float)curBossHealth / (float)maxBossHealth);
    }

    public void respawnPlayer(){
        PlayerHealthBar.fillAmount = 1f;
    }

}
