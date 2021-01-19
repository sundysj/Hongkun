using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.商家App
{
    public class PrintMsg
    {
        /// <summary>
        /// 商家名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime order_time { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal order_amt { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal discount_amt { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal s_amt { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string receiver_phone { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string receiver_address { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public List<OrderType> list { get; set; }

        public class OrderType
        {
            public string type { get; set; }
            public List<Order> list { get; set; }
        }
        public class Order
        {
            public string title { get; set; }
            public decimal price { get; set; }
            public int num { get; set; }
        }
        public string GetPrintMsg()
        {
            string content = string.Empty;

            #region 固定配置项，请勿轻易修改
            // 58mm的机器,一行打印16个汉字,32个字母
            int line_length = 32;
            // 名称列占用字节
            int b1 = 10;
            // 单价*数量列
            int b2 = 10;
            // 金额列
            int b3 = 8;
            #endregion

            #region 标题处理
            // 标题会自动换行，对于中文标题，一行只会显示8个汉子
            // 对于英文标题，一行只会显示16个字母
            content += $"<CB>{title}</CB>";
            content += $"<BR>";
            content += $"<BR>";
            content += $"<BR>";
            #endregion

            #region 订单号处理
            content += $"订单号  ：{order_no}";
            content += $"<BR>";
            content += $"<BR>";
            #endregion
            #region 下单时间处理
            content += $"下单日期：{order_time:yyyy-MM-dd HH:mm:ss}";
            content += $"<BR>";
            content += $"<BR>";
            #endregion
            #region 配送方式
            content += $"配送方式：{type}";
            content += $"<BR>";
            content += $"<BR>";
            #endregion
            #region 商品以及金额信息
            // 商品总金额
            decimal totals = 0.00M;
            content += "商品名称    价格*数量   金额    ";
            content += $"<BR>";
            content += "--------------------------------";
            content += $"<BR>";
            content += $"<BR>";
            list.ForEach((item) =>
            {
                #region 数据检查
                if (null == item)
                {
                    return;
                }
                if (string.IsNullOrEmpty(item.type))
                {
                    return;
                }
                if (null == item.list || item.list.Count <= 0)
                {
                    return;
                }
                #endregion

                #region 添加分类
                string item_type = $"--{item.type}--";
                content += $"<C>{item_type}</C>";
                content += "<BR>";
                #endregion

                #region 添加商品
                item.list.ForEach((commodity) =>
                {
                    #region 数据检查
                    string commodity_title = commodity.title;
                    decimal price = decimal.Round(commodity.price, 2);
                    int num = commodity.num;
                    decimal total = decimal.Round(price * num, 2);
                    if (string.IsNullOrEmpty(commodity_title))
                    {
                        return;
                    }
                    if (price <= 0.00M)
                    {
                        return;
                    }
                    if (num <= 0)
                    {
                        return;
                    }
                    if (total <= 0.00M)
                    {
                        return;
                    }
                    totals += total;
                    #endregion

                    #region 将每个数据都进行长度截取换行，防止样式错误
                    List<string> commodity_list = GetStringLineList(commodity_title, b1);
                    List<string> price_list = GetStringLineList($"{price}*{num}", b2);
                    List<string> total_list = GetStringLineList($"{total}", b3);
                    // 获取行数最多的数据，用来当循环条件
                    int line = commodity_list.Count >= price_list.Count ? (commodity_list.Count >= total_list.Count ? commodity_list.Count : total_list.Count) : (price_list.Count >= total_list.Count ? price_list.Count : total_list.Count);
                    if (line <= 0)
                    {
                        return;
                    }
                    for (int i = 0; i < line; i++)
                    {
                        string a = commodity_list.Count > i ? commodity_list[i] : "";
                        string b = price_list.Count > i ? price_list[i] : "";
                        string c = total_list.Count > i ? total_list[i] : "";
                        content += $"{AddSpace(a, b1)}  {AddSpace(b, b2)}  {AddSpace(c, b3)}";
                        content += "<BR>";
                    }
                    content += "<BR>";
                    #endregion
                });
                content += "<BR>";
                #endregion
            });
            content += "--------------------------------";
            content += $"<BR>";
            string li = string.Empty;
            GetStringLineList($"{totals}", 26).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("总计：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("      ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            content += $"<BR>";
            li = string.Empty;
            GetStringLineList($"{order_amt}", 22).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("订单金额：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("          ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            li = string.Empty;
            GetStringLineList($"{discount_amt}", 22).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("活动减免：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("          ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            content += "--------------------------------";
            content += $"<BR>";
            #endregion
            #region 收货人信息部分
            li = string.Empty;
            GetStringLineList($"{s_amt}", 22).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("实付金额：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("          ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            content += $"<BR>";
            li = string.Empty;
            GetStringLineList(receiver, 20).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("收货人：    ", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("            ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            li = string.Empty;
            GetStringLineList(receiver_phone, 20).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("收货人电话：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("            ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            li = string.Empty;
            GetStringLineList(receiver_address, 20).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("收货人地址：", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("            ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            content += "<BR>";
            li = string.Empty;
            GetStringLineList(memo, 20).ForEach((str) =>
            {
                if (string.IsNullOrEmpty(li))
                {
                    str = str.Trim(' ');
                    li += $"{AddSpace("订单备注：  ", line_length - GetLength($"{str}"))}{str}";
                }
                else
                {
                    li += $"{AddSpace("            ", line_length - GetLength($"{str}"))}{str}";
                }
                li += "<BR>";
            });
            content += li;
            content += "<BR>";
            #endregion
            #region 订单二维码部分
            content += $"<QR>{order_no}</QR>";
            #endregion
            return content;
        }
        #region 公共方法

        private bool isEn(string str)
        {
            bool b = false;
            try
            {
                b = GetLength(str) == str.Length;
            }
            catch (Exception e)
            {
            }
            return b;
        }
        private static int GetLength(string str)
        {
            return Encoding.GetEncoding("GBK").GetBytes(str).Length;
        }
        private string Substring(string str, int s, int l)
        {
            if (s > str.Length)
                return null;
            if (s + l > str.Length)
            {
                return str.Substring(s, str.Length - s);
            }
            else
            {
                return str.Substring(s, l);
            }
        }
        private List<string> GetStrList(string inputString, int length)
        {
            int size = inputString.Length / length;
            if (inputString.Length % length != 0)
            {
                size += 1;
            }
            return GetStrList(inputString, length, size);
        }
        private List<string> GetStrList(string inputString, int length, int size)
        {
            List<string> list = new List<string>();
            for (int index = 0; index < size; index++)
            {
                string childStr = Substring(inputString, index * length, length);
                list.Add(childStr);
            }
            return list;
        }
        private string AddSpace(string str, int size)
        {
            int len = GetLength(str);
            if (len < size)
            {
                for (int i = 0; i < size - len; i++)
                {
                    str += " ";
                }
            }
            return str;
        }
        private List<string> GetStringLineList(string content, int length)
        {
            List<string> string_list = new List<string>();

            if (string.IsNullOrEmpty(content))
            {
                return string_list;
            }
            int tl = GetLength(content);
            int space_num = (tl / length + 1) * length - tl;
            if (tl < length)
            {
                string_list.Add(AddSpace(content, GetLength(content) + space_num));
            }
            else if (tl == length)
            {
                string_list.Add(content);
            }
            else
            {
                if (isEn(content))
                {
                    string_list = GetStrList(content, length);
                }
                else
                {
                    string_list = GetStrList(content, length / 2);
                }
            }
            return string_list;
        }
        #endregion
    }
}
