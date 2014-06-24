using System;
using System.IO;

namespace FileUtilities
{
    public sealed class Compose
    {

        #region PUBLIC METHODS

        public void MergeFiles(
                               string outputPath,
                               bool deleteOriginFile = true)
        {
            string[] files = GetFilesToMerge(outputPath);
            if (files.Length > 0)
            {
                string previousFileName = String.Empty;
                var fileName = PrepareFileName(files[0]);
                using (var outputFile = new FileStream(Path.Combine(outputPath, fileName), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    foreach (string file in files)
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[1024];
                        using (var inputTempFile = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                        {
                            while ((bytesRead = inputTempFile.Read(buffer, 0, 1024)) > 0)
                            {
                                outputFile.Write(buffer, 0, bytesRead);
                            }
                        }
                        if (deleteOriginFile)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private string[] GetFilesToMerge(string outPutPath)
        {
            return Directory.GetFiles(outPutPath,
                                      "*" + Constants.CutUpExtension);
        }

        private string PrepareFileName(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            return fileName.Substring(0, fileName.IndexOf(Constants.Separator)) +
                   Path.GetExtension(fileName);
        }

        #endregion

    }

}