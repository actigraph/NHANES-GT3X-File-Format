# eeprom.bin Format

eeprom.bin file is a binary file that stores device information.


<table>
  <tr>
    <th>Offset (dec)</th>
    <th>Offset (hex)</th>
	<th>Size</th>
	<th>Name</th>
	<th>Description</th>
  </tr>
    <tr>
    <td>0</td>
    <td>0000</td>
    <td>16</td>
    <td>SERIAL_NUMBER</td>
    <td>Device unique serial number (NEOxxxxxxxxx)</td>
  </tr>
  <tr>
    <td>16</td>
    <td>0010</td>
    <td>6</td>
    <td>FAULT_RESET_DATETIME</td>
    <td>Date and time at which the last watchdog reset occurred</td>
  </tr>
  <tr>
    <td>22</td>
    <td>0016</td>
    <td>4</td>
    <td>FAULT_PROGRAM_COUNTER</td>
    <td>Value of the PC register when the hard fault occurred</td>
  </tr>
  <tr>
    <td>26</td>
    <td>001A</td>
    <td>12</td>
    <td>FAULT_CLOCK_EVENT_COUNTS</td>
    <td>The current count of RTT events; matching RTC heartbeat and count of RTC events</td>
  </tr>
  <tr>
    <td>38</td>
    <td>0026</td>
    <td>1</td>
    <td>FAULT_FSM_STATE</td>
    <td>The current state of the main finite state machine</td>
  </tr>
  <tr>
    <td>39</td>
    <td>0027</td>
    <td>217</td>
    <td>PERMANENT_SPARE</td>
    <td>Spare area for storage of future permanent variables</td>
  </tr>
  <tr>
    <td>256</td>
    <td>0100</td>
    <td>4</td>
    <td>CALIBRATION_PATTERN</td>
    <td>0x454E5554 "TUNE"</td>
  </tr>
  <tr>
    <td>260</td>
    <td>0104</td>
    <td>2</td>
    <td>ZERO_G_OFFSET_X</td>
    <td>Accelerometer zero-g calibration value (I16)</td>
  </tr>
  <tr>
    <td>262</td>
    <td>0106</td>
    <td>2</td>
    <td>ZERO_G_OFFSET_Y</td>
    <td>Accelerometer zero-g calibration value (I16)</td>
  </tr>
  <tr>
    <td>264</td>
    <td>0108</td>
    <td>2</td>
    <td>ZERO_G_OFFSET_Z</td>
    <td>Accelerometer zero-g calibration value (I16)</td>
  </tr>
  <tr>
    <td>266</td>
    <td>010A</td>
    <td>2</td>
    <td>POSITIVE_G_OFFSET_X</td>
    <td>Accelerometer +1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>268</td>
    <td>010C</td>
    <td>2</td>
    <td>POSITIVE_G_OFFSET_Y</td>
    <td>Accelerometer +1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>270</td>
    <td>010E</td>
    <td>2</td>
    <td>POSITIVE_G_OFFSET_Z</td>
    <td>Accelerometer +1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>272</td>
    <td>0110</td>
    <td>2</td>
    <td>NEGATIVE_G_OFFSET_X</td>
    <td>Accelerometer -1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>274</td>
    <td>0112</td>
    <td>2</td>
    <td>NEGATIVE_G_OFFSET_Y</td>
    <td>Accelerometer -1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>276</td>
    <td>0114</td>
    <td>2</td>
    <td>NEGATIVE_G_OFFSET_Z</td>
    <td>Accelerometer -1 G calibration value (I16)</td>
  </tr>
  <tr>
    <td>278</td>
    <td>0116</td>
    <td>6</td>
    <td>CALIBRATION_DATETIME</td>
    <td>Device calibration date and time</td>
  </tr>
  <tr>
    <td>284</td>
    <td>011C</td>
    <td>32</td>
    <td>SLEEP_FILTER_COEFFICIENTS</td>
    <td>Sleep filter coefficients for each sample rate from lowest (30 Hz) to highest (100 Hz)</td>
  </tr>
  <tr>
    <td>316</td>
    <td>013C</td>
    <td>32</td>
    <td>BATTERY_FILTER_COEFFICIENTS</td>
    <td>Spare for future battery voltage filter coefficients</td>
  </tr>
  <tr>
    <td>348</td>
    <td>015C</td>
    <td>1</td>
    <td>FACTORY_TESTING_COMPLETE</td>
    <td>Indicates whether or not the device has passed front- and back-end manufacturing tests</td>
  </tr>
  <tr>
    <td>349</td>
    <td>015D</td>
    <td>4</td>
    <td>FIRMWARE_VERSION</td>
    <td>The installed firmware version in the format major (MM) minor (mm) and build (bbbb) or 0xMMmmbbbb. This is a little-endian long integer.</td>
  </tr>
  <tr>
    <td>353</td>
    <td>0161</td>
    <td>157</td>
    <td>CALIBRATION_SPARE</td>
    <td>Spare area for storage of future calibration variables</td>
  </tr>
  <tr>
    <td>510</td>
    <td>01FE</td>
    <td>2</td>
    <td>CALIBRATION_CRC</td>
    <td>CRC-CCITT of the calibration section (including the CALIBRATION_PATTERN)</td>
  </tr>
  <tr>
    <td>512</td>
    <td>0200</td>
    <td>4</td>
    <td>RECOVERY_PATTERN</td>
    <td>0x45564153 "SAVE"</td>
  </tr>
  <tr>
    <td>516</td>
    <td>0204</td>
    <td>32</td>
    <td>SUBJECT_NAME</td>
    <td>Subject name for current activity records</td>
  </tr>
  <tr>
    <td>548</td>
    <td>0224</td>
    <td>6</td>
    <td>START_DATETIME</td>
    <td>Desired data logging start date and time</td>
  </tr>
  <tr>
    <td>554</td>
    <td>022A</td>
    <td>6</td>
    <td>STOP_DATETIME</td>
    <td>Desired data logging stop date and time</td>
  </tr>
  <tr>
    <td>560</td>
    <td>0230</td>
    <td>1</td>
    <td>STOP_DATETIME_ENABLE</td>
    <td>Defines whether a predefined stop date/time is used (u8)</td>
  </tr>
  <tr>
    <td>561</td>
    <td>0231</td>
    <td>1</td>
    <td>SAMPLE_RATE</td>
    <td>Accelerometer sample rate in Hertz (U8)</td>
  </tr>
  <tr>
    <td>562</td>
    <td>0232</td>
    <td>1</td>
    <td>DEVICE_STATE</td>
    <td>Last saved device state (see DEVICE_STATE in the Device Parameter Database)</td>
  </tr>
  <tr>
    <td>563</td>
    <td>0233</td>
    <td>1</td>
    <td>ACTIVE_LED_ENABLE</td>
    <td>Defines whether a the LED is active while logging data (u8)</td>
  </tr>
  <tr>
    <td>564</td>
    <td>0234</td>
    <td>4</td>
    <td>UNEXPECTED_RESETS</td>
    <td>Count of the number of device resets experienced while in delay; active; or halt modes</td>
  </tr>
  <tr>
    <td>568</td>
    <td>0238</td>
    <td>6</td>
    <td>INITIALIZATION_DATETIME</td>
    <td>Current date/time when the device entered delay mode</td>
  </tr>
  <tr>
    <td>574</td>
    <td>023E</td>
    <td>6</td>
    <td>ACTUAL_STOP_DATETIME</td>
    <td>Current date/time when the device entered halt mode</td>
  </tr>
  <tr>
    <td>580</td>
    <td>0244</td>
    <td>1</td>
    <td>REASON_FOR_HALT</td>
    <td>Enumerated reason that the unit entered halt mode: 0 - client/1 - stop time reached/2 - memory full/3 - battery shutdown</td>
  </tr>
  <tr>
    <td>581</td>
    <td>0245</td>
    <td>4</td>
    <td>ECC_CORRECTION_COUNT</td>
    <td>A count of the number of 1-bit NAND flash errors corrected since initialization.</td>
  </tr>
  <tr>
    <td>585</td>
    <td>0249</td>
    <td>4</td>
    <td>ECC_ERROR_COUNT</td>
    <td>A count of the number of multiple bit NAND flash errors detected since initialization.</td>
  </tr>
  <tr>
    <td>589</td>
    <td>024D</td>
    <td>6</td>
    <td>ACTUAL_START_DATETIME</td>
    <td>Current date/time when the device entered active mode</td>
  </tr>
  <tr>
    <td>595</td>
    <td>0253</td>
    <td>1</td>
    <td>DELAY_LED_ENABLE</td>
    <td>Defines whether a the LED is active while in delay mode (u8)</td>
  </tr>
  <tr>
    <td>596</td>
    <td>0254</td>
    <td>2</td>
    <td>BATTERY_AT_START</td>
    <td>Battery voltage reading sample at the start time.</td>
  </tr>
  <tr>
    <td>598</td>
    <td>0256</td>
    <td>1</td>
    <td>SLEEP_ENABLE</td>
    <td>Enables/disables the power saving sleep mode.</td>
  </tr>
  <tr>
    <td>599</td>
    <td>0257</td>
    <td>167</td>
    <td>RECOVERY_SPARE</td>
    <td>Spare area for storage of future recovery variables</td>
  </tr>
  <tr>
    <td>766</td>
    <td>02FE</td>
    <td>2</td>
    <td>RECOVERY_CRC</td>
    <td>CRC-CCITT of the recovery section (including the RECOVERY_PATTERN)</td>
  </tr>
  <tr>
    <td>768</td>
    <td>0300</td>
    <td>256</td>
    <td>MAIN_TASK_DUMP</td>
    <td>Device failure TCB and stack dump storage</td>
  </tr>
  <tr>
    <td>1024</td>
    <td>0400</td>
    <td>256</td>
    <td>LOGGER_TASK_DUMP</td>
    <td>Device failure TCB and stack dump storage</td>
  </tr>
  <tr>
    <td>1280</td>
    <td>0500</td>
    <td>256</td>
    <td>MSD_TASK_DUMP</td>
    <td>Device failure TCB and stack dump storage</td>
  </tr>
  <tr>
    <td>1536</td>
    <td>0600</td>
    <td>256</td>
    <td>HID_TASK_DUMP</td>
    <td>Device failure TCB and stack dump storage</td>
  </tr>
  <tr>
    <td>1792</td>
    <td>0700</td>
    <td>256</td>
    <td>IDLE_TASK_DUMP</td>
    <td>Device failure TCB and stack dump storage</td>
  </tr>

</table>