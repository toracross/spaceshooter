using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image playerLivesImage;
    public Text scoreText;
    public int score;
    public GameObject titleScreen;

    public void UpdateLives(int currentLives) {
        Debug.Log(currentLives);
        playerLivesImage.sprite = lives[currentLives];
    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen() {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen() {
        titleScreen.SetActive(false);
        score = 0;
        scoreText.text = "Score: 0";
    }

}
