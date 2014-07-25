# Technical Information

This sample library is written in C# and can be easily referenced in any .NET application.

## Valid .gt3x Files ##

* This documentation and library are valid for .gt3x files downloaded from GT3X+ devices with serial numbers that start with "NEO" or ActiSleep+ devices with serial numbers that start with "MRA." The devices must have a firmware version of 2.5.0 or earlier. Files that don't fall under these conditions have a different format and are not parsable using the sample library.

## .NET Ticks
[.NET Ticks](http://msdn.microsoft.com/en-us/library/system.datetime.ticks.aspx) are used in the info.txt file to store DateTimes.

* A single tick represents one hundred nanoseconds or one ten-millionth of a second. There are 10,000 ticks in a millisecond, or 10 million ticks in a second.
* The value of this property represents the number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight, January 1, 0001, which represents DateTime.MinValue. It does not include the number of ticks that are attributable to leap seconds. 

## Units of Measurement

Axis data will be returned as G's.

## Date Formatting

The DateTime for each sample is in the same timezone as the original computer that initialized the device.