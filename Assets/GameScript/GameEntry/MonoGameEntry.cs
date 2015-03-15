using UnityEngine;
using System.Collections;

public class MonoGameEntry : MonoBehaviour {
	bool _loadFileFinished = false;
	string _firstLine = "";
	// Use this for initialization
	IEnumerator Start () {
		yield return StartCoroutine(AssetBundleLoader.Initialize());

		yield return StartCoroutine(LoadFromFile());

		Debug.Log("Start " + _firstLine);
	}
	
	// Update is called once per frame
	void Update () {
		if (_loadFileFinished)
		{
			//xxx
			//Debug.Log("Update " + _firstLine);
		}

	}

	IEnumerator LoadFromFile()
	{
		string fileName = "Data/AchievementsData.txt";

		yield return StartCoroutine(AssetBundleLoader.LoadAssestAsync(fileName, delegate(UnityEngine.Object retAsset)
		{
			TextAsset textAsset = retAsset as TextAsset;

			string[] lines = textAsset.text.Split("\n"[0]);

			_firstLine = lines[0];

			_loadFileFinished = true;

		}));

	}
	
}
