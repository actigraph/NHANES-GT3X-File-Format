# ActiGraph NHANES .gt3x File Format

**Prepared By:**

* [Daniel Judge](https://github.com/dwjref "Daniel's GitHub Profile") - Software Engineer
* [Judge Maygarden](https://github.com/jmaygarden "Judge's GitHub Profile") - Hardware/Firmware Engineer

**Last Update:** 2014-07-17

## Introduction

This documentation and sample library provide information on getting activity data out of [ActiGraph](http://www.actigraphcorp.com/ "ActiGraph site") .gt3x files that were used in the [NHANES](http://www.cdc.gov/nchs/nhanes.htm) project. 

## File Format

The .gt3x file is a zip archive contains several files needed to parse activity data. Click on a file for detailed information.

<table>
  <tr>
    <th>FileName</th>
    <th>Description</th>
	<th>Size</th>
	<th>Used in Parsing?</th>
  </tr>
  <tr>
    <td><a href="fileformats/activity.bin.md">activity.bin</a></td>
    <td>Binary Activity Data Log</td>
	<td>Up to 237.5 MB</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>battery.bin</td>
    <td>Binary Battery Data Log</td>
	<td>Up to 256 kB</td>
	<td>No</td>
  </tr>
  <tr>
    <td>eeprom.bin</td>
    <td>EEPROM device contents</td>
	<td>2 kB</td>
	<td>No</td>
  </tr>
  <tr>
    <td>firmware.bin</td>
    <td>Firmware Update Storage</td>
	<td>128 kB</td>
	<td>No</td>
  </tr>
  <tr>
    <td>gt3xplus.ico</td>
    <td>Windows Drive Icon</td>
	<td>9,662 bytes</td>
	<td>No</td>
  </tr>
  <tr>
    <td><a href="fileformats/info.txt.md">info.txt</a></td>
    <td>Device information including start date and download date.</td>
	<td>No limit (usually less than 1 kB)</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>log.txt</td>
    <td>Firmware Diagnostics Log</td>
	<td>Up to 128 kB</td>
	<td>No</td>
  </tr>
  <tr>
    <td><a href="fileformats/lux.bin.md">lux.bin</a></td>
    <td>Binary Lux Data Log</td>
	<td>Up to 3.5 MB</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>metadata</td>
    <td>Subject Biometric Data</td>
	<td>128 kB</td>
	<td>No</td>
  </tr>
  <tr>
    <td>ram.bin</td>
    <td>Microcontroller RAM contents</td>
	<td>Up to 128 kB</td>
	<td>No</td>
  </tr>
</table>

[Technical information](technical.md) is also available.

## Steps to Parse Activity and Light Data from a .gt3x File

1. Verify .gt3x file is a zip file
2. Verify .gt3x file has activity.bin file
3. Verify .gt3x file has info.txt file
4. Verify .gt3x file has lux.bin file
5. Extract activity.bin, info.txt and lux.bin files
6. Verify the serial number in the info.txt file starts with either "NEO" or "MRA"
7. Parse and save the sample rate from the info.txt file (it's stored in Hz)
8. Parse and save the start date from the info.txt file (it's stored in [.NET Ticks](technical.md))
9. Optional: calculate total number of samples by multiplying the file size (in bytes) of the activity.bin file by 8 (bits per byte) and then dividing by 36 (bits per sample).
10. Open a stream connection to the extracted activity.bin file from #5 and [parse the activity data](fileformats/activity.bin.md).
11. Optional: Open a stream connection to the extracted lux.bin file from #5 and [parse the light data](fileformats/lux.bin.md).

## Tools Used to Prepare Documentation and Library ##
- [MarkdownPad](http://markdownpad.com/ "MarkdownPad site") for documentation
- [LinqPad](http://www.linqpad.net/ "Linqpad site") for quick C# parsing
- [xUnit.net](https://github.com/xunit/xunit "xunit site") for unit tests
- [DotNetZip](https://github.com/haf/DotNetZip.Semverd/ "dotnetzip site") for easy .NET zip file parsing
- [Visual Studio](http://www.visualstudio.com/ "visual studio site") for library creation and testing 
- [ReSharper](http://www.jetbrains.com/resharper/ "resharper site") for improved productivity in Visual Studio and unit test runner.
- [Json.NET](http://james.newtonking.com/json "json dot net site") for amazing JSON usability in .NET.
- [CsvHelper](https://github.com/JoshClose/CsvHelper "csv helper site") to help write CSV files quickly.