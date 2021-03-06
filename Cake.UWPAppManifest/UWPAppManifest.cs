﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Cake.UWPAppManifest
{
    public class UWPAppManifest
    {
        private readonly XDocument _doc;
        private readonly XElement _package;
        private readonly XElement _identity;
        private readonly XElement _phoneIdentity;
        private readonly XElement _properties;
        private readonly XElement _dependencies;
        private readonly XElement _resources;
        private readonly XElement _applications;
        private readonly XElement _capabilities;
        private XNamespace _nsDoc = "http://schemas.microsoft.com/appx/manifest/foundation/windows10";
        private XNamespace _nsMp = "http://schemas.microsoft.com/appx/2014/phone/manifest";
        private XNamespace _nsUap = "http://schemas.microsoft.com/appx/manifest/uap/windows10";

        private UWPAppManifest(XDocument doc)
        {
            _doc = doc;
            _package = _doc.Root;
            if (_package.Name.LocalName != "Package")
            {
                throw new Exception("App manifest does not have 'Package' root element");
            }

            _identity = _package.Element(_nsDoc + "Identity");
            if (_identity == null)
            {
                _package.Add(_identity = new XElement(_nsDoc + "Identity"));
            }

            _phoneIdentity = _package.Element(_nsMp + "PhoneIdentity");
            if (_phoneIdentity == null)
            {
                _package.Add(_phoneIdentity = new XElement(_nsMp + "PhoneIdentity"));
            }

            _properties = _package.Element(_nsDoc + "Properties");
            if (_properties == null)
            {
                _package.Add(_properties = new XElement(_nsDoc + "Properties"));
            }

            _dependencies = _package.Element(_nsDoc + "Dependencies");
            if (_dependencies == null)
            {
                _package.Add(_dependencies = new XElement(_nsDoc + "Dependencies"));
            }

            _resources = _package.Element(_nsDoc + "Resources");
            if (_resources == null)
            {
                _package.Add(_resources = new XElement(_nsDoc + "Resources"));
            }

            _applications = _package.Element(_nsDoc + "Applications");
            if (_applications == null)
            {
                _package.Add(_applications = new XElement(_nsDoc + "Applications"));
            }

            _capabilities = _package.Element(_nsDoc + "Capabilities");
            if (_capabilities == null)
            {
                _package.Add(_capabilities = new XElement(_nsDoc + "Capabilities"));
            }
        }

        public string Identity_Name
        {
            get => (string) _identity.Attribute("Name");
            set => _identity.SetAttributeValue("Name", NullIfEmpty(value));
        }

        public string Identity_Publisher
        {
            get => (string) _identity.Attribute("Publisher");
            set => _identity.SetAttributeValue("Publisher", NullIfEmpty(value));
        }

        public string Identity_Version
        {
            get => (string) _identity.Attribute("Version");
            set => _identity.SetAttributeValue("Version", NullIfEmpty(value));
        }

        public string PhoneIdentity_PhoneProductId
        {
            get => (string) _phoneIdentity.Attribute("PhoneProductId");
            set => _phoneIdentity.SetAttributeValue("PhoneProductId", NullIfEmpty(value));
        }

        public string PhoneIdentity_PhonePublisherId
        {
            get => (string) _phoneIdentity.Attribute("PhonePublisherId");
            set => _phoneIdentity.SetAttributeValue("PhonePublisherId", NullIfEmpty(value));
        }

        public string Properties_DisplayName
        {
            get => _properties.Element(_nsDoc + "DisplayName")?.Value;
            set => _properties.SetElementValue(_nsDoc + "DisplayName", NullIfEmpty(value));
        }

        public string Properties_PublisherDisplayName
        {
            get => _properties.Element(_nsDoc + "PublisherDisplayName")?.Value;
            set => _properties.SetElementValue(_nsDoc + "PublisherDisplayName", NullIfEmpty(value));
        }

        public string Properties_Logo
        {
            get => _properties.Element(_nsDoc + "Logo")?.Value;
            set => _properties.SetElementValue(_nsDoc + "Logo", NullIfEmpty(value));
        }

        public string Dependencies_TargetDeviceFamilyName
        {
            get => (string) _dependencies.Element(_nsDoc + "TargetDeviceFamily")?.Attribute("Name");
            set
            {
                if (_dependencies.Element(_nsDoc + "TargetDeviceFamily") == null)
                    _dependencies.Add(new XElement(_nsDoc + "TargetDeviceFamily"));
                _dependencies.Element(_nsDoc + "TargetDeviceFamily")?.SetAttributeValue("Name", NullIfEmpty(value));
            }
        }

        public string Dependencies_TargetDeviceFamily_MinVersion
        {
            get => (string)_dependencies.Element(_nsDoc + "TargetDeviceFamily")?.Attribute("MinVersion");
            set
            {
                if (_dependencies.Element(_nsDoc + "TargetDeviceFamily") == null)
                    _dependencies.Add(new XElement(_nsDoc + "TargetDeviceFamily"));
                _dependencies.Element(_nsDoc + "TargetDeviceFamily")
                    ?.SetAttributeValue("MinVersion", NullIfEmpty(value));
            }
        }

        public string Dependencies_TargetDeviceFamily_MaxVersionTested
        {
            get => (string)_dependencies.Element(_nsDoc + "TargetDeviceFamily")?.Attribute("MaxVersionTested");
            set
            {
                if (_dependencies.Element(_nsDoc + "TargetDeviceFamily") == null)
                    _dependencies.Add(new XElement(_nsDoc + "TargetDeviceFamily"));
                _dependencies.Element(_nsDoc + "TargetDeviceFamily")
                    ?.SetAttributeValue("MaxVersionTested", NullIfEmpty(value));
            }
        }

        public string Resources_Resource_Language
        {
            get => (string) _resources.Element(_nsDoc + "Resource")?.Attribute("Language");
            set
            {
                if (_resources.Element(_nsDoc + "Resource") == null)
                    _resources.Add(new XElement(_nsDoc + "Resource"));
                _resources.Element(_nsDoc + "Resource")?.SetAttributeValue("Language", NullIfEmpty(value));
            }
        }

        public IEnumerable<ApplicationElement> Applications
        {
            get
            {
                return _applications.Elements(_nsDoc + "Application").Select(element => new ApplicationElement
                {
                    Id = (string) element.Attribute("Id"),
                    Executable = (string) element.Attribute("Executable"),
                    EntryPoint = (string) element.Attribute("EntryPoint"),
                    VisualElementsDisplayName = (string) element.Element(_nsUap + "VisualElements")?.Attribute("DisplayName"),
                    VisualElementsSquare150x150Logo = (string) element.Element(_nsUap + "VisualElements")?.Attribute("Square150x150Logo"),
                    VisualElementsSquare44x44Logo = (string) element.Element(_nsUap + "VisualElements")?.Attribute("Square44x44Logo"),
                    VisualElementsDescription = (string) element.Element(_nsUap + "VisualElements")?.Attribute("Description"),
                    VisualElementsBackgroundColor = (string) element.Element(_nsUap + "VisualElements")?.Attribute("BackgroundColor"),
                    VisualElementsDefaultTitleWide310x150Logo = (string) element.Element(_nsUap + "VisualElements")?.Element(_nsUap + "DefaultTile")?.Attribute("Wide310x150Logo"),
                    VisualElementsSplashScreenImage = (string) element.Element(_nsUap + "VisualElements")?.Element(_nsUap + "SplashScreen")?.Attribute("Image")
                });
            }
            set
            {
                _applications.RemoveAll();
                foreach (var element in value)
                {
                    _applications.Add(new XElement(_nsDoc + "Application", 
                        new XAttribute("Id", element.Id), 
                        new XAttribute("Executable", element.Executable),
                        new XAttribute("EntryPoint", element.EntryPoint),
                        new XElement(_nsUap + "VisualElements",
                            new XAttribute("DisplayName", element.VisualElementsDisplayName),
                            new XAttribute("Square150x150Logo", element.VisualElementsSquare150x150Logo),
                            new XAttribute("Square44x44Logo", element.VisualElementsSquare44x44Logo),
                            new XAttribute("Description", element.VisualElementsDescription),
                            new XAttribute("BackgroundColor", element.VisualElementsBackgroundColor),
                            new XElement(_nsUap + "DefaultTile", new XAttribute("Wide310x150Logo", element.VisualElementsDefaultTitleWide310x150Logo)),
                            new XElement(_nsUap + "SplashScreen", new XAttribute("Image", element.VisualElementsSplashScreenImage))
                            )
                        )
                    );
                }
            }
        }

        public string Capabilities_Capability_Name
        {
            get => _capabilities.Element(_nsDoc + "Capability")?.Attribute("Name")?.Value;
            set
            {
                if (_capabilities.Element(_nsDoc + "Capability") == null)
                    _capabilities.Add(new XElement(_nsDoc + "Capability"));
                _capabilities.Element(_nsDoc + "Capability")?.SetAttributeValue("Name", NullIfEmpty(value));
            }
        }

        /// <summary>
        /// Reads the attribute value given by the path, seperator for elements is '$', seperator for namespace is '%'.
        /// </summary>
        /// <param name="path">The path, beginning at the root.</param>
        /// <returns>The attribute value as a string.</returns>
        public string ReadAttributeValue(string path)
        {
            var pathParts = path.Split(new[]{ '$' }, StringSplitOptions.RemoveEmptyEntries);

            XElement element = _doc.Root;
            XAttribute attribute = null;
            foreach (var part in pathParts)
            {
                var split = part.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 1 && part == pathParts.Last())
                    attribute = element?.Attribute(split[0]);
                if (split.Length == 1 && part != pathParts.Last())
                    element = element?.Element(_nsDoc + split[0]);
                if (split.Length == 2 && part == pathParts.Last()) 
                    attribute = element?.Attribute(XName.Get("{" + split[0] + "}" + split[1]));
                if (split.Length == 2 && part != pathParts.Last())
                    element = element?.Element(XName.Get("{" + split[0] + "}" + split[1]));
            }

            return attribute?.Value;
        }

        /// <summary>
        /// Reads the element value given by the path, seperator for elements is '$', seperator for namespace is '%'.
        /// </summary>
        /// <param name="path">The path, beginning at the root.</param>
        /// <returns>The element Value as a string.</returns>
        public string ReadElementValue(string path)
        {
            var pathParts = path.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            XElement element = _doc.Root;
            foreach (var part in pathParts)
            {
                var split = part.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);

                switch (split.Length)
                {
                    case 1:
                        element = element?.Element(_nsDoc + split[0]);
                        break;
                    case 2:
                        element = element?.Element(XName.Get("{" + split[0] + "}" + split[1]));
                        break;
                }
            }

            return element?.Value;
        }

        /// <summary>
        /// Writes the attribute value given by the path, seperator for elements is '$', seperator for namespace is '%'.
        /// </summary>
        /// <param name="path">The path, beginning at the root.</param>
        /// <param name="value">The value in string form.</param>
        public void WriteAttributeValue(string path, string value)
        {
            var pathParts = path.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            XElement element = _doc.Root;
            foreach (var part in pathParts)
            {
                var split = part.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 1 && part == pathParts.Last())
                    element?.SetAttributeValue(split[0], NullIfEmpty(value));
                if (split.Length == 1 && part != pathParts.Last())
                    element = element?.Element(_nsDoc + split[0]);
                if (split.Length == 2 && part == pathParts.Last())
                   element?.SetAttributeValue(XName.Get("{" + split[0] + "}" + split[1]), NullIfEmpty(value));
                if (split.Length == 2 && part != pathParts.Last())
                    element = element?.Element(XName.Get("{" + split[0] + "}" + split[1]));
            }
        }

        /// <summary>
        /// Writes the element value given by the path, seperator for elements is '$', seperator for namespace is '%'.
        /// </summary>
        /// <param name="path">The path, beginning at the root.</param>
        /// <param name="value">The value in string form.</param>
        public void WriteElementValue(string path, string value)
        {
            var pathParts = path.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            XElement element = _doc.Root;
            foreach (var part in pathParts)
            {
                var split = part.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);

                switch (split.Length)
                {
                    case 1:
                        element = element?.Element(_nsDoc + split[0]);
                        break;
                    case 2:
                        element = element?.Element(XName.Get("{" + split[0] + "}" + split[1]));
                        break;
                }
            }

            element?.SetValue(value);
        }

        /// <summary>
        /// Adds an element as a child to the last element of the path, seperator for elements is '$', seperator for namespace is '%'.
        /// </summary>
        /// <param name="path">The path, beginning at the root.</param>
        /// <param name="name">The name of the new element.</param>
        public void AddElement(string path, string name)
        {
            var pathParts = path.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            XElement element = _doc.Root;
            foreach (var part in pathParts)
            {
                var split = part.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);

                switch (split.Length)
                {
                    case 1:
                        element = element?.Element(_nsDoc + split[0]);
                        break;
                    case 2:
                        element = element?.Element(XName.Get("{" + split[0] + "}" + split[1]));
                        break;
                }
            }

            element?.Add(new XElement(_nsDoc + name));
        }

        public static UWPAppManifest Create()
        {
            return new UWPAppManifest(XDocument.Parse(
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<Package
  xmlns=""http://schemas.microsoft.com/appx/manifest/foundation/windows10""
  xmlns:mp=""http://schemas.microsoft.com/appx/2014/phone/manifest""
  xmlns:uap=""http://schemas.microsoft.com/appx/manifest/uap/windows10""
  IgnorableNamespaces=""uap mp"">
</Package>"));
        }

        public static UWPAppManifest Load(string filename)
        {
            return Load(XDocument.Load(filename));
        }

        public static UWPAppManifest Load(XDocument doc)
        {
            return new UWPAppManifest(doc);
        }

        public void Write(XmlWriter writer)
        {
            _doc.Save(writer);
        }

        public void WriteToFile(string fileName)
        {
            var xmlSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                CloseOutput = false,
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\n"
            };
            using (var stream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = XmlWriter.Create(stream, xmlSettings))
            {
                Write(writer);
            }
        }

        private static string NullIfEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public class ApplicationElement
        {
            public string Id { get; set; }
            public string Executable { get; set; }
            public string EntryPoint { get; set; }
            public string VisualElementsDisplayName { get; set; }
            public string VisualElementsSquare150x150Logo { get; set; }
            public string VisualElementsSquare44x44Logo { get; set; }
            public string VisualElementsDescription { get; set; }
            public string VisualElementsBackgroundColor { get; set; }
            public string VisualElementsDefaultTitleWide310x150Logo { get; set; }
            public string VisualElementsSplashScreenImage { get; set; }
        }
    }
}
