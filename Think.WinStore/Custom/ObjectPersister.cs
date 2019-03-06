using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Storage
{
    public enum ObjectPersisterLocation
    { 
        Local,
        Roaming,
        Temp,
    }

    public static class ObjectPersister
    {
        public static async void Save(string name, ObjectPersisterLocation location, object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            StorageFolder folder = null;
            switch (location)
            {
                case ObjectPersisterLocation.Local:
                    folder = ApplicationData.Current.LocalFolder;
                    break;
                case ObjectPersisterLocation.Roaming:
                    folder = ApplicationData.Current.RoamingFolder;
                    break;
                case ObjectPersisterLocation.Temp:
                    folder = ApplicationData.Current.TemporaryFolder;
                    break;
            }

            var types = new Dictionary<Type, Type>();
            AddTypes(obj, types);
            var typeList = new List<string>();
            foreach (var type in types.Keys)
            {
                typeList.Add(type.FullName);
            }

            var metaFile = await folder.CreateFileAsync(name + ".meta", CreationCollisionOption.ReplaceExisting);
            using (var stream = await metaFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var serializer = new DataContractSerializer(typeof(List<string>));
                serializer.WriteObject(stream.AsStreamForWrite(), typeList);
                await stream.FlushAsync();
            }

            var file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var settings = new DataContractSerializerSettings();
                settings.PreserveObjectReferences = true;
                settings.KnownTypes = types.Keys;
                var serializer = new DataContractSerializer(obj.GetType(), settings);
                serializer.WriteObject(stream.AsStreamForWrite(), obj);
                await stream.FlushAsync();
            }
        }

        public static async Task<T> Load<T>(string name, ObjectPersisterLocation location)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            StorageFolder folder = null;
            switch (location)
            {
                case ObjectPersisterLocation.Local:
                    folder = ApplicationData.Current.LocalFolder;
                    break;
                case ObjectPersisterLocation.Roaming:
                    folder = ApplicationData.Current.RoamingFolder;
                    break;
                case ObjectPersisterLocation.Temp:
                    folder = ApplicationData.Current.TemporaryFolder;
                    break;
            }

            List<string> typeList;

            try
            {
                var metaFile = await folder.GetFileAsync(name + ".meta");
                using (var stream = await metaFile.OpenReadAsync())
                {
                    var serializer = new DataContractSerializer(typeof(List<String>));
                    typeList = (List<String>)(serializer.ReadObject(stream.AsStreamForRead()));
                }

                var file = await folder.GetFileAsync(name);
                using (var stream = await file.OpenReadAsync())
                {
                    var settings = new DataContractSerializerSettings();
                    settings.PreserveObjectReferences = true;
                    var types = new List<Type>();
                    foreach (var typeName in typeList)
                    {
                        types.Add(Type.GetType(typeName));
                    }
                    settings.KnownTypes = types;
                    var serializer = new DataContractSerializer(typeof(T), settings);
                    return (T)(serializer.ReadObject(stream.AsStreamForRead()));
                }
            }
            catch (FileNotFoundException)
            {
                return default(T);
            }
        }

        private static void AddTypes(object obj, Dictionary<Type, Type> types)
        {
            if (obj == null)
                return;
            var type = obj.GetType();
            var typeInfo = type.GetTypeInfo();
            if (type == typeof(string) || typeInfo.IsPrimitive)
                return;
            types[type] = type;
            foreach (var field in typeInfo.DeclaredFields)
            {
                if (field.IsPublic && !field.IsStatic)
                {
                    var value = field.GetValue(obj);
                    AddTypes(value, types);
                }
            }
            foreach (var property in typeInfo.DeclaredProperties)
            {
                if (property.CanRead)
                {
                    var value = property.GetValue(obj);
                    AddTypes(value, types);
                }
            }
        }
    }
}
