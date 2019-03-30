using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PositionManage
{
    public static class CaculateLocation
    {
        private static readonly int _ReferPointCount = Convert.ToInt32(Program.GetAppSettingValue("ReferPointCount", "8"));

        private static int[][] _loc = new int[_ReferPointCount][];

        private static int _VertexX = -1;

        public static void SetGridSize(int x, int y)
        {
            if (_ReferPointCount <= 0)
            {
                throw new Exception("参考点个数定义错误!");
            }

            for (int ii = 0; ii < _ReferPointCount; ii++)
            {
                _loc[ii] = new int[x * y];
            }

            _VertexX = x;
        }

        /// <summary>
        /// 学习参考点信息
        /// </summary>
        /// <param name="ReferPointIndex">参考定位器索引</param>
        /// <param name="Strength">信号强度</param>
        /// <param name="pt">坐标点</param>
        public static void InputStudyLocation(int ReferPointIndex, int Strength, Point pt)
        {
            if (_VertexX > 0)
            {
                _loc[ReferPointIndex][pt.Y * _VertexX + pt.X] = Strength;
            }
            else
            {
                throw new Exception("未初始化网格信息!");
            }
        }

        /// <summary>
        /// 计算坐标点
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static Point OutputResultLocation(Dictionary<int,int> dict)
        {
            //网格数每边都比顶点少1
            int iGridX = _VertexX - 1, iGridY = _loc[0].Length/_VertexX - 1;

            //定位判断数组，实为矩阵，横向长iGridX，
            int[] iLocJudgeArray = new int[iGridX * iGridY];

            foreach (KeyValuePair<int, int> kv in dict)
            {
                int[] iStrengths = _loc[kv.Key];

                for(int ii =0;ii< iLocJudgeArray.Length;ii++)
                {
                    if ((kv.Value - iStrengths[ii + ii / iGridX]) * (kv.Value - iStrengths[ii + ii / iGridX + iGridX + 2]) > 0
                        && (kv.Value - iStrengths[ii + ii / iGridX + 1]) * (kv.Value - iStrengths[ii + ii / iGridX + iGridX + 1]) > 0)
                    {
                        //对角比对均同向，由于信号强度跟距离相关，不会存在大大、小小组合。故为全大于或全小于，筛去。
                        continue;
                    }
                    else
                    {
                        //可能权重加1
                        iLocJudgeArray[ii] += kv.Value + 3 - ii;
                    }
                }
            }

            int iResult = 0;
            for (int jj = 1; jj < iLocJudgeArray.Length; jj++)
            {
                if (iLocJudgeArray[jj] > iLocJudgeArray[iResult])
                {
                    iResult = jj;
                }
            }

            Point pt = new Point();
            pt.X = iResult % iGridX;
            pt.Y = iResult / iGridX;
            return pt;
        }
    }
}
