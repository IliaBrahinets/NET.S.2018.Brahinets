using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] sourceBytes;

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, 
                FileAccess.Read, FileShare.Read))
            {
                long sourceLength = sourceStream.Length;

                if(sourceLength > Int32.MaxValue)
                {
                    throw new IOException("fileLength is greater than 2GB");
                }

                int length = (int)sourceLength;
                int toRead = length;

                sourceBytes = new byte[length];

                while(toRead > 0)
                {
                    toRead -= sourceStream.Read(sourceBytes, length - toRead, toRead);
                }
            }

            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Truncate, 
                FileAccess.Write, FileShare.None))
            {
                destinationStream.Write(sourceBytes, 0, sourceBytes.Length);
            }

            return sourceBytes.Length;
            
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            // TODO: step 1. Use StreamReader to read entire file in string
            string sourceString;
            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, 
                FileAccess.Read, FileShare.Read))
            {
                StreamReader sourceReader = new StreamReader(sourceStream);

                sourceString = sourceReader.ReadToEnd();
            }

            Encoding encoding = Encoding.ASCII;
            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class
            byte[] sourceBytes = encoding.GetBytes(sourceString);
            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)
            MemoryStream memoryStream = new MemoryStream(sourceBytes);
            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array
            byte[] destinationBytes = new byte[sourceBytes.Length];
            memoryStream.Read(destinationBytes, 0, (int)memoryStream.Length);
            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content
            char[] destinationChars = encoding.GetChars(destinationBytes);
            // TODO: step 6. Use StreamWriter here to write char array content in new file
            using (StreamWriter destinationWriter = new StreamWriter(destinationPath))
            {
                destinationWriter.Write(destinationChars);
            }

            return destinationBytes.Length;

        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            const int bufferSize = 1000;

            int length = 0;

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open,
                FileAccess.Read, FileShare.Read, bufferSize))
            using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Truncate,
                FileAccess.Write, FileShare.None, bufferSize))
            {
                length = BlockCopy(sourceStream, destinationStream, bufferSize);
            }

            return length;

        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            const int blockSize = 1000;
            const int doubleBlock = blockSize * 2;

            char[] block = new char[blockSize];

            StringBuilder sourceChars = new StringBuilder();

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open,
                FileAccess.Read, FileShare.Read, doubleBlock))
            {
                StreamReader sourceReader = new StreamReader(sourceStream);

                int readed = 0;

                do
                {
                    readed = sourceReader.Read(block, 0, blockSize);
                    sourceChars.Append(block, 0, readed);
                } while (readed != 0);
            }

            Encoding encoding = Encoding.ASCII;

            byte[] sourceBytes = encoding.GetBytes(sourceChars.ToString());

            MemoryStream memoryStream = new MemoryStream(sourceBytes);

            using (StreamWriter destination = new StreamWriter(destinationPath))
            {
                int readed = 0;

                byte[] bytesBlock = new byte[doubleBlock];

                do
                {   
                    readed = memoryStream.Read(bytesBlock, 0, doubleBlock);

                    char[] charsBlock = encoding.GetChars(bytesBlock);
                    destination.Write(charsBlock);

                } while (readed != 0);
            }

            return sourceBytes.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            const int blockSize = 1000;

            int length = 0;

            using (FileStream source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (FileStream destination = new FileStream(destinationPath, FileMode.Truncate, FileAccess.Write, FileShare.None))
            using (BufferedStream sourceBuffered = new BufferedStream(source, blockSize))
            using (BufferedStream destinationBuffered = new BufferedStream(destination, blockSize))
            {
                length = BlockCopy(sourceBuffered, destinationBuffered, blockSize);
            }

            return length;
        }

        #endregion

        private static int BlockCopy(Stream source, Stream destination, int blockSize)
        {
            byte[] block = new byte[blockSize];

            int length = 0;

            int readed;

            do
            {
                readed = source.Read(block, 0, blockSize);
                destination.Write(block, 0, readed);
                length += readed;

            } while (readed != 0);

            return length;
        }

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int length = 0;

            Encoding encoding = Encoding.ASCII;

            using (FileStream source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sourceReader = new StreamReader(source, encoding))
            using (FileStream destination = new FileStream(destinationPath, FileMode.Truncate, FileAccess.Write, FileShare.None))
            using (StreamWriter destinationWriter = new StreamWriter(destination, encoding))
            {
                string readed;

                readed = sourceReader.ReadLine();
                destinationWriter.Write(readed);
                length += encoding.GetByteCount(readed);

                while (true)
                {
                    readed = sourceReader.ReadLine();
                    if (readed != null)
                    {
                        destinationWriter.WriteLine();
                        length += encoding.GetByteCount(Environment.NewLine);
                        destinationWriter.Write(readed);
                        length += encoding.GetByteCount(readed);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return length;

        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] sourceBytes = File.ReadAllBytes(sourcePath);

            byte[] destinationBytes = File.ReadAllBytes(destinationPath);

            if(sourceBytes.Length != destinationBytes.Length)
            {
                return false;
            }

            for(int i = 0; i < sourceBytes.Length; i++)
            {
                if (sourceBytes[i] != destinationBytes[i])
                    return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"the file {sourcePath} can't be found");
            }

            if (!File.Exists(destinationPath))
            {
                throw new FileNotFoundException($"the file {destinationPath} can't be found");
            }


        }

        #endregion

        #endregion

    }
}
