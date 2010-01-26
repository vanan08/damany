using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace SuspectsRepository
{
    //重要 50-58
    //一般 58-68
    //普通 68-100

    public class PersonInfo
    {
        public PersonInfo() { }
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 相似度  范围值
        /// </summary>
        public int Similarity { get; set; }
    }
}
