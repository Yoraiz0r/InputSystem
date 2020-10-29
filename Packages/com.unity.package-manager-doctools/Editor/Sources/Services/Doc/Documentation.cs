using System;
using Debug = UnityEngine.Debug;

namespace UnityEditor.PackageManager.DocumentationTools.UI
{
    /// <summary>
    /// Example usage
    ///         Documentation.Instance.Generate("com.unity.ads", "2.0.3");
    ///         Documentation.Instance.GenerateRedirect("com.unity.ads", "2.0.3");
    /// </summary>
    public class Documentation
    {
        internal static Documentation instance;
        public static Documentation Instance
        {
            get
            {
                if (instance == null)
                    instance = new Documentation();

                return instance;
            }
        }

        internal static string GetShortVersionId(string packageName, string version)
        {
            var shortVersions = version.Split('.');
            if (shortVersions.Length < 3)
                throw new Exception("Semver has in invalid format: " + version);

            var shortVersion = string.Format("{0}.{1}", shortVersions[0], shortVersions[1]);
            var shortVersionId = string.Format("{0}@{1}", packageName, shortVersion);

            return shortVersionId;
        }

        private IDocumentationBuilder Builder;

        internal Documentation(IDocumentationBuilder documentationBuilder = null)
        {
            // Need to cache this value since it cannot be access from a thread.
            var persistentDataPath = DocumentationBuilder.PersistentDataPath;

            if (documentationBuilder == null)
                documentationBuilder = new DocumentationBuilder();

            Builder = documentationBuilder;
        }

        /// <summary>
        /// Open the url for the documentation micro site.
        ///
        /// This method will also generate the site locally if needed.
        /// </summary>
        /// <param name="packageInfo">The package properties.</param>
        /// <param name="shortVersionId">eg: com.unity.package-manager-ui@1.2 (note: not @1.2.0)</param>
        /// <param name="isEmbedded">If the package is embedded</param>
        /// <param name="isInstalled">If the package is currently installed</param>
        internal void View(PackageInfo packageInfo, string shortVersionId, bool isEmbedded, bool isInstalled)
        {
            Builder.OpenPackageUrl(packageInfo, shortVersionId, isEmbedded, isInstalled);
        }

        /// <summary>
        /// Open the URL of the changelog.
        ///
        /// This method will also generate the site locally if needed.
        /// </summary>
        /// <param name="packageInfo">Package properties.</param>
        /// <param name="shortVersionId">eg: com.unity.package-manager-ui@1.2 (note: not @1.2.0)</param>
        /// <param name="isEmbedded">If the package is embedded</param>
        /// <param name="isInstalled">If the package is currently installed</param>
        internal void ViewChangelog(PackageInfo packageInfo, string shortVersionId, bool isEmbedded, bool isInstalled)
        {
            Builder.OpenPackageUrl(packageInfo, shortVersionId, isEmbedded, isInstalled, "changelog/CHANGELOG.html");
        }

        /// <summary>
        /// Generate the all the necessary documentation sites.
        ///
        /// The documentation folders will be generated in
        ///     /Users/<USER NAME>/Library/Application Support/Unity/Editor/documentation/packages on Mac
        ///     C:\Users\<USER NAME>\AppData\Local\Unity\Editor\documentation\packages on Windows
        ///
        /// This will generate two folder, one which contains the documentation website, and another wich will contain a page to re-direct to the
        /// latest documentation website address.
        /// </summary>
        /// <param name="packageInfo">The package info object.</param>
        /// <param name="shortVersionId">eg: com.unity.package-manager-ui@1.2 (note: not @1.2.0)</param>
        /// <param name="isEmbedded">If the package is embedded</param>
        /// <param name="latestShortVersionId">Short version id of the latest available package. This is used when creating the redirect page to latest version.</param>
        /// <param name="absoluteLatestShortVersionId">Short version id of the latest available package, including preview. Used to link to latest preview version of a package. eg: 1.4.0-preview</param>
        internal string GenerateFullSite(PackageInfo packageInfo, string shortVersionId, bool isEmbedded, string latestShortVersionId = "", string absoluteLatestShortVersionId = "",
            bool openWebsite = true, bool revealInFinder = true)
        {
            var buildLog = "";
            if (!Builder.TryBuildRedirectToManual(packageInfo.name, shortVersionId))
                buildLog = Builder.BuildWithProgress(packageInfo, shortVersionId);                    // Always re-build

            if (string.IsNullOrEmpty(latestShortVersionId))
                latestShortVersionId = shortVersionId;

            Builder.BuildRedirectToLatestPage(packageInfo.name, latestShortVersionId, absoluteLatestShortVersionId);

            if (revealInFinder)
                EditorUtility.RevealInFinder(Builder.GetPackageSiteFolder(shortVersionId));

            if (openWebsite)
                Builder.OpenLocalPackageUrl(packageInfo, shortVersionId, isEmbedded, "index.html", true);

            Builder.BuildPackageMetaData(packageInfo.name);
            return buildLog;
        }

