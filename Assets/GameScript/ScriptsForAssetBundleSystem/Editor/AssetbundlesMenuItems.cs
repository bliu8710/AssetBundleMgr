using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetbundlesMenuItems
{
	[MenuItem ("AssetBundles/Build AssetBundles")]
	static public void BuildAssetBundles ()
	{
		BuildScript.BuildAssetBundles();
	}

	[MenuItem ("AssetBundles/Build Player")]
	static void BuildPlayer ()
	{
		BuildScript.BuildPlayer();
	}
}
