using UnityEngine;
using System.Collections;

public class MonoGameEntry : MonoBehaviour {
	bool _loadFileFinished = false;
	string _firstLine = "";
	// Use this for initialization
	IEnumerator Start () {
		yield return StartCoroutine(LoadFromFile());

		Debug.Log("Start " + _firstLine);
	}
	
	// Update is called once per frame
	void Update () {
		if (_loadFileFinished)
		{
			//xxx

			Debug.Log("Update " + _firstLine);
		}

	}

	IEnumerator LoadFromFile()
	{
		string fileName = "Data/AchievementsData.txt";

		AssetBundleLoader myLoader = new AssetBundleLoader();
		yield return StartCoroutine(myLoader.Initialize());
		yield return StartCoroutine(myLoader.LoadAssestAsync(fileName));

		TextAsset textAsset = myLoader.GetAsset() as TextAsset;

		string[] lines = textAsset.text.Split("\n"[0]);

		_firstLine = lines[0];

		myLoader.UnLoadAsset();

		_loadFileFinished = true;

	}

	
}