        /// <summary>
        /// Generate the documentation for a single package.
        ///
        /// By default, the documentation folders will be generated in
        ///     /Users/USER_NAME/Library/Application Support/Unity/Editor/documentation/packages on Mac
        ///     C:\Users\USER_NAME\AppData\Local\Unity\Editor\documentation\packages on Windows
        ///
        /// </summary>
        /// <param name="packageInfo">The package info object</param>
        /// <param name="version">Version in Semantic version format -- eg: 1.2.0</param>
        ///<param name="outputFolder">(Optional) Output folder where the doc site should be created.</param>
        public void Generate(PackageInfo packageInfo, string version, string outputFolder = null)
        {
            var packageName = packageInfo.name;
            var shortVersionId = GetShortVersionId(packageName, version);

            if (!Builder.TryBuildRedirectToManual(packageName, shortVersionId, outputFolder))
                Builder.BuildWithProgress(packageInfo, shortVersionId, outputFolder);                    // Always re-build
        }

        /// <summary>
        /// Generate the a site which will redirect to the latest version for a package.
        ///
        /// By default, the documentation folders will be generated in
        ///     /Users/USER_NAME/Library/Application Support/Unity/Editor/documentation/packages on Mac
        ///     C:\Users\USER_NAME\AppData\Local\Unity\Editor\documentation\packages on Windows
        ///
        /// </summary>
        /// <param name="packageName">eg: com.unity.package-manager-ui</param>
        /// <param name="latestVersionId">Version in Semantic version format that the site will redirect to -- eg: 1.2.0</param>
        /// <param name="outputFolder">(Optional) Output folder where the doc site should be created.</param>
        /// <param name="absoluteLatestShortVersionId">Short version id of the latest available package, including preview. Used to link to latest preview version of a package. eg: 1.4.0-preview</param>
        public void GenerateRedirect(string packageName, string latestVersionId, string outputFolder = null, string absoluteLatestShortVersionId = "")
        {
            if (string.IsNullOrEmpty(absoluteLatestShortVersionId))
                absoluteLatestShortVersionId = GetShortVersionId(packageName, latestVersionId);

            Builder.BuildRedirectToLatestPage(packageName, GetShortVersionId(packageName, latestVersionId), absoluteLatestShortVersionId, outputFolder);
        }

        /// <summary>
        /// Generate metadata for a package.
        /// </summary>
        /// <param name="packageName">eg: com.unity.package-manager-ui</param>
        /// <param name="outputFolder">(Optional) Output folder where the metadata should be created.</param>
        public void GenerateMetadata(string packageName, string outputFolder = null)
        {
            Builder.BuildPackageMetaData(packageName, outputFolder);
        }

        // TODO: this doesn't seem to be the right place for a "serve the docs" function, but serving is tightly bound to the building right now.
        public void ServeDocs(PackageInfo packageInfo, string shortVersionId, bool isEmbedded, string path = "index.html", bool doNotRebuild = false)
        {
            Builder.OpenLocalPackageUrl(packageInfo, shortVersionId, isEmbedded, path, doNotRebuild);
        }

        internal static void GeneratePackageDocTest()
        {
        }

/*
        ///<summary>Define Constant Test</summary>
#if DOCTOOLS_CONSTANT_EXAMPLE
        public static void GeneratePackageDocTestConstants()
        {
        }
#endif
*/
    }
}
