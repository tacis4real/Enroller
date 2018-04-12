using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DPFP;
using DPFP.Capture;

namespace BioEnumerator.DataAccessManager.CommonUtils
{
    public class DigitalPersonaFingerScanner : DPFP.Capture.EventHandler
    {



        private ManualResetEvent _mainThread = new ManualResetEvent(false);
        private Capture _capture;
        private Sample _sample;


        public void Enroll()
        {
            _capture = new Capture
            {
                EventHandler = this
            };
            _capture.StartCapture();
            _mainThread.WaitOne();
        }



        public void CreateBitmapFile(string pathToSaveBitmapTo)
        {
            if (_sample == null)
            {
                throw new NullReferenceException("_sample");
            }

            var sampleConvertor = new SampleConversion();
            Bitmap bitmap = null;
            sampleConvertor.ConvertToPicture(_sample, ref bitmap);

            bitmap.Save(pathToSaveBitmapTo);
        }

        public void Dispose()
        {
            _capture.StopCapture();
            _capture.Dispose();
        }


        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            throw new NotImplementedException();
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            throw new NotImplementedException();
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            throw new NotImplementedException();
        }
    }
}
