using System;
using System.Drawing;
using System.IO;

namespace ClawLibrary.Services.UnitTests.Helpers
{
    public static class PictureHelper
    {
        public static string GetCorrectPictureBase64()
        {
            //var y = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), "Assets", "Correct.jpg").Replace(@"file:\", "");
            using (Image image = Image.FromFile(path) )
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static string GetPictureBase64WithHugeSize()
        {
            //var y = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), "Assets", "File_Size_Is_Too_Big.jpg").Replace(@"file:\", "");
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        public static string GetWrongPictureBase64()
        {
            var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), "Assets", "File_Is_Wrong.txt").Replace(@"file:\", "");
            Byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }
    }
}
