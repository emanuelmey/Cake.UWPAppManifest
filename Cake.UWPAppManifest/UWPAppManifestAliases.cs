using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.UWPAppManifest
{
    /// <summary>
    ///     Contains functionality to work with Android AppManifest files.
    /// </summary>
    [CakeAliasCategory("UWPAppManifest")]
    public static class UWPAppManifestAliases
    {

        /// <summary>
        ///     Deserializes the Android AppManifest
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xml">appmanifest xml as string</param>
        /// <returns>deserialized appmanifest</returns>
        [CakeMethodAlias]
        public static UWPAppManifest DeserializeAppManifest(this ICakeContext context, string xml)
        {
            return UWPAppManifest.Load(xml);
        }

        /// <summary>
        ///     Serializes the Android AppManifest
        /// </summary>
        /// <param name="context"></param>
        /// <param name="file">filepath to appmanifest xml</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static UWPAppManifest DeserializeAppManifest(this ICakeContext context, FilePath file)
        {
            return UWPAppManifest.Load(file.FullPath);
        }

        /// <summary>
        ///     Serializes a AppManifest into the specified file.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="file">target file</param>
        /// <param name="manifest"></param>
        [CakeMethodAlias]
        public static void SerializeAppManifest(this ICakeContext context, FilePath file, UWPAppManifest manifest)
        {
            manifest.WriteToFile(file.FullPath);
        }
    }
}
