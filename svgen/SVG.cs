using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace svgen
{
    public class SVG
    {

        private static string[] fileTypes = {
            "png",
            "jpg",
            "jpeg"
        };

        int Height { get; set; }
        int Width { get; set; }
        string FileType { get; set; }
        string Base64 { get; set; }

        // <summary>
        // Returns the full directory including the file
        // </summary>
        private static string FormatDirectory(string file)
        {
            return String.Format("{0}\\{1}", Directory.GetCurrentDirectory(), file);
        }

        // <summary>
        // Gets the base64 string from the file
        // </summary>
        private string GetBase64String(string fileName)
        {
            byte[] fileContent = File.ReadAllBytes(fileName);
            string base64 = Convert.ToBase64String(fileContent);

            return base64;
        }

        // <summary>
        // Gets the width and height of the source image
        // </summary>
        private Dictionary<string, int> GetImageDimensions(string imageFilepath)
        {
            Bitmap image = new Bitmap(imageFilepath);

            Height = image.Height;
            Width = image.Width;

            Dictionary<string, int> dimensions = new Dictionary<string, int>
            {
                ["height"] = Height,
                ["width"] = Width
            };

            return dimensions;
        }
        // <summary>
        // Gets the file extension
        // </summary>
        private string GetFileType(string fileDirectory)
        {
            
            string fileExtension = Path.GetExtension(fileDirectory).Replace(".", "");

            if (!fileTypes.Contains(fileExtension))
            {
                throw new Exception(String.Format("{0} is not a supported file type", fileExtension));
            }
            return fileExtension;
        }
        // <summary>
        // Reads all necessary information from the source file and creates
        // the SVG file
        // </summary>
        public void CreateSVG(string sourceFile)
        {
            try
            {
                string sourceFileDirectory = FormatDirectory(sourceFile);
                // Get file information to create the SVG
                FileType = GetFileType(sourceFileDirectory);
                Base64 = GetBase64String(sourceFileDirectory);
                GetImageDimensions(sourceFileDirectory);
                
                // Change the file extension of the source file to SVG
                // for the newly created SVG file
                string svgFile = sourceFile.Replace(FileType, "svg");

                string[] lines = {
                    String.Format("<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 {0} {1}\">", Width, Height),
                    String.Format("<image width=\"{0}\" height=\"{1}\" xlink:href=\"data:image/{2}; base64,{3}\"></image>", Width, Height, FileType, Base64),
                    "</svg>"
                    };

                // Create the file
                File.WriteAllLines(@svgFile, lines);

                // Display success message
                Console.WriteLine(String.Format("Successfully created {0}", svgFile));

            } catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
            
        }

    }
}

