using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DTO
{
    public class TrafficPoliceDepartment
    {
        /// <summary>
        /// 所管辖地点
        /// </summary>
        public IList<E_Police.DTO.TrafficMonitorSpot> LocationsUnderMonitor
        {
            get;
            set;
        }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartmentID
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 违法处理科室
        /// </summary>
        public string Office
        {
            get;
            set;
        }

        /// <summary>
        /// 违法处理地址
        /// </summary>
        public string OfficeAddress
        {
            get;
            set;
        }
    }
}
