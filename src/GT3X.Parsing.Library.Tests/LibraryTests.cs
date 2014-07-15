using System.IO;
using Xunit;
using Ionic.Zip;

namespace GT3X.Parsing.Library.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void EmptyFileNameIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile(string.Empty));
        }

        [Fact]
        public void NullFileNameIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile(null));
        }

        [Fact]
        public void NonExistentFileIsNotParsable()
        {
            Assert.Throws<FileDoesNotExist>(() => new Gt3XFile("thisfiledoesntexist"));
        }

        [Fact]
        public void NonZipFileIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            Assert.Throws<FileIsNotAZipFile>(() => new Gt3XFile(filename));

            //clean up temporary file
            try { File.Delete(filename); }
            catch {  }
        }

        [Fact]
        public void ZipFileWithNoFilesIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            using (ZipFile zip = new ZipFile())
                zip.Save(filename);

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(filename));

            //clean up temporary file
            try { File.Delete(filename); }
            catch { }
        }

        [Fact]
        public void ZipFileWithNoActivityBinIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            const string LUX_BIN = "lux.bin";
            using (File.Create(LUX_BIN)) { }
            const string INFO_TXT = "info.txt";
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(filename);
            }

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(filename));

            //clean up temporary file
            try 
            { 
                File.Delete(filename);
                File.Delete(LUX_BIN);
                File.Delete(INFO_TXT);
            }
            catch { }
        }

        [Fact]
        public void ZipFileWithNoLuxBinIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            const string ACTIVITY_BIN = "activity.bin";
            using (File.Create(ACTIVITY_BIN)) { }
            const string INFO_TXT = "info.txt";
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(filename);
            }

            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(filename));

            //clean up temporary file
            try
            {
                File.Delete(filename);
                File.Delete(ACTIVITY_BIN);
                File.Delete(INFO_TXT);
            }
            catch { }
        }

        [Fact]
        public void ZipFileWithNoInfoTxtIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            const string ACTIVITY_BIN = "activity.bin";
            using (File.Create(ACTIVITY_BIN)) { }
            const string LUX_BIN = "lux.bin";
            using (File.Create(LUX_BIN)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.Save(filename);
            }
            
            Assert.Throws<MissingRequiredFiles>(() => new Gt3XFile(filename));

            //clean up temporary file
            try
            {
                File.Delete(filename);
                File.Delete(ACTIVITY_BIN);
                File.Delete(LUX_BIN);
            }
            catch { }
        }

        [Fact]
        public void ZipFileWithEmptyInfoTxtIsNotParsable()
        {
            string filename = Path.GetTempFileName();

            const string ACTIVITY_BIN = "activity.bin";
            using (File.Create(ACTIVITY_BIN)) { }
            const string LUX_BIN = "lux.bin";
            using (File.Create(LUX_BIN)) { }
            const string INFO_TXT = "info.txt";
            using (File.Create(INFO_TXT)) { }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(ACTIVITY_BIN);
                zip.AddFile(LUX_BIN);
                zip.AddFile(INFO_TXT);
                zip.Save(filename);
            }

            Assert.Throws<InvalidInfoFile>(() => new Gt3XFile(filename));

            //clean up temporary file
            try
            {
                File.Delete(filename);
                File.Delete(ACTIVITY_BIN);
                File.Delete(LUX_BIN);
                File.Delete(INFO_TXT);
            }
            catch { }
        }
    }
}
