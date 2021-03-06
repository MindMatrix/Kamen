﻿using System.Collections.Generic;

namespace Kamen.FileSystem
{
    public static class Directory
    {
        public static string FindWithParents(string basePath, string directory)
        {
            foreach (var path in Parents(basePath, true))
            {
                var installsPath = System.IO.Path.Combine(path, directory);
                if (System.IO.Directory.Exists(installsPath))
                    return installsPath;
            }

            return null;
        }

        public static IEnumerable<string> Parents(string basePath, bool includeBase = false)
        {
            if (includeBase)
                yield return basePath;

            while (((basePath = System.IO.Path.GetDirectoryName(basePath)) != null))
                yield return basePath;
        }
    }
}