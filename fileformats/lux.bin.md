# lux.bin Format

The lux.bin file is a binary file that stores the lux readings from the device.

Light readings are stored continuously for every second the device is recording in ushort (16-bit unsigned integer) values in [little endian format](http://en.wikipedia.org/wiki/Endianness).

## Special Conditions for Lux Values
1. If a light reading is less than 20, it should be set to zero.
2. If a light reading is 65535 (0xFFFF), it should be set to zero.

##Scaling Lux values
If a light reading doesn't fall under special condition #1 or 2, we have to scale the values to get actual lux values. GT3x+ and ActiSleep+ have different scaling factors due to the difference in case colors.

<table>
  <tr>
    <th>Device Type</th>
    <th>Scale Factor</th>
	<th>Maximum Lux Value (after scaling)</th>
  </tr>
  <tr>
    <td>GT3X+</td>
    <td>1.25</td>
	<td>2500</td>
  </tr>
  <tr>
    <td>ActiSleep+</td>
    <td>3.25</td>
	<td>6000</td>
  </tr>
</table>

##Example

We have a .gt3x file with the following information:

1. The device used to record the data was a GT3X+ (serial number starts with "NEO")
2. Device started recording at 2008/3/29 12:00:00
3. Device recorded 5 seconds of data
4. The data from the lux.bin file is: "00001300FFFF98084C04"

<table>
  <tr>
	<th>Timestamp</th>
    <th>Bytes</th>
	<th>ushort</th>
    <th>Scale Factor</th>
	<th>Actual Lux Value</th>
	<th>Notes</th>
  </tr>
  <tr>
	<td>2008/3/29 12:00:00</td>
    <td>0x00 0x00</td>
	<td>0</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 1</td>
  </tr>
  <tr>
    <td>2008/3/29 12:00:01</td>
    <td>0x13 0x00</td>
	<td>19</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 1</td>
  </tr>
  <tr>
    <td>2008/3/29 12:00:02</td>
    <td>0xFF 0xFF</td>
	<td>65535</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 2</td>
  </tr>
  <tr>
    <td>2008/3/29 12:00:03</td>
    <td>0x98 0x08</td>
	<td>2200</td>
    <td>1.25</td>
	<td>2500</td>
	<td>Multiplying 2200 * 1.25 is above 2500, so we use 2500</td>
  </tr>
  <tr>
    <td>2008/3/29 12:00:04</td>
    <td>0x4C 0x44</td>
	<td>1100</td>
    <td>1.25</td>
	<td>1375</td>
	<td>Multiplying 1375 * 1.25 is below 2500, so we use that result.</td>
  </tr>
</table>