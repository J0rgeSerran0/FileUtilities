using System;
using System.IO;

namespace FileUtilities
{

    public sealed class Discompose
    {

        #region CONSTRUCTORS

            public Discompose(int zerosInFilesNumbered = 3)
            {
                if (zerosInFilesNumbered > 0)
                {
                    this.ZerosInFilesNumbered = zerosInFilesNumbered;
                }
            }

        #endregion

        #region VARIABLES

            private int ZerosInFilesNumbered = 3;

        #endregion

        #region PUBLIC METHODS

            public void SplitFileByNumberOfFiles(
                                             string inputFile,
                                             int chunkFiles)
            {
                using (var fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    int sizeOfEachFile = (int)Math.Ceiling((double)fileStream.Length / chunkFiles);
                    for (int i = 0; i <= chunkFiles; i++)
                    {
                        using (var outputFile = new FileStream(PreparePathFileName(inputFile, i), FileMode.Create, FileAccess.Write))
                        {
                            int bytesRead = 0;
                            byte[] buffer = new byte[sizeOfEachFile];

                            if ((bytesRead = fileStream.Read(buffer, 0, sizeOfEachFile)) > 0)
                            {
                                outputFile.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }

            public void SplitFileBySizeOfFiles(
                                               string inputFile,
                                               int chunkSize,
                                               SizeType sizeType = SizeType.Bytes)
            {
                int fileSize = (sizeType == SizeType.Bytes ? chunkSize : GetFileSize(chunkSize, sizeType));
                using (var fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    int i = 0;
                    while (fileStream.Position < fileStream.Length)
                    {

                        using (var outputFile = new FileStream(PreparePathFileName(inputFile, i), FileMode.Create, FileAccess.Write))
                        {
                            int remaining = fileSize, bytesRead;
                            byte[] buffer = new byte[fileSize];
                            while (remaining > 0 &&
                                  (bytesRead = fileStream.Read(buffer, 0, Math.Min(remaining, fileSize))) > 0)
                            {
                                outputFile.Write(buffer, 0, bytesRead);
                                remaining -= bytesRead;
                            }
                            i++;
                        }
                    }
                }
            }

        #endregion

        #region PRIVATE METHODS

            private int GetFileSize(
                                     int chunkSize,
                                     SizeType sizeType)
            {
                switch (sizeType)
                {
                    case SizeType.KBytes:
                        return chunkSize * 1024;
                    case SizeType.MBytes:
                        return chunkSize * 1024 * 1024;
                    case SizeType.GBytes:
                        return chunkSize * 1024 * 1024 * 1024;
                    default:
                        return chunkSize;
                }
            }

            private string PreparePathFileName(
                                               string inputFile,
                                               int position)
            {
                string fileName = Path.GetFileNameWithoutExtension(inputFile) +
                                  Constants.Separator +
                                  position.ToString().PadLeft(this.ZerosInFilesNumbered, '0') +
                                  Path.GetExtension(inputFile) +
                                  Constants.CutUpExtension;
                return Path.Combine(Path.GetDirectoryName(inputFile), fileName);
            }

        #endregion

    }

}