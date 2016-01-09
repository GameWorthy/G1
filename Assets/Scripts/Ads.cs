using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {


	private string adIdAndroid = "ca-app-pub-3865236618689243/7848532614";
	private string adIdIOS = "ca-app-pub-3865236618689243/1801999017";
	private string adId;

	private BannerView bannerView = null;

	void Start () {

		adId = adIdAndroid;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			adId = adIdIOS;
		}

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
		bannerView.Show ();

	}

	void OnDestroy() {
		bannerView.Hide ();
	}
}
