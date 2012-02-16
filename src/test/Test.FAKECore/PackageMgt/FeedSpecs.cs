﻿using System.IO;
using Fake;
using Machine.Specifications;

namespace Test.FAKECore.PackageMgt
{
    public class when_getting_the_nuget_feed_url
    {
        It should_return_the_package_url = () => NuGetHelper.getRepoUrl().ShouldEqual(NugetData.RepositoryUrl);
    }

    public class when_discorvering_the_lastest_FAKE_package
    {
        static NuGetHelper.NuSpecPackage _package;
        Because of = () => _package = NuGetHelper.getLatestPackage(NuGetHelper.getRepoUrl(), "FAKE");

        It should_be_the_latest_version =
            () => _package.IsLatestVersion.ShouldBeTrue();

        It should_contain_steffen_as_author =
            () => _package.Authors.ShouldContain("Steffen Forkmann");

        It should_contain_the_creation_date = 
            () => _package.Created.Year.ShouldBeGreaterThanOrEqualTo(2012);

        It should_contain_the_id = () => _package.Id.ShouldEqual("FAKE");

        It should_contain_the_packet_hash = () => _package.PackageHash.ShouldNotBeNull();

        It should_contain_the_packet_hash_algorithm = 
            () => _package.PackageHashAlgorithm.ShouldEqual("SHA512");

        It should_contain_the_project_url = 
            () => _package.ProjectUrl.ShouldEqual("https://github.com/forki/Fake");

        It should_contain_the_publiNuSpecPackageshing_date = 
            () => _package.Published.Year.ShouldBeGreaterThanOrEqualTo(2012);

        It should_contain_the_license_url = 
            () => _package.LicenseUrl.ShouldEqual("https://github.com/forki/Fake/blob/master/License.txt");

        It should_contain_the_version = () => _package.Version.ShouldContain(".");

        It should_contain_the_package_url =
            () => _package.Url.ShouldEqual("http://packages.nuget.org/api/v1/package/FAKE/" + _package.Version);

        It should_build_the_FileName_from_id_and_version =
            () => _package.FileName.ShouldEqual("FAKE.1.64.5.nupkg");
    }

    public class when_discovering_a_specific_outdated_FAKE_package
    {
        static NuGetHelper.NuSpecPackage _package;
        Because of = () => _package = NuGetHelper.getPackage(NuGetHelper.getRepoUrl(), "FAKE", "1.56.10");

        It should_be_the_latest_version = () => _package.IsLatestVersion.ShouldBeFalse();
        It should_contain_the_id = () => _package.Id.ShouldEqual("FAKE");
        It should_contain_the_version = () => _package.Version.ShouldEqual("1.56.10");
    }

    public class when_downloading_the_lastest_SignalR_package
    {
        static NuGetHelper.NuSpecPackage _package;
        static string _fileName;
        Establish context = () => _package = NuGetHelper.getLatestPackage(NuGetHelper.getRepoUrl(), "SignalR");
        Because of = () => _fileName = NuGetHelper.downloadPackage(NugetData.OutputDir, _package);

        It should_have_downloaded_the_file = () =>
        {
            File.Exists(_fileName).ShouldBeTrue();
            File.Delete(_fileName);
        };
    }
}