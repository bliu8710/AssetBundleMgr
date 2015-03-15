using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AssetBundleLoader
{
	const string kAssetBundlesPath = "/AssetBundles/";
	private string _assetBundleName;
	private string _assetName;
	private UnityEngine.Object _asset;

	// Initialize the downloading url and AssetBundleManifest object.
	public IEnumerator Initialize()
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
			yield return request;
	}

	public IEnumerator LoadAssestAsync(string name)
	{
		_assetBundleName = "data.unity3d";
		_assetName = "Assets/Resources/Data/AchievementsData.txt";

		Debug.Log("Start to load " + _assetName + " at frame " + Time.frameCount);

		// Load asset from assetBundle.
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(_assetBundleName, _assetName, typeof(UnityEngine.Object));
		if (request == null)
			yield break;
		yield return request;

		// Get the asset.
		_asset = request.GetAsset<UnityEngine.Object>();
		Debug.Log(_assetName + (_asset == null ? " isn't" : " is") + " loaded successfully at frame " + Time.frameCount);
	}

	public void UnLoadAsset()
	{
		AssetBundleManager.UnloadAssetBundle(_assetBundleName);
	}

	public UnityEngine.Object GetAsset()
	{
		return _asset;
	}

	#region GetRelativePath GetPlatformFolderForAssetBundles
	public string GetRelativePath()
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
	public static string GetPlatformFolderForAssetBundles(BuildTarget target)
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

	static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
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
