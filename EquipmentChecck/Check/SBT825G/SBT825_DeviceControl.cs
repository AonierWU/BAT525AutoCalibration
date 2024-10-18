using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSystem_Pack
{
    public partial class SBT825G
    {
        private bool ChgVolCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x03, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x61, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x61, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC8, 0xCC, 0x71, 0x05, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x1B, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x04, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;
                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x02, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;
                byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x01, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte7);
                if (ret < 0)
                    return false;
                byte[] WriteByte8 = { 0xC8, 0xCC, 0x71, 0x2C, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte8);
                if (ret < 0)
                    return false;
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x71, 0x2F, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                byte[] WriteByte10 = { 0xC8, 0xCC, 0x71, 0x2E, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte10);
                if (ret < 0)
                    return false;
                byte[] WriteByte11 = { 0xC8, 0xCC, 0x71, 0x2B, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte11);
                if (ret < 0)
                    return false;
                byte[] WriteByte12 = { 0xC8, 0xCC, 0x71, 0x03, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte12);
                if (ret < 0)
                    return false;
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool DsgCurCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x03, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x61, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x61, 0x04, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC8, 0xCC, 0x71, 0x05, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x1B, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x04, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;
                byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x01, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte7);
                if (ret < 0)
                    return false;
                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x02, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;
                byte[] WriteByte8 = { 0xC8, 0xCC, 0x71, 0x2C, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte8);
                if (ret < 0)
                    return false;
                byte[] WriteByte10 = { 0xC8, 0xCC, 0x71, 0x2E, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte10);
                if (ret < 0)
                    return false;
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x71, 0x2F, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                byte[] WriteByte11 = { 0xC8, 0xCC, 0x71, 0x2B, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte11);
                if (ret < 0)
                    return false;
                byte[] WriteByte12 = { 0xC8, 0xCC, 0x71, 0x03, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte12);
                if (ret < 0)
                    return false;
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool OCVCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x0E, 0x02, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x10, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x12, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool ProVoltCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC0, 0xCC, 0x78, 0x00, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x17, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC4, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC4, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CellVoltCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC1, 0xCC, 0x08, 0x61, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool CellCurCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC1, 0xCC, 0x08, 0x61, 0x04, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CNTCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x1B, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x2B, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x2F, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte21 = { 0xC8, 0xCC, 0x71, 0x2E, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte21);
                if (ret < 0)
                    return false;
                byte[] WriteByte22 = { 0xC8, 0xCC, 0x71, 0x2D, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte22);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                byte[] WriteByte5 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool NTCCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x25, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x26, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x10, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC5, 0xCC, 0x08, 0x70, 0x04, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                byte[] WriteByte5 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool IDRCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x25, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x26, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x10, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC5, 0xCC, 0x08, 0x70, 0x04, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                byte[] WriteByte5 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool DCIRCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x03, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC8, 0xCC, 0x61, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x61, 0x04, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x05, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                byte[] WriteByte10 = { 0xC8, 0xCC, 0x71, 0x1B, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte10);
                if (ret < 0)
                    return false;
                byte[] WriteByte11 = { 0xC8, 0xCC, 0x71, 0x04, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte11);
                if (ret < 0)
                    return false;
                byte[] WriteByte12 = { 0xC8, 0xCC, 0x71, 0x0D, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte12);
                if (ret < 0)
                    return false;
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x71, 0x02, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x71, 0x01, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                byte[] WriteByte15 = { 0xC8, 0xCC, 0x71, 0x2C, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte15);
                if (ret < 0)
                    return false;
                byte[] WriteByte16 = { 0xC8, 0xCC, 0x71, 0x2F, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte16);
                if (ret < 0)
                    return false;
                byte[] WriteByte17 = { 0xC8, 0xCC, 0x71, 0x2E, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte17);
                if (ret < 0)
                    return false;
                byte[] WriteByte18 = { 0xC8, 0xCC, 0x71, 0x2B, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte18);
                if (ret < 0)
                    return false;
                byte[] WriteByte19 = { 0xC8, 0xCC, 0x71, 0x03, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte19);
                if (ret < 0)
                    return false;
                byte[] WriteByte20 = { 0xC8, 0xCC, 0x70, 0x02, 0x10, 0x27, 0x00 };
                ret = comm.WriteCommand(WriteByte20);
                if (ret < 0)
                    return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool StCurCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC1, 0xCC, 0x08, 0x61, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;

                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x18, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x19, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool NTCRange(byte range)
        {
            int ret = -1;
            try
            {

                byte[] WriteByte0 = { 0xC5, 0xCC, 0x08, 0x70, 0x01, range, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool StCurRange(byte R, byte range)
        {
            int ret = -1;
            try
            {

                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x1A, R, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC1, 0xCC, 0x08, 0x70, 0x01, range, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool LoadVoltCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x70, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x13, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x16, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;
                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x15, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;
                byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x14, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte7);
                if (ret < 0)
                    return false;
                byte[] WriteByte8 = { 0xC5, 0xCC, 0x08, 0x61, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte8);
                if (ret < 0)
                    return false;
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x70, 0x03, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool LoadCurCalibration()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x70, 0x03, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x13, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x14, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte7);
                if (ret < 0)
                    return false;
                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x15, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x16, 0x01, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;
                byte[] WriteByte8 = { 0xC5, 0xCC, 0x08, 0x61, 0x01, 0x00, 0xFF };
                ret = comm.WriteCommand(WriteByte8);
                if (ret < 0)
                    return false;
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x70, 0x03, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool SetCurRange(byte range)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC8, 0xCC, 0x71, 0x05, range, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool SendChgVolt(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x61, 0x01, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SendProVolt(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC4, 0xCC, 0x08, 0x61, 0x01, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool SendCellVolt(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC1, 0xCC, 0x08, 0x61, 0x01, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //OCVID + "-CC-08-61-01-";//设定cvset
        private bool SendLoadPartVolt(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x61, 0x01, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool SendLoadPartCur(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x61, 0x02, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SendDsgCur(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x61, 0x02, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool SendCellCur(byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC1, 0xCC, 0x08, 0x61, 0x02, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadVoltADCCaliValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x60, 0x05, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[4];
                DH = ReadBuff[5];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCurADCCaliValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x60, 0x06, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[4];
                DH = ReadBuff[5];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ReadProADCCaliValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC4, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private bool WriteVoltDAC_ADCCaliValue(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x53, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool WriteOCV_DACCaliValue(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool WritePro_ADCValue(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte9 = { 0xC4, 0xCC, 0x09, 0x50, 0x04, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool ReadOCV_DACCaliValue(byte Addr)
        {
            //int ret = -1;
            try
            {
                //byte[] WriteByte9 = { 0xC5, 0xCC, 0x09, 0x50, 0x03, Addr, 0x00, 0x00 };
                //ret = communication.WriteCommand(WriteByte9);
                //if (ret < 0)
                //    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool SetVoltValue(double voltage)
        {
            int ret = -1;
            byte VolDH, VolDM, VolDL;
            VolDL = Convert.ToByte(Convert.ToInt32(voltage * 10) & 0x00FF);
            VolDM = Convert.ToByte((Convert.ToInt32(voltage * 10) & 0xFF00) >> 8);
            VolDH = Convert.ToByte((Convert.ToInt32(voltage * 10) & 0xFF0000) >> 16);
            try
            {
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x70, 0x01, VolDL, VolDM, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool SetProVoltValue(int voltage)
        {
            int ret = -1;
            byte VolDH, VolDL;
            VolDL = Convert.ToByte(voltage & 0x00FF);
            VolDH = Convert.ToByte((voltage & 0xFF00) >> 8);
            try
            {
                byte[] WriteByte1 = { 0xC4, 0xCC, 0x08, 0x61, 0x03, VolDL, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool SetCellVoltValue(double voltage)
        {
            int ret = -1;
            byte VolDH, VolDL;
            VolDL = Convert.ToByte(Convert.ToInt32(voltage * 10) & 0x00FF);
            VolDH = Convert.ToByte((Convert.ToInt32(voltage * 10) & 0xFF00) >> 8);
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x61, 0x05, VolDL, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool SetLoadPartVoltValue(int Voltage)
        {
            int ret = -1;
            byte VolDH, VolDL;
            VolDL = Convert.ToByte(Voltage * 10 & 0x00FF);
            VolDH = Convert.ToByte((Voltage * 10 & 0xFF00) >> 8);
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x61, 0x05, VolDL, VolDH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool SetCellCurValue(int current)
        {
            int ret = -1;
            byte VolDH, VolDL;
            VolDL = Convert.ToByte(current * 10 & 0x00FF);
            VolDH = Convert.ToByte((current * 10 & 0xFF00) >> 8);
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x61, 0x06, VolDL, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool SetLoadPartCurValue(int current)
        {
            int ret = -1;
            byte VolDH, VolDL;
            VolDL = Convert.ToByte(current * 10 & 0x00FF);
            VolDH = Convert.ToByte((current * 10 & 0xFF00) >> 8);
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x61, 0x06, VolDL, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool SetCurValue(double Current)
        {
            int ret = -1;
            byte VolDH, VolDM, VolDL;
            VolDL = Convert.ToByte(Convert.ToInt32(Current * 10) & 0x00FF);
            VolDM = Convert.ToByte((Convert.ToInt32(Current * 10) & 0xFF00) >> 8);
            VolDH = Convert.ToByte((Convert.ToInt32(Current * 10) & 0xFF0000) >> 16);
            try
            {
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x70, 0x02, VolDL, VolDM, VolDH };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool ReadVoltValue(out double dblVoltage)
        {
            int ret = -1;
            dblVoltage = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x81, 0x01, 0x00, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblVoltage = ((double)(readBuff[6] * 256 * 256 + readBuff[5] * 256 + readBuff[4])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCNTValue(byte Statu, out double dblVoltage)
        {
            int ret = -1;
            dblVoltage = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x60, 0x0B, Statu, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblVoltage = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5]));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadProVoltValue(out double dblVoltage)
        {
            int ret = -1;
            dblVoltage = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC4, 0xCC, 0x08, 0x60, 0x05, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblVoltage = ((double)(readBuff[6] * 256 + readBuff[5]));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCurValue(out double dblCurrent)
        {
            int ret = -1;
            dblCurrent = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x81, 0x02, 0x00, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCurrent = ((double)(readBuff[6] * 256 * 256 + readBuff[5] * 256 + readBuff[4])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadOCVValue(out double dblOCV)
        {
            int ret = -1;
            dblOCV = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x60, 0x07, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblOCV = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCellVoltValue(out double dblCellVolt)
        {
            int ret = -1;
            dblCellVolt = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x60, 0x05, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCellVolt = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadLoadPatVoltValue(out double dblCellVolt)
        {
            int ret = -1;
            dblCellVolt = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x60, 0x05, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCellVolt = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCellCurValue(out double dblCellCur)
        {
            int ret = -1;
            dblCellCur = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x60, 0x06, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCellCur = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadStCurValue(byte R, byte Range, out double dblCellCur)
        {
            int ret = -1;
            dblCellCur = 0;
            byte[] readBuff = null;
            try
            {

                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x60, 0x07, R, Range };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;

                dblCellCur = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadNTCValue(byte R, byte Range, out double dblCellCur)
        {
            int ret = -1;
            dblCellCur = 0;
            byte[] readBuff = null;
            try
            {

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x60, 0x0A, R, Range };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;

                dblCellCur = ((double)(readBuff[9] * 256 * 256 * 256 + readBuff[8] * 256 * 256 + readBuff[7] * 256 + readBuff[6])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadDCIRValue(out double dblCellCur)
        {
            int ret = -1;
            dblCellCur = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x81, 0x03, 0x00, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCellCur = ((double)(readBuff[5] * 256 + readBuff[4])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadLoadPartCurValue(out double dblCellCur)
        {
            int ret = -1;
            dblCellCur = 0;
            byte[] readBuff = null;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x60, 0x06, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out readBuff);
                if (ret < 0)
                    return false;
                dblCellCur = ((double)(readBuff[7] * 256 * 256 + readBuff[6] * 256 + readBuff[5])) * 0.01;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteOCV_DAC(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteProVolt_DAC(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC4, 0xCC, 0x09, 0x50, 0x04, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteDCIR_DAC(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC8, 0xCC, 0x53, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteCellVolt_DAC(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteNTC_DAC(byte Addr1, byte DL1, byte DH1, byte Addr2, byte DL2, byte DH2, byte Addr3, byte DL3, byte DH3)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Addr1, DL1, DH1 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Addr2, DL2, DH2 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte2 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Addr3, DL3, DH3 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteLoadPart_DAC(byte Addr, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Addr, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteDCIR_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                for (int j = 0; j < 4; j++)
                {
                    byte Add = Convert.ToByte(0x9D + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //WriteIDR_ADC
        private bool WriteIDR_ADC(string Write, byte[] ADC_L, byte[] ADC_H, byte[] Buf_L, byte[] Buf_M, byte[] Buf_H, byte[] Buf_HH)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00, };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                switch (Write)
                {
                    case "NTC1":
                        for (int j = 0; j < 6; j++)
                        {
                            byte Add = Convert.ToByte(0x5E + j * 2);
                            byte Add1 = Convert.ToByte(0xA9 + j * 4);
                            byte Add2 = Convert.ToByte(0xA9 + j * 4 + 2);

                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;

                            byte DL1 = Convert.ToByte(Buf_L[j]);//低地址
                            byte DH1 = Convert.ToByte(Buf_M[j]);//高地址
                            byte[] WriteByte2 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add1, DL1, DH1 };
                            ret = comm.WriteCommand(WriteByte2);
                            if (ret < 0)
                                return false;

                            byte DL2 = Convert.ToByte(Buf_H[j]);//低地址
                            byte DH2 = Convert.ToByte(Buf_HH[j]);//高地址
                            byte[] WriteByte3 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add2, DL2, DH2 };
                            ret = comm.WriteCommand(WriteByte3);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    case "NTC2":
                        for (int j = 0; j < 5; j++)
                        {
                            byte Add = Convert.ToByte(0x6E + j * 2);
                            byte Add1 = Convert.ToByte(0xC2 + j * 4);
                            byte Add2 = Convert.ToByte(0xC2 + j * 4 + 2);

                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;

                            byte DL1 = Convert.ToByte(Buf_L[j]);//低地址
                            byte DH1 = Convert.ToByte(Buf_M[j]);//高地址
                            byte[] WriteByte2 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add1, DL1, DH1 };
                            ret = comm.WriteCommand(WriteByte2);
                            if (ret < 0)
                                return false;

                            byte DL2 = Convert.ToByte(Buf_H[j]);//低地址
                            byte DH2 = Convert.ToByte(Buf_HH[j]);//高地址
                            byte[] WriteByte3 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add2, DL2, DH2 };
                            ret = comm.WriteCommand(WriteByte3);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    //200K
                    case "NTC3":
                        for (int j = 0; j < 5; j++)
                        {
                            byte Add = Convert.ToByte(0x7E + j * 2);
                            byte Add1 = Convert.ToByte(0xD6 + j * 4);
                            byte Add2 = Convert.ToByte(0xD6 + j * 4 + 2);

                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;

                            byte DL1 = Convert.ToByte(Buf_L[j]);//低地址
                            byte DH1 = Convert.ToByte(Buf_M[j]);//高地址
                            byte[] WriteByte2 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add1, DL1, DH1 };
                            ret = comm.WriteCommand(WriteByte2);
                            if (ret < 0)
                                return false;

                            byte DL2 = Convert.ToByte(Buf_H[j]);//低地址
                            byte DH2 = Convert.ToByte(Buf_HH[j]);//高地址
                            byte[] WriteByte3 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add2, DL2, DH2 };
                            ret = comm.WriteCommand(WriteByte3);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    //3000K
                    case "NTC4":
                        for (int j = 0; j < 5; j++)
                        {
                            byte Add = Convert.ToByte(0x8E + j * 2);
                            byte Add1 = Convert.ToByte(0xEB + j * 4);
                            byte Add2 = Convert.ToByte(0xEB + j * 4 + 2);

                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;

                            byte DL1 = Convert.ToByte(Buf_L[j]);//低地址
                            byte DH1 = Convert.ToByte(Buf_M[j]);//高地址
                            byte[] WriteByte2 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add1, DL1, DH1 };
                            ret = comm.WriteCommand(WriteByte2);
                            if (ret < 0)
                                return false;

                            byte DL2 = Convert.ToByte(Buf_H[j]);//低地址
                            byte DH2 = Convert.ToByte(Buf_HH[j]);//高地址
                            byte[] WriteByte3 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add2, DL2, DH2 };
                            ret = comm.WriteCommand(WriteByte3);
                            if (ret < 0)
                                return false;
                        }
                        break;
                }
                byte[] WriteByte12 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte12);
                if (ret < 0)
                    return false;

                byte[] WriteByte13 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00, };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //

        private bool ReadCNT_ADC(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x04, 0x00 };//ADC通道选择
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool WriteCNT_ADC(byte Add, byte DL, byte DH)
        {
            int ret = -1;
            try
            {
                //byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                //ret = comm.WriteCommand(WriteByte);
                //if (ret < 0)
                //    return false;

                //byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00, };
                //ret = comm.WriteCommand(WriteByte1);
                //if (ret < 0)
                //    return false;

                //int[] ADC_H1 = new int[8] { 0x7F, 0x97, 0xC8, 0xF8, 0x7F, 0x67, 0x38, 0x08 };//ADC设置的高地址
                //int[] ADC_L1 = new int[8] { 0x96, 0xB4, 0x11, 0x6D, 0xD5, 0xFF, 0x4E, 0x96 };//ADC设置的低地址
                //for (int j = 0; j < 8; j++)
                //{
                //byte Add = Convert.ToByte(0x98 + j * 2);
                //byte DL = Convert.ToByte(ADC_L1[j]);//低地址
                //byte DH = Convert.ToByte(ADC_H1[j]);//高地址
                byte[] WriteByte = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                //}

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool RoughWriteCNT_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                switch (Write)
                {
                    case "CNT静态(正)":
                        for (int j = 0; j < 4; j++)
                        {
                            byte Add = Convert.ToByte(0x98 + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    case "CNT静态(负)":
                        for (int j = 0; j < 4; j++)
                        {
                            byte Add = Convert.ToByte(0xA0 + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                }
                byte[] WriteByte111 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool WritePVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC4, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC4, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x05 + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC4, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写ADC
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x2C + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC4, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC4, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC4, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteOCV_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                //写DAC
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x4B + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteLVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x05 + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写ADC
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x2C + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteCellVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                //写ADC
                //int[] ADC_H1 = new int[9] { 0x02, 0x03, 0x07, 0x1B, 0x33, 0x64, 0x96, 0xC7, 0xF8 };//ADC设置的高地址
                //int[] ADC_L1 = new int[9] { 0xB4, 0xB0, 0xA1, 0x45, 0xDB, 0xDF, 0x80, 0x88, 0x89 };//ADC设置的低地址
                for (int j = 0; j < 9; j++)
                {
                    byte Add = Convert.ToByte(0x05 + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写ADC
                //int[] ADC_H2 = new int[9] { 0x00, 0x00, 0x04, 0x18, 0x31, 0x62, 0x93, 0xC4, 0xF5 };//ADC设置的高地址
                //int[] ADC_L2 = new int[9] { 0x00, 0xF8, 0xE4, 0x95, 0x2B, 0x5F, 0x8B, 0xB8, 0xEA };//ADC设置的低地址
                for (int j = 0; j < 9; j++)
                {
                    byte Add = Convert.ToByte(0x2C + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC1, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC1, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteCellCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                //int[] ADC_H1 = new int[5] { 0x00, 0x15, 0x63, 0xC3, 0xF3 };//ADC设置的高地址
                //int[] ADC_L1 = new int[5] { 0x00, 0xF5, 0x80, 0x88, 0x89 };//ADC设置的低地址
                for (int j = 0; j < 5; j++)
                {
                    byte Add = Convert.ToByte(0x1F + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写DAC
                //int[] ADC_H2 = new int[5] { 0x00, 0x13, 0x60, 0xC0, 0xF1 };//ADC设置的高地址
                //int[] ADC_L2 = new int[5] { 0x07, 0xF8, 0x8B, 0xB8, 0x3A };//ADC设置的低地址
                for (int j = 0; j < 5; j++)
                {
                    byte Add = Convert.ToByte(0x3E + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC1, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC1, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteLVCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                for (int j = 0; j < 5; j++)
                {
                    byte Add = Convert.ToByte(0x1F + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写ADC
                int[] ADC_H2 = new int[5] { 0x00, 0x13, 0x60, 0xC1, 0xF2 };//ADC设置的高地址
                int[] ADC_L2 = new int[5] { 0x07, 0xF8, 0x8B, 0xB8, 0x3A };//ADC设置的低地址
                for (int j = 0; j < 5; j++)
                {
                    byte Add = Convert.ToByte(0x3E + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC5, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteCHGVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                //写DAC
                for (int j = 0; j < 12; j++)
                {
                    byte Add = Convert.ToByte(0x07 + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写ADC
                for (int j = 0; j < 12; j++)
                {
                    byte Add = Convert.ToByte(0x51 + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }

                byte[] WriteByte111 = { 0xC8, 0xCC, 0x80, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC8, 0xCC, 0x80, 0x06, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteDSGCur1_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                //int[] ADC_H1 = new int[8] { 0x00, 0x02, 0x08, 0x26, 0x3F, 0x7C, 0x9A, 0xB9 };//ADC设置的高地址
                //int[] ADC_L1 = new int[8] { 0x00, 0x91, 0x34, 0xC5, 0x2B, 0x25, 0x80, 0x28 };//ADC设置的低地址
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x25 + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写DAC
                //int[] ADC_H2 = new int[8] { 0x02, 0x02, 0x08, 0x26, 0x3F, 0x7C, 0x9A, 0xB9 };//ADC设置的高地址
                //int[] ADC_L2 = new int[8] { 0x5E, 0xE2, 0x84, 0xC5, 0x6B, 0xDF, 0xE0, 0x75 };//ADC设置的低地址
                for (int j = 0; j < 8; j++)
                {
                    byte Add = Convert.ToByte(0x6F + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }


                byte[] WriteByte111 = { 0xC8, 0xCC, 0x80, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC8, 0xCC, 0x80, 0x06, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool WriteDSGCur2_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)//大电流
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                //int[] ADC_H1 = new int[7] { 0x00, 0x02, 0x08, 0x26, 0x3F, 0x7C, 0x9A };//ADC设置的高地址
                //int[] ADC_L1 = new int[7] { 0x00, 0x91, 0x34, 0xC5, 0x2B, 0x25, 0x80 };//ADC设置的低地址
                for (int j = 0; j < 7; j++)
                {
                    byte Add = Convert.ToByte(0x3B + j * 2);
                    byte DL = Convert.ToByte(DAC_L[j]);//低地址
                    byte DH = Convert.ToByte(DAC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }
                //写DAC
                //int[] ADC_H2 = new int[7] { 0x02, 0x02, 0x08, 0x26, 0x3F, 0x7C, 0x9A };//ADC设置的高地址
                //int[] ADC_L2 = new int[7] { 0x5E, 0xE2, 0x84, 0xC5, 0x6B, 0xDF, 0xE0 };//ADC设置的低地址
                for (int j = 0; j < 7; j++)
                {
                    byte Add = Convert.ToByte(0x85 + j * 2);
                    byte DL = Convert.ToByte(ADC_L[j]);//低地址
                    byte DH = Convert.ToByte(ADC_H[j]);//高地址
                    byte[] WriteByte10 = { 0xC8, 0xCC, 0x53, Add, DL, DH };
                    ret = comm.WriteCommand(WriteByte10);
                    if (ret < 0)
                        return false;
                }


                byte[] WriteByte111 = { 0xC8, 0xCC, 0x80, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte111);
                if (ret < 0)
                    return false;
                byte[] WriteByte112 = { 0xC8, 0xCC, 0x80, 0x06, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte112);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //WriteSTC_ADC
        private bool WriteSTC_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC1, 0xCC, 0x08, 0x50, 0x07, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;

                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x08, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                switch (Write)
                {
                    case "静态电流1":
                        //0-200uA
                        for (int j = 0; j < 16; j++)
                        {
                            byte Add = Convert.ToByte(0x8F + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    //0-2000uA
                    case "静态电流2":
                        for (int j = 0; j < 16; j++)
                        {
                            byte Add = Convert.ToByte(0xAF + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    //0-2000nA
                    case "静态电流3":
                        for (int j = 0; j < 16; j++)
                        {
                            byte Add = Convert.ToByte(0x4C + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                    //0-20000nA
                    case "静态电流4":
                        for (int j = 0; j < 16; j++)
                        {
                            byte Add = Convert.ToByte(0x6C + j * 2);
                            byte DL = Convert.ToByte(ADC_L[j]);//低地址
                            byte DH = Convert.ToByte(ADC_H[j]);//高地址
                            byte[] WriteByte10 = { 0xC1, 0xCC, 0x09, 0x50, 0x04, Add, DL, DH };
                            ret = comm.WriteCommand(WriteByte10);
                            if (ret < 0)
                                return false;
                        }
                        break;
                }
                byte[] WriteByte12 = { 0xC1, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte12);
                if (ret < 0)
                    return false;

                byte[] WriteByte13 = { 0xC1, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCellVolt_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x70, 0x02, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte9 = { 0xC1, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ReadCellCur_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x70, 0x02, 0x01, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte9 = { 0xC1, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadStCur_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x70, 0x02, 0x02, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                //System.Threading.Thread.Sleep(1000);
                byte[] WriteByte9 = { 0xC1, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadNTC_ADCValue(byte Index, byte R, out byte DL1, out byte DH1, out byte DL2, out byte DH2, out byte DL3, out byte DH3)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL1 = 0x00;
            DH1 = 0x00;
            DL2 = 0x00;
            DH2 = 0x00;
            DL3 = 0x00;
            DH3 = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x09, 0x61, 0x09, R, Index, 0x00 };
                ret = comm.WriteReadCommand(WriteByte1, out ReadBuff);
                if (ret < 0)
                    return false;

                DL1 = ReadBuff[5];
                DH1 = ReadBuff[6];
                DL2 = ReadBuff[7];
                DH2 = ReadBuff[8];
                DL3 = ReadBuff[9];
                DH3 = ReadBuff[10];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadDCIR_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {


                byte[] WriteByte9 = { 0xC8, 0xCC, 0x60, 0x07, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[4];
                DH = ReadBuff[5];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadLoadPartVolt_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x02, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadLoadPartCur_ADCValue(out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x70, 0x02, 0x03, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;

                byte[] WriteByte9 = { 0xC5, 0xCC, 0x08, 0x60, 0x03, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[5];
                DH = ReadBuff[6];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //C8-CC-71-17-01-00-00
        private bool ProVoltEnable(byte Status)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x17, Status, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Enable(byte Status)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x03, Status, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CHGEnable()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x01, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x02, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x2C, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;
                byte[] WriteByte3 = { 0xC8, 0xCC, 0x71, 0x2E, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte3);
                if (ret < 0)
                    return false;
                byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x2F, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte4);
                if (ret < 0)
                    return false;
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x04, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool DCIREnable()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x0D, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool OCVEnable(byte Status)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x71, 0x03, Status, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte10 = { 0xC8, 0xCC, 0x71, 0x0E, 0x02, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte10);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x71, 0x10, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                byte[] WriteByte2 = { 0xC8, 0xCC, 0x71, 0x12, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte2);
                if (ret < 0)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // CellID + "-CC-08-70-03-00-00"
        private bool CellEnable(byte Status)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC1, 0xCC, 0x08, 0x70, 0x03, Status, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool StCurEnable()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x18, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x19, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CNTEnable()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x2B, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x2D, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool NTCEnable()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x25, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte5);
                if (ret < 0)
                    return false;

                byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x26, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte6);
                if (ret < 0)
                    return false;

                byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x10, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte7);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool LoadPartEnable(byte Status)
        {
            int ret = -1;
            try
            {
                byte[] WriteByte = { 0xC5, 0xCC, 0x08, 0x70, 0x03, Status, 0x00 };
                ret = comm.WriteCommand(WriteByte);
                if (ret < 0)
                    return false;
                //byte[] WriteByte4 = { 0xC8, 0xCC, 0x71, 0x13, 0x00, 0x00, 0x00 };
                //ret = comm.WriteCommand(WriteByte4);
                //if (ret < 0)
                //    return false;
                //byte[] WriteByte5 = { 0xC8, 0xCC, 0x71, 0x16, 0x00, 0x00, 0x00 };
                //ret = comm.WriteCommand(WriteByte5);
                //if (ret < 0)
                //    return false;
                //byte[] WriteByte6 = { 0xC8, 0xCC, 0x71, 0x15, 0x00, 0x00, 0x00 };
                //ret = comm.WriteCommand(WriteByte6);
                //if (ret < 0)
                //    return false;
                //byte[] WriteByte7 = { 0xC8, 0xCC, 0x71, 0x14, 0x00, 0x00, 0x00 };
                //ret = comm.WriteCommand(WriteByte7);
                //if (ret < 0)
                //    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x80, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x80, 0x06, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool UpdateOCV()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UpdateProVolt()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC4, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC4, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool UpdateCellVolt()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC1, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC1, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UpdateDCIR()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC8, 0xCC, 0x80, 0x05, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x80, 0x06, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UpdateNTC()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool UpdateLoadPart()
        {
            int ret = -1;
            try
            {
                byte[] WriteByte0 = { 0xC5, 0xCC, 0x08, 0x50, 0x09, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte0);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC5, 0xCC, 0x08, 0x50, 0x0A, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte1);
                if (ret < 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ReadChgVolDAC(byte addr, out byte DL, out byte DH)//设置值
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x52, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[4];
                DH = ReadBuff[5];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadChgVolADC(byte addr, out byte DL, out byte DH)//读取值
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC8, 0xCC, 0x52, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[4];
                DH = ReadBuff[5];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadOcvDAC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadOcvADC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC5, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadPrgDAC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC4, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadPrgADC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC4, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCellDAC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC1, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ReadCellADC(byte addr, out byte DL, out byte DH)
        {
            int ret = -1;
            byte[] ReadBuff = null;
            DL = 0x00;
            DH = 0x00;
            try
            {
                byte[] WriteByte9 = { 0xC1, 0xCC, 0x09, 0x50, 0x03, addr, 0x00, 0x00 };
                ret = comm.WriteReadCommand(WriteByte9, out ReadBuff);
                if (ret < 0)
                    return false;
                DL = ReadBuff[6];
                DH = ReadBuff[7];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool WriteSNCode(string EqmType, string dateTime, string SNnum,string status)
        {
            int ret = -1;
            try
            {
                string str = EqmType + dateTime + SNnum+ status;
               // List<byte[]> WriteByte1 = new List<byte[]> { 0xC8, 0xCC, 0x57, 0x00};
                byte[] byteArray = new byte[] { 0xC8, 0xCC, 0x57, 0x00 };
                byte[] array = System.Text.Encoding.ASCII.GetBytes(str);
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                //using (MemoryStream memoryStream = new MemoryStream())//方法一
                //{
                //    using (BinaryWriter writer = new BinaryWriter(memoryStream))
                //    {
                //        // 先写入数组中现有的字节
                //        writer.Write(byteArray);
                //        // 写入新的字节
                //        foreach (byte data in array)
                //        {
                //            writer.Write(data);
                //        }
                //        // 重新获取内存流的缓冲区
                //        byteArray = memoryStream.ToArray();
                //    }
                //}
                //ret = comm.WriteCommand(byteArray);
                //C8 CC 57 00 42 41 54 35 32 35 48 32 30 32 34 30 39 32 39 31 32 31 31 30 C8 
                byte[] combinedArray = byteArray.Concat(array).ToArray();//方法二
                ret = comm.WriteSNCode(combinedArray);
                //C8 CC 57 00 42 41 54 35 32 35 48 32 30 32 34 30 39 32 39 31 31 31 31 30 C9 

                if (ret < 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ReadSNCode(out string EqmType, out string dateTime, out string SNnum)
        {
            int ret = -1;
            EqmType = "";
            dateTime = "";
            SNnum = "";
            byte[] data1;// = new byte[19];
            try
            {
                byte[] WriteByte13 = { 0xC8, 0xCC, 0x60, 0x11, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte13);
                if (ret < 0)
                    return false;
                byte[] WriteByte14 = { 0xC8, 0xCC, 0x60, 0x12, 0x00, 0x00 };
                ret = comm.WriteCommand(WriteByte14);
                if (ret < 0)
                    return false;
                byte[] WriteByte1 = { 0xC8, 0xCC, 0x80, 0x57, 0x00, 0x00 };
                comm.WriteReadNew(WriteByte1, out WriteByte14);
                if (WriteByte14.Length != 24)
                {
                    EqmType = "-1";
                    dateTime = "-1";
                    SNnum = "-1";
                    return false;
                }
                else
                {
                    data1 = new byte[7];
                    Array.Copy(WriteByte14, 4, data1, 0, 7);
                    EqmType = System.Text.Encoding.ASCII.GetString(data1);
                    // txtVoltFSAcc.Text = s;

                    data1 = new byte[8];
                    Array.Copy(WriteByte14, 11, data1, 0, 8);
                    dateTime = System.Text.Encoding.ASCII.GetString(data1);

                    data1 = new byte[4];
                    Array.Copy(WriteByte14, 19, data1, 0, 4);
                    SNnum = System.Text.Encoding.ASCII.GetString(data1);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
