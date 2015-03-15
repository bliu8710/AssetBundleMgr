using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AssetBundleLoader
{
	const string kAssetBundlesPath = "/AssetBundles/";

	// Initialize the downloading url and AssetBundleManifest object.
	public static IEnumerator Initialize()
	{
		// Don't destroy the game object as we base on it to run the loading script.

		string platformFolderForAssetBundles =
#if UNITY_EDITOR
		GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
		GetPlatformFolderForAssetBundles(Application.platform);
#endif

		Debug.Log("platformFolderForAssetBundles= " + platformFolderForAssetBundles);

		// Set base downloading url.
		string relativePath = GetRelativePath();
		AssetBundleManager.BaseDownloadingURL = relativePath + kAssetBundlesPath + platformFolderForAssetBundles + "/";

		Debug.Log("BaseDownloadingURL= " + AssetBundleManager.BaseDownloadingURL);

		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
		var request = AssetBundleManager.Initialize(platformFolderForAssetBundles);
		if (request != null)
			yield return AssetBundleManager.GetInstance().StartCoroutine(request);
	}

	public static IEnumerator LoadAssestAsync(string name, System.Action<UnityEngine.Object> callBack)
	{
		string assetBundleName = "cube2.unity3d"; //"data.unity3d";
		string assetName = "Assets/Resources/Prefab/Cube2.prefab"; //"Assets/Resources/Data/AchievementsData.txt";

		Debug.Log("Start to load " + assetName + " at frame " + Time.frameCount);

		// Load asset from assetBundle.
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(UnityEngine.Object));
		if (request == null)
			yield break;
		yield return AssetBundleManager.GetInstance().StartCoroutine(request);

		// Get the asset.
		UnityEngine.Object asset = request.GetAsset<UnityEngine.Object>();

		if (asset != null)
		{
			Debug.Log(assetName + "is loaded successfully at frame " + Time.frameCount);

			callBack(request.GetAsset<UnityEngine.Object>());

			AssetBundleManager.UnloadAssetBundle(assetBundleName);
		}
		else
		{
			Debug.LogError(assetName + "is NOT loaded successfully at frame " + Time.frameCount);
		}
		
	}

	#region GetRelativePath GetPlatformFolderForAssetBundles
	private static string GetRelativePath()
	{
		if (Application.isEditor)
			return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/"); // Use the build output folder directly.
		else if (Application.isWebPlayer)
			return System.IO.Path.GetDirectoryName(Application.absoluteURL).Replace("\\", "/") + "/StreamingAssets";
		else if (Application.isMobilePlatform || Application.isConsolePlatform)
			return Application.streamingAssetsPath;
		else // For standalone player.
			return "file://" + Application.streamingAssetsPath;
	}
#if UNITY_EDITOR
	private static string GetPlatformFolderForAssetBundles(BuildTarget target)
	{
		switch (target)
		{
			case BuildTarget.Android:
				return "Android";
			case BuildTarget.iOS:
				return "iOS";
			case BuildTarget.WebPlayer:
				return "WebPlayer";
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64:
				return "Windows";
			case BuildTarget.StandaloneOSXIntel:
			case BuildTarget.StandaloneOSXIntel64:
			case BuildTarget.StandaloneOSXUniversal:
				return "OSX";
			// Add more build targets for your own.
			// If you add more targets, don't forget to add the same platforms to GetPlatformFolderForAssetBundles(RuntimePlatform) function.
			default:
				return null;
		}
	}
#endif

	private static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
	{
		switch (platform)
		{
			case RuntimePlatform.Android:
				return "Android";
			case RuntimePlatform.IPhonePlayer:
				return "iOS";
			case RuntimePlatform.WindowsWebPlayer:
			case RuntimePlatform.OSXWebPlayer:
				return "WebPlayer";
			case RuntimePlatform.WindowsPlayer:
				return "Windows";
			case RuntimePlatform.OSXPlayer:
				return "OSX";
			// Add more build platform for your own.
			// If you add more platforms, don't forget to add the same targets to GetPlatformFolderForAssetBundles(BuildTarget) function.
			default:
				return null;
		}
	}
	#endregion

}
