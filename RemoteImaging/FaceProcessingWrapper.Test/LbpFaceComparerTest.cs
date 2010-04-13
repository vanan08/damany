using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.Util.Extensions;
using Damany.Imaging.Extensions;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class LbpFaceComparerTest
    {
        [Test]
        [Timeout(0)]
        public void Test()
        {
//            string path = @"M:\imageSearch\target.jpg";
//            var target = path.LoadIntoIpl();
//
//            var targetRects = target.LocateFaces();
//            if (targetRects.Length > 0)
//            {
//                target.ROI  = targetRects[0];
//            }
//
//            target.CheckWithBmp();
//
//            var poi = PersonOfInterest.FromIplImage(target);
//            var pois = new[] {poi};
//
//            var comparer = new LbpFaceComparer();
//            comparer.Load(pois.ToList());
//
//            var destDir = @"D:\searchResult";
//
//            foreach (var file in System.IO.Directory.GetFiles(@"M:\imageSearch", "*.jpg"))
//            {
//                var imgToCompare = file.LoadIntoIpl();
//
//                imgToCompare.CheckWithBmp();
//
//                var results = comparer.CompareTo(imgToCompare);
//
//                var filtered = from r in results
//                               where r.Similarity > 60
//                               select r;
//
//                if (filtered.Count() > 0)
//                {
//                    var fileName = System.IO.Path.GetFileName(file);
//                    var destFile = System.IO.Path.Combine(destDir, fileName);
//
//                    System.IO.File.Copy(file, destFile);
//                }
//            }
//            
        }
    }
}
