﻿namespace CarDealer.XmlHelper
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public static class XmlConverter
    {
        public static string Serialize<T>
            (T dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            var builder = new StringBuilder();

            using var writer = new StringWriter(builder);
            serializer.Serialize(writer, dataTransferObjects, GetXmlNamespaces());

            return builder.ToString();
        }

        public static string Serialize<T>
            (T[] dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(xmlRootAttributeName));

            var builder = new StringBuilder();

            using var writer = new StringWriter(builder);
            serializer.Serialize(writer, dataTransferObjects, GetXmlNamespaces());

            return builder.ToString();
        }

        public static T[] Deserializer<T>
            (string xmlObjectsAsString, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(xmlRootAttributeName));

            using StringReader reader = new StringReader(xmlObjectsAsString);

            var dataTransferObjects = serializer.Deserialize(reader) as T[];
            //var dataTransferObjects = serializer.Deserialize(new StringReader(xmlObjectsAsString)) as T[];

            return dataTransferObjects;
        }

        public static T Deserializer<T>
            (string xmlObjectsAsString, string xmlRootAttributeName, bool isSampleObject)
            where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            using StringReader reader = new StringReader(xmlObjectsAsString);

            var dataTransferObjects = serializer.Deserialize(reader) as T;
            //var dataTransferObjects = serializer.Deserialize(new StringReader(xmlObjectsAsString)) as T;

            return dataTransferObjects;
        }

        private static XmlSerializerNamespaces GetXmlNamespaces()
        {
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);
            return xmlNamespaces;
        }
    }
}
