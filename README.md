# ActiGraph NHANES .gt3x File Format

* **Document #:** 
* **Revision:** A [changelog](changelog.md)
* **Revision Date:** 2013-12-23

**Prepared By:**

* [Brian Bell](https://github.com/brianbell) - Software Manager
* [Daniel Judge](https://github.com/dwjref) - Software Developer

## Introduction

This documentation and sample library provide information on getting activity data out of ActiGraph .gt3x files that were used in the [NHANES](http://www.cdc.gov/nchs/nhanes.htm) project. 

## File Format

The .gt3x file is a zip archive that has the following contents:

<table>
  <tr>
    <th>FileName</th>
    <th>Description of File</th>
	<th>Used in Parsing?</th>
  </tr>
  <tr>
    <td>activity.bin</td>
    <td>Binary Activity Data Log</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>battery.bin</td>
    <td>Binary Battery Data Log</td>
	<td>No</td>
  </tr>
  <tr>
    <td>eeprom.bin</td>
    <td>EEPROM device contents</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>firmware.bin</td>
    <td>Firmware Update Storage</td>
	<td>No</td>
  </tr>
  <tr>
    <td>gt3xplus.ico</td>
    <td>Windows Drive Icon</td>
	<td>No</td>
  </tr>
  <tr>
    <td>info.txt</td>
    <td>Device information including start date and download date.</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>log.txt</td>
    <td>Firmware Diagnostics Log</td>
	<td>No</td>
  </tr>
  <tr>
    <td>lux.bin</td>
    <td>Binary Lux Data Log</td>
	<td>Yes</td>
  </tr>
  <tr>
    <td>metadata</td>
    <td>Subject Biometric Data</td>
	<td>No</td>
  </tr>
  <tr>
    <td>ram.bin/td>
    <td>Microcontroller RAM contents</td>
	<td>No</td>
  </tr>
</table>

[Technical information](technical.md) is also available.

