using System;
using System.Linq;
using Xunit;

namespace Kamen.FileSystem
{
    public class DirectoryTests
    {
        #region Temp Directory Helper
        private class TempDirectory : IDisposable
        {
            private string _tempPath;
            public string Path => _tempPath;

            public TempDirectory()
            {
                var tempDirectory = System.IO.Path.GetTempPath();
                var tempName = System.IO.Path.GetRandomFileName();
                _tempPath = System.IO.Path.Combine(tempDirectory, tempName);
                System.IO.Directory.CreateDirectory(_tempPath);
            }

            public void Dispose()
            {
                System.IO.Directory.Delete(_tempPath, true);
            }
        }
        #endregion

        [Fact]
        public void ReturnsAllParents()
        {
            //arrange
            var path = @"C:\first\second\third";

            //act
            var pathes = Directory.Parents(path).ToArray();

            //assert
            Assert.Equal(new string[] { @"C:\first\second", @"C:\first", @"C:\" }, pathes);
        }

        [Fact]
        public void ReturnsAllParentsAndBase()
        {
            //arrange
            var path = @"C:\first\second\third";

            //act
            var pathes = Directory.Parents(path, true).ToArray();

            //assert
            Assert.Equal(new string[] { @"C:\first\second\third", @"C:\first\second", @"C:\first", @"C:\" }, pathes);
        }

        [Fact]
        public void FindWithParentMatchesFirst()
        {
            //arrange
            using (var tempPath = new TempDirectory())
            {
                var directoryName = "first";
                var directoryPath = System.IO.Path.Combine(tempPath.Path, directoryName);
                System.IO.Directory.CreateDirectory(directoryPath);

                //act
                var matchedPath = Directory.FindWithParents(tempPath.Path, directoryName);

                //assert
                Assert.Equal(directoryPath, matchedPath);
            }
        }

        [Fact]
        public void FindWithParentMatchesSecond()
        {
            //arrange
            using (var tempPath = new TempDirectory())
            {
                var directoryName = "second";
                var directoryPath = System.IO.Path.Combine(tempPath.Path, directoryName );

                var subDirectory = System.IO.Path.Combine(directoryPath, "first");
                System.IO.Directory.CreateDirectory(subDirectory);

                //act
                var matchedPath = Directory.FindWithParents(tempPath.Path, directoryName);

                //assert
                Assert.Equal(directoryPath, matchedPath);
            }

        }
    }
}
