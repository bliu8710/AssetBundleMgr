using UnityEngine;
using System.Collections;

public class MonoAssetBundleLoader : BaseLoader
{
	public string assetBundleName = "data.unity3d";
	public string assetName = "data";

	// Use this for initialization
	IEnumerator Start () {

		yield return StartCoroutine(Initialize() );

		// Load asset.
		yield return StartCoroutine(Load (assetBundleName, assetName) );

		// Unload assetBundles.
		AssetBundleManager.UnloadAssetBundle(assetBundleName);
	}
}
