using System;
using System.IO;
using FluentAssertions;
using KafkaTopicExtractor.Configurations;
using KafkaTopicExtractor.Helpers;
using Xunit;

namespace KafkaTopicExtractor.Tests
{
    public class ExtractorHelperTests
    {
        public class GetDestinationCsvFilenameTests
        {
            [Fact]
            public void Should_create_filename_with_datetime_stamp()
            {
                var dateTimeOffset = new DateTimeOffset(2020, 1, 2, 5, 34, 43, TimeSpan.Zero);
                var actual = ExtractorHelper.GetDestinationCsvFilename("topic", new KafkaTopicExtractorSettings(), new FileTagProvider(dateTimeOffset));
                actual.Name.Should().Be("topic_20200102_053443.csv");
            }

            [Fact]
            public void Should_create_filename_in_current_folder()
            {
                var dateTimeOffset = new DateTimeOffset(2020, 1, 2, 5, 34, 43, TimeSpan.Zero);
                var actual = ExtractorHelper.GetDestinationCsvFilename("topic", new KafkaTopicExtractorSettings(), new FileTagProvider(dateTimeOffset));

                var fileFolder = new FileInfo("tmp");
                actual.DirectoryName.Should().Be(fileFolder.DirectoryName);
            }

            [Fact]
            public void Should_create_filename_with_folder()
            {
                var dateTimeOffset = new DateTimeOffset(2020, 1, 2, 5, 34, 43, TimeSpan.Zero);
                var settings = new KafkaTopicExtractorSettings {Destination = "disk:/destination"};
                var actual = ExtractorHelper.GetDestinationCsvFilename("topic", settings, new FileTagProvider(dateTimeOffset));

                var filePath = Path.Combine(settings.Destination, "tmp");
                var fileFolder = new FileInfo(filePath);
                actual.DirectoryName.Should().Be(fileFolder.DirectoryName);
            }

            [Fact]
            public void Should_create_filename_with_folder_specified_with_slash()
            {
                var dateTimeOffset = new DateTimeOffset(2020, 1, 2, 5, 34, 43, TimeSpan.Zero);
                var settings = new KafkaTopicExtractorSettings {Destination = "disk:/destination/"};
                var actual = ExtractorHelper.GetDestinationCsvFilename("topic", settings, new FileTagProvider(dateTimeOffset));

                var filePath = Path.Combine(settings.Destination, "tmp");
                var fileFolder = new FileInfo(filePath);
                actual.DirectoryName.Should().Be(fileFolder.DirectoryName);
            }
        }
    }
}