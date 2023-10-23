   
using UnityEditor;
using System.IO;

public class CopyResourcesOnImport
{

    [MenuItem("SkyUUI/Import All")]
    private static void CopyFolder()
    {
        string destinationFolderPath = "Assets/SkyUUI/";

        string[] sourceFolderPath = new string[] {
             "Packages/sky9th.skyuui/Fonts/",
             "Packages/sky9th.skyuui/Images/",
             "Packages/sky9th.skyuui/Page/",
             "Packages/sky9th.skyuui/Resources/",
             "Packages/sky9th.skyuui/Scripts/"
        };
        for (int i = 0; i < sourceFolderPath.Length; i ++)
        {
            CopyFolderRecursive(sourceFolderPath[i], destinationFolderPath);
        }


        AssetDatabase.Refresh();
    }

    private static void CopyFolderRecursive(string sourceFolderPath, string destinationFolderPath)
    {
        Directory.CreateDirectory(destinationFolderPath);

        string[] files = Directory.GetFiles(sourceFolderPath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);
            File.Copy(file, destinationFilePath, true);
        }

        string[] folders = Directory.GetDirectories(sourceFolderPath);
        foreach (string folder in folders)
        {
            string folderName = Path.GetFileName(folder);
            string destinationSubfolderPath = Path.Combine(destinationFolderPath, folderName);
            CopyFolderRecursive(folder, destinationSubfolderPath);
        }
    }
}
