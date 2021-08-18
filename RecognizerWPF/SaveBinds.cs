using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace BinderWPF
{
    public class SaveBinds
    {
        XmlSerializer xmlFormatter;

        public SaveBinds()
        {
            xmlFormatter = new XmlSerializer(typeof(List<Bind>));
        }

        public void Save(List<Bind> binds)
        {
            using(var file = new FileStream("binds.xml", FileMode.Create))
            {
                xmlFormatter.Serialize(file, binds);
            }
        }

        public List<Bind> GetBinds()
        {
            List<Bind> list;
            using(var file = new FileStream("binds.xml", FileMode.OpenOrCreate))
            {
                list = xmlFormatter.Deserialize(file) as List<Bind>;
            }
            return list;
        }

    }
}
