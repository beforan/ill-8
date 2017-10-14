namespace ill8.Cpu
{

    /// <summary>
    /// The public API for the CPU to interface with RAM
    /// </summary>
    public interface IMemory
    {

        /// <summary>
        /// Read a byte value at a 12bit memory address
        /// </summary>
        /// <param name="address">The memory address to read from</param>
        /// <returns>The byte stored at the requested address</returns>
        byte Read(ushort address);

        /// <summary>
        /// Read a 16 bit value from two consecutive memory addresses.
        /// `address` contains LSB, `address+1` contains MSB.
        /// The bytes are put back together little endian, as follows:
        /// 
        /// address (LSB): 0xee
        /// address + 1 (MSB): 0xff
        /// result (MSBLSB): 0xffee
        /// </summary>
        /// <param name="address">The memory address to start reading at</param>
        /// <returns>
        /// The 16 bit value comprised of the byte stored at `address`,
        /// and the byte stored at `address+1`
        /// </returns>
        ushort ReadWord(ushort address);

        /// <summary>
        /// Write a byte value to the specified 12bit memory address.
        /// </summary>
        /// <param name="address">The memory address to write to</param>
        /// <param name="value">The byte value to write</param>
        void Write(ushort address, byte value);
    }
}