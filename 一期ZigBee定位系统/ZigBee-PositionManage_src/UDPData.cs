using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PositionManage
{
    public class UDPData
    {
        /// <summary>
        /// 卡片卡号
        /// </summary>
        public string TagNo;

        /// <summary>
        /// 3个定位点
        /// </summary>
        public Location[] Loc = new Location[3];
    }

    public struct Location
    {
        public string ReferNo;

        public byte Destance;
    }
}
