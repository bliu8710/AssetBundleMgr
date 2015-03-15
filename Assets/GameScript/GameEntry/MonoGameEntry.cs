using UnityEngine;
using System.Collections;

public class MonoGameEntry : MonoBehaviour {
	bool _loadFileFinished = false;
	string _firstLine = "";
	// Use this for initialization
	IEnumerator Start () {
		yield return StartCoroutine(AssetBundleLoader.Initialize());

		//yield return StartCoroutine(LoadFromFile());
		yield return StartCoroutine(LoadPrefab());

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
		string fileName = "Data/AchievementsData";

		yield return StartCoroutine(AssetBundleLoader.LoadAssestAsync(fileName, delegate(UnityEngine.Object retAsset)
		{
			TextAsset textAsset = retAsset as TextAsset;

			string[] lines = textAsset.text.Split("\n"[0]);

			_firstLine = lines[0];

			_loadFileFinished = true;

		}));

	}

	IEnumerator LoadPrefab()
	{
		string fileName = "Assets/Resources/Prefab/Cube";

		GameObject myCube = null;

		yield return StartCoroutine(AssetBundleLoader.LoadAssestAsync(fileName, delegate(UnityEngine.Object retAsset)
		{
			myCube = GameObject.Instantiate(retAsset) as GameObject;
		}));

		//GameObject.Destroy(myCube);
	}
	
}
