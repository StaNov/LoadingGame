using UnityEngine;
using System.Collections;

public class BottomButton : MonoBehaviour {

    public void Donate() {
        Application.OpenURL("http://wiki.stanov.cz/Hlavn%C3%AD_strana#P.C5.99isp.C4.9Bt_na_dal.C5.A1.C3.AD_tvorbu");
    }

    public void LikeOnFacebook() {
        Application.OpenURL("https://www.facebook.com/StaNovStandaNovak");
    }

    public void MyWebPage() {
        Application.OpenURL("http://www.stanov.cz");
    }

    public void RateThisGame() {
        Application.OpenURL("https://play.google.com/store/apps/details?id=cz.stanov.loading");
    }

    public void ShowAchievements() {
        Social.ShowAchievementsUI();
    }
}
