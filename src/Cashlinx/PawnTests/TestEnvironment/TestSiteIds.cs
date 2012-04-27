using System.IO;
using System.Xml.Serialization;
using Common.Libraries.Objects;

namespace PawnTests.TestEnvironment
{
    public static class TestSiteIds
    {
        static TestSiteIds()
        {
            Store00152 = LoadStore("Store_00152.xml");
            Store00604 = LoadStore("Store_00604.xml");
            Store00901 = LoadStore("Store_00901.xml");
        }

        public static SiteId Store00152 { get; private set; }
        public static SiteId Store00604 { get; private set; }
        public static SiteId Store00901 { get; private set; }

        private static SiteId LoadStore(string xmlFile)
        {
            var serializer = new XmlSerializer(typeof(SiteId));
            var reader = new StreamReader(Path.Combine(TestEnvironmentInfo.Instance.ProjectDirectory, "TestEnvironment\\MockSiteIds\\" + xmlFile));
            return serializer.Deserialize(reader) as SiteId;
        }
    }
}
