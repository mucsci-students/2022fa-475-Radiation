using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealthBar : MonoBehaviour
{
    public Image TurretBar;
    public GameObject TurretDataPanel;

    // Start is called before the first frame update
    void Start()
    {
        TurretBar.fillAmount = 1f;
    }

    public void updateBar(int enemyHealth, int maxEnemyHealth){
        TurretBar.fillAmount = ((float)enemyHealth / (float)maxEnemyHealth);
    }

    public void killTurret(){
        TurretDataPanel.SetActive(false);
    }
}
