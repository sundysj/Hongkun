using Dapper;
using System;
using System.Data;

namespace DapperExtensions
{
#pragma warning disable CS3009 // 基类型不符合 CLS
    public class GuidHandler : SqlMapper.TypeHandler<Guid>
#pragma warning restore CS3009 // 基类型不符合 CLS
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid(value.ToString());
        }
    }
}
