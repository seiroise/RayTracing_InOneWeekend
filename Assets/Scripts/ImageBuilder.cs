using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ImageBuilder
{

    [MenuItem("ImageBuilder/Test")]
    public static void Test001()
    {
        int w = 200;
        int h = 100;
        Color[] colors = new Color[w * h];
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; ++y)
            {
                colors[y * w + x] = new Color((float)x / w, (float)y / h, 0f);
            }
        }
        SaveImage(colors, w, h);
    }

    public static void SaveImage(Vec3[] c, int w, int h)
    {
        Color[] colors = new Color[w * h];
        for (int i = 0; i < w * h; i++)
        {
            colors[i] = new Color(c[i].r, c[i].g, c[i].b, 1f);
        }
        SaveImage(colors, w, h);
    }

    public static void SaveImage(Color[] colors, int w, int h)
    {
        var tex = new Texture2D(w, h, TextureFormat.RGBAFloat, false, true);
        tex.SetPixels(colors);
        byte[] bytes = tex.EncodeToPNG();
        var path = AssetDatabase.GenerateUniqueAssetPath("./Assets/output.png");
        File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);
        AssetDatabase.Refresh();
    }
}