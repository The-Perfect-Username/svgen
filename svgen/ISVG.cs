using System.Collections.Generic;

namespace svgen
{
    public interface ISVG
    {
        int Height { get; set; } 
        int Width { get; set; } 
        string FileType { get; set; } 
        string Base64 { get; set; } 
        string GetBase64String(string fileName);
        Dictionary<string, string> GetImageDimensions(string fileName);
        string GetFileType(string fileName);
        void CreateSVGFile();
        
    }
}
