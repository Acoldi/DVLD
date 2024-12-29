using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD.GloabalClasses
{
    public static class clsUtil
    {
        public static bool CreateFolderIfDoesntExist(string FolderName)
        {

            if (!Directory.Exists(FolderName))
            {
                try
                {
                    Directory.CreateDirectory(FolderName);
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        private static string GenerateGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            FileInfo DestFileName = new FileInfo(SourceFile);
            string Extention = DestFileName.Extension;
            return GenerateGuid() + Extention;
        }

        static public bool SaveImageToImagesFolder(ref string ImageFile)
        {
            string FolderLocation = @"C:\Temp\DVLD_People_Images\";

            if (!CreateFolderIfDoesntExist(FolderLocation))
            {
                MessageBox.Show("Couldn't create image folder", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string DestinationFile = FolderLocation + ReplaceFileNameWithGuid(ImageFile);
            try
            {
                File.Copy(ImageFile, DestinationFile, true);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ImageFile = DestinationFile;
            return true;
        }
    }
}
