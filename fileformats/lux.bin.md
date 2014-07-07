# lux.bin Format

lux.bin file is a binary file that stores the lux readings from the device

Light readings are stored continuously for every second the device is recording in ushort (16-bit unsigned integer) values.

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
If the .gt3x file is from a GT3X+ (serial number starts with "NEO") and the bytes from the lux.bin file are:

<table>
  <tr>
    <th>Bytes</th>
	<th>ushort</th>
    <th>Scale Factor</th>
	<th>Actual Lux Value</th>
	<th>Notes</th>
  </tr>
  <tr>
    <td>0x0000</td>
	<td>65535</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 1</td>
  </tr>
  <tr>
    <td>0x0013</td>
	<td>19</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 1</td>
  </tr>
  <tr>
    <td>0xFFFF</td>
	<td>65535</td>
    <td>1.25</td>
	<td>0</td>
	<td>Special condition 2</td>
  </tr>
  <tr>
    <td>0x0898</td>
	<td>2200</td>
    <td>1.25</td>
	<td>2500</td>
	<td>Multiplying 2200 * 1.25 is above 2500, so we use 2500</td>
  </tr>
  <tr>
    <td>0x044C</td>
	<td>1100</td>
    <td>1.25</td>
	<td>1375</td>
	<td>Multiplying 1375 * 1.25 is below 2500, so we use that result.</td>
  </tr>
</table>