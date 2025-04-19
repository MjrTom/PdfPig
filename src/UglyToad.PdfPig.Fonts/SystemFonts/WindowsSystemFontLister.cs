namespace UglyToad.PdfPig.Fonts.SystemFonts
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal sealed class WindowsSystemFontLister : ISystemFontLister
    {
        public IEnumerable<SystemFontRecord> GetAllFonts()
        {
            var winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            var fonts = Path.Combine(winDir, "Fonts");
            var resolvedFontsPath = Path.GetFullPath(fonts);

            if (resolvedFontsPath.StartsWith(winDir + Path.DirectorySeparatorChar) && Directory.Exists(resolvedFontsPath))
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

            if (resolvedPsFontsPath.StartsWith(winDir + Path.DirectorySeparatorChar) && Directory.Exists(resolvedPsFontsPath))
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