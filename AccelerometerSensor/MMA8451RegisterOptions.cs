namespace AccelerometerSensor
{
    internal sealed class MMA8451RegisterOptions
    {
        internal const byte MMA8451_REG_OUT_X_MSB = 0x01;
        internal const byte MMA8451_REG_SYSMOD = 0x0B;
        internal const byte MMA8451_REG_WHOAMI = 0x0D;
        internal const byte MMA8451_REG_XYZ_DATA_CFG = 0x0E;
        internal const byte MMA8451_REG_PL_STATUS = 0x10;
        internal const byte MMA8451_REG_PL_CFG = 0x11;
        internal const byte MMA8451_REG_CTRL_REG1 = 0x2A;
        internal const byte MMA8451_REG_CTRL_REG2 = 0x2B;
        internal const byte MMA8451_REG_CTRL_REG4 = 0x2D;
        internal const byte MMA8451_REG_CTRL_REG5 = 0x2E;

        internal const byte MMA8451_RANGE_8_G = 0x2; // +/- 8g
        internal const byte MMA8451_RANGE_4_G = 0x1; // +/- 4g
        internal const byte MMA8451_RANGE_2_G = 0x0; // +/- 2g (default value)
    }
}