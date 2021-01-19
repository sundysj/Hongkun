using System.Collections.Generic;

namespace TWTools.Push
{
    /// <summary>
    /// 推送设备对象
    /// </summary>
    public class PushAudienceObject
    {
        private List<string> _objects;
        private PushAudienceSecondObject _secondObjects;

        /// <summary>
        /// 推送设备类型，默认为Alias
        /// </summary>
        public PushAudienceCategory Category { get; set; }

        /// <summary>
        /// 推送对象
        /// </summary>
        public List<string> Objects
        {
            get
            {
                if (_objects == null)
                {
                    _objects = new List<string>();
                }
                return _objects;
            }
        }

        /// <summary>
        /// 推送二级关联对象
        /// </summary>
        public PushAudienceSecondObject SecondObjects {
            get
            {
                if (_secondObjects == null)
                {
                    _secondObjects = new PushAudienceSecondObject();
                }
                return _secondObjects;
            }
        }
    }

    /// <summary>
    /// 推送二级关联对象
    /// </summary>
    public class PushAudienceSecondObject
    {
        private List<string> _objects;

        /// <summary>
        /// 二级关联对象关联方式
        /// </summary>
        public PushAudienceSecondCategory Category { get; set; }

        /// <summary>
        /// 二级关联推送对象
        /// </summary>
        public List<string> Objects
        {
            get
            {
                if (_objects == null)
                {
                    _objects = new List<string>();
                }
                return _objects;
            }
        }
    }
}
