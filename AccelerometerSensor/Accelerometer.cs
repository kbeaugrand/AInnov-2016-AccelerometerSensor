
namespace AccelerometerSensor
{
    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Enumeration;
    using Windows.Devices.I2c;
    using Windows.Foundation;

    public sealed class Accelerometer : IDisposable
    {
        private I2cDevice device;

        public void Dispose()
        {
            if (this.device == null)
            {
                return;
            }

            device.Dispose();
            device = null;
        }

        public IAsyncAction InitializeAsync()
        {
            return InitializeSensorAsync().AsAsyncAction();
        }

        private async Task<bool> InitializeSensorAsync()
        {
            if (this.device != null)
            {
                this.Dispose();
            }

            // 0x1D is the I2C device address
            var settings = new I2cConnectionSettings(0x1D);
            settings.BusSpeed = I2cBusSpeed.FastMode;

            // Create an I2cDevice with the specified I2C settings
            var controller = await I2cController.GetDefaultAsync();

            I2cDevice device = controller.GetDevice(settings);

            if (device == null)
            {               
                return false;
            }

            this.device = device;

            if (ReadRegister(MMA8451RegisterOptions.MMA8451_REG_WHOAMI) != 0x1A)
            {
                return false;
            }

            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_CTRL_REG2, 0x40); // reset

            // enable 4G range
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_XYZ_DATA_CFG, MMA8451RegisterOptions.MMA8451_RANGE_4_G);
            // High res
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_CTRL_REG2, 0x02);
            // DRDY on INT1
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_CTRL_REG4, 0x01);
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_CTRL_REG5, 0x01);

            // Turn on orientation config
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_PL_CFG, 0x40);

            // Activate at max rate, low noise mode
            WriteRegister(MMA8451RegisterOptions.MMA8451_REG_CTRL_REG1, 0x01 | 0x04);

            return true;
        }

        private byte ReadRegister(byte registerId)
        {
            var readBuffer = new byte[1];
            this.device.WriteRead(new byte[] { registerId }, readBuffer);

            return readBuffer[0];
        }

        private void WriteRegister(byte registerId, byte value)
        {
            this.device.Write(new byte[] { registerId, value });
        }
    }
}
