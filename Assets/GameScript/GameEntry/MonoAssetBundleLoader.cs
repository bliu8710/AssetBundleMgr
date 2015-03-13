using UnityEngine;
using System.Collections;

public class MonoAssetBundleLoader : BaseLoader
{
	public string assetBundleName = "data.unity3d";
	public string assetName = "data";
	private bool isLoadFinished = false;

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return StartCoroutine(Initialize() );
	}

	public void Load()
	{
		Debug.Log("function: Load");

		StartCoroutine(LoadAsset());

		return;
	}

	public IEnumerator LoadAsset()
	{
		isLoadFinished = false;

		Debug.Log("IEnumerator LoadAsset");
		// Load asset.
		yield return StartCoroutine(Load(assetBundleName, assetName));

		isLoadFinished = true;
		Debug.Log("IEnumerator isLoadFinished");
	}

	public void UnLoadAsset()
	{
		if (isLoadFinished)
		{
			// Unload assetBundles.
			AssetBundleManager.UnloadAssetBundle(assetBundleName);
			isLoadFinished = false;
		}
	}


}
