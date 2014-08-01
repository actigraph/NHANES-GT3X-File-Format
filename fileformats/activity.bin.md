# activity.bin Format

The activity.bin file is a binary file that stores activity data from the device.

Activity data is stored continuously for every sample the device records.  Each sample contains all three axis of data in the following order: [**y-axis, x-axis, and z-axis**](https://help.theactigraph.com/entries/49654814 "axis information website"). 

To help conserve space, activity samples are bit-packed. A single 3-axis sample takes up 36 bits of data (12 bits per axis). To parse this data, you will have to portion the byte data into [nibbles](http://en.wikipedia.org/wiki/Nibble "Nibble").

The activity samples are encoded as 12-bit [two’s complement values](http://en.wikipedia.org/wiki/Two’s_complement "Two's Complement Wikipedia Page"). Two’s complement is the standard signed integer encoding used in modern architectures.

To convert the 12-bit values to 16-bit signed integers (Int16) for use, they must be [sign-extended](http://en.wikipedia.org/wiki/Sign_extension "Sign Extension wikipedia page"). Endianness doesn’t exactly apply for 12-bit values, but it is basically big-endian. In other words, the bits are in order from most-significant to least-significant.

**Note: Once sign-extended, activity values will only fall between the interval of -2046 and 2046. The remaining values (-2048, -2047, and 2047) are not valid and do not occur in the data stream. This prevents the resultant Gs from being above +6 Gs and below -6 Gs.**

## Special Conditions for Activity Values ##
1. If an axis value is greater than 2047, you need to bitwise OR it with 0xF000 (or just add 61440 to the value). This is the sign-extension from above.
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

## C# Source Code Example ##
See the method ParseAcceleration(Stream stream) in [Gt3xFile.cs](../blob/master/src/GT3X.Parsing.Library/Gt3xFile.cs)

    /// <summary> Parse activity data from a stream of data </summary>
	/// <param name="stream">The activity.bin stream of data to parse.</param>
	/// <returns>All of the activity samples in a stream.</returns>
	private IEnumerable<AccelerationSample> ParseAcceleration(Stream stream)
	{
	    if (!stream.CanRead)
	        throw new Exception("Unable to read from activity stream.");
	
	    double[] sample = new double[3];
	    int offset = 0;
	
	    int current = 0;
	
	    var timestampHelper = new TimestampHelper(1000, SampleRateInHz);
	    long milliSeconds = 0;
	    while (true)
	    {
	        for (int axis = 0; axis < 3; ++axis)
	        {
	            UInt16 shifter;
	
	            if (0 == (offset & 0x7))
	            {
	                current = stream.ReadByte();
	                if (current == -1)
	                {
	                    yield break;
	                }
	                shifter = (UInt16)((current & 0xFF) << 4);
	                offset += 8;
	
	                current = stream.ReadByte();
	                if (current == -1)
	                {
	                    yield break;
	                }
	                shifter |= (UInt16)((current & 0xF0) >> 4);
	                offset += 4;
	            }
	            else
	            {
	                shifter = (UInt16)((current & 0x0F) << 8);
	                offset += 4;
	
	                current = stream.ReadByte();
	                if (current == -1)
	                {
	                    yield break;
	                }
	                shifter |= (UInt16)(current & 0xFF);
	                offset += 8;
	            }
	            if (0 != (shifter & 0x0800))
	                shifter |= 0xF000;
	
	            sample[axis] = (Int16)shifter / ACCELERATION_SCALE_FACTOR;
	        }
	
	        //round to 3 decimal places
	        sample[0] = Math.Round(sample[0], 3, MidpointRounding.AwayFromZero);
	        sample[1] = Math.Round(sample[1], 3, MidpointRounding.AwayFromZero);
	        sample[2] = Math.Round(sample[2], 3, MidpointRounding.AwayFromZero);
	
	        yield return
	            new AccelerationSample(sample[1], sample[0], sample[2],
	                FirstSample.AddTicks(milliSeconds*TimeSpan.TicksPerMillisecond));
	
	        milliSeconds += timestampHelper.Next();
	    }
	}