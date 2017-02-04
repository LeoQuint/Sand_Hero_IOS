using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenter : MonoBehaviour {

	public static GameCenter instance; 

	public string leaderboardName = "Biggest Sandwich";
	public string leaderboardID = "AspiringFires.Sandwich-Hero.grp.sandwichheroleaderboard";

	#if UNITY_IPHONE
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		
		// AUTHENTICATE AND REGISTER A ProcessAuthentication CALLBACK
		// THIS CALL NEEDS OT BE MADE BEFORE WE CAN PROCEED TO OTHER CALLS IN THE Social API
		Social.localUser.Authenticate (ProcessAuthentication);


	}
		
	public void ShowLeaderboard()
	{
		Social.ShowLeaderboardUI();
	}

	public void ShowAchievements()
	{
		Social.ShowAchievementsUI();
	}




	///////////////////////////////////////////////////
	// INITAL AUTHENTICATION (MUST BE DONE FIRST)
	///////////////////////////////////////////////////

	// THIS FUNCTION GETS CALLED WHEN AUTHENTICATION COMPLETES
	// NOTE THAT IF THE OPERATION IS SUCCESSFUL Social.localUser WILL CONTAIN DATA FROM THE GAME CENTER SERVER
	void ProcessAuthentication (bool success) {
		if (success) {
			Debug.Log ("Authenticated, checking achievements");


		}
		else
			Debug.Log ("Failed to authenticate with Game Center.");
	}
		
	///////////////////////////////////////////////////
	// GAME CENTER ACHIEVEMENT INTEGRATION
	///////////////////////////////////////////////////

	public void ReportAchievement( string achievementId, double progress ){
		Social.ReportProgress( achievementId, progress, (result) => {
			Debug.Log( result ? string.Format("Successfully reported achievement {0}", achievementId)
				: string.Format("Failed to report achievement {0}", achievementId));
		});
	}



	#region Game Center Integration
	///////////////////////////////////////////////////
	// GAME CENTER LEADERBOARD INTEGRATION
	///////////////////////////////////////////////////

	///
	/// Reports the score to the leaderboards.
	///
	/// Score.
	/// Leaderboard I.
	void ReportScore (long score, string leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log(success ? "Reported score to leaderboard successfully" : "Failed to report score");
		});
	}



	#endregion
	#endif
}

