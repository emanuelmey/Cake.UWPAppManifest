using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Cake.Core;
using Cake.Core.IO;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.UWPAppManifest.Test
{
    public class UWPAppManifestAliasesTest
    {
        private const string SaveTestPath = "Test.xml";
        private ICakeContext Cake { get; }

        public UWPAppManifestAliasesTest()
        {
            Cake = Substitute.For<ICakeContext>();
        }

        [Fact]
        public void DeserializeTest()
        {
            var manifest = Cake.DeserializeAppManifest(new FilePath("Package.appxmanifest"));

            manifest.Identity_Name.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            manifest.Identity_Publisher.Should().Be("CN=oliverkeidel");
            manifest.Identity_Version.Should().Be("1.0.0.0");

            manifest.PhoneIdentity_PhoneProductId.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            manifest.PhoneIdentity_PhonePublisherId.Should().Be("00000000-0000-0000-0000-000000000000");

            manifest.Properties_DisplayName.Should().Be("VisApp.UWP");
            manifest.Properties_PublisherDisplayName.Should().Be("oliverkeidel");
            manifest.Properties_Logo.Should().Be(@"Assets\StoreLogo.png");

            manifest.Dependencies_TargetDeviceFamilyName.Should().Be("Windows.Universal");
            manifest.Dependencies_TargetDeviceFamily_MinVersion.Should().Be("10.0.0.0");
            manifest.Dependencies_TargetDeviceFamily_MaxVersionTested.Should().Be("10.0.0.0");

            manifest.Resources_Resource_Language.Should().Be("x-generate");

            manifest.Applications.Should().BeEquivalentTo(new List<UWPAppManifest.ApplicationElement>
            {
                new UWPAppManifest.ApplicationElement
                {
                    Id = "App",
                    Executable = "$targetnametoken$.exe",
                    EntryPoint = "VisApp.UWP.App",
                    VisualElementsDisplayName = "VisApp.UWP",
                    VisualElementsSquare150x150Logo = @"Assets\Square150x150Logo.png",
                    VisualElementsSquare44x44Logo = @"Assets\Square44x44Logo.png",
                    VisualElementsDescription = "VisApp.UWP",
                    VisualElementsBackgroundColor = "transparent",
                    VisualElementsDefaultTitleWide310x150Logo = @"Assets\Wide310x150Logo.png",
                    VisualElementsSplashScreenImage = @"Assets\SplashScreen.png"
                }
            });

            manifest.Capabilities_Capability_Name.Should().Be("internetClient");
        }

        [Fact]
        public void SerializeTest()
        {
            if (File.Exists(SaveTestPath))
            {
                File.Delete(SaveTestPath);
            }

            var originalManifest = UWPAppManifest.Create();

            originalManifest.Identity_Name = "ad32948d-3ef9-4134-a243-2b20b88c04fc";
            originalManifest.Identity_Publisher = "CN=oliverkeidel";
            originalManifest.Identity_Version = "1.0.0.0";

            originalManifest.PhoneIdentity_PhoneProductId = "ad32948d-3ef9-4134-a243-2b20b88c04fc";
            originalManifest.PhoneIdentity_PhonePublisherId = "00000000-0000-0000-0000-000000000000";

            originalManifest.Properties_DisplayName = "VisApp.UWP";
            originalManifest.Properties_PublisherDisplayName = "oliverkeidel";
            originalManifest.Properties_Logo = @"Assets\StoreLogo.png";

            originalManifest.Dependencies_TargetDeviceFamilyName = "Windows.Universal";
            originalManifest.Dependencies_TargetDeviceFamily_MinVersion = "10.0.0.0";
            originalManifest.Dependencies_TargetDeviceFamily_MaxVersionTested = "10.0.0.0";

            originalManifest.Resources_Resource_Language = "x-generate";

            originalManifest.Applications = new List<UWPAppManifest.ApplicationElement>
            {
                new UWPAppManifest.ApplicationElement
                {
                    Id = "App",
                    Executable = "$targetnametoken$.exe",
                    EntryPoint = "VisApp.UWP.App",
                    VisualElementsDisplayName = "VisApp.UWP",
                    VisualElementsSquare150x150Logo = @"Assets\Square150x150Logo.png",
                    VisualElementsSquare44x44Logo = @"Assets\Square44x44Logo.png",
                    VisualElementsDescription = "VisApp.UWP",
                    VisualElementsBackgroundColor = "transparent",
                    VisualElementsDefaultTitleWide310x150Logo = @"Assets\Wide310x150Logo.png",
                    VisualElementsSplashScreenImage = @"Assets\SplashScreen.png"
                }
            };

            originalManifest.Capabilities_Capability_Name = "internetClient";

            Cake.SerializeAppManifest(SaveTestPath, originalManifest);

            var modifiedManifest = Cake.DeserializeAppManifest(new FilePath(SaveTestPath));

            modifiedManifest.Identity_Name.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            modifiedManifest.Identity_Publisher.Should().Be("CN=oliverkeidel");
            modifiedManifest.Identity_Version.Should().Be("1.0.0.0");

            modifiedManifest.PhoneIdentity_PhoneProductId.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            modifiedManifest.PhoneIdentity_PhonePublisherId.Should().Be("00000000-0000-0000-0000-000000000000");

            modifiedManifest.Properties_DisplayName.Should().Be("VisApp.UWP");
            modifiedManifest.Properties_PublisherDisplayName.Should().Be("oliverkeidel");
            modifiedManifest.Properties_Logo.Should().Be(@"Assets\StoreLogo.png");

            modifiedManifest.Dependencies_TargetDeviceFamilyName.Should().Be("Windows.Universal");
            modifiedManifest.Dependencies_TargetDeviceFamily_MinVersion.Should().Be("10.0.0.0");
            modifiedManifest.Dependencies_TargetDeviceFamily_MaxVersionTested.Should().Be("10.0.0.0");

            modifiedManifest.Resources_Resource_Language.Should().Be("x-generate");

            modifiedManifest.Applications.Should().BeEquivalentTo(new List<UWPAppManifest.ApplicationElement>
            {
                new UWPAppManifest.ApplicationElement
                {
                    Id = "App",
                    Executable = "$targetnametoken$.exe",
                    EntryPoint = "VisApp.UWP.App",
                    VisualElementsDisplayName = "VisApp.UWP",
                    VisualElementsSquare150x150Logo = @"Assets\Square150x150Logo.png",
                    VisualElementsSquare44x44Logo = @"Assets\Square44x44Logo.png",
                    VisualElementsDescription = "VisApp.UWP",
                    VisualElementsBackgroundColor = "transparent",
                    VisualElementsDefaultTitleWide310x150Logo = @"Assets\Wide310x150Logo.png",
                    VisualElementsSplashScreenImage = @"Assets\SplashScreen.png"
                }
            });

            modifiedManifest.Capabilities_Capability_Name.Should().Be("internetClient");
        }

        [Fact]
        public void FlexibleTest()
        {
            var manifest = Cake.DeserializeAppManifest(new FilePath("Package.appxmanifest"));

            manifest.AddElement("Properties", "Test");
            
            manifest.WriteAttributeValue("Applications$Application$http://schemas.microsoft.com/appx/manifest/uap/windows10%VisualElements$DisplayName", "Hello");
            manifest.WriteElementValue("Properties$DisplayName", "World!");
            manifest.WriteAttributeValue("Properties$Test$Hello", "Bird");

            

            manifest.ReadAttributeValue("Applications$Application$http://schemas.microsoft.com/appx/manifest/uap/windows10%VisualElements$DisplayName").Should().Be("Hello");
            manifest.ReadElementValue("Properties$DisplayName").Should().Be("World!");
            manifest.ReadAttributeValue("Properties$Test$Hello").Should().Be("Bird");
        }
    }
}
