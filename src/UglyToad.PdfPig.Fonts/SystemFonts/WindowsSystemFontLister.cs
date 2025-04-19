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
            fonts = Path.GetFullPath(fonts);

            if (fonts.StartsWith(winDir, StringComparison.OrdinalIgnoreCase) && Directory.Exists(fonts))
            {
                var files = Directory.GetFiles(fonts);

                foreach (var file in files)
                {
                    if (SystemFontRecord.TryCreate(file, out var record))
                    {
                        yield return record;
                    }
                }
            }

            var psFonts = Path.Combine(winDir, "PSFonts");
            psFonts = Path.GetFullPath(psFonts);

            if (psFonts.StartsWith(winDir + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) && Directory.Exists(psFonts))
            {
                var files = Directory.GetFiles(psFonts);

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