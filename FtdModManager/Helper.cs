﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrilliantSkies.Ui.TreeSelection;
using UnityEngine;

namespace FtdModManager
{
    public static class Helper
    {
        public const string tempExtension = ".modmanager_temp";

        public static void RemoveTempFilesInDirectory(string dir)
        {
            foreach (string file in Directory.EnumerateFiles(dir, $"*{tempExtension}", SearchOption.AllDirectories))
            {
                try
                {
                    File.Delete(file);
                    Log($"Deleted file: {file}");
                }
                catch (Exception e)
                {
                    LogException(e);
                }
            }
        }

        public static TreeSelectorGuiElement<GeneralFile, GeneralFolder> GetFileBrowser(GeneralFolder root)
        {
            var treeSelector = new TreeSelectorGuiElement<GeneralFile, GeneralFolder>(
                root,
                x => x.FileNameWithExtension,
                x => x.Name,
                x => x.GetFiles(),
                x => x.GetFolders().Cast<GeneralFolder>(),
                x => x.Sort((a, b) => a.ModifiedTime.CompareTo(b.ModifiedTime))
            );

            return treeSelector;
        }

        public static void Log(string message)
        {
            Debug.Log("[ModManager] " + message);
        }

        public static void LogException(Exception e)
        {
            Debug.LogException(e);
        }
    }
}