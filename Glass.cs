namespace WaterSortPuzzleSolver
{
    internal class Glass
    {
        /// <summary>
        /// 最大水量
        /// </summary>
        public const int MaximumWaterVolume = 4;

        private Color[] colors = new Color[MaximumWaterVolume];

        public Glass(Glass glass)
        {
            Color[] colorList = glass.Colors;
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                Colors[i] = colorList[i];
            }
        }

        public Glass(string glass)
        {
            string[] colorStrs = glass.Split("\t");
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                this.Colors[i] = (Color)int.Parse(colorStrs[i]);
            }
        }

        /// <summary>
        /// 当前杯子中的所有水,顺序为从上至下
        /// </summary>
        internal Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public static bool CanPour(Glass pouringGlass, Glass pouredGlass)
        {
            Color color;
            int num = pouringGlass.GetTopWaterNumAndColor(out color);
            if (num == 0)
            {
                return false;
            }
            if (pouredGlass.CanBePoured(num, color))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 当前杯子是否可以继续被倾倒
        /// </summary>
        /// <param name="num">倾倒过来水的数量</param>
        /// <param name="color">倾倒过来水的颜色</param>
        /// <returns></returns>
        public bool CanBePoured(int num, Color color)
        {

            //判断杯子会不会满
            if (num > MaximumWaterVolume - this.GetWaterNum())
            {
                return false;
            }

            //判断是否是相同颜色
            Color topColor = this.GetTopColor();
            if (color != topColor && topColor != Color.NULL)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得杯子水总数
        /// </summary>
        /// <returns></returns>
        internal int GetWaterNum()
        {
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                if (Colors[i] != Color.NULL)
                {
                    return MaximumWaterVolume - i;
                }
            }
            return 0;
        }

        /// <summary>
        /// 获得最顶部水的颜色
        /// </summary>
        /// <returns></returns>
        public Color GetTopColor()
        {
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                if (Colors[i] != Color.NULL)
                {
                    return Colors[i];
                }
            }
            return Color.NULL;
        }

        /// <summary>
        /// 获得最顶部水的数量
        /// </summary>
        /// <returns></returns>
        public int GetTopWaterNum()
        {
            Color tempColor = Color.NULL;
            int result = 0;
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                //排除上面空的位置
                if (Colors[i] == Color.NULL)
                {
                    continue;
                }
                //顶部
                if (tempColor == Color.NULL)
                {
                    tempColor = Colors[i];
                    result++;
                    continue;
                }
                if (tempColor != Colors[i])
                {
                    break;
                }
                result++;
            }
            return result;
        }

        /// <summary>
        /// 获得最顶部水的数量与颜色
        /// </summary>
        /// <param name="color">最顶部水的颜色</param>
        /// <returns>最顶部水的数量</returns>
        public int GetTopWaterNumAndColor(out Color color)
        {
            color = Color.NULL;
            int result = 0;
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                //排除上面空的位置
                if (Colors[i] == Color.NULL)
                {
                    continue;
                }
                //顶部
                if (color == Color.NULL)
                {
                    color = Colors[i];
                    result++;
                    continue;
                }
                if (color != Colors[i])
                {
                    break;
                }
                result++;
            }
            return result;
        }

        /// <summary>
        /// 是否杯中水全部为同一颜色或无水
        /// </summary>
        /// <returns></returns>
        public bool IsCompleted()
        {
            int num = this.GetTopWaterNum();
            if (num == MaximumWaterVolume || num == 0)
            {
                return true;
            }
            return false;
        }

        public void Pouring()
        {
            Glass glass = this;
            Color tempColor = Color.NULL;
            for (int i = 0; i < MaximumWaterVolume; i++)
            {
                if (glass.Colors[i] == Color.NULL)
                {
                    continue;
                }
                if (tempColor != Color.NULL && tempColor != glass.Colors[i])
                {
                    break;
                }
                if (tempColor == Color.NULL && glass.Colors[i] != Color.NULL)
                {
                    tempColor = glass.Colors[i];
                }
                glass.Colors[i] = Color.NULL;
            }
        }

        public void Poured(int num, Color color)
        {
            Glass glass = this;
            for (int i = MaximumWaterVolume - 1; i >= 0; i--)
            {
                if (num < 1)
                {
                    break;
                }
                if (glass.Colors[i] == Color.NULL)
                {
                    glass.Colors[i] = color;
                    num--;
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (Color color in this.Colors)
            {
                result += color.ToString() + " ";
            }
            return result.Trim();
        }
    }
}
