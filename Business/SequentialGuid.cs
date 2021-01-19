using System;
using System.Security.Cryptography;

namespace Business
{
    /// <summary>
    /// 有序 GUID 模式。
    /// </summary>
    public enum SequentialGuidSchema
    {
        /// <summary>
        /// 生成的 GUID 按照字符串顺序排列，适用于 MySQL、PostgreSQL。
        /// </summary>
        SequentialAsString,

        /// <summary>
        /// 生成的 GUID 按照二进制的顺序排列，适用于 Oracle。
        /// </summary>
        SequentialAsBinary,

        /// <summary>
        /// 生成的 GUID 按照末尾部分排列，适用于 Microsoft SQL Server。
        /// </summary>
        SequentialAtEnd
    }

    public static class SequentialGuid
    {
        /// <summary>
        /// 生成一个有序的 GUID。
        /// </summary>
        public static Guid NewGuid()
        {
            return NewGuid(SequentialGuidSchema.SequentialAtEnd);
        }

        /// <summary>
        /// 生成一个有序的 GUID。
        /// </summary>
        public static Guid NewGuid(SequentialGuidSchema schema)
        {
            byte[] randomBytes = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (schema)
            {
                case SequentialGuidSchema.SequentialAsString:
                case SequentialGuidSchema.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // 在 little-endian 系统上如果需要格式化为字符串则需要颠倒数据 a1 和数据 a2 块的顺序。
                    if (schema == SequentialGuidSchema.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidSchema.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}
