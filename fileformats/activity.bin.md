# activity.bin Format

The activity.bin file is a binary file that stores activity data from the device.

Activity data is stored continuously for every sample the device records.  Each sample contains all three axis of data in the following order: [**y-axis, x-axis, and z-axis**](https://help.theactigraph.com/entries/49654814 "axis information website"). 

To help conserve space, activity samples are bit-packed. A single 3-axis sample takes up 4.5 bytes of data (1.5 bytes per axis). To parse this data, you will have to portion the byte data into [nibbles](http://en.wikipedia.org/wiki/Nibble "Nibble").

The activity samples are encoded as 12-bit [two’s complement values](http://en.wikipedia.org/wiki/Two’s_complement "Two's Complement Wikipedia Page"). Two’s complement is the standard signed integer encoding used in modern architectures.

To convert the 12-bit values to 16-bit signed integers (Int16) for use, they must be [sign-extended](http://en.wikipedia.org/wiki/Sign_extension "Sign Extension wikipedia page").

Endianness doesn’t exactly apply for 12-bit values, but it is basically big-endian. In other words, the bits are in order from most-significant to least-significant.

## Special Conditions for Activity Values ##
1. If an activity value is greater than 2047, you need to bitwise OR it with 0xF000 (or just add 61440 to the value). This is the sign-extension from above.
2. If there are an odd number of samples, the last nibble of data is not used in parsing. See the the last Z-axis in the example below.

## Scaling Activity Values ##
Once a sample has been unpacked and the special conditions are accounted for, we must:

1. Cast the UInt16 values into *signed* 16-bit values. 
2. Scale the resultant by 341.0 (this gives us an acceleration value in G's).
3. Round the value from #2 to three decimal places.

## Example ##

We have a .gt3x file with the following information:

1. The device used to record the data was a GT3X+ (serial number starts with "NEO")
2. Device started recording at 2008/3/29 12:00:00
3. The data in the activity.bin file is: "00 60 08 EB D0 07 00 9E BF 00 70 08 EB F0"

<table>
  <tr>
	<th>Sample Count</th>
    <th>Axis</th>
	<th>Bytes to Use</th>
	<th>Binary Equivalent</th>
	<th>UInt16 from binary</th>
	<th>After Special Conditions</th>
	<th>Cast to Int16</th>
	<th>Scaling</th>
	<th>Rounding</th>
  </tr>
  <tr>
	<td>1</td>
    <td>Y</td>
	<td>0x0060</td>
    <td>000000000110<s>0000</s></td>
	<td>6</td>
	<td>6</td>
    <td>6</td>
    <td>0.0175953</td>
	<td>0.018</td>
  </tr>
  <tr>
	<td>1</td>
    <td>X</td>
	<td>0x6008</td>
    <td><s>0110</s>000000001000</td>
	<td>8</td>
	<td>8</td>
    <td>8</td>
    <td>0.023460</td>
    <td>0.023</td>
  </tr>
  <tr>
	<td>1</td>
    <td>Z</td>
	<td>0xEBD0</td>
    <td>111010111101<s>0000</s></td>
	<td>3773</td>
	<td>65213</td>
    <td>-323</td>
    <td>-0.947214</td>
    <td>-0.947</td>
  </tr>
  <tr>
	<td>2</td>
    <td>Y</td>
	<td>0xD007</td>
    <td><s>1101</s>000000000111</td>
	<td>7</td>
	<td>7</td>
    <td>7</td>
    <td>0.0205278</td>
    <td>0.021</td>
  </tr>
  <tr>
	<td>2</td>
    <td>X</td>
	<td>0x009E</td>
    <td>000000001001<s>1110</s></td>
	<td>9</td>
	<td>9</td>
    <td>9</td>
    <td>0.0263929</td>
    <td>0.026</td>
  </tr>
  <tr>
	<td>2</td>
    <td>Z</td>
	<td>0x9EBF</td>
    <td><s>1001</s>111010111111</td>
	<td>3775</td>
	<td>65215</td>
    <td>-321</td>
    <td>-0.941348</td>
    <td>-0.941</td>
  </tr>
  <tr>
	<td>3</td>
    <td>Y</td>
	<td>0x0070</td>
    <td>000000000111<s>0000</s></td>
	<td>7</td>
	<td>7</td>
    <td>7</td>
    <td>0.0205278</td>
    <td>0.021</td>
  </tr>
  <tr>
	<td>3</td>
    <td>X</td>
	<td>0x7008</td>
    <td><s>0111</s>000000001000</td>
	<td>8</td>
	<td>8</td>
    <td>8</td>
    <td>0.023460</td>
    <td>0.023</td>
  </tr>
  <tr>
	<td>3</td>
    <td>Z</td>
	<td>0xEBF0</td>
    <td>111010111111<s>0000</s></td>
	<td>3775</td>
	<td>65215</td>
    <td>-321</td>
    <td>-0.941348</td>
    <td>-0.941</td>
  </tr>
</table>

*binary values that are have <s>strikethrough</s> are the nibbles that are ignored.