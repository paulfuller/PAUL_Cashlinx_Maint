using JsonFx.Json;

namespace Common.Libraries.Utility.Serialization
{
    public static class ObjectSerializer
    {
        /// <summary>
        /// Convenience wrapper for serializing a strongly typed object
        /// to a JSON string. Utilizes the JsonFx.Json library
        /// </summary>
        /// <typeparam name="T">Type of object being passed in</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <returns>JSON encoded string representation of the object</returns>
        public static string Serialize<T>(T objectToSerialize) where T : class
        {
            if (objectToSerialize == null)
            {
                return (string.Empty);
            }
            return (JsonWriter.Serialize(objectToSerialize));
        }

        /// <summary>
        /// Convenience wrapper for deserializing a JSON encoded string
        /// into a strongly typed object.  Utilizes the JsonFx.Json library
        /// </summary>
        /// <typeparam name="T">Type of object contained in the string</typeparam>
        /// <param name="stringToDeSerialize">JSON encoded string representation of the object</param>
        /// <returns>An instance of the object encoded by the JSON string</returns>
        public static T DeSerialize<T>(string stringToDeSerialize) where T : class
        {
            if (string.IsNullOrEmpty(stringToDeSerialize))
            {
                return (default(T));
            }
            return (JsonReader.Deserialize<T>(stringToDeSerialize));
        }

        /// <summary>
        /// Clone object convenience method.  This must be utilized application wide
        /// for any and all serialization purposes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToClone"></param>
        /// <returns></returns>
        public static T CloneObject<T>(T objectToClone) where T : class
        {
            if (objectToClone == null)
            {
                return (null);                                
            }

            string tSer = Serialize(objectToClone);
            if (string.IsNullOrEmpty(tSer))
            {
                return (null);
            }
            var clonedObject = DeSerialize<T>(tSer);
            return (clonedObject);
        }
    }
}
