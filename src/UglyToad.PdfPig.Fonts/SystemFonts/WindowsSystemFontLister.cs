namespace UglyToad.PdfPig.Fonts.SystemFonts
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal sealed class WindowsSystemFontLister : ISystemFontLister
    {
        private static bool IsSubdirectory(string baseDir, string subDir)
        {
            var baseDirFullPath = Path.GetFullPath(baseDir).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            var subDirFullPath = Path.GetFullPath(subDir).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            return subDirFullPath.StartsWith(baseDirFullPath, StringComparison.OrdinalIgnoreCase);
        }
        public IEnumerable<SystemFontRecord> GetAllFonts()
        {
            var winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            var fonts = Path.Combine(winDir, "Fonts");
            var resolvedFontsPath = Path.GetFullPath(fonts);

            if (IsSubdirectory(winDir, resolvedFontsPath) && Directory.Exists(resolvedFontsPath))
            {
                var files = Directory.GetFiles(resolvedFontsPath);

                foreach (var file in files)
                {
                    if (SystemFontRecord.TryCreate(file, out var record))
                    {
                        yield return record;
                    }
                }
            }

            var psFonts = Path.Combine(winDir, "PSFonts");
            var resolvedPsFontsPath = Path.GetFullPath(psFonts);

            if (IsSubdirectory(winDir, resolvedPsFontsPath) && Directory.Exists(resolvedPsFontsPath))
            {
                var files = Directory.GetFiles(resolvedPsFontsPath);

                foreach (var file in files)
                {
                    if (SystemFontRecord.TryCreate(file, out var record))
                    {
                        yield return record;
                    }
                }
            }
        }
    }
}