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
            var fontsFullPath = Path.GetFullPath(fonts);

            if (fontsFullPath.StartsWith(winDir + Path.DirectorySeparatorChar) && Directory.Exists(fontsFullPath))
            {
                var files = Directory.GetFiles(fontsFullPath);

                foreach (var file in files)
                {
                    if (SystemFontRecord.TryCreate(file, out var record))
                    {
                        yield return record;
                    }
                }
            }

            var psFonts = Path.Combine(winDir, "PSFonts");
            var psFontsFullPath = Path.GetFullPath(psFonts);

            if (psFontsFullPath.StartsWith(winDir + Path.DirectorySeparatorChar) && Directory.Exists(psFontsFullPath))
            {
                var files = Directory.GetFiles(psFontsFullPath);

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