using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Text;

namespace KernelDev.DataAccess
{
    /// <summary>
    /// ExtensibleObjectBase 的摘要说明。
    /// </summary>
    public abstract class ExtensibleObjectBase : ISerializable
    {
        private StringDictionary m_dictValues;

        public ExtensibleObjectBase()
        {
            this.m_dictValues = new StringDictionary();

        }

        public void Deserialize(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry1 in info)
            {
                this.m_dictValues[entry1.Name] = (string)entry1.Value;
            }
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (DictionaryEntry entry1 in this.m_dictValues)
            {
                info.AddValue(entry1.Key.ToString(), entry1.Value);
            }
        }


        public string GetValue(string Key)
        {
            return this.m_dictValues[Key];
        }

        public StringDictionary GetValues()
        {
            return this.m_dictValues;
        }


        public void SetValue(string Key, string NewValue)
        {
            this.m_dictValues[Key] = NewValue;
        }


        public void SetValues(StringDictionary dictValues)
        {
            this.m_dictValues = dictValues;
        }


        public string XML(string strElementName)
        {
            MemoryStream stream1 = new MemoryStream();
            XmlTextWriter writer1 = new XmlTextWriter(stream1, Encoding.Default);
            writer1.Formatting = Formatting.Indented;
            writer1.WriteStartElement(strElementName);
            foreach (DictionaryEntry entry1 in this.m_dictValues)
            {
                writer1.WriteAttributeString(entry1.Key.ToString(), entry1.Value.ToString());
            }
            writer1.WriteEndElement();
            writer1.Flush();
            stream1.Position = 0;
            StreamReader reader1 = new StreamReader(stream1);
            return reader1.ReadToEnd();
        }



    }
}
