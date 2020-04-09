using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum Menu {
    open,
    rank,
    start,
    play,
    over
}
public class UIManager : MonoBehaviour
{
    public GameObject panelGameOpen;
    public GameObject panelGameStart;    
    public GameObject panelGamePlay;
    public GameObject panelRank;
    public Image imgMedal;
    public Sprite[] spritesScore;
    public Sprite[] spritesMedal;
    private NativeShare share;
    public Text txtScore;
    public Text txtRankScore;
    public Text txtRankBestScore;

    //Share
    private bool isFocus = false;
	const string shareSubject = "I challenge you to beat my high score in Fluffy Bird";
    private string shareMessage = "";
	const string screenshotName = "fluffybird_share.png";
    private string urlPlayStore;

    // Start is called before the first frame update
    void Start()
    {
        urlPlayStore = "market://details?id=" + Application.identifier;
        shareMessage = shareSubject +
		"! Get the Fluffy Bird game from Play Store the link below. \nCheers :D\n" +
		"\nhttps://play.google.com/store/apps/details?id=" + Application.identifier;

    }

    void OnApplicationFocus (bool focus) {
		isFocus = focus;
	}

    public void SwitchMenu(Menu menu){
        switch (menu)
        {
            case Menu.open :
            panelGameOpen.SetActive(true);
            break;

            case Menu.rank :
            panelRank.SetActive(true);
            panelRank.transform.GetChild(0).gameObject.SetActive(false); //Gameover
            panelGameOpen.SetActive(false);
            setRankScore();
            setMedalRank();
            break;

            case Menu.start :
            panelGameStart.SetActive(true);
            break;

            case Menu.play :
            panelGameStart.SetActive(false);
            panelGamePlay.SetActive(true);
            panelRank.SetActive(false);
            break;
            
            case Menu.over :
            panelGamePlay.SetActive(false);
            panelRank.SetActive(true);
            GameManager.Instance.scoreManager.SaveScore();
            setRankScore();
            setMedalRank();
            break;

            default:
            break;
        }
    }

    public void UpdateScore(int score){
        txtScore.text = score.ToString();
    }

    private void setMedalRank(){
        Medal medal = GameManager.Instance.scoreManager.currentMedal;
        imgMedal.enabled = true;
        switch(medal){
            case Medal.none:
            imgMedal.enabled = false;
            break;
            case Medal.bronze:
            imgMedal.sprite = spritesMedal[0];
            break;
            case Medal.silver:
            imgMedal.sprite = spritesMedal[1];
            break;
            case Medal.gold:
            imgMedal.sprite = spritesMedal[2];
            break;
            case Medal.platinum:
            imgMedal.sprite = spritesMedal[3];
            break;
            default:
            imgMedal.enabled = false;
            break;
        }
    }

    private void setRankScore(){
        txtRankScore.text = GameManager.Instance.scoreManager.score.ToString();
        txtRankBestScore.text = GameManager.Instance.scoreManager.bestScore.ToString();
    }

    public void PlayGame(){
        GameManager.Instance.Replay();
    }

    public void ShowRank(){
        SwitchMenu(Menu.rank);
    }

    public void Share(){
		Debug.Log("Share");

        ScreenCapture.CaptureScreenshot(screenshotName);
        string filePath = Application.persistentDataPath + "/" + screenshotName;
        Debug.Log(filePath);
        
        share = new NativeShare();
        share.SetSubject(shareSubject);
        share.SetText(shareMessage);
        share.SetTitle(shareSubject);
        share.AddFile(filePath, null);
        share.Share();
    }

    public void Rate(){
        Debug.Log("Rate");
        Application.OpenURL(urlPlayStore);
    }
}
