using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using RemoteImaging;

namespace Test
{
    [TestFixture]
    public class VideoRepositoryTest
    {
        private RemoteImaging.FileSystemStorage _repository;

        [FixtureSetUp]
        public void Init()
        {
            _repository = new FileSystemStorage(@"d:\imageOutput");
        }

        [Test]
        public void DeleteTheOldestVideo()
        {
            var oldest = from v in _repository.Videos
                         where v.Deleted == false
                         orderby v.Date ascending
                         select v;

            var first = oldest.FirstOrDefault();

            _repository.DeleteVideos(first);

            first = (from v in _repository.Videos
                    orderby v.Date ascending
                    select v).First();

            Assert.IsTrue(first.Deleted);
        }



    }
}
