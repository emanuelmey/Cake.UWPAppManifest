using System;
using System.Collections.Generic;
using System.IO;
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

            manifest.IdentityName.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            manifest.IdentityPublisher.Should().Be("CN=oliverkeidel");
            manifest.IdentityVersion.Should().Be("1.0.0.0");

            manifest.PhoneIdentityPhoneProductId.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            manifest.PhoneIdentityPhonePublisherId.Should().Be("00000000-0000-0000-0000-000000000000");

            manifest.PropertiesDisplayName.Should().Be("VisApp.UWP");
            manifest.PropertiesPublisherDisplayName.Should().Be("oliverkeidel");
            manifest.PropertiesLogo.Should().Be(@"Assets\StoreLogo.png");

            manifest.DependenciesTargetDeviceFamilyName.Should().Be("Windows.Universal");
            manifest.DependenciesTargetDeviceFamilyMinVersion.Should().Be("10.0.0.0");
            manifest.DependenciesTargetDeviceFamilyMaxVersionTested.Should().Be("10.0.0.0");

            manifest.ResourcesResourceLanguage.Should().Be("x-generate");

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

            manifest.CapabilitiesCapabilityName.Should().Be("internetClient");
        }

        [Fact]
        public void SerializeTest()
        {
            if (File.Exists(SaveTestPath))
            {
                File.Delete(SaveTestPath);
            }

            var originalManifest = UWPAppManifest.Create();

            originalManifest.IdentityName = "ad32948d-3ef9-4134-a243-2b20b88c04fc";
            originalManifest.IdentityPublisher = "CN=oliverkeidel";
            originalManifest.IdentityVersion = "1.0.0.0";

            originalManifest.PhoneIdentityPhoneProductId = "ad32948d-3ef9-4134-a243-2b20b88c04fc";
            originalManifest.PhoneIdentityPhonePublisherId = "00000000-0000-0000-0000-000000000000";

            originalManifest.PropertiesDisplayName = "VisApp.UWP";
            originalManifest.PropertiesPublisherDisplayName = "oliverkeidel";
            originalManifest.PropertiesLogo = @"Assets\StoreLogo.png";

            originalManifest.DependenciesTargetDeviceFamilyName = "Windows.Universal";
            originalManifest.DependenciesTargetDeviceFamilyMinVersion = "10.0.0.0";
            originalManifest.DependenciesTargetDeviceFamilyMaxVersionTested = "10.0.0.0";

            originalManifest.ResourcesResourceLanguage = "x-generate";

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

            originalManifest.CapabilitiesCapabilityName = "internetClient";

            Cake.SerializeAppManifest(SaveTestPath, originalManifest);

            var modifiedManifest = Cake.DeserializeAppManifest(new FilePath(SaveTestPath));

            modifiedManifest.IdentityName.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            modifiedManifest.IdentityPublisher.Should().Be("CN=oliverkeidel");
            modifiedManifest.IdentityVersion.Should().Be("1.0.0.0");

            modifiedManifest.PhoneIdentityPhoneProductId.Should().Be("ad32948d-3ef9-4134-a243-2b20b88c04fc");
            modifiedManifest.PhoneIdentityPhonePublisherId.Should().Be("00000000-0000-0000-0000-000000000000");

            modifiedManifest.PropertiesDisplayName.Should().Be("VisApp.UWP");
            modifiedManifest.PropertiesPublisherDisplayName.Should().Be("oliverkeidel");
            modifiedManifest.PropertiesLogo.Should().Be(@"Assets\StoreLogo.png");

            modifiedManifest.DependenciesTargetDeviceFamilyName.Should().Be("Windows.Universal");
            modifiedManifest.DependenciesTargetDeviceFamilyMinVersion.Should().Be("10.0.0.0");
            modifiedManifest.DependenciesTargetDeviceFamilyMaxVersionTested.Should().Be("10.0.0.0");

            modifiedManifest.ResourcesResourceLanguage.Should().Be("x-generate");

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

            modifiedManifest.CapabilitiesCapabilityName.Should().Be("internetClient");
        }

        [Fact]
        public void FlexibleTest()
        {
            var manifest = Cake.DeserializeAppManifest(new FilePath("Package.appxmanifest"));
            
            manifest.WriteAttributeValue("Applications$Application$http://schemas.microsoft.com/appx/manifest/uap/windows10%VisualElements$DisplayName", "Hello");
            manifest.WriteElementValue("Properties$DisplayName", "World!");

            manifest.ReadAttributeValue("Applications$Application$http://schemas.microsoft.com/appx/manifest/uap/windows10%VisualElements$DisplayName").Should().Be("Hello");
            manifest.ReadElementValue("Properties$DisplayName").Should().Be("World!");
        }
    }
}
