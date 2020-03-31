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
    public Transform tfScore;
    public Transform tfRankScore;
    public Transform tfRankBestScore;
    public Image imgMedal;
    public Sprite[] spritesScore;
    public Sprite[] spritesMedal;
    private NativeShare share;

    //Share
    private bool isFocus = false;
	const string shareSubject = "I challenge you to beat my high score in Fluffy Bird";
    public string shareMessage = "";
	const string screenshotName = "fluffybird_share.png";
    public string urlPlayStore;

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
            setMedalRank();
            showScoreTransform(GameManager.Instance.scoreManager.bestScore, tfRankBestScore);
            showScoreTransform(GameManager.Instance.scoreManager.score, tfRankScore);
            tfRankScore.localScale = Vector3.one * 0.4f;
            tfRankBestScore.localScale = Vector3.one * 0.4f;
            break;

            default:
            break;
        }
    }

    public void UpdateScore(int score){
        showScoreTransform(score, tfScore);
    }

    private void showScoreTransform(int score, Transform parent) {
        int count = parent.childCount;
        List<int> splitsScore = new List<int>();
        string strScore = score.ToString();
        for(int i = 0; i<strScore.Length; i++){
            splitsScore.Add(int.Parse(strScore[i].ToString()));
        }

        int countNewBlock = strScore.Length - count;
        for(int i = 0; i < countNewBlock; i++){
            addNewBlockScore(parent);
        }
        
        for(int i = 0; i<parent.childCount; i++){
            parent.transform.GetChild(i).GetComponent<Image>().sprite = spritesScore[splitsScore[i]];
        }
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

    private void addNewBlockScore(Transform parent){
        int countBlock = parent.childCount;
        GameObject imgScore = new GameObject();
        Image image = imgScore.AddComponent<Image>();
        image.sprite = spritesScore[0];
        imgScore.transform.SetParent(parent);
        imgScore.transform.SetAsLastSibling();
        imgScore.transform.localScale = Vector3.one;
        imgScore.transform.localPosition = Vector3.zero;
        image.SetNativeSize();
        float width = image.rectTransform.rect.width * 9;
        float height = image.rectTransform.rect.height * 9;
        float spacing = 5;
        float xMultiplier = width / 2;
        float startXPos = -(xMultiplier * countBlock);
        image.rectTransform.sizeDelta = new Vector2(width, height);
        width = width * (countBlock + 1) + spacing * (countBlock + 1);
        RectTransform rectScore = parent.GetComponent<RectTransform>();
        rectScore.sizeDelta = new Vector2(width, height);

        for(int i = 0; i<parent.childCount; i++){
            parent.transform.GetChild(i).localPosition = new Vector3(startXPos, 0, 0);
            startXPos += (xMultiplier * 2 + spacing);
        }
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
