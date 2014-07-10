# ActiGraph NHANES .gt3x File Format

* **Document #:** 
* **Revision:** A [changelog](changelog.md)
* **Revision Date:** 

**Prepared By:**

* [Brian Bell](https://github.com/brianbell) - Software Manager
* [Daniel Judge](https://github.com/dwjref) - Software Developer

## Introduction

This documentation and sample library provide information on getting activity data out of ActiGraph .gt3x files that were used in the [NHANES](http://www.cdc.gov/nchs/nhanes.htm) project. 

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

