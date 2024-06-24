using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateHud : MonoBehaviour
{
    public GameObject points;
    public GameObject gold;
    public GameObject combo;
    public GameObject superCombo;
    public GameObject slider;
    public GameObject player;


    private Text pointsText;
    private Text goldText;
    private Text comboText;
    private Text superComboText;
    private Slider sliderComponent;
    private NewBehaviourScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        pointsText = points.GetComponent<Text>();
        goldText = gold.GetComponent<Text>();
        comboText = combo.GetComponent<Text>();
        superComboText = superCombo.GetComponent<Text>();
        sliderComponent = slider.GetComponent<Slider>();
        playerScript = player.GetComponent<NewBehaviourScript>();

    }

    // Update is called once per frame
    void Update()
    {
        sliderComponent.value = 100 * playerScript.hp / playerScript.hpMax;
        pointsText.text = "Points: " + playerScript.points.ToString();
        goldText.text = "Gold: " + playerScript.gold.ToString();
        comboText.text = "Combo: " + playerScript.combo.ToString();
        superComboText.text = "Super Combo: " + playerScript.specialCombo.ToString();
    }
}
